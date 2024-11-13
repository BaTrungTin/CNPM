using KoiDeliveryOrdering.Repositories.Entities;
using KoiDeliveryOrdering.Repositories.Interfaces;
using KoiDeliveryOrdering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IVoucherService _voucherService;

        public OrderService(IOrderRepository orderRepository, IVoucherService voucherService)
        {
            _orderRepository = orderRepository;
            _voucherService = voucherService;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _orderRepository.GetAllOrders();
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _orderRepository.GetById(id);
        }

        public async Task<Order> CreateOrder(Order order, string? voucherCode = null)
        {
            if (!string.IsNullOrEmpty(voucherCode))
            {
                var voucher = await _voucherService.GetVoucherByCode(voucherCode);
                if (voucher != null)
                {
                    order.TotalPrice = ApplyVoucher(order, voucher);
                }
            }

            return await _orderRepository.Create(order);
        }

        public async Task<bool> DeleteOrder(int id)
        {
            return await _orderRepository.DelOrder(id);
        }

        public async Task<bool> UpdateOrderStatus(int id, string status)
        {
            return await _orderRepository.UppOrderStatus(id, status);
        }

        public async Task<List<Order>> GetOrdersByStatus(string status)
        {
            return await _orderRepository.GetByStatus(status);
        }

        public decimal ApplyVoucher(Order order, Voucher voucher)
        {
            if (order == null || voucher == null)
                return order?.TotalPrice ?? 0;

            if (voucher.DiscountType == "Percentage")
                return order.TotalPrice - (order.TotalPrice * voucher.DiscountValue / 100);

            else if (voucher.DiscountType == "Fixed")
                return order.TotalPrice - voucher.DiscountValue;

            return order.TotalPrice;
        }

        public decimal CalculateShippingCost(Order order)
        {
            const decimal shippingFee = 5.00m;
            return shippingFee;
        }

        public async Task<Order?> GetOrderByVoucherCode(string voucherCode)
        {
            return await _orderRepository.GetByVoucherCode(voucherCode);
        }
    }
}
