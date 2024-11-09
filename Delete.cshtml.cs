using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using quanlyvanchuyencakoi.web3.Models;

namespace quanlyvanchuyencakoi.web3.Pages.Admin.NguoiDung
{
    public class DeleteModel : PageModel
    {
        private readonly QuanlyContext _context;

        public DeleteModel(QuanlyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public QlUser User { get; set; }

        public async Task<IActionResult> OnGetAsync(int itemid)
        {
            User = await _context.Users.FindAsync(itemid);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int itemid)
        {
            User = await _context.Users.FindAsync(itemid);

            if (User != null)
            {
                _context.Users.Remove(User);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
