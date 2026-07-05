using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SarasaviLibrary.Data;

namespace SarasaviLibrary.Pages.Librarian
{
    [Authorize(Roles = "Librarian")]
    public class ReturnsModel : PageModel
    {
        private readonly AppDbContext _context;
        public ReturnsModel(AppDbContext context) { _context = context; }

        public void OnGet() { }

        public IActionResult OnPost(string returnCode)
        {
            if (string.IsNullOrWhiteSpace(returnCode)) return RedirectToPage();

            // 1. Locate matching Return reference ticket
            var returnRecord = _context.Returns.FirstOrDefault(r => r.ReturnCode.Trim().ToUpper() == returnCode.Trim().ToUpper());
            if (returnRecord == null)
            {
                TempData["ReturnErrorMessage"] = "Invalid or non-existent check-in return verification key code input entry.";
                return RedirectToPage();
            }

            // 2. Extract corresponding Loan parameters
            var associatedLoan = _context.Loans.FirstOrDefault(l => l.LoanId == returnRecord.LoanId);
            if (associatedLoan != null)
            {
                // 3. Increment book count variables and restore Borrowable status settings safely
                var targetBook = _context.Books.FirstOrDefault(b => b.BookNumber == associatedLoan.BookNumber);
                if (targetBook != null)
                {
                    targetBook.CopyCount += 1;

                    if (targetBook.CopyCount > 0)
                    {
                        targetBook.Status = "Borrowable";
                    }

                    // Remove processed Return entry token to prevent reuse attacks
                    _context.Returns.Remove(returnRecord);
                    _context.SaveChanges();

                    TempData["RestockMessage"] = $"Inventory replenished! Book '{targetBook.Title}' copy count increased. Circulation pipeline completed.";
                }
            }

            return RedirectToPage();
        }
    }
}