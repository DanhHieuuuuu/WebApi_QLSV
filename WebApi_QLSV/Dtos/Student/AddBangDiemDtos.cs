namespace WebApi_QLSV.Dtos.Student
{
    public class AddBangDiemDtos
    {
        public string? KiHocNamHoc { get; set; }
        public double? DiemTB10 { get; set; }
        public double? DiemTB4 { get; set; }
        public double? DiemTichLuy10 { get; set; }
        public double? DiemTichLuy4 { get; set; }
        public int? TongTCKi { get; set; }
        public int? TongTCTichLuy {  get; set; }
        public int? TongTCDat { get; set; }
        public int? TienHocKi { get; set; }
        public int? SoTienNop { get; set; }
        public int? CongNo { get; set; }

        public List<BangDiemDtos> BangDiems { get; set; }
        public AddBangDiemDtos(string? kiHocNamHoc, double? diemTB10, double? diemTB4, double? diemTichLuy10, double? diemTichLuy4, int? tongTCKi, int? tongTCTichLuy, int? tongTCDat, int? tienHocKi, int? soTienNop, int? congNo)
        {
            KiHocNamHoc = kiHocNamHoc;
            DiemTB10 = diemTB10;
            DiemTB4 = diemTB4;
            DiemTichLuy10 = diemTichLuy10;
            DiemTichLuy4 = diemTichLuy4;
            TongTCKi = tongTCKi;
            TongTCTichLuy = tongTCTichLuy;
            TongTCDat = tongTCDat;
            TienHocKi = tienHocKi;
            SoTienNop = soTienNop;
            CongNo = congNo;
            BangDiems = new List<BangDiemDtos>();

        }
    }
}
