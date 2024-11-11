using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using quanlyvanchuyencakoi.web3.Models;

namespace quanlyvanchuyencakoi.web3.Pages.Admin.NguoiDung
{
    public class CreateModel : PageModel
    {
        private readonly QuanlyContext _quanlyContext;

        public CreateModel(QuanlyContext quanlyContext)
        {
            _quanlyContext = quanlyContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public QlUser user { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid || _quanlyContext.Users == null || user == null)
            {
                return Page();
            }
            _quanlyContext.Users.Add(user);
            await _quanlyContext.SaveChangesAsync();
            return RedirectToPage(nameof(Index));

        }

    }
}
