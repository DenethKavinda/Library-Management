using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SarasaviLibrary.Data;

namespace SarasaviLibrary.Pages
{

    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; } = string.Empty;

        // Container tracking books array data matching lookup variables
        public List<Book> BooksList { get; set; } = new List<Book>();

        public void OnGet()
        {
            // Fetch live database records
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                string cleanSearch = SearchQuery.Trim().ToLower();

                // Filters by Book Title OR Publisher Name matching criteria rules
                query = query.Where(b => b.Title.ToLower().Contains(cleanSearch) ||
                                         b.Publisher.ToLower().Contains(cleanSearch));
            }

            // Bind sorted outputs to frontend iteration layer loop
            BooksList = query.OrderBy(b => b.Title).ToList();
        }
    }
}