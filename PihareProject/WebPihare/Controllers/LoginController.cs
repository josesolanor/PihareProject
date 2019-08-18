using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebPihare.Context;
using WebPihare.Core;
using WebPihare.Library;
using WebPihare.Models;

namespace WebPihare.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private LibUsers _usuarios;
        private readonly PihareiiContext _context;
        private readonly Hash _hash;

        public LoginController(PihareiiContext context, Hash hash, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _hash = hash;
            _usuarios = new LibUsers(userManager, signInManager, roleManager);
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(LoginViewModels model)
        {
            if (ModelState.IsValid)
            {
                var listObject = await _usuarios.userLogin(model.Input.Username, model.Input.Password);

                //model.Input.Password = _hash.EncryptString(model.Input.Password);
                //var login = _context.Commisioner.Where(m => m.Nic == model.Input.Username && m.CommisionerPassword == model.Input.Password).FirstOrDefault();

                //if (login is null)
                //{                    
                //    model.ErrorMessage = "Credenciales Incorrectos";
                //    return View(model);
                //}
                //var RoleName = _context.Role.FirstOrDefault(m => m.RoleId == login.RoleId).RoleValue;
                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, login.Nic),
                //    new Claim("FullName", $"{login.FirstName} {login.LastName} {login.SecondLastName}"),
                //    new Claim("Id", login.CommisionerId.ToString()),
                //    new Claim(ClaimTypes.Email, login.Email),
                //    new Claim(ClaimTypes.Role, RoleName)
                //};

                //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //var authProperties = new AuthenticationProperties
                //{

                //};
                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            }
            return RedirectToAction("Index", "Departments");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Denied()
        {
            return View();
        }
    }
}