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
        }

        public static string username { get; set; }
        public static string password { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            username = login_username.Text;
            password = login_password.Password;
            if (AuthLogin.StartSession(username, password))
            {
                MessageBox.Show("OK!");
                this.Close();
            }
        }
    }
}
