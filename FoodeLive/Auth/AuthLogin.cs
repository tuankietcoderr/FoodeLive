using FoodeLive.Database;
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
        public static bool StartSession(string username, string password)
        {
            string sqlCommand = @"select * from nhanvien where tennguoidung=@username and matkhau=@password";
            var command = new SqlCommand();
            command.CommandText = sqlCommand;
            DBConnection.Connect();
            command.Connection = DBConnection._SQLConnection;
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            bool flag = false;
            try
            {
                var result = command.ExecuteReader();
                if (result.HasRows)
                {
                    AuthState.IsLoggedIn = true;
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DBConnection.Disconnect();
            return flag;
        }
    }
}
