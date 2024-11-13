using KoiDeliveryOrdering.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();

        Task<Order?> GetById(int id);

        Task<Order> Create(Order order);

        Task<bool> DelOrder(int id);

        Task<bool> UppOrderStatus(int id, string status);

        Task<List<Order>> GetByStatus(string status);

        Task<Order?> GetByVoucherCode(string voucherCode);

        Task<List<Order>> GetOrdersByCustomerId(int customerId);
    }
}
