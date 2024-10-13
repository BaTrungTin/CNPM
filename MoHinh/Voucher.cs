using System;

namespace QuanLyDonHangVaVanChuyen.MoHinh
{
    public class Voucher
    {
        public string MaVoucher { get; set; }
        public double GiaTriGiam { get; set; }
        public DateTime ThoiGianHieuLuc { get; set; }
        public bool TrangThai { get; set; }

        public Voucher(string maVoucher, double giaTriGiam, DateTime thoiGianHieuLuc, bool trangThai)
        {
            MaVoucher = maVoucher;
            GiaTriGiam = giaTriGiam;
            ThoiGianHieuLuc = thoiGianHieuLuc;
            TrangThai = trangThai;
        }
    }
}
