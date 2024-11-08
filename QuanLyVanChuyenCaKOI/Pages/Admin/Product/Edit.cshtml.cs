using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using quanlyvanchuyencakoi.web3.Models;

namespace quanlyvanchuyencakoi.web3.Pages.Admin.Product
{
    public class EditModel : PageModel
    {
        private readonly QuanlyContext _context;

        // Inject the database context into the page model
        public EditModel(QuanlyContext context)
        {
            _context = context;
        }

        // Bind the user model to handle form data
        [BindProperty]
        public QlProduct product { get; set; }

        // Load user data based on ID passed as a route parameter
        public async Task<IActionResult> OnGetAsync(int id)
        {
            product = await _context.Products.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }

        // Handle form submission
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var productInDb = await _context.Products.FindAsync(product.IdProduct);
            if (productInDb == null)
            {
                return NotFound();
            }

            // Update user properties here
            productInDb.NameProduct = product.NameProduct;
            productInDb.Note = product.Note;
            productInDb.Price = product.Price;
            productInDb.ImgProduct = product.ImgProduct;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index"); // Redirect to the index page after editing
        }
    }
}
