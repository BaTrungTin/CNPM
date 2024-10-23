using QuanLyDonHangVaVanChuyen.MoHinh;
using System.Collections.Generic;

namespace QuanLyDonHangVaVanChuyen.DichVu.Interfaces
{
    public interface IDichVuDonHang
    {
        DonHang TaoDonHang(List<CaKoi> danhSachCa, double khoiLuong, int soLuong, DiaDiemGiaoHang diaDiem, string loaiVanChuyen, bool laVanChuyenQuocTe);
        void ApDungVoucher(string idDonHang, Voucher voucher);
        double TinhGiaVanChuyen(DonHang donHang);
        void CapNhatTrangThaiVanChuyen(string idDonHang, TrangThaiVanChuyen trangThaiMoi);
        DonHang? LayDonHang(string id);
    }
}