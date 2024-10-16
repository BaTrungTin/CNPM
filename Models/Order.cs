using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
// Class đặt hàng
namespace KOI.Core.Models
{
    public class Order
    {
        public int Id { get; set; }   // id đặt hàng
        public int ProductId { get; set; }   // id sản phẩm
        public DateTime Time { get; set; }   // thời gian đặt hàng
        public OrderStatus OrderStatus { get; set; }  // trạng thái đặt hàng

    }

    public enum OrderStatus
    {
        Processing,
        Shipped,
        Cancelled
    }
}
