using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvccore.Context;
using mvccore.dto.Book;
using mvccore.Models;
using System.Diagnostics;
using mvccore.GenericClass;
using AutoMapper;
using mvccore.Models.Book;
using mvccore.Models.CentralAccess;
using Microsoft.AspNetCore.Authorization;
using mvccore.Abstraction;
using Microsoft.Data.SqlClient;
using Dapper;
using BCrypt.Net;

namespace mvccore.Controllers
{
    #region hindi na makakabalik sa index kapag nag logout
    [Authorize(Policy = "AdminOnly")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    #endregion

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookContext _bookContext;
        private readonly IMapper _mapper;
        private readonly ISqlConnection _sqlConnection;

        Pagination _pagination =new Pagination();

        public HomeController(ILogger<HomeController> logger, 
                              BookContext bookContext, 
                              IMapper mapper, ISqlConnection sqlConnection)
        {
            _logger = logger;
            _bookContext = bookContext;
            _mapper = mapper;
            _sqlConnection = sqlConnection;
        }
        #region EF - Entity Frameworks
        public async Task<IActionResult> Index(int page = 1, 
                                               int pageSize = 5)
        {


            //var getUserDTO = _bookContext.Users.Select(userdto => new UserDTO
            //{
            //    UserId = userdto.UserId,
            //    Name = userdto.Name,
            //    MiddleName = userdto.MiddleName,
            //    Surname = userdto.Surname,
            //    CreatedDate = userdto.CreatedDate,
            //}).OrderBy(userdto => userdto.Name);

            //var getBook = _bookContext.
            //    Users.OrderBy(orderuser => orderuser.Name).
            //    Select(user => _mapper.Map<UserDTO>(user)).AsNoTracking();

            var getUserDTO = _bookContext.
                 Users.OrderBy(usr => usr.Name)
                .Select(user => _mapper.Map<UserDTO>(user));


            var (Ipage, totalPages, IpageSize, totalRecords) =
                _pagination.CalculateTotalRecordsAndPages(getUserDTO, page, pageSize);

            ViewBag.CurrentPage = Ipage;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;


            var bookPaginatedList = await getUserDTO.
                 Skip((Ipage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            #region Dapper
            //Dapper
            //await using SqlConnection sqlConnection = _sqlConnection.CreateConnection();

            //var getbook = await sqlConnection
            //    .QueryAsync<UserDTO>(@"Select UserId,
            //                                   Name,
            //                                   MiddleName,  
            //                                   Surname,
            //                                   CreatedDate
            //                            From Users");

            //var getBooks = await sqlConnection
            //           .QueryAsync<UserDTO>($"EXEC SP NAME {param1} {param2}");
            #endregion

            return View(bookPaginatedList);
        }
        #endregion
        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(User addUser)
        {
            var isExist = _bookContext.Users.Any(yow => yow.Name == addUser.Name);
            if (ModelState.IsValid && !isExist)
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(addUser.PassWord);

                addUser.Active = true;
                addUser.CreatedDate = DateTime.Now;
                _bookContext.Users.Add(addUser);
                _bookContext.SaveChanges();

                return RedirectToAction("Index");
            }

            if (isExist)
            {
                ModelState.AddModelError(string.Empty, "User is already exist");
            }

            return View(addUser);
        }

        public async Task<IActionResult> Login(Users user)
        {
            return View(user);
        }


        public async Task<IActionResult> EditUser(int id)
        {
            var userDetail = await _bookContext
                                        .Users
                                        .SingleOrDefaultAsync(detail => detail.UserId == id);
            var dtoDetails = _mapper.Map<UserDTO>(userDetail);
            return View(dtoDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserUpdateDTO usrupdatedetails)
        {
            var userDetail = _bookContext
                                .Users
                                .SingleOrDefault(detail => detail.UserId == usrupdatedetails.UserId);
            if (ModelState.IsValid)
            {
                _mapper.Map(usrupdatedetails,userDetail);
                await _bookContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(usrupdatedetails);
        }

        public async Task<IActionResult> _DeleteUser(int id)
        {
            var userDetail = await _bookContext
                                        .Users
                                        .SingleOrDefaultAsync(detail => detail.UserId == id);
            return PartialView(userDetail);
        }

        public async Task<IActionResult> DeleteUser(User usrDetail)
        {
            var userDetail = _bookContext
                                .Users
                                .SingleOrDefault(detail => detail.UserId == usrDetail.UserId);
            if (userDetail != null)
            {
                _bookContext.Remove(userDetail);
                _bookContext.SaveChanges();
                

            }
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
