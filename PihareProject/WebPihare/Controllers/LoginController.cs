using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPihare.Context;
using WebPihare.Models;

namespace WebPihare.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {

        private readonly PihareiiContext _context;

        public LoginController(PihareiiContext context)
        {
            _context = context;
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
                var login = _context.Commisioner.Where(m => m.Email == model.Input.Email && m.CommisionerPassword == model.Input.Password).FirstOrDefault();

                if (login is null)
                {
                    model.ErrorMessage = "Credenciales Incorrectos";
                    return View(model);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.Email),
                    new Claim("FullName", $"{login.FirstName} {login.LastName} {login.SecondLastName}"),
                    new Claim(ClaimTypes.Email, login.Email),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var userIdentity = new ClaimsIdentity(claims, "Login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
}