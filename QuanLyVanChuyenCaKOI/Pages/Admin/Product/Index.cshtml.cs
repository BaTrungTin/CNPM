using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quanlyvanchuyencakoi.web3.Models;

namespace quanlyvanchuyencakoi.web3.Pages.Admin.Product
{
    public class IndexModel : PageModel
    {
		private readonly QuanlyContext _quanlyContext;

		public IndexModel(QuanlyContext quanlyContext)
		{
			_quanlyContext = quanlyContext;
		}

		public List<QlProduct> product { get; set; }


		public async Task OnGetAsync()
		{
			if (_quanlyContext.Products != null)
			{
				product = await _quanlyContext.Products.ToListAsync();
			}
		}
	}
}
