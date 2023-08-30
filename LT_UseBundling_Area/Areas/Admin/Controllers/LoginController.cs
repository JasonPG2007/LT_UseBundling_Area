using LT_UseBundling_Area.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Security.Claims;

namespace LT_UseBundling_Area.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            //if (Request.Cookies.ContainsKey("userName"))
            //{
            //    Response.Cookies.Delete("userName");
            //    return View();
            //}
            //else
            //{
            //    //return NotFound();
            //}
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserDAO();
                var result = user.Login(model.Name, model.Password);
                if (result == 1)
                {
                    Response.Cookies.Append("userName", model.Name);
                    var claims = new List<Claim>() {
                        new Claim(ClaimTypes.Name, model.Name),
                        new Claim(ClaimTypes.Role, "Admin"),
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = true
                    });
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong user name or password");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please input all information");
            }
            return View(model);
        }
    }
}
