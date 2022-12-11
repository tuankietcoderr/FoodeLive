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
    public static class AuthSignUp
    {
        public static void CreateAccount(string username, string password)
        {
            string newMaNV = "";
            string getLastRowCommand = "select top 1 * from NHANVIEN order by manv DESC";
            SqlCommand get = new SqlCommand();
            get.CommandText = getLastRowCommand;
            get.Connection = DBConnection._SQLConnection;
            
            var reader = get.ExecuteReader();
            if(reader.HasRows)
                while (reader.Read())
                {
                    var manv = reader["manv"];
                    newMaNV = manv.ToString();
                }
            
            string command = @"insert into nhanvien(manv, tennguoidung, matkhau) values (@id, @username, @password)";
            SqlCommand create = new SqlCommand();
            create.CommandText = command;
            create.Connection = DBConnection._SQLConnection;
            create.Parameters.AddWithValue("@id", newMaNV);
            create.Parameters.AddWithValue("@username", username);
            create.Parameters.AddWithValue("@password", password);
            try
            {
                var result = create.ExecuteNonQuery();
                MessageBox.Show(result.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
