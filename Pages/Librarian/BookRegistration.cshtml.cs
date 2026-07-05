using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SarasaviLibrary.Data;

namespace SarasaviLibrary.Pages.Librarian
{
    public class BookRegistrationModel : PageModel
    {
        private readonly AppDbContext _context;

        public BookRegistrationModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookFormInput BookInput { get; set; } = new BookFormInput();

        public List<Book> RegisteredBooks { get; set; } = new List<Book>();

        public void OnGet()
        {
            LoadInventoryCatalog();
        }

        public IActionResult OnPostRegister()
        {
            if (!ModelState.IsValid)
            {
                LoadInventoryCatalog();
                return Page();
            }

            string cleanedClassification = BookInput.Classification.Trim().ToUpper();
            int standardNextId = 1;

            var lastBookInClass = _context.Books
                .Where(b => b.Classification == cleanedClassification)
                .ToList()
                .OrderByDescending(b => b.BookNumber)
                .FirstOrDefault();

            if (lastBookInClass != null)
            {
                var parts = lastBookInClass.BookNumber.Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[1], out int currentHighestIndex))
                {
                    standardNextId = currentHighestIndex + 1;
                }
            }

            string generatedBookNumber = $"{cleanedClassification} {standardNextId:D4}";

            // 💡 Business Rule Update: 0 copies = Reference, >=1 = Borrowable
            string finalStatus = BookInput.CopyCount == 0 ? "Reference" : "Borrowable";

            var newBookEntity = new Book
            {
                BookNumber = generatedBookNumber,
                Classification = cleanedClassification,
                Title = BookInput.Title.Trim(),
                Publisher = BookInput.Publisher.Trim(),
                Status = finalStatus,
                CopyCount = BookInput.CopyCount,
                CreatedDate = DateTime.Now
            };

            _context.Books.Add(newBookEntity);
            _context.SaveChanges();

            TempData["SuccessMessage"] = $"Successfully stored '{BookInput.Title}' to Catalog! Generated ID: {generatedBookNumber}.";
            return RedirectToPage();
        }

        public IActionResult OnPostUpdateCopies(string bookNumber, int newCopyCount)
        {
            // Safeguard bounds safely between 0 to 10
            if (newCopyCount < 0) newCopyCount = 0;
            if (newCopyCount > 10) newCopyCount = 10;

            var matchingBook = _context.Books.FirstOrDefault(b => b.BookNumber == bookNumber);
            if (matchingBook != null)
            {
                matchingBook.CopyCount = newCopyCount;

                // 💡 Business Rule Update for update handler: 0 copies = Reference
                matchingBook.Status = newCopyCount == 0 ? "Reference" : "Borrowable";

                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Updated Book {bookNumber} copy count to {newCopyCount} ({matchingBook.Status}).";
            }

            return RedirectToPage();
        }

        private void LoadInventoryCatalog()
        {
            RegisteredBooks = _context.Books.OrderByDescending(b => b.CreatedDate).ToList();
        }
    }

    public class BookFormInput
    {
        [Required]
        [StringLength(1, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z]$")]
        public string Classification { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Publisher { get; set; } = string.Empty;

        [Required]
        [Range(0, 10, ErrorMessage = "Inventory metrics must track between 0 to 10 copies maximum.")] // 💡 Minimum allowed is now 0
        public int CopyCount { get; set; } = 0;
    }
}