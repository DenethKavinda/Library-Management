using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace SarasaviLibrary.Pages.Users
{
    [Authorize(Roles = "User")] // 💡 3. PROTECT THIS PAGE WITH ROLE-BASED AUTHORIZATION
    public class HomeModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}