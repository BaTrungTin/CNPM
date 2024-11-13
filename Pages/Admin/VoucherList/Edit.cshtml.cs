using KoiDeliveryOrdering.Repositories.Entities;
using KoiDeliveryOrdering.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.WebApp.Pages.Admin.VoucherList
{
    public class EditModel : PageModel
    {
        private readonly IVoucherRepository _voucherRepository;

        public EditModel(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
            Voucher = new Voucher();
        }

        [BindProperty]
        public Voucher? Voucher { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Voucher = await _voucherRepository.GetById(id);
            if (Voucher == null)
            {
                TempData["ErrorMessage"] = "Voucher không tồn tại.";
                return RedirectToPage("/Admin/VoucherList/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Voucher == null)
            {
                TempData["ErrorMessage"] = "Voucher không tồn tại.";
                return RedirectToPage("/Admin/VoucherList/Index");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var success = await _voucherRepository.UpdateVoucher(Voucher);
            if (success)
            {
                TempData["SuccessMessage"] = "Voucher đã được cập nhật thành công!";
                return RedirectToPage("/Admin/VoucherList/Index"); // Chuyển hướng về trang Index sau khi cập nhật thành công
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật voucher!";
                return Page();
            }
        }
    }
}
