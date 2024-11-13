using KoiDeliveryOrdering.Repositories.Entities;
using KoiDeliveryOrdering.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.WebApp.Pages.Admin.VoucherList
{
    public class CreateModel : PageModel
    {
        private readonly IVoucherRepository _voucherRepository;

        public CreateModel(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
            Voucher = new Voucher();
        }

        [BindProperty]
        public Voucher Voucher { get; set; }

        public void OnGet()
        {
            Voucher.VoucherCode = "VOUCHER" + new Random().Next(1000, 9999).ToString();
            Voucher.StartDate = DateTime.Now;
            Voucher.EndDate = DateTime.Now.AddMonths(1);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingVoucher = await _voucherRepository.GetVoucherByCode(Voucher.VoucherCode);
            if (existingVoucher != null)
            {
                TempData["ErrorMessage"] = "Mã voucher này đã tồn tại. Vui lòng tạo mã khác!";
                return Page();
            }

            // Thêm voucher vào cơ sở dữ liệu
            var result = await _voucherRepository.Create(Voucher);
            if (result != null)
            {
                TempData["SuccessMessage"] = "Voucher đã được tạo thành công!";
                return RedirectToPage("/Admin/VoucherList/Index"); // Chuyển hướng về Index không cần forceRefresh
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tạo voucher!";
                return Page();
            }
        }
    }
}
