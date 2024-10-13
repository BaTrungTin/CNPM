using System;
using System.Collections.Generic;

namespace QuanLyDonHangVaVanChuyen.MoHinh
{
    public class DonHang
    {
        public string Id { get; set; }
        public List<CaKoi> DanhSachCa { get; set; }
        public double KhoiLuong { get; set; }
        public int SoLuong { get; set; }
        public DiaDiemGiaoHang DiaDiem { get; set; }
        public double TongGiaTri { get; set; }
        public Voucher? VoucherApDung { get; set; }
        public string LoaiVanChuyen { get; set; }
        public TrangThaiVanChuyen TrangThai { get; set; }
        public bool LaVanChuyenQuocTe { get; set; }

        public DonHang(string id, List<CaKoi> danhSachCa, double khoiLuong, int soLuong, DiaDiemGiaoHang diaDiem, string loaiVanChuyen, bool laVanChuyenQuocTe)
        {
            Id = id;
            DanhSachCa = danhSachCa;
            KhoiLuong = khoiLuong;
            SoLuong = soLuong;
            DiaDiem = diaDiem;
            LoaiVanChuyen = loaiVanChuyen;
            TrangThai = TrangThaiVanChuyen.DangChuanBi;
            LaVanChuyenQuocTe = laVanChuyenQuocTe;
            TongGiaTri = danhSachCa.Sum(ca => ca.GiaTri);
        }
    }
}