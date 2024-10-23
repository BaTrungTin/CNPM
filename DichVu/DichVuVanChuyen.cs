using QuanLyDonHangVaVanChuyen.DichVu.Interfaces;
using QuanLyDonHangVaVanChuyen.MoHinh;
using QuanLyDonHangVaVanChuyen.VanChuyen;

namespace QuanLyDonHangVaVanChuyen.DichVu
{
    public class DichVuVanChuyen : IDichVuVanChuyen
    {
        public double TinhChiPhi(DonHang donHang)
        {
            double chiPhi = 0;
            if (donHang.LaVanChuyenQuocTe)
            {
                chiPhi = new VanChuyenQuocTe().TinhChiPhi(donHang.KhoiLuong, donHang.LoaiVanChuyen);
            }
            else
            {
                chiPhi = new VanChuyenNoiDia().TinhChiPhi(donHang.KhoiLuong, donHang.LoaiVanChuyen);
            }

            // Tính thêm phí bảo hiểm
            chiPhi += TinhPhiBaoHiem(donHang);

            return chiPhi;
        }

        private double TinhPhiBaoHiem(DonHang donHang)
        {
            // Logic tính phí bảo hiểm
            return donHang.TongGiaTri * 0.01; // Giả sử phí bảo hiểm là 1% giá trị đơn hàng
        }
    }
}