namespace QuanLyDonHangVaVanChuyen.MoHinh
{
    public class TaiXe
    {
        public string Id { get; set; }
        public string Ten { get; set; }
        public string SoDienThoai { get; set; }
        public bool CoSan { get; set; }

        public TaiXe(string id, string ten, string soDienThoai, bool coSan)
        {
            Id = id;
            Ten = ten;
            SoDienThoai = soDienThoai;
            CoSan = coSan;
        }
    }
}