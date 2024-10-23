using QuanLyDonHangVaVanChuyen.DichVu.Interfaces;
using QuanLyDonHangVaVanChuyen.MoHinh;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyDonHangVaVanChuyen.DichVu
{
    public class DichVuDonHang : IDichVuDonHang
    {
        private List<DonHang> danhSachDonHang = new List<DonHang>();
        private IDichVuVoucher dichVuVoucher;
        private IDichVuVanChuyen dichVuVanChuyen;

        public DichVuDonHang(IDichVuVoucher dichVuVoucher, IDichVuVanChuyen dichVuVanChuyen)
        {
            this.dichVuVoucher = dichVuVoucher;
            this.dichVuVanChuyen = dichVuVanChuyen;
        }

        public DonHang TaoDonHang(List<CaKoi> danhSachCa, double khoiLuong, int soLuong, DiaDiemGiaoHang diaDiem, string loaiVanChuyen, bool laVanChuyenQuocTe)
        {
            string id = Guid.NewGuid().ToString();
            var donHang = new DonHang(id, danhSachCa, khoiLuong, soLuong, diaDiem, loaiVanChuyen, laVanChuyenQuocTe);
            danhSachDonHang.Add(donHang);
            return donHang;
        }

        public void ApDungVoucher(string idDonHang, Voucher voucher)
        {
            var donHang = LayDonHang(idDonHang);
            if (donHang != null && dichVuVoucher.KiemTraVoucher(voucher))
            {
                donHang.VoucherApDung = voucher;
                donHang.TongGiaTri -= voucher.GiaTriGiam;
            }
        }

        public double TinhGiaVanChuyen(DonHang donHang)
        {
            return dichVuVanChuyen.TinhChiPhi(donHang);
        }

        public void CapNhatTrangThaiVanChuyen(string idDonHang, TrangThaiVanChuyen trangThaiMoi)
        {
            var donHang = LayDonHang(idDonHang);
            if (donHang != null)
            {
                donHang.TrangThai = trangThaiMoi;
            }
        }

        public DonHang? LayDonHang(string id)
        {
            return danhSachDonHang.FirstOrDefault(d => d.Id == id);
        }
    }
}
