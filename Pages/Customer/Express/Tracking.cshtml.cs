using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrdering.Repositories.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.WebApp.Pages.Customer.Express
{
    public class TrackingModel : PageModel
    {
        private readonly KoiDeliveryOrderingContext _dbContext;

        public TrackingModel(KoiDeliveryOrderingContext dbContext)
        {
            _dbContext = dbContext;
            Orders = new List<Order>();
        }

        public IList<Order> Orders { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? OrderId { get; set; } // Biến để lưu OrderId được truyền vào từ Create

        public async Task OnGetAsync()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(currentUserId, out var userId))
            {
                // Nếu có OrderId, chỉ lấy đơn hàng với ID đó, ngược lại lấy tất cả đơn hàng của người dùng
                Orders = OrderId.HasValue
                    ? await _dbContext.Orders.Where(o => o.CustomerId == userId && o.OrderId == OrderId).ToListAsync()
                    : await _dbContext.Orders.Where(o => o.CustomerId == userId).OrderByDescending(o => o.OrderId).ToListAsync();
            }
            else
            {
                Orders = new List<Order>();
            }
        }
    }
}
