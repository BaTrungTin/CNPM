using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quanlyvanchuyencakoi.web3.Models;
using System.Threading.Tasks;

namespace quanlyvanchuyencakoi.web3.Pages.Admin.Shipper
{
    public class EditModel : PageModel
    {
        private readonly QuanlyContext _context;

        public EditModel(QuanlyContext context)
        {
            _context = context;
        }

        // Bind the Shipper model to handle form data
        [BindProperty]
        public QlShipper Shipper { get; set; }

        // Load shipper data based on ID passed as a route parameter
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Shipper = await _context.Shippers.FindAsync(id);

            if (Shipper == null)
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

            var shipperInDb = await _context.Shippers.FindAsync(Shipper.IdShipper);
            if (shipperInDb == null)
            {
                return NotFound();
            }

            // Update the properties of the shipper in the database
            shipperInDb.NameShipper = Shipper.NameShipper;
            shipperInDb.PhoneShipper = Shipper.PhoneShipper;
            shipperInDb.ImgShipper = Shipper.ImgShipper;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index"); // Redirect to the list of shippers after saving
        }
    }
}
