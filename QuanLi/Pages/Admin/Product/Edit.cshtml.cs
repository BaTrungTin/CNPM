using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quanlyvanchuyencakoi.web3.Models;
using System.Threading.Tasks;

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

        // Bind the product model to handle form data
        [BindProperty]
        public QlProduct Product { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var productInDb = await _context.Products.FindAsync(Product.IdProduct);
            if (productInDb == null)
            {
                return NotFound();
            }

            
            productInDb.NameProduct = Product.NameProduct;
            productInDb.Note = Product.Note;
            productInDb.Price = Product.Price;
            productInDb.ImgProduct = Product.ImgProduct;

            
            _context.Entry(productInDb).Property(p => p.NameProduct).IsModified = true;
            _context.Entry(productInDb).Property(p => p.Note).IsModified = true;
            _context.Entry(productInDb).Property(p => p.Price).IsModified = true;
            _context.Entry(productInDb).Property(p => p.ImgProduct).IsModified = true;

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
