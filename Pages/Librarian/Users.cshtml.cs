using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SarasaviLibrary.Data;

namespace SarasaviLibrary.Pages.Librarian
{
    public class UsersModel : PageModel
    {
        private readonly AppDbContext _context;

        public UsersModel(AppDbContext context)
        {
            _context = context;
        }

        // Collection to store matching user accounts pulled from the database
        public List<User> RegisteredUsers { get; set; } = new List<User>();

        public void OnGet()
        {
            // Query users from the SQL database and sort by their creation order
            RegisteredUsers = _context.Users
                .OrderByDescending(u => u.CreatedDate)
                .ToList();
        }
    }
}