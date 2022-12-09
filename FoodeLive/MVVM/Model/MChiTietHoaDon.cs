using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodeLive.MVVM.Model
{
    public class MChiTietHoaDon
    {
        public int SoHoaDon { get; set; }
        public string MaMonAn { get; set; }
        public int SoLuong { get; set; }
        MChiTietHoaDon()
        {
            SoHoaDon = 0;
            MaMonAn = string.Empty;
            SoLuong = 0;
        }

        MChiTietHoaDon(int SoHoaDon, string MaMonAn, int SoLuong)
        {
            this.SoHoaDon = SoHoaDon;
            this.MaMonAn = MaMonAn;
            this.SoLuong= SoLuong;
        }

        ~MChiTietHoaDon()
        {
            SoHoaDon = 0;
            MaMonAn = string.Empty;
            SoLuong = 0;
        }
    }
}
