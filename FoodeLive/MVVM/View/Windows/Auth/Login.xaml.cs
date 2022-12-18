using FoodeLive.Auth;
using FoodeLive.MVVM.Model;
using FoodeLive.MVVM.ViewModel;
using System;
using System.Collections.Generic;
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

namespace FoodeLive.View.Windows.Auth
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        MainViewModel viewModel { get; }
        public Login()
        {
            InitializeComponent();
            viewModel = this.DataContext as MainViewModel;
        }

        ~Login() { }

        public static string username { get; set; }
        public static string password { get; set; }

        private void LoginToSignUp_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            RestaurantRegister signUp = new RestaurantRegister();
            this.Close();
            signUp.ShowDialog();
        }

        private void HandleLogin_Click(object sender, RoutedEventArgs e)
        {
            username = login_username.Text;
            password = login_password.Password;
            if (AuthLogin.HandleLogin(username, password))
            {
                NguoiQuanLy nguoiQuanLy = DataProvider.Ins.DB.NguoiQuanLies.ToList().Find(ql => ql.TenNguoiDung == username);
                NhanVien nhanVien = DataProvider.Ins.DB.NhanViens.ToList().Find(nv => nv.TenNguoiDung == username);
                if (nguoiQuanLy != null)
                {
                    viewModel.CuaHangHoatDong = nguoiQuanLy.CuaHang;
                    viewModel.NguoiQuanLy = nguoiQuanLy;
                    //NhanVien nhanVienAlias = new NhanVien() { Ma };
                    //viewModel.NhanVienHoatDong 
                }
                else
                {
                    viewModel.CuaHangHoatDong = DataProvider.Ins.DB.CuaHangs.ToList().Find(r => r.MaQuanLy == nhanVien.MaQuanLy);
                    viewModel.NhanVienHoatDong = nhanVien;
                }
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
            else
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
