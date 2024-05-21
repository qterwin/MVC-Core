using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvccore.Context;
using mvccore.Models;
using System.Diagnostics;
using mvccore.dto.Book;
using mvccore.GenericClass;
using mvccore.Models.Book;

namespace mvccore.Controllers
{
    public class BookController : Controller
    {

        private readonly ILogger<BookController> _bookController;
        private readonly BookContext _bookContext;
        private readonly IMapper _mapper;

        Pagination _pagination = new Pagination();

        public BookController(ILogger<BookController> bookController, BookContext bookContext, IMapper mapper)
        {
            _bookController = bookController;
            _bookContext = bookContext;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1, 
                                               int pageSize = 5)
        {
            var getBook = _bookContext.
                Books.OrderBy(orderuser => orderuser.Name).
                Select(user => _mapper.Map<bookDTO>(user)).AsNoTracking();

            var (Ipage, totalPages, IpageSize, totalRecors) =
                _pagination.CalculateTotalRecordsAndPages(getBook, page, pageSize);

            ViewBag.CurrentPage = Ipage;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            var bookPaginatedList = new List<bookDTO>();

            if (totalPages == 0) {
                ViewBag.TotalRecords = "No Data";
                return View(bookPaginatedList);
            }

               bookPaginatedList = await getBook.
                 Skip((Ipage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            return View(bookPaginatedList);
        }

        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            return View();
        }

        public async Task<IActionResult> Addbook(Book addBook)
        {
            var isExist = _bookContext.Books.Any(yow => yow.Name == addBook.Name);
            if (ModelState.IsValid && !isExist)
            {


                addBook.Active = true;
                addBook.CreatedDate = DateTime.Now;
                _bookContext.Books.Add(addBook);
                _bookContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(addBook);
        }

        public async Task<IActionResult> EditBook(int id)
        {
            var bookDetail = await _bookContext
                                        .Books
                                        .SingleOrDefaultAsync(detail => detail.BookId == id);
            var dtoDetails = _mapper.Map<BookUpdateDTO>(bookDetail);
            return View(dtoDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(BookUpdateDTO bookUpdateDetails)
        {
            var bookDetail = _bookContext
                                .Books
                                .SingleOrDefault(detail => detail.BookId == bookUpdateDetails.BookId);
            if (ModelState.IsValid)
            {
                _mapper.Map(bookUpdateDetails, bookDetail);
                await _bookContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(bookUpdateDetails);
        }

        public async Task<IActionResult> _DeleteBook(int id)
        {
            var deleteBook = await _bookContext
                                        .Books
                                        .SingleOrDefaultAsync(detail => detail.BookId == id);
            return PartialView(deleteBook);
        }

        public async Task<IActionResult> DeleteBoook(Book bookDetails)
        {
            var bookDetail = _bookContext
                                .Books
                                .SingleOrDefault(detail => detail.BookId == bookDetails.BookId);
            if (bookDetail != null)
            {
                _bookContext.Remove(bookDetail);
                _bookContext.SaveChanges();


            }
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
