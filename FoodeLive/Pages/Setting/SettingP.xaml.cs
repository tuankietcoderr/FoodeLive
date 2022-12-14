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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodeLive.Pages.Setting
{
    /// <summary>
    /// Interaction logic for SettingP.xaml
    /// </summary>
    public partial class SettingP : Page
    {
        public SettingP()
        {
            InitializeComponent();
        }

        ~SettingP() { }

        private void click_upd_inf(object sender, MouseButtonEventArgs e)
        {
            Windows.Setting.UpdateForm updateForm = new Windows.Setting.UpdateForm();
            updateForm.Show();
        }

        private void chang_pass(object sender, MouseButtonEventArgs e)
        {
            Windows.Setting.PasswordChange passwordChange = new Windows.Setting.PasswordChange();
            passwordChange.Show();
        }

        private void LogOut(object sender, MouseButtonEventArgs e)
        {
        }




        private void on_mouse(object sender, MouseEventArgs e)
        {
            update.FontStyle = FontStyles.Oblique;
            update.TextDecorations = TextDecorations.Underline;
        }

        private void on_mouse1(object sender, MouseEventArgs e)
        {
            change.FontStyle = FontStyles.Oblique;
            change.TextDecorations = TextDecorations.Underline;
        }

        private void on_mouse2(object sender, MouseEventArgs e)
        {
            logout.FontStyle = FontStyles.Oblique;
        }
        private void on_mouse3(object sender, MouseEventArgs e)
        {
            static_.FontStyle = FontStyles.Oblique;
            static_.TextDecorations = TextDecorations.Underline;
        }
        private void on_mouse4(object sender, MouseEventArgs e)
        {
            problem.FontStyle = FontStyles.Oblique;
            problem.TextDecorations = TextDecorations.Underline;
        }


        private void l_m(object sender, MouseEventArgs e)
        {
            update.FontStyle = FontStyles.Normal;
            update.TextDecorations = null;
        }

        private void l_m1(object sender, MouseEventArgs e)
        {
            change.FontStyle = FontStyles.Normal;
            change.TextDecorations = null;
        }

        private void l_m2(object sender, MouseEventArgs e)
        {
            logout.FontStyle = FontStyles.Normal;
        }

        private void l_m3(object sender, MouseEventArgs e)
        {
            static_.FontStyle = FontStyles.Normal;
            static_.TextDecorations = null;
        }

        private void l_m4(object sender, MouseEventArgs e)
        {
            problem.FontStyle = FontStyles.Normal;
            problem.TextDecorations = null;
        }
    }
}
