using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoodeLive.Database
{
    public static class DBConnection
    {
        #region SQL Connection String
        public const string SQLConnectionString = @"Data Source=LAPTOP-9HTSBQGT;Initial Catalog=QUANLYNHAHANG;Integrated Security=True";
        #endregion

        static readonly string _SQLConnectionString = SQLConnectionString;
        public static SqlConnection _SQLConnection { get; set; }
        public static bool ConnectionState { get; set; }

        static DBConnection()
        {
            ConnectionState= false;
        }

        public static bool Connect()
        {
            try
            {
                _SQLConnection = new SqlConnection(_SQLConnectionString);
                _SQLConnection.Open();
                ConnectionState= true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ConnectionState= false;
                return false;
            }
        }

        public static bool Disconnect()
        {
            try
            {
                _SQLConnection.Close();
                ConnectionState= false;
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

    }
}
