using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using quanlyvanchuyencakoi.web3.Models;

namespace quanlyvanchuyencakoi.web3.Pages.Admin.FeedBack
{
    public class DeleteModel : PageModel
    {
        private readonly QuanlyContext _context;

        public DeleteModel(QuanlyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public QlFeedback feedback { get; set; }

        public async Task<IActionResult> OnGetAsync(int itemid)
        {
            feedback = await _context.Feedbacks.FindAsync(itemid);

            if (feedback == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int itemid)
        {
            feedback = await _context.Feedbacks.FindAsync(itemid);

            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
