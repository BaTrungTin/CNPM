namespace QuanLyDonHangVaVanChuyen.MoHinh
{
    public class CaKoi
    {
        public string ID { get; set; }
        public string Giong { get; set; }
        public double KichThuoc { get; set; }
        public double GiaTri { get; set; }

        public CaKoi(string id, string giong, double kichThuoc, double giaTri)
        {
            ID = id;
            Giong = giong;
            KichThuoc = kichThuoc;
            GiaTri = giaTri;
        }
    }
}