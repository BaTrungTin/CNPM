using KoiDeliveryOrdering.Repositories.Entities;
using KoiDeliveryOrdering.Repositories.Interfaces;
using KoiDeliveryOrdering.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Services.Implementations
{
    public class VoucherService : IVoucherService
    {
        private readonly KoiDeliveryOrderingContext _dbContext;
        private readonly IVoucherRepository _voucherRepository;

        public VoucherService(KoiDeliveryOrderingContext dbContext, IVoucherRepository voucherRepository)
        {
            _dbContext = dbContext;
            _voucherRepository = voucherRepository;
        }

        public async Task<List<Voucher>> GetAllVouchers()
        {
            return await _voucherRepository.GetAllVouchers();
        }

        public async Task<Voucher?> GetVoucherById(int id)
        {
            return await _voucherRepository.GetById(id);
        }

        public async Task<Voucher?> GetVoucherByCode(string voucherCode)
        {
            return await _dbContext.Vouchers.FirstOrDefaultAsync(v => v.VoucherCode == voucherCode);
        }

        public async Task<Voucher> CreateVoucher(Voucher voucher)
        {
            return await _voucherRepository.Create(voucher);
        }

        public async Task<bool> DeleteVoucher(int id)
        {
            return await _voucherRepository.DelVoucher(id);
        }

        public async Task<bool> UpdateVoucher(Voucher voucher)
        {
            return await _voucherRepository.UpdateVoucher(voucher);
        }

        public async Task<bool> ValidateVoucher(int voucherId, decimal orderTotal)
        {
            var voucher = await _voucherRepository.GetById(voucherId);
            if (voucher != null && voucher.StartDate <= DateTime.Now && voucher.EndDate >= DateTime.Now)
            {
                return true;
            }
            return false;
        }

        public decimal CalculateDiscount(Voucher voucher, decimal orderTotal)
        {
            if (voucher.DiscountType == "Percentage")
            {
                return orderTotal - (orderTotal * voucher.DiscountValue / 100);
            }
            else if (voucher.DiscountType == "Fixed")
            {
                return orderTotal - voucher.DiscountValue;
            }
            return orderTotal;
        }
    }
}
