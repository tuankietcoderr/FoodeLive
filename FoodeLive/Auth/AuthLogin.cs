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
    internal class AuthLogin
    {
        public static bool StartSession(string username, string password)
        {
            if (!AuthState.IsLoggedIn && DBConnection.ConnectionState)
            {
                string sqlCommand = @"select * from nhanvien where tennguoidung=@username and matkhau=@password";
                var command = new SqlCommand();
                command.CommandText = sqlCommand;
                command.Connection = DBConnection._SQLConnection;
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                var result = command.ExecuteReader();
                if (result.HasRows)
                {
                    AuthState.IsLoggedIn = true;
                    return true;
                }
            }
            return false;
        }
    }
}
