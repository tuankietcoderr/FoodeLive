using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodeLive.MVVM.Model
{
    public class MNhanVien
    {
        public string MaNhanVien { get; set; }
        public string TenNguoiDung { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public long Luong { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
    
        MNhanVien()
        {
            MaNhanVien = string.Empty;
            TenNguoiDung= string.Empty;
            MatKhau = string.Empty;
            HoTen = string.Empty;
            NgaySinh = DateTime.Now;
            Luong = 0;
            SoDienThoai= string.Empty;
            DiaChi = string.Empty;
        }

        MNhanVien(string MaNhanVien, string TenNguoiDung, string MatKhau, string HoTen, DateTime NgaySinh, long Luong, string SoDienThoai, string DiaChi)
        {
            this.MaNhanVien = MaNhanVien;
            this.TenNguoiDung = TenNguoiDung;
            this.MatKhau = MatKhau;
            this.HoTen = HoTen;
            this.NgaySinh = NgaySinh;
            this.Luong = Luong;
            this.SoDienThoai = SoDienThoai;
            this.DiaChi = DiaChi;
        }

        ~MNhanVien()
        {
            MaNhanVien = string.Empty;
            TenNguoiDung = string.Empty;
            MatKhau = string.Empty;
            HoTen = string.Empty;
            NgaySinh = DateTime.Now;
            Luong = 0;
            SoDienThoai = string.Empty;
            DiaChi = string.Empty;
        }
    }
}
