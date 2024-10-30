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
	public class ShipperSvc : GenericSvc<ShipperRep, Shipper>
	{
		private ShipperRep ShiperRep;
		public ShipperSvc()
		{
			ShiperRep = new ShipperRep();
		}

		public override SingleRsp Read(int id) // Đọc thông tin Shipper
		{
			var res = new SingleRsp();
			res.Data = _rep.Read(id);
			return res;
		}

		public SingleRsp Create(Shipper newShipper) // Tạo mới User
		{
			var res = new SingleRsp();

			try
			{
				// Thực hiện thêm người dùng vào cơ sở dữ liệu
				_rep.Create(newShipper);
				res.Data = newShipper;
			}
			catch (Exception ex)
			{
				res.SetError("EZ107", "Lỗi khi tạo người dùng: " + ex.Message);
			}

			return res;
		}

		public override SingleRsp Update(Shipper upd) // Update thông tin User
		{
			var res = new SingleRsp();

			// Tìm User từ CSDL dựa trên IdShipper
			var existingShipper = _rep.Read(upd.IdShipper);

			// Kiểm tra nếu Shipper không tồn tại
			if (existingShipper == null)
			{
				res.SetError("EZ103", "User not found.");
			}
			else
			{
				// Cập nhật các thông tin cho Shipper
				existingShipper.NameS = upd.NameS;
				existingShipper.NumberPhone = upd.NumberPhone;
				existingShipper.BienXo = upd.BienXo;
				// Gọi hàm Update của lớp cha để cập nhật vào CSDL
				res = base.Update(existingShipper);

				// Trả về đối tượng User sau khi cập nhật
				res.Data = existingShipper;
			}

			return res;
		}

		public SingleRsp Delete(int id) // Xóa User
		{
			var res = new SingleRsp();

			// Xóa user bằng id
			var deletedShipper = _rep.DeleteById(id);
			if (deletedShipper == null)
			{
				res.SetError("EZ104", "Shipper không tồn tại.");
			}
			else
			{
				res.Data = deletedShipper;
			}

			return res;
		}
	}

}
