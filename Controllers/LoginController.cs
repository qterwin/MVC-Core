using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvccore.Context;
using mvccore.Models.Book;
using mvccore.Models.CentralAccess;
using System.Security.Claims;

namespace mvccore.Controllers
{
    public class LoginController : Controller
    {
        private readonly CentralAccessContext _userLogin;
        private readonly BookContext _bookContext;

        public LoginController(CentralAccessContext userLogin,
                                BookContext bookContext)
        {
            _userLogin = userLogin;
            _bookContext = bookContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Users usrLogin)
        {
            try
            {
                var userDetails = await _bookContext
                                       .Users
                                       .SingleOrDefaultAsync(userDetails => userDetails.UserName == usrLogin.Username
                                                                         && userDetails.PassWord == usrLogin.Password);

                if (userDetails == null)
                {
                    ModelState.AddModelError(string.Empty, "Username and Password is Invalid");
                }

                if (ModelState.IsValid && userDetails != null)
                {
                    List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,usrLogin.UserCode.ToString()),
                    new Claim(ClaimTypes.Role,"Admin")
                };

                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                  new ClaimsPrincipal(claimIdentity));

                    return RedirectToAction("Index", "Home");
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }

            //BCrypt

            #region BCrypt
            if (ModelState.IsValid)
            {
                var getUserDetails = await _bookContext
                                            .Users
                                            .SingleOrDefaultAsync(usrDetails => usrDetails.UserName == usrLogin.Username);
                if (getUserDetails == null)
                {
                    ModelState.AddModelError(string.Empty, "Username and Password is invalid");
                }

                if (!BCrypt.Net.BCrypt.Verify(usrLogin.Password, getUserDetails.PassWord) &&
                            getUserDetails != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            #endregion

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(User regUser)
        {
            var isExist = _bookContext.Users.Any(yow => yow.Name == regUser.Name);
            if (ModelState.IsValid /*&& !isExist*/)
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(regUser.PassWord);

                regUser.Active = true;
                regUser.CreatedDate = DateTime.Now;
                _bookContext.Users.Add(regUser);
                _bookContext.SaveChanges();

                return RedirectToAction("Index");
            }

            if (isExist)
            {
                ModelState.AddModelError(string.Empty, "User is already exist");
            }

            return View(regUser);
           
        }

        //public async Task<IActionResult> Register()
        //{
        //    return View();
        //}


        //public async Task<IActionResult> Register(User regUser)
        //{
        //    var isExist = _bookContext.Users.Any(yow => yow.Name == regUser.Name);
        //    if (ModelState.IsValid && !isExist)
        //    {
        //        var passwordHash = BCrypt.Net.BCrypt.HashPassword(regUser.PassWord);

        //        regUser.Active = true;
        //        regUser.CreatedDate = DateTime.Now;
        //        _bookContext.Users.Add(regUser);
        //        _bookContext.SaveChanges();

        //        return RedirectToAction("Index");
        //    }

        //    if (isExist)
        //    {
        //        ModelState.AddModelError(string.Empty, "User is already exist");
        //    }

        //    return View(regUser);
        //}


    }
}
