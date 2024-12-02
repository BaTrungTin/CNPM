using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using quanlyvanchuyencakoi.web3.Models;
using System.IO;
using System.Threading.Tasks;

namespace quanlyvanchuyencakoi.web3.Pages.Admin.Shipper
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
		public QlShipper shipper { get; set; } // Đối tượng QlShipper mà chúng ta sẽ thao tác

		[BindProperty]
		public IFormFile ImgFileShipper { get; set; } // Thuộc tính để nhận tệp ảnh từ form

		public async Task<IActionResult> OnPostAsync()
		{
			// Kiểm tra tính hợp lệ của dữ liệu
			if (!ModelState.IsValid || shipper == null)
			{
				return Page();
			}

			/// abd to database
			_quanlyContext.Shippers.Add(shipper);
			await _quanlyContext.SaveChangesAsync();
			return RedirectToPage("Index");
		}
	}
}
