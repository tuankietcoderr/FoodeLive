using FoodeLive.Auth;
using FoodeLive.Database;
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

namespace FoodeLive.Windows.Auth
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            DBConnection.Connect();
        }

        ~Login() { }

        public static string username { get; set; }
        public static string password { get; set; }

        private void LoginToSignUp_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUp signUp = new SignUp();
            this.Close();
            signUp.ShowDialog();
        }

        private void HandleLogin_Click(object sender, RoutedEventArgs e)
        {
            username = login_username.Text;
            password = login_password.Password;
            if (AuthLogin.StartSession(username, password))
            {
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
            else
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
