using KoiDeliveryOrdering.Repositories.Entities;
using KoiDeliveryOrdering.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.WebApp.Pages.Customer.Express
{
    public class HistoryModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public HistoryModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            Orders = new List<Order>();
        }

        public List<Order> Orders { get; set; }

        public async Task OnGetAsync(int customerId)
        {
            Orders = await _orderRepository.GetOrdersByCustomerId(customerId);

            if (Orders == null || Orders.Count == 0)
                return;
        }
    }
}
