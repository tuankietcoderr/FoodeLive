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
        public static bool CreateAccount(string username, string password)
        {
            string newMaNV = "";
            string getLastRowCommand = "select top 1 * from NHANVIEN order by manv DESC";
            SqlCommand get = new SqlCommand();
            get.CommandText = getLastRowCommand;
            get.Connection = DBConnection._SQLConnection;

            var reader = get.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    var manv = reader["manv"];
                    newMaNV = manv.ToString();
                }
            DBConnection._SQLConnection.Close();

            DBConnection.Connect();

            int newIndex = Convert.ToInt32(newMaNV.Substring(2, newMaNV.Length - 2)) + 1;
            string newId = newIndex < 10 ? "NV0" + newIndex : "NV" + newIndex;
            string command = @"insert into nhanvien(manv, tennguoidung, matkhau) values (@id, @username, @password)";
            SqlCommand create = new SqlCommand();
            create.CommandText = command;
            create.Connection = DBConnection._SQLConnection;
            create.Parameters.AddWithValue("@id", newId);
            create.Parameters.AddWithValue("@username", username);
            create.Parameters.AddWithValue("@password", password);
            try
            {
                create.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
