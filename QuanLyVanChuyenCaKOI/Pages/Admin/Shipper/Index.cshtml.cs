using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quanlyvanchuyencakoi.web3.Models;

namespace quanlyvanchuyencakoi.web3.Pages.Admin.Shipper
{
    public class IndexModel : PageModel
    {
        private readonly QuanlyContext _quanlyContext;

        public IndexModel(QuanlyContext quanlyContext)
        {
            _quanlyContext = quanlyContext;
        }

        public List<QlShipper> shipper { get; set; }


        public async Task OnGetAsync()
        {
            if (_quanlyContext.Users != null)
            {
                shipper = await _quanlyContext.Shippers.ToListAsync();
            }
        }
    }
}
