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
    public class LoansModel : PageModel
    {
        private readonly AppDbContext _context;
        public LoansModel(AppDbContext context) { _context = context; }

        public List<Loan> ActiveLoansList { get; set; } = new List<Loan>();

        public void OnGet()
        {
            ActiveLoansList = _context.Loans.OrderByDescending(l => l.IssuedDate).ToList();
        }

        public IActionResult OnPost(string loanId)
        {
            var loan = _context.Loans.FirstOrDefault(l => l.LoanId == loanId && l.Status == "Active");
            if (loan != null)
            {
                // 1. Mark status tracking parameter as Received
                loan.Status = "Received";

                // 2. Generate custom unique code properties for processing return increments (ex: RET-3958)
                string generatedReturnCode = $"RET-{new Random().Next(1000, 9999)}";

                var newReturnRecord = new Return
                {
                    ReturnCode = generatedReturnCode,
                    LoanId = loan.LoanId,
                    ReturnedDate = DateTime.Now
                };

                _context.Returns.Add(newReturnRecord);
                _context.SaveChanges();

                TempData["ReturnSuccessCode"] = generatedReturnCode;
            }
            return RedirectToPage();
        }
    }
}