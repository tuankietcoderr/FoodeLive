using FoodeLive.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoodeLive.Auth
{
    public static class AuthLogin
    {
        public static bool HandleLogin(string username, string password)
        {
            var nvCount = DataProvider.Ins.DB.NhanViens.Where(u => u.TenNguoiDung == username & u.MatKhau == password).Count();
            var qlCount = DataProvider.Ins.DB.NguoiQuanLies.Where(u => u.TenNguoiDung == username & u.MatKhau == password).Count();
            if (nvCount > 0 || qlCount > 0)
            {
                AuthState.IsLoggedIn = true;
                return true;
            }
            return false;
        }
    }
}
