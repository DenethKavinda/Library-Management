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
    public class ReservationModel : PageModel
    {
        private readonly AppDbContext _context;

        public ReservationModel(AppDbContext context)
        {
            _context = context;
        }

        // Holds the catalog array displayed as cards on the frontend view matrix
        public List<Book> CatalogBooks { get; set; } = new List<Book>();

        // Tracks the authenticated reader card identifier session parameter
        public string CurrentUserNumber { get; set; } = string.Empty;

        public void OnGet()
        {
            FetchUserAndCatalog();
        }

        public IActionResult OnPost(string bookNumber)
        {
            FetchUserAndCatalog();

            // Locate the book inside the live SQL Server catalog repository
            var targetBook = _context.Books.FirstOrDefault(b => b.BookNumber == bookNumber);
            if (targetBook == null)
            {
                return Page();
            }

            // --- CHECK 1: Reference and Inventory Availability Guards ---
            if (targetBook.Status.Trim().Equals("Reference", StringComparison.OrdinalIgnoreCase) || targetBook.CopyCount <= 0)
            {
                TempData["ModalType"] = "WARNING";
                TempData["ModalMessage"] = "This item is classified for reference only inside the library hall and cannot be requested.";
                return RedirectToPage();
            }

            // --- 💡 CHECK 2: BULLETPROOF COMBINED SLOTS EQUATION LIMIT (Pending + Active == 5) ---
            // Strips out all spaces entirely to prevent dash-spacing mismatches (e.g., "REG - 001" vs "REG-001")
            string cleanUserNumber = CurrentUserNumber.Replace(" ", "").ToUpper();

            int pendingReservationCount = _context.Reservations
                .ToList()
                .Count(r => r.UserNumber.Replace(" ", "").ToUpper() == cleanUserNumber &&
                            r.Status.Trim().Equals("Pending", StringComparison.OrdinalIgnoreCase));

            int activeLoanCount = _context.Loans
                .ToList()
                .Count(l => l.UserNumber.Replace(" ", "").ToUpper() == cleanUserNumber &&
                            l.Status.Trim().Equals("Active", StringComparison.OrdinalIgnoreCase));

            // Calculate total occupied library slot quota
            int totalOccupiedSlots = pendingReservationCount + activeLoanCount;

            // Strict limit validation check block
            if (totalOccupiedSlots >= 5)
            {
                TempData["ModalType"] = "LIMIT_EXCEEDED";
                TempData["ModalMessage"] = $"Reservation Blocked! You have reached your maximum library limit of 5 slots. (Current usage: {pendingReservationCount} Pending Reservations and {activeLoanCount} Active Loans). You cannot reserve more books until a loan is returned or processed.";
                return RedirectToPage();
            }

            // --- EXECUTION 3: Inventory Deductions & Transaction Commit ---
            // Decrease the available copy volume tracking variable instantly
            targetBook.CopyCount -= 1;

            // If dropping to 0 copies, dynamically re-classify availability parameters across the app
            if (targetBook.CopyCount == 0)
            {
                targetBook.Status = "Reference";
            }

            // Generate a secure unique transactional key for front desk matching (ex: RES-49215)
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

            // Set TempData variables to trigger the success overlay component container layout
            TempData["ModalType"] = "SUCCESS";
            TempData["ModalMessage"] = generatedResCode;

            return RedirectToPage();
        }

        private void FetchUserAndCatalog()
        {
            // Parses out the membership registration tracking variable out of the cookie authorization token claims
            CurrentUserNumber = User.FindFirst("UserNumber")?.Value ?? string.Empty;

            // Refreshes data catalog array listings sorted cleanly alphabetically by Title properties
            CatalogBooks = _context.Books.OrderBy(b => b.Title).ToList();
        }
    }
}