using KoiDeliveryOrdering.Repositories.Entities;
using KoiDeliveryOrdering.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.WebApp.Pages.Admin
{
    public class ManageOrdersModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public ManageOrdersModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            Orders = new List<Order>();
        }

        public List<Order> Orders { get; set; }

        [BindProperty(SupportsGet = true)]
        public string StatusFilter { get; set; } // Để lọc theo trạng thái đơn hàng

        public async Task OnGetAsync()
        {
            // Lấy danh sách đơn hàng và áp dụng bộ lọc trạng thái (nếu có)
            Orders = string.IsNullOrEmpty(StatusFilter)
                ? await _orderRepository.GetAllOrders() // Lấy tất cả đơn hàng nếu không có bộ lọc
                : await _orderRepository.GetByStatus(StatusFilter);
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int orderId, string newStatus)
        {
            // Cập nhật trạng thái của đơn hàng
            var success = await _orderRepository.UppOrderStatus(orderId, newStatus);
            if (success)
            {
                TempData["SuccessMessage"] = "Trạng thái đơn hàng đã được cập nhật.";
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật trạng thái đơn hàng.";
            }

            return RedirectToPage(); // Làm mới trang để hiển thị cập nhật
        }

        public async Task<IActionResult> OnPostDeleteOrderAsync(int orderId)
        {
            // Xóa đơn hàng
            var success = await _orderRepository.DelOrder(orderId);
            if (success)
            {
                TempData["SuccessMessage"] = "Đơn hàng đã được xóa thành công.";
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa đơn hàng.";
            }

            return RedirectToPage(); // Làm mới trang để cập nhật danh sách
        }
    }
}
