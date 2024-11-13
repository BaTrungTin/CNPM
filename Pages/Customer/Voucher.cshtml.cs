using KoiDeliveryOrdering.Repositories.Entities;
using KoiDeliveryOrdering.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.WebApp.Pages.Customer
{
    public class VoucherModel : PageModel
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherModel(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
            Vouchers = new List<Voucher>();
        }

        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public List<Voucher> Vouchers { get; set; }

        public async Task OnGetAsync()
        {
            // Lấy danh sách tất cả các voucher
            var allVouchers = await _voucherRepository.GetAllVouchers();

            // Lọc voucher có trạng thái hoạt động và ngày hiện tại nằm trong khoảng thời gian áp dụng
            Vouchers = allVouchers
                .Where(v => v.Status == 1 && v.StartDate <= DateTime.Now && v.EndDate >= DateTime.Now)
                .ToList();

            // Thông tin người dùng (có thể lấy từ hệ thống xác thực)
            UserName = "Admin";
            UserEmail = "admin@koi.com";
        }
    }
}
