using FoodeLive.MVVM.Model;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FoodeLive.Auth
{
    public static class AuthSignUp
    {
        public static bool CreateAccount(string username, string password, CuaHang cuaHang)
        {
            try
            {
                var quanlies = DataProvider.Ins.DB.NguoiQuanLies.ToList();
                var users = DataProvider.Ins.DB.NhanViens.ToList();
                if (users.Exists(p => p.TenNguoiDung == username && p.MaQuanLy == cuaHang.MaQuanLy))
                {
                    MessageBox.Show("Người dùng đã tồn tại!");
                    return false;
                }
                string newId = string.Empty;
                if (users.Count() == 0) // User moi
                    newId = "NV01";
                else
                {
                    var lastMaNV = users.Last().MaNV;
                    int newIndex = Convert.ToInt32(lastMaNV.Substring(2, lastMaNV.Length - 2)) + 1;
                    newId = newIndex < 10 ? "NV0" + newIndex : "NV" + newIndex;
                }

                var newUser = new NhanVien() { MaNV = newId, TenNguoiDung = username, MatKhau = password, NgayVaoLam = DateTime.Now, MaQuanLy = cuaHang.MaQuanLy };

                DataProvider.Ins.DB.NhanViens.Add(newUser);
                DataProvider.Ins.DB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool CreateRestaurant(string username, string password, string tenCuaHang)
        {
            try
            {
                var cuaHangs = DataProvider.Ins.DB.CuaHangs.ToList();
                var quanlies = DataProvider.Ins.DB.NguoiQuanLies.ToList();
                var nhanviens = DataProvider.Ins.DB.NhanViens.ToList();
                if (quanlies.Exists(p => p.TenNguoiDung == username) || nhanviens.Exists(nv => nv.TenNguoiDung == username))
                {
                    MessageBox.Show("Người dùng đã tồn tại!");
                    return false;
                }

                string newMaCuaHang = string.Empty;
                if (cuaHangs.Count == 0)
                    newMaCuaHang = "CH01";
                else
                {
                    var lastMaCH = cuaHangs.Last().MaCuaHang;
                    int newIndex = Convert.ToInt32(lastMaCH.Substring(2, lastMaCH.Length - 2)) + 1;
                    newMaCuaHang = newIndex < 10 ? "CH0" + newIndex : "CH" + newIndex;
                }

                string newId = string.Empty;
                if (quanlies.Count() == 0) // User moi
                    newId = "QL01";
                else
                {
                    var lastMaNV = quanlies.Last().MaQuanLy;
                    int newIndex = Convert.ToInt32(lastMaNV.Substring(2, lastMaNV.Length - 2)) + 1;
                    newId = newIndex < 10 ? "QL0" + newIndex : "QL" + newIndex;
                }
                CuaHang newCuaHang = new CuaHang() { TenCuaHang = tenCuaHang, MaCuaHang = newMaCuaHang, MaQuanLy = newId, NgayThanhLap = DateTime.Now };
                var newQuanLy = new NguoiQuanLy() { MaQuanLy = newId, CuaHang = newCuaHang, MatKhau = password, TenNguoiDung = username, NgayThamGia = DateTime.Now, MaCuaHang = newMaCuaHang };
                DataProvider.Ins.DB.NguoiQuanLies.Add(newQuanLy);
                DataProvider.Ins.DB.CuaHangs.Add(newCuaHang);
                DataProvider.Ins.DB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
