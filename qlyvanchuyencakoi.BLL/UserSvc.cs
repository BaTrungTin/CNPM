using qlyvanchuyencakoi.DAL;
using qlyvanchuyencakoi.Common.BLL;
using qlyvanchuyencakoi.Common.Rsp;
using qlyvanchuyencakoi.DAL.Models;


namespace qlyvanchuyencakoi.BLL
{
    public class UserSvc : GenericSvc<UserRep, User>
    {
        private UserRep userRep;
        public UserSvc()
        {
            userRep = new UserRep();
        }

        public override SingleRsp Read(int id) // Đọc thông tin User 
		{
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            return res;
        }




		//public override SingleRsp Read(string name)
		//{
		//	var res = new SingleRsp();
		//	res.Data = _rep.Read(name);
		//	return res;
		//}

		public override SingleRsp Update(User upd) // Update thông tin User
		{
			var res = new SingleRsp();

			// Tìm User từ CSDL dựa trên IdUser
			var existingUser = _rep.Read(upd.Id);

			// Kiểm tra nếu User không tồn tại
			if (existingUser == null)
			{
				res.SetError("EZ103", "User not found.");
			}
			else
			{
				// Cập nhật các thông tin cho User
				existingUser.Name = upd.Name;
				existingUser.Address = upd.Address;
				existingUser.PhoneNumber = upd.PhoneNumber;
				existingUser.Age = upd.Age > 0 ? upd.Age : existingUser.Age;

				// Gọi hàm Update của lớp cha để cập nhật vào CSDL
				res = base.Update(existingUser);

				// Trả về đối tượng User sau khi cập nhật
				res.Data = existingUser;
			}

			return res;
		}

		public SingleRsp Delete(int id) // Xóa User
		{
			var res = new SingleRsp();

			// Xóa user bằng id
			var deletedUser = _rep.DeleteById(id);
			if (deletedUser == null)
			{
				res.SetError("EZ104", "User không tồn tại.");
			}
			else
			{
				res.Data = deletedUser;
			}

			return res;
		}

		public SingleRsp Create(User newUser) // Tạo mới User
		{
			var res = new SingleRsp();

			try
			{
				// Thực hiện thêm người dùng vào cơ sở dữ liệu
				_rep.Create(newUser);
				res.Data = newUser;
			}
			catch (Exception ex)
			{
				res.SetError("EZ107", "Lỗi khi tạo người dùng: " + ex.Message);
			}

			return res;
		}


	}
}