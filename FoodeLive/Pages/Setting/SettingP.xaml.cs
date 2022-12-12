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

        private void click_upd_inf(object sender, MouseButtonEventArgs e)
        {
            Setting.UpdateForm updateForm = new Setting.UpdateForm();
            updateForm.Show();
        }

        private void chang_pass(object sender, MouseButtonEventArgs e)
        {

        }

        private void LogOut(object sender, MouseButtonEventArgs e)
        {

        }

        private void on_mouse(object sender, MouseEventArgs e)
        {
            update.FontStyle = FontStyles.Italic;
        }

        private void on_mouse1(object sender, MouseEventArgs e)
        {
            change.FontStyle = FontStyles.Italic;
        }

        private void on_mouse2(object sender, MouseEventArgs e)
        {
            logout.FontStyle = FontStyles.Italic;
        }

        

        private void l_m(object sender, MouseEventArgs e)
        {
            update.FontStyle = FontStyles.Normal;
        }

        private void l_m1(object sender, MouseEventArgs e)
        {
            change.FontStyle = FontStyles.Normal;
        }

        private void l_m2(object sender, MouseEventArgs e)
        {
            logout.FontStyle = FontStyles.Normal;
        }
    }
}
