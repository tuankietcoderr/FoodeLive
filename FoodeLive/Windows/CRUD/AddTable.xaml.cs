using FoodeLive.Database;
using FoodeLive.Pages.Home;
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
using System.Windows.Shapes;

namespace FoodeLive.Windows.CRUD
{
    /// <summary>
    /// Interaction logic for AddTable.xaml
    /// </summary>
    public partial class AddTable : Window
    {
        public string newMaBan { get; set; }
        public string loai { get; set; }
        public AddTable()
        {
            InitializeComponent();
            table_type_normal.IsSelected= true;
            newMaBan = GetNewMaBan();
            loai = table_type.Text;
            int newIndex = Convert.ToInt32(newMaBan.Substring(1, newMaBan.Length - 1)) + 1;
            string newId = newIndex < 10 ? "B0" + newIndex : "B" + newIndex;
            newMaBan = newId;
            table_id.Text = newId;
        }

        ~AddTable() { }

        string GetNewMaBan()
        {
            string newMaBan = string.Empty;
            string getLastRowCommand = "select top 1 * from banan order by mabanan DESC";
            SqlCommand get = new SqlCommand();
            get.CommandText = getLastRowCommand;
            DBConnection.Connect();
            get.Connection = DBConnection._SQLConnection;
            var reader = get.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    var maban = reader["mabanan"];
                    newMaBan = maban.ToString();
                }

            DBConnection.Disconnect();
            return newMaBan;
        }

        void InsertToDB(string newMaNV, string loai)
        {
            string command = @"insert into banan(mabanan, loai) values (@id, @loai)";
            SqlCommand create = new SqlCommand();
            create.CommandText = command;
            DBConnection.Connect();
            create.Connection = DBConnection._SQLConnection;
            create.Parameters.AddWithValue("@id", newMaNV);
            create.Parameters.AddWithValue("@loai", loai);
            try
            {
                create.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            DBConnection.Disconnect();
        }

        private void add_table_Click(object sender, RoutedEventArgs e)
        {
            InsertToDB(newMaBan, loai);
        }

        private void table_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = table_type.SelectedItem as ComboBoxItem;
            if (comboBoxItem == null)
                return;
            loai = comboBoxItem.Content.ToString();
        }
    }
}
