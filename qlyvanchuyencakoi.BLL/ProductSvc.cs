using qlyvanchuyencakoi.Common.BLL;
using qlyvanchuyencakoi.DAL.Models;
using qlyvanchuyencakoi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using qlyvanchuyencakoi.Common.Rsp;

namespace qlyvanchuyencakoi.BLL
{
	public class ProductSvc : GenericSvc<ProductRep, Product>
	{
		private ProductRep ProductRep;
		public ProductSvc()
		{
			ProductRep = new ProductRep();
		}

		public override SingleRsp Read(int id) // Đọc thông tin product
		{
			var res = new SingleRsp();
			res.Data = _rep.Read(id);
			return res;
		}

		public SingleRsp Create(Product newProduct) // Tạo mới Product
		{
			var res = new SingleRsp();

			try
			{
				// Thực hiện thêm người dùng vào cơ sở dữ liệu
				_rep.Create(newProduct);
				res.Data = newProduct;
			}
			catch (Exception ex)
			{
				res.SetError("EZ107", "Lỗi khi tạo người dùng: " + ex.Message);
			}

			return res;
		}

		public override SingleRsp Update(Product upd) // Update thông tin Product
		{
			var res = new SingleRsp();

			var existingProduct = _rep.Read(upd.IdProduct);

			// Kiểm tra nếu không tồn tại
			if (existingProduct == null)
			{
				res.SetError("EZ103", "User not found.");
			}
			else
			{
				existingProduct.ProductName = upd.ProductName;
				existingProduct.Price = upd.Price;
				existingProduct.Image = upd.Image;
				existingProduct.Note = upd.Note;
				res = base.Update(existingProduct);


				res.Data = existingProduct;
			}

			return res;
		}

		public SingleRsp Delete(int id)
		{
			var res = new SingleRsp();

			var deletedProduct = _rep.DeleteById(id);
			if (deletedProduct == null)
			{
				res.SetError("EZ104", "Product không tồn tại.");
			}
			else
			{
				res.Data = deletedProduct;
			}

			return res;
		}
	}
}
