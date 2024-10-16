using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Class thông tin giao hàng

namespace KOI.Core.Models
{
    public class ShippingInformation
    {
        public int Id { get; set; }  // id giao hàng
        public string ShippingUnit { get; set; }   // đơn vị giao hàng
        public DateTime Day { get; set; }
        public ShippingMethod shippingMethod { get; set; }
    }
    
    public enum ShippingMethod
    {
        Ordered,                        // đã đặt hàng
        Processing,                     // đang sử lí
        Packaged,                       // Đã đóng gói
        Delivering,                     // Đang giao hàng                    
        Piad,                          // Đã thanh toán 
        Completed,                      // Giao thành công
        Canceled                        // Đã hủy
    }
}
