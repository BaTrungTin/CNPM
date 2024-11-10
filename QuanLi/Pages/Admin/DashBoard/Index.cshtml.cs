using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using quanlyvanchuyencakoi.web3.Models;
using System;
using System.Linq;

namespace quanlyvanchuyencakoi.web3.Pages.Admin.DashBoard
{
    public class IndexModel : PageModel
    {
		private readonly QuanlyContext _context;

		public IndexModel(QuanlyContext context)
		{
			_context = context;
		}

		public int[] MonthlyUserCounts { get; set; } = new int[12];

		public void OnGet()
		{
			// Tạo dữ liệu người dùng cho mỗi tháng trong năm
			MonthlyUserCounts = Enumerable.Range(1, 12).Select(month =>
				_context.Users.Count()
			).ToArray();
		}
	}
}
