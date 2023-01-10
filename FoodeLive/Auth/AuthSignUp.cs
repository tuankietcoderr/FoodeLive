using FoodeLive.Converter;
using FoodeLive.MVVM.Model;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
                if (users.Exists(p => p.TenNguoiDung == username))
                {
                    MessageBox.Show("Người dùng đã tồn tại!");
                    return false;
                }
                string newId = string.Empty;
                var listNhaHang = users.Where(nv => nv.MaQuanLy == cuaHang.MaQuanLy);
                if (listNhaHang.Count() == 0) // User moi
                    newId = cuaHang.MaCuaHang + "NV01";
                else
                {
                    newId = cuaHang.MaCuaHang + "NV";
                    var lastMaNV = listNhaHang.Last().MaNV;
                    int newIndex = Convert.ToInt32(lastMaNV.Substring(cuaHang.MaCuaHang.Length + 2)) + 1;
                    for (int i = 0; i < 8 - cuaHang.MaCuaHang.Length - newIndex.ToString().Length; i++)
                        newId += "0";
                    newId += newIndex.ToString();
                }

                var newUser = new NhanVien() { MaNV = newId, TenNguoiDung = username, MatKhau = password, NgayVaoLam = DateTime.Now, MaQuanLy = cuaHang.MaQuanLy, Luong = 30000, MaCuaHang = cuaHang.MaCuaHang };

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
                    newMaCuaHang = "CH0001";
                else
                {
                    var lastMaCH = cuaHangs.Last();
                    newMaCuaHang = "CH";

                    int newIndex = Convert.ToInt32(lastMaCH.MaCuaHang.Substring(2)) + 1;
                    for (int i = 0; i < 4 - newIndex.ToString().Length; i++)
                        newMaCuaHang += "0";
                    newMaCuaHang += newIndex.ToString();
                }

                string newId = string.Empty;
                newId = newMaCuaHang + "QL";
                // chi co 1 quan ly cho moi cua hang
                CuaHang newCuaHang = new CuaHang() { TenCuaHang = tenCuaHang, MaCuaHang = newMaCuaHang, MaQuanLy = newId, NgayThanhLap = DateTime.Now, TenCuaHangKhongDau = VietnameseStringConverter.LocDau(tenCuaHang) };
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
