using FoodeLive.Windows.Setting;
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

        private void changepassword_dialog_Click(object sender, RoutedEventArgs e)
        {
            PasswordChange passwordChange = new PasswordChange();
            passwordChange.ShowDialog();
        }

        private void update_inform_dialog_Click(object sender, RoutedEventArgs e)
        {
            UpdateForm updateForm = new UpdateForm();
            updateForm.ShowDialog();
        }
    }
}
