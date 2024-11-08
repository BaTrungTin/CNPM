using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using quanlyvanchuyencakoi.web3.Models;

namespace quanlyvanchuyencakoi.web3.Pages.Admin.Product
{
    public class DeleteModel : PageModel
    {
        private readonly QuanlyContext _context;

        public DeleteModel(QuanlyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public QlProduct product { get; set; }

        public async Task<IActionResult> OnGetAsync(int itemid)
        {
            product = await _context.Products.FindAsync(itemid);

            if (product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int itemid)
        {
            product = await _context.Products.FindAsync(itemid);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
