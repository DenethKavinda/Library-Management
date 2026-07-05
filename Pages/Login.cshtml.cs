using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string cleanUserNumber = LoginInput.UserNumber.Trim().ToUpper();
            string cleanNIC = LoginInput.NIC.Trim().ToUpper();

            // Validate that the user matches BOTH conditions inside your SQL Database
            var validUser = _context.Users.FirstOrDefault(u =>
                u.UserNumber.ToUpper() == cleanUserNumber &&
                u.NIC.ToUpper() == cleanNIC);

            if (validUser == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid User Number or NIC matching records not found.");
                return Page();
            }

            // Successful authorization verification path -> Send over to user home
            return RedirectToPage("/Users/Home");
        }
    }

    public class UserLoginInput
    {
        [Required(ErrorMessage = "User registration account number is required.")]
        public string UserNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "NIC context validation identity number is required.")]
        public string NIC { get; set; } = string.Empty;
    }
}