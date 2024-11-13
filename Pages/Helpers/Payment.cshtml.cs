using KoiDeliveryOrdering.Repositories.Entities;
using KoiDeliveryOrdering.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.WebApp.Pages.Customer
{
    public class PaymentModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public PaymentModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int orderId)
        {
            Order = await _orderRepository.GetById(orderId);
            if (Order == null)
            {
                TempData["ErrorMessage"] = "Đơn hàng không tồn tại.";
                return RedirectToPage("/Customer/Tracking");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);
            if (order == null)
            {
                TempData["ErrorMessage"] = "Đơn hàng không tồn tại.";
                return RedirectToPage("/Customer/Tracking");
            }

            // Cập nhật trạng thái thanh toán
            order.PaymentStatus = "Paid";
            await _orderRepository.UppOrderStatus(order.OrderId, order.OrderStatus); // Giả định đã có phương thức cập nhật trạng thái

            TempData["SuccessMessage"] = "Thanh toán đã được xác nhận.";
            return RedirectToPage("/Customer/Tracking");
        }
    }
}
