using KoiDeliveryOrdering.Repositories.Entities;
using KoiDeliveryOrdering.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.WebApp.Pages.Customer.Express
{
    public class CreateModel : PageModel
    {
        private readonly IOrderService _orderService;

        public CreateModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public Order Order { get; set; }

        public decimal TotalAmount { get; set; } // Thêm thuộc tính TotalAmount

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var createdOrder = await _orderService.CreateOrder(Order);

            if (Order.PaymentMethod == "Chuyển khoản ngân hàng")
            {
                // Nếu phương thức thanh toán là chuyển khoản, chuyển hướng đến trang Payment
                return RedirectToPage("/Customer/Payment", new { orderId = createdOrder.OrderId });
            }

            TempData["SuccessMessage"] = "Đơn hàng đã được tạo thành công.";
            return RedirectToPage("/Customer/Tracking");
        }

        private decimal CalculateTotalAmount(Order order)
        {
            // Tính tổng chi phí dựa trên trọng lượng, dịch vụ, phương thức vận chuyển, v.v.
            // Giả sử tính phí cơ bản là 10000 VND cho mỗi kg
            decimal baseRate = 10000m;
            return order.Weight * baseRate;
        }
    }
}
