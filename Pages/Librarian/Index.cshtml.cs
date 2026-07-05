using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace SarasaviLibrary.Pages.Librarian
{
    [Authorize(Roles = "Librarian")] // 💡 3. PROTECT THIS PAGE WITH ROLE-BASED AUTHORIZATION
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}