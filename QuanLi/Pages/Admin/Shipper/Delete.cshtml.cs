using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using quanlyvanchuyencakoi.web3.Models;
using System.Threading.Tasks;

namespace quanlyvanchuyencakoi.web3.Pages.Admin.Shipper
{
	public class DeleteModel : PageModel
	{
		private readonly QuanlyContext _context;

		public DeleteModel(QuanlyContext context)
		{
			_context = context;
		}

		[BindProperty]
		public QlShipper Shipper { get; set; }

		public async Task<IActionResult> OnGetAsync(int itemid)
		{
			Shipper = await _context.Shippers.FindAsync(itemid);

			if (Shipper == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int itemid)
		{
			Shipper = await _context.Shippers.FindAsync(itemid);

			if (Shipper != null)
			{
				_context.Shippers.Remove(Shipper);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
