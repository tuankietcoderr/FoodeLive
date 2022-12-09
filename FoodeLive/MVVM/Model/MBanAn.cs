using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodeLive.MVVM.Model
{
    public class MBanAn
    {
        public string MaBanAn { get; set; }
        public string Loai { get; set; }
        MBanAn()
        {
            MaBanAn = string.Empty;
            Loai = string.Empty;
        }
        MBanAn(string MaBanAn, string Loai)
        {
            this.MaBanAn = MaBanAn;
            this.Loai = Loai;
        }

        ~MBanAn()
        {
            MaBanAn = string.Empty;
            Loai = string.Empty;
        }
    }
}
