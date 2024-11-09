using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quanlyvanchuyencakoi.web3.Models;

namespace quanlyvanchuyencakoi.web3.Pages.Admin.NguoiDung
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
        public QlUser User { get; set; }


        // Handle form submission
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var USerInDb = await _context.Users.FindAsync(User.IdUser);
            if (USerInDb == null)
            {
                return NotFound();
            }
            else
            {
                // Update user properties here
                USerInDb.UserName = User.UserName;
                USerInDb.Password = User.Password;
                USerInDb.Email = User.Email;
                USerInDb.PhoneNumber = User.PhoneNumber;
                USerInDb.Name = User.Name;
                USerInDb.Address = User.Address;
                USerInDb.Access = User.Access;

                await _context.SaveChangesAsync();
                return RedirectToPage("./Index"); // Redirect to the index page after editing
            }
        }
    }
}
