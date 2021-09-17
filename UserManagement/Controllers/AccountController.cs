using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IDataAccessService _dataAccess;
        public AccountController(IDataAccessService dataAccess)
        {
            _dataAccess = dataAccess;
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginView login, string ReturnUrl = "")
        {
            try
            {
                string message = "";
                if (ModelState.IsValid)
                {
                    bool isValidUser = _dataAccess.ValidateUserLogin(login.EmailId, Crypto.Hash(login.Password));
                    if (isValidUser)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; //525600 min = 1year
                        var ticket = new FormsAuthenticationTicket(login.EmailId, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    message = "Invalid Credentials Email Address or Password is Incorrect";
                }
                ViewBag.Message = message;
                return View(login);
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
                return View(login);
            }
        }


        public ActionResult CreateUser()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUser(SignUpView signUp)
        {
            bool isError = false;
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _dataAccess.AddUserAsync(BindSignUp(signUp));
                    ViewBag.Message = res.Message;
                    ViewBag.Status = res.Result;
                    return View();
                }
                return View(signUp);
            }
            catch (Exception exception)
            {
                isError = true;
                ViewBag.IsError = isError;
                ModelState.AddModelError("", exception.Message);
                return View(signUp);
            }
        }


        public ActionResult CreateGroup()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateGroup(GroupModel group)
        {
            bool isError = false;
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _dataAccess.AddGroupAsync(group);
                    ViewBag.Message = res.Message;
                    ViewBag.Status = res.Result;
                    return View();
                }
                return View(group);
            }
            catch (Exception exception)
            {
                isError = true;
                ViewBag.IsError = isError;
                ModelState.AddModelError("", exception.Message);
                return View(group);
            }
        }
        public async Task<ActionResult> Users()
        {
            return View(await _dataAccess.GetAllUserAsync());
        }

        public async Task<ActionResult> Groups()
        {
            return View(await _dataAccess.GetAllActiveGroupsAsync());
        }

        public async Task<ActionResult> LinkGroup(int id)
        {
            return View(await _dataAccess.GetPermissionsByGroupIdAsync(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LinkGroup(string id, List<NavigationMenuView> viewModel)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }





        [Authorize]
        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        private SignUpModel BindSignUp(SignUpView signUp)
        {
            var EmailVerifyLink = Guid.NewGuid().ToString();
            return new SignUpModel()
            {
                FirstName = signUp.FirstName,
                Surname = signUp.Surname,
                Email = signUp.Email,
                Password = Crypto.Hash(signUp.Password),
                GroupId = 1,
                EmailOTP = EmailVerifyLink
            };
        }
    }
}