using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SarasaviLibrary.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                // We will add the logic to query SarasaviLibraryDB here later!
                _logger.LogInformation($"User searched for: {SearchQuery}");
            }
        }
    }
}