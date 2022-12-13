using FoodeLive.MVVM.Model;
using FoodeLive.Windows;
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
        public static bool CreateAccount(string username, string password)
        {
            try
            {
                var users = DataProvider.Ins.DB.NhanViens.ToList();
                string newId = string.Empty;
                if (users.Count() == 0) // User moi
                    newId = "NV01";
                else
                {
                    var lastMaNV = users.Last().MaNV;
                    int newIndex = Convert.ToInt32(lastMaNV.Substring(2, lastMaNV.Length - 2)) + 1;
                    newId = newIndex < 10 ? "NV0" + newIndex : "NV" + newIndex;
                }
                if(users.Exists(p => p.TenNguoiDung == username))
                {
                    MessageBox.Show("Người dùng đã tồn tại!");
                    return false;
                }

                var newUser = new NhanVien() { MaNV = newId, TenNguoiDung = username, MatKhau = password };

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
    }
}
