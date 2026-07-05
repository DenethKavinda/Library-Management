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
    public class HomeModel : PageModel
    {
        private readonly AppDbContext _context;
        public HomeModel(AppDbContext context) { _context = context; }

        public string CurrentUserName { get; set; } = string.Empty;
        public string CurrentUserNumber { get; set; } = string.Empty;
        public string CurrentUserNIC { get; set; } = string.Empty;

        public List<Loan> ActiveLoans { get; set; } = new List<Loan>();
        public List<Reservation> PendingReservations { get; set; } = new List<Reservation>();
        public int TotalOccupiedSlots { get; set; }

        public void OnGet()
        {
            // 1. Pull current user identification particulars out of authentication cookie claims
            CurrentUserNumber = User.FindFirst("UserNumber")?.Value ?? string.Empty;
            CurrentUserName = User.FindFirst(ClaimTypes.Name)?.Value ?? "Valued Member";

            // Fetch secondary verification specifics from Database profile safely if needed
            var userProfile = _context.Users.FirstOrDefault(u => u.UserNumber == CurrentUserNumber);
            CurrentUserNIC = userProfile?.NIC ?? "N/A";

            // 2. Fetch active collections to list directly inside layout components
            string cleanUser = CurrentUserNumber.Replace(" ", "").ToUpper();

            ActiveLoans = _context.Loans
                .Where(l => l.UserNumber.Replace(" ", "").ToUpper() == cleanUser && l.Status == "Active")
                .ToList();

            PendingReservations = _context.Reservations
                .Where(r => r.UserNumber.Replace(" ", "").ToUpper() == cleanUser && r.Status == "Pending")
                .ToList();

            // 3. Mathematical calculation layout properties for quota indicators ($Pending + Active == 5)
            TotalOccupiedSlots = ActiveLoans.Count + PendingReservations.Count;
        }
    }
}