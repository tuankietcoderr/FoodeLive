using FoodeLive.Database;
using FoodeLive.MVVM.Model;
using FoodeLive.MVVM.ViewModel;
using FoodeLive.utils;
using FoodeLive.Windows;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace FoodeLive.Pages.Home
{
    /// <summary>
    /// Interaction logic for All.xaml
    /// </summary>
    public partial class All : Page
    {
        public All()
        {
            InitializeComponent();
            List<MBanAn> rows = InitData();
            if (rows.Count > 0)
                all_table.ItemsSource = rows;
        }

        List<MBanAn> InitData()
        {
            string command = @"select * from banan";
            SqlCommand sqlCommand = new SqlCommand();
            DBConnection.Connect();
            sqlCommand.Connection = DBConnection._SQLConnection;
            sqlCommand.CommandText = command;

            var reader = sqlCommand.ExecuteReader();

            List<MBanAn> rows = new List<MBanAn>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var maBanAn = reader["mabanan"];
                    var loai = reader["loai"];
                    rows.Add(new MBanAn(maBanAn.ToString(), loai.ToString()));
                }
            }
            DBConnection.Disconnect();
            return rows;
        }


        private void all_table_Selected(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("H");
        }

        private void table_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Card card = sender as Card;
            string MaBanAn = card.Tag.ToString();
            VMOrderOrBook vMOrderOrBook = new VMOrderOrBook();
            vMOrderOrBook.OrderCommand.Execute(MaBanAn);
            Windows.OrderOrBook orderOrBook = new Windows.OrderOrBook(vMOrderOrBook);
            orderOrBook.ShowDialog();
        }
    }
}
