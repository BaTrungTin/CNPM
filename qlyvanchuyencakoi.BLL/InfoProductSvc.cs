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
	public class InfoProductSvc : GenericSvc<InfoProductRep, InfoProduct>
	{
		private InfoProductRep InfoProductRep;
		public InfoProductSvc()
		{
			InfoProductRep = new InfoProductRep();
		}

		public override SingleRsp Read(int id)
		{
			var res = new SingleRsp();
			res.Data = _rep.Read(id);
			return res;
		}

		public SingleRsp Create(InfoProduct newInfoProduct) 
		{
			var res = new SingleRsp();

			try
			{
				_rep.Create(newInfoProduct);
				res.Data = newInfoProduct;
			}
			catch (Exception ex)
			{
				res.SetError("EZ107", "Lỗi khi tạo người dùng: " + ex.Message);
			}

			return res;
		}

		public override SingleRsp Update(InfoProduct upd)
		{
			var res = new SingleRsp();

			var existingInfoProduct = _rep.Read(upd.IdInfoProduct);

			if (existingInfoProduct == null)
			{
				res.SetError("EZ103", "User not found.");
			}
			else
			{
				existingInfoProduct.ProducId = upd.ProducId;
				existingInfoProduct.ShippingMethod = upd.ShippingMethod;
				existingInfoProduct.PaymentMethod = upd.PaymentMethod;
				existingInfoProduct.AllPrice = upd.AllPrice;
				existingInfoProduct.StatusShipping = upd.StatusShipping;
				res = base.Update(existingInfoProduct);


				res.Data = existingInfoProduct;
			}

			return res;
		}

		public SingleRsp Delete(int id)
		{
			var res = new SingleRsp();

			var deletedInfoProduct = _rep.DeleteById(id);
			if (deletedInfoProduct == null)
			{
				res.SetError("EZ104", "InfoProduct không tồn tại.");
			}
			else
			{
				res.Data = deletedInfoProduct;
			}

			return res;
		}

	}
}
