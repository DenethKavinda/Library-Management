using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SarasaviLibrary.Data;

namespace SarasaviLibrary.Pages
{
    public class SignupModel : PageModel
    {
        private readonly AppDbContext _context;

        public SignupModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserSignupInput RegisterInput { get; set; } = new UserSignupInput();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool nicExists = _context.Users.Any(u => u.NIC.ToUpper() == RegisterInput.NIC.Trim().ToUpper());
            if (nicExists)
            {
                ModelState.AddModelError("RegisterInput.NIC", "This NIC identity number is already registered.");
                return Page();
            }

            int nextSequentialId = 1;
            var lastUserRecord = _context.Users
                .ToList()
                .OrderByDescending(u => u.UserNumber)
                .FirstOrDefault();

            if (lastUserRecord != null)
            {
                var parts = lastUserRecord.UserNumber.Split('-');
                if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int highestId))
                {
                    nextSequentialId = highestId + 1;
                }
            }

            string generatedUserNumber = $"REG - {nextSequentialId:D8}";

            var newUser = new User
            {
                UserNumber = generatedUserNumber,
                FullName = RegisterInput.FullName.Trim(),
                Sex = RegisterInput.Sex,
                NIC = RegisterInput.NIC.Trim().ToUpper(),
                Address = RegisterInput.Address.Trim(),
                CreatedDate = DateTime.Now
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            // 💡 Save variables inside TempData to trigger the modal popup state on the front-end view
            TempData["UserNumber"] = generatedUserNumber;
            TempData["UserNIC"] = newUser.NIC;

            // Clear input model fields so the form goes back to clean default states behind the modal overlay
            RegisterInput = new UserSignupInput();
            ModelState.Clear();

            return Page();
        }
    }

    public class UserSignupInput
    {
        [Required(ErrorMessage = "Full Name context value is required.")]
        [StringLength(150, ErrorMessage = "Name parameter entry lengths are bounded within 150 characters.")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Sex { get; set; } = "Male";

        [Required(ErrorMessage = "NIC validation number is required.")]
        [RegularExpression(@"^(?:\d{9}[vVxX]|\d{12})$", ErrorMessage = "Please enter a valid National Identity Card (NIC) number format.")]
        public string NIC { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address details location block required.")]
        [StringLength(250, ErrorMessage = "Address context string bounds exceed 250 characters max limits.")]
        public string Address { get; set; } = string.Empty;
    }
}