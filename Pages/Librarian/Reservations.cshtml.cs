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
    public class ReservationsModel : PageModel
    {
        private readonly AppDbContext _context;
        public ReservationsModel(AppDbContext context) { _context = context; }

        public List<Reservation> ReservationQueue { get; set; } = new List<Reservation>();

        public void OnGet()
        {
            ReservationQueue = _context.Reservations.OrderByDescending(r => r.ReservedDate).ToList();
        }

        public IActionResult OnPost(string resCode)
        {
            if (string.IsNullOrWhiteSpace(resCode)) return RedirectToPage();

            var res = _context.Reservations.FirstOrDefault(r => r.ReservationCode.Trim().ToUpper() == resCode.Trim().ToUpper() && r.Status == "Pending");
            if (res == null)
            {
                TempData["ErrorMessage"] = "Active pending reservation code matching data entry not found.";
                return RedirectToPage();
            }

            // 1. Update Reservation status parameters
            res.Status = "Completed";

            // 2. Build the unique Loan Identity (Format: LN-847291)
            string generatedLoanId = $"LN-{new Random().Next(100000, 999990)}";

            var newLoan = new Loan
            {
                LoanId = generatedLoanId,
                ReservationCode = res.ReservationCode,
                UserNumber = res.UserNumber,
                BookNumber = res.BookNumber,
                IssuedDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14), // 💡 14-Day dynamic countdown constraint
                Status = "Active"
            };

            _context.Loans.Add(newLoan);
            _context.SaveChanges();

            TempData["SuccessMessage"] = $"Reservation verified! Loan record generated successfully: {generatedLoanId}. Due in 14 Days.";
            return RedirectToPage();
        }
    }
}