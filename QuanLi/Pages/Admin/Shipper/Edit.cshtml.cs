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

        
        [BindProperty]
        public QlShipper shipper { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var shipperInDb = await _context.Shippers.FindAsync(shipper.IdShipper);
            if (shipperInDb == null)
            {
                return NotFound();
            }
            else
            {
                
                shipperInDb.NameShipper = shipper.NameShipper;
                shipperInDb.PhoneShipper = shipper.PhoneShipper;
                shipperInDb.ImgShipper = shipper.ImgShipper;

                await _context.SaveChangesAsync();
                return RedirectToPage("./Index"); 
            }
        }
    }
}
