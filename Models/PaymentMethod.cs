using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// phương thức chuyển khoản
namespace KOI.Core.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string PaymentMethodName { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
    public enum PaymentStatus
    {
        Unpaid,
        Paid
    }

}
