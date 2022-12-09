using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodeLive.MVVM.Model
{
    public class MHoaDon
    {
        public int SoHoaDon { get; set; }
        public DateTime NgayHoaDon { get; set; }
        public string MaBanAn { get; set; }
        public string MaNhanVien { get; set; }
        public long TriGia { get; set; }

        MHoaDon()
        {
            SoHoaDon = 0;
            NgayHoaDon = DateTime.Now;
            MaBanAn = string.Empty;
            MaNhanVien = string.Empty;
            TriGia = 0;
        }
        MHoaDon(int SoHoaDon, DateTime NgayHoaDon, string MaBanAn, string MaNhanVien, long TriGia)
        {
            this.SoHoaDon = SoHoaDon;
            this.NgayHoaDon = NgayHoaDon;
            this.MaBanAn = MaBanAn;
            this.MaNhanVien = MaNhanVien;
            this.TriGia = TriGia;
        }
        ~MHoaDon()
        {
            SoHoaDon = 0;
            NgayHoaDon = DateTime.Now;
            MaBanAn = string.Empty;
            MaNhanVien = string.Empty;
            TriGia = 0;
        }
    }
}
