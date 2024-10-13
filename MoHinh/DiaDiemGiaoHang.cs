namespace QuanLyDonHangVaVanChuyen.MoHinh
{
    public class DiaDiemGiaoHang
    {
        public string DiaChi { get; set; }
        public string ThanhPho { get; set; }
        public string MaBuuDien { get; set; }

        public DiaDiemGiaoHang(string diaChi, string thanhPho, string maBuuDien)
        {
            DiaChi = diaChi;
            ThanhPho = thanhPho;
            MaBuuDien = maBuuDien;
        }
    }
}