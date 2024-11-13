using KoiDeliveryOrdering.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrdering.Repositories.Interfaces
{
    public interface IVoucherRepository
    {
        Task<List<Voucher>> GetAllVouchers();
        Task<Voucher?> GetById(int id);
        Task<Voucher> Create(Voucher voucher);
        Task<bool> DelVoucher(int id);
        Task<bool> UpdateVoucher(Voucher voucher);

        Task<Voucher?> GetVoucherByCode(string voucherCode);
    }
}
