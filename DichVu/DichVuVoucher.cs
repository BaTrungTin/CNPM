using QuanLyDonHangVaVanChuyen.DichVu.Interfaces;
using QuanLyDonHangVaVanChuyen.MoHinh;
using System;

namespace QuanLyDonHangVaVanChuyen.DichVu
{
    public class DichVuVoucher : IDichVuVoucher
    {
        public bool KiemTraVoucher(Voucher voucher)
        {
            return voucher.TrangThai && voucher.ThoiGianHieuLuc > DateTime.Now;
        }
    }
}