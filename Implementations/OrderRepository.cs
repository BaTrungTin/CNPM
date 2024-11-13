using KoiDeliveryOrdering.Repositories.Entities;
using KoiDeliveryOrdering.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly KoiDeliveryOrderingContext _dbContext;

        public OrderRepository(KoiDeliveryOrderingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order?> GetById(int id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<Order> Create(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DelOrder(int id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UppOrderStatus(int id, string status)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
            if (order != null)
            {
                order.OrderStatus = status;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Order>> GetByStatus(string status)
        {
            return await _dbContext.Orders.Where(o => o.OrderStatus == status).ToListAsync();
        }

        public async Task<Order?> GetByVoucherCode(string voucherCode)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(o => o.Voucher != null && o.Voucher.VoucherCode == voucherCode);
        }

        public async Task<List<Order>> GetOrdersByCustomerId(int customerId)
        {
            return await _dbContext.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
        }
    }
}
