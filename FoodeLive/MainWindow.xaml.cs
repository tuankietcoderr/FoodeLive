using FoodeLive.Auth;
using FoodeLive.Database;
using FoodeLive.Windows.Auth;
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

namespace FoodeLive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /*DBConnection.Connect();
            if (!AuthState.IsLoggedIn)
                StayWindow();*/
        }

        void StayWindow()
        {
            /*Login login = new Login();
            login.ShowDialog();
            if (!AuthState.IsLoggedIn)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Bạn vẫn chưa đăng nhập được. Bạn muốn thoát chứ?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.No)
                {
                    login.Close();
                    StayWindow();
                }
                else
                    Close();
            }*/
        }
    }
}
