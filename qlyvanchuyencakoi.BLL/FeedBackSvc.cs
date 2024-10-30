using qlyvanchuyencakoi.Common.Rsp;
using qlyvanchuyencakoi.DAL.Models;
using qlyvanchuyencakoi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using qlyvanchuyencakoi.Common.BLL;

namespace qlyvanchuyencakoi.BLL
{
	public class FeedBackSvc : GenericSvc<FeedBackRep, FeedBack>
	{
		private FeedBackRep FeedBackRep;
		public FeedBackSvc()
		{
			FeedBackRep = new FeedBackRep();
		}

		public override SingleRsp Read(int id)
		{
			var res = new SingleRsp();
			res.Data = _rep.Read(id);
			return res;
		}

		public SingleRsp Create(FeedBack newFeedBack)
		{
			var res = new SingleRsp();

			try
			{
				_rep.Create(newFeedBack);
				res.Data = newFeedBack;
			}
			catch (Exception ex)
			{
				res.SetError("EZ107", "Lỗi khi tạo người dùng: " + ex.Message);
			}

			return res;
		}

		public override SingleRsp Update(FeedBack upd)
		{
			var res = new SingleRsp();

			var existingFeedBack = _rep.Read(upd.IdFeedBack);

			// Kiểm tra nếu không tồn tại
			if (existingFeedBack == null)
			{
				res.SetError("EZ103", "User not found.");
			}
			else
			{
				existingFeedBack.IdFeedBack = upd.IdFeedBack;
				existingFeedBack.IdProduct1 = upd.IdProduct1;
				existingFeedBack.FeedBackContent = upd.FeedBackContent;
				res = base.Update(existingFeedBack);


				res.Data = existingFeedBack;
			}

			return res;
		}

		public SingleRsp Delete(int id)
		{
			var res = new SingleRsp();

			var deletedFeedBack = _rep.DeleteById(id);
			if (deletedFeedBack == null)
			{
				res.SetError("EZ104", "Product không tồn tại.");
			}
			else
			{
				res.Data = deletedFeedBack;
			}

			return res;
		}
	}
}
