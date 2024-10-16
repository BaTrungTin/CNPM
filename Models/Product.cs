using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Class in4 sản phẩm 
namespace KOI.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }      // tên sản phẩm
        public long ProductPrice { get; set; }      // giá sản phẩm
        public int ProductCapacity { get; set; }    // số lượng sản phẩm
        public ProductStatus ProductStatus { get; set; }  // trạng thái sản phẩm
    }

    public enum ProductStatus
    {
        Available,
        OutOfOrder
    }
}
