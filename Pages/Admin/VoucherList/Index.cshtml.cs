using KoiDeliveryOrdering.Repositories.Entities;
using KoiDeliveryOrdering.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.WebApp.Pages.Admin.VoucherList
{
    public class IndexModel : PageModel
    {
        private readonly IVoucherRepository _voucherRepository;

        public IndexModel(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
            Vouchers = new List<Voucher>();
        }

        public List<Voucher> Vouchers { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(string SearchString, string Status, int pageIndex = 1)
        {
            // Lấy tất cả voucher từ repository
            var allVouchers = await _voucherRepository.GetAllVouchers();

            // Áp dụng bộ lọc tìm kiếm theo mã voucher
            if (!string.IsNullOrEmpty(SearchString))
            {
                allVouchers = allVouchers.Where(v => v.VoucherCode.Contains(SearchString)).ToList();
            }

            // Áp dụng bộ lọc trạng thái
            if (!string.IsNullOrEmpty(Status))
            {
                if (Status == "Active")
                    allVouchers = allVouchers.Where(v => v.Status == 1).ToList();
                else if (Status == "Inactive")
                    allVouchers = allVouchers.Where(v => v.Status == 0).ToList();
            }

            // Thiết lập kích thước trang và tính toán số trang tổng cộng
            int pageSize = 20; // Điều chỉnh kích thước trang nếu cần
            TotalPages = (int)Math.Ceiling(allVouchers.Count / (double)pageSize);
            Vouchers = allVouchers.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            CurrentPage = pageIndex;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var success = await _voucherRepository.DelVoucher(id);
            if (success)
            {
                TempData["SuccessMessage"] = "Voucher đã được xóa thành công!";
                return RedirectToPage();
            }
            else
            {
                TempData["ErrorMessage"] = "Không thể xóa voucher. Vui lòng thử lại!";
                return RedirectToPage();
            }
        }
    }
}
