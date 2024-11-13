using KoiDeliveryOrdering.Repositories.Entities;
using KoiDeliveryOrdering.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Repositories.Implementations
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly KoiDeliveryOrderingContext _dbContext;

        public VoucherRepository(KoiDeliveryOrderingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Voucher>> GetAllVouchers()
        {
            return await _dbContext.Vouchers.ToListAsync();
        }

        public async Task<Voucher?> GetById(int voucherId)
        {
            return await _dbContext.Vouchers.FirstOrDefaultAsync(v => v.VoucherId == voucherId);
        }

        public async Task<Voucher> Create(Voucher voucher)
        {
            try
            {
                await _dbContext.Vouchers.AddAsync(voucher); // Thêm voucher vào dbContext
                await _dbContext.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
                return voucher; // Trả về voucher đã tạo
            }
            catch (Exception ex)
            {
                // Log lỗi và có thể ném lỗi nếu cần
                throw new InvalidOperationException("Có lỗi khi tạo voucher.", ex);
            }
        }

        public async Task<bool> DelVoucher(int id)
        {
            var voucher = await _dbContext.Vouchers.FirstOrDefaultAsync(v => v.VoucherId == id);
            if (voucher != null)
            {
                _dbContext.Vouchers.Remove(voucher);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateVoucher(Voucher voucher)
        {
            _dbContext.Vouchers.Update(voucher);
            var updated = await _dbContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<Voucher?> GetVoucherByCode(string voucherCode)
        {
            return await _dbContext.Vouchers.FirstOrDefaultAsync(v => v.VoucherCode == voucherCode);
        }
    }
}
