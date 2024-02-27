using ContactNumbersOnlineApplication.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactNumbersOnlineApplication.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Hardcoded users for demonstration
            var users = new Dictionary<string, string> { { "user1", "user1" }, { "user2", "user2" } };

            // Check if the username exists in the dictionary
            if (users.ContainsKey(model.Username))
            {
                // Username found, now check password
                if (users[model.Username] == model.Password)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username)
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties();

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Contacts");
                }
                else
                {
                    // Correct username but wrong password                              
                    ModelState.AddModelError(string.Empty, "Password is incorrect.");
                    TempData["ErrorMessage"] = "Password is incorrect.";
                    return View(model);
                }
            }

            // Username does not exist
            ModelState.AddModelError(string.Empty, "Invalid username.");
            TempData["ErrorMessage"] = "username is incorrect.";
            return View(model);
        }


    }
}
