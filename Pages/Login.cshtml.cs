using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SarasaviLibrary.Data;

namespace SarasaviLibrary.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserLoginInput LoginInput { get; set; } = new UserLoginInput();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string inputUser = LoginInput.UserNumber.Trim();
            string inputNIC = LoginInput.NIC.Trim();

            List<Claim> claims;
            string detectedRole = "";

            // 💡 BRANCH 1: Check against hardcoded Librarian Seed Credentials
            if (inputUser.ToLower() == "admin" && inputNIC == "1234")
            {
                detectedRole = "Librarian";
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "System Librarian"),
                    new Claim(ClaimTypes.Role, "Librarian")
                };
            }
            // 💡 BRANCH 2: Fallback validation checking against SarasaviLibraryDB Users table
            else
            {
                var validUser = _context.Users.FirstOrDefault(u =>
                    u.UserNumber.ToUpper() == inputUser.ToUpper() &&
                    u.NIC.ToUpper() == inputNIC.ToUpper());

                if (validUser != null)
                {
                    detectedRole = "User";
                    claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, validUser.FullName),
                        new Claim(ClaimTypes.Role, "User"),
                        new Claim("UserNumber", validUser.UserNumber)
                    };
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login credentials. Please try again.");
                    return Page();
                }
            }

            // 💡 BUILD INTERNAL IDENTITY TICKET AND COOKIE AUTHORIZATION LOCK
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // 💡 REDIRECT ROUTING BASED ON THE ASSIGNED ROLE
            if (detectedRole == "Librarian")
            {
                return RedirectToPage("/Librarian/Index");
            }
            else
            {
                return RedirectToPage("/Users/Home");
            }
        }
    }

    public class UserLoginInput
    {
        [Required(ErrorMessage = "Enter User Number (or 'admin' for staff)")]
        public string UserNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter NIC Number (or '1234' for staff)")]
        public string NIC { get; set; } = string.Empty;
    }
}