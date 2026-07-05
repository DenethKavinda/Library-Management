using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SarasaviLibrary.Data;

namespace SarasaviLibrary.Pages.Users
{
    [Authorize(Roles = "User")]
    public class ReservationModel : PageModel // 💡 FIX: Renamed from ReservationsModel to ReservationModel
    {
        private readonly AppDbContext _context;

        public ReservationModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Book> CatalogBooks { get; set; } = new List<Book>();
        public string CurrentUserNumber { get; set; } = string.Empty;

        public void OnGet()
        {
            FetchUserAndCatalog();
        }

        public IActionResult OnPost(string bookNumber)
        {
            FetchUserAndCatalog();

            var targetBook = _context.Books.FirstOrDefault(b => b.BookNumber == bookNumber);
            if (targetBook == null) return Page();

            // Check 1: Reference constraints restriction rule checking
            if (targetBook.Status == "Reference" || targetBook.CopyCount <= 0)
            {
                TempData["ModalType"] = "WARNING";
                TempData["ModalMessage"] = "This item is classified for reference only inside the library hall and cannot be requested.";
                return RedirectToPage();
            }

            // Check 2: Max quota saturation limitation allocation (Max 5 items per card)
            int currentBorrowCount = _context.Reservations.Count(r => r.UserNumber == CurrentUserNumber && r.Status == "Pending");
            if (currentBorrowCount >= 5)
            {
                TempData["ModalType"] = "LIMIT_EXCEEDED";
                TempData["ModalMessage"] = "Maximum quota limit exceeded! You cannot reserve more than 5 books under your current membership account tier.";
                return RedirectToPage();
            }

            // Execution 3: Transaction verification & live count manipulation
            targetBook.CopyCount -= 1;

            if (targetBook.CopyCount == 0)
            {
                targetBook.Status = "Reference";
            }

            string generatedResCode = $"RES-{new Random().Next(10000, 99999)}";

            var newReservation = new Reservation
            {
                ReservationCode = generatedResCode,
                UserNumber = CurrentUserNumber,
                BookNumber = bookNumber,
                ReservedDate = DateTime.Now,
                Status = "Pending"
            };

            _context.Reservations.Add(newReservation);
            _context.SaveChanges();

            TempData["ModalType"] = "SUCCESS";
            TempData["ModalMessage"] = generatedResCode;

            return RedirectToPage();
        }

        private void FetchUserAndCatalog()
        {
            CurrentUserNumber = User.FindFirst("UserNumber")?.Value ?? string.Empty;
            CatalogBooks = _context.Books.OrderBy(b => b.Title).ToList();
        }
    }
}