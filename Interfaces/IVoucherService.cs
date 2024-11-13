using KoiDeliveryOrdering.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Services.Interfaces
{
    public interface IVoucherService
    {
        Task<List<Voucher>> GetAllVouchers();
        Task<Voucher?> GetVoucherById(int id);

        Task<Voucher?> GetVoucherByCode(string voucherCode);

        Task<Voucher> CreateVoucher(Voucher voucher);
        Task<bool> DeleteVoucher(int id);
        Task<bool> UpdateVoucher(Voucher voucher);
        Task<bool> ValidateVoucher(int voucherId, decimal orderTotal);
        decimal CalculateDiscount(Voucher voucher, decimal orderTotal);
    }
}
