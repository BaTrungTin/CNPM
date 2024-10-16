using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// in4 người dùng
namespace KOI.Core.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; } // id người dùng
        public string CustomerAddress { get; set; } // địa chỉ người dùng
        public string PhoneNumber { get; set; }  // số điện thoại
        public string CustomerName { get; set; }  // tên người dùng
        public string Email { get; set; }  // email
    }
}
