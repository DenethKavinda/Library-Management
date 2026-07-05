using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SarasaviLibrary.Data;

namespace SarasaviLibrary.Pages.Users
{
    [Authorize(Roles = "User")]
    public class InquiryModel : PageModel
    {
        private readonly AppDbContext _context;

        public InquiryModel(AppDbContext context)
        {
            _context = context;
        }

        // Auto-Populated Identity Metrics View Properties
        public string CurrentUserNumber { get; set; } = string.Empty;
        public string CurrentUserName { get; set; } = string.Empty;
        public string CurrentUserNIC { get; set; } = string.Empty;

        [BindProperty]
        public InquiryInput ModelInput { get; set; } = new InquiryInput();

        public void OnGet()
        {
            PopulateUserMetadata();
        }

        public IActionResult OnPost()
        {
            PopulateUserMetadata();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Map UI transaction parameters directly over into your Entity Model context structure
            var newInquiry = new Inquiry
            {
                UserNumber = CurrentUserNumber,
                FullName = CurrentUserName,
                NIC = CurrentUserNIC,
                Subject = ModelInput.Subject.Trim(),
                Message = ModelInput.Message.Trim(),
                SubmittedDate = DateTime.Now,
                Status = "Pending"
            };

            _context.Inquiries.Add(newInquiry);
            _context.SaveChanges();

            // Store successful visual feedback notification flag markers inside TempData dictionary matrix
            TempData["ModalType"] = "SUCCESS";
            TempData["ModalMessage"] = "Your support inquiry ticket has been submitted successfully to the administration helpdesk network panel.";

            // Reset target binding input model properties cleanly
            ModelInput = new InquiryInput();
            ModelState.Clear();

            return Page();
        }

        private void PopulateUserMetadata()
        {
            CurrentUserNumber = User.FindFirst("UserNumber")?.Value ?? string.Empty;
            CurrentUserName = User.FindFirst(ClaimTypes.Name)?.Value ?? "Valued Member";

            // Safeguard verification search to extract the physical database registry NIC reference
            var userProfile = _context.Users.FirstOrDefault(u => u.UserNumber == CurrentUserNumber);
            CurrentUserNIC = userProfile?.NIC ?? "N/A";
        }
    }

    public class InquiryInput
    {
        [Required(ErrorMessage = "Please provide a subject line title for this inquiry ticket notice.")]
        [StringLength(150, ErrorMessage = "Subject descriptions are restricted under a maximum of 150 characters.")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Detailed descriptive communication message content is required.")]
        [StringLength(1000, ErrorMessage = "Inquiry message content bounds cannot cross a limit of 1000 characters.")]
        public string Message { get; set; } = string.Empty;
    }
}