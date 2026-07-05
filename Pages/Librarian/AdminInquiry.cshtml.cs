using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SarasaviLibrary.Data;

namespace SarasaviLibrary.Pages.Librarian
{
    [Authorize(Roles = "Librarian")]
    public class AdminInquiryModel : PageModel
    {
        private readonly AppDbContext _context;

        public AdminInquiryModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Inquiry> ActiveInquiries { get; set; } = new List<Inquiry>();

        public void OnGet()
        {
            ActiveInquiries = _context.Inquiries.OrderByDescending(i => i.SubmittedDate).ToList();
        }

        public IActionResult OnPostResolve(int ticketId)
        {
            var targetTicket = _context.Inquiries.FirstOrDefault(i => i.Id == ticketId);
            if (targetTicket != null)
            {
                targetTicket.Status = "Resolved";
                _context.SaveChanges();
            }

            TempData["ModalType"] = "SUCCESS";
            TempData["ModalMessage"] = "Ticket has been successfully marked as Resolved.";
            return RedirectToPage();
        }
    }
}