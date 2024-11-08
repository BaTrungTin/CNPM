using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using quanlyvanchuyencakoi.web3.Models;

namespace quanlyvanchuyencakoi.web3.Pages.Admin.Product
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
		public QlProduct product { get; set; }

		[BindProperty]
		public IFormFile ImgFileProduct { get; set; }

		public async Task<IActionResult> OnPost()
		{
			if (!ModelState.IsValid || product == null)
			{
				return Page();
			}

			if (ImgFileProduct != null)
			{

				var folderPath = Path.Combine("wwwroot/images/product");
				Directory.CreateDirectory(folderPath);


				var filePath = Path.Combine(folderPath, ImgFileProduct.FileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await ImgFileProduct.CopyToAsync(stream);
				}


				product.ImgProduct = "/images/produc/" + ImgFileProduct.FileName;
			}

			_quanlyContext.Products.Add(product);
			await _quanlyContext.SaveChangesAsync();
			return RedirectToPage(nameof(Index));

		}
	}
}
