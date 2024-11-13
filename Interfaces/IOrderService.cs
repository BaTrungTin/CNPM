using KoiDeliveryOrdering.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<Order?> GetOrderById(int id);
        Task<Order> CreateOrder(Order order, string? voucherCode = null);
        Task<bool> DeleteOrder(int id);
        Task<bool> UpdateOrderStatus(int id, string status);
        Task<List<Order>> GetOrdersByStatus(string status);

        decimal ApplyVoucher(Order order, Voucher voucher);

        decimal CalculateShippingCost(Order order);

        Task<Order?> GetOrderByVoucherCode(string voucherCode);
    }
}
