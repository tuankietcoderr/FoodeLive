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

namespace FoodeLive.Windows.Setting
{
    /// <summary>
    /// Interaction logic for PasswordChange.xaml
    /// </summary>
    public partial class PasswordChange : Window
    {
        public PasswordChange()
        {
            InitializeComponent();
        }

        ~PasswordChange() { }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        bool IsShowOld = false;
        bool IsShowNew = false;
        bool IsShowVerify = false;
        private void showoldPassword(object sender, RoutedEventArgs e)
        {
            if (IsShowOld)
                IsShowOld = false;
            else
                IsShowOld = true;

            if(IsShowOld)
            {
                showOldBut.Icon = Wpf.Ui.Common.SymbolRegular.Eye16;
                showOldPass.Text = oldPass.Password;
                oldPass.Visibility = Visibility.Collapsed;
                showOldPass.Visibility = Visibility.Visible;
            }
            else
            {
                showOldBut.Icon = Wpf.Ui.Common.SymbolRegular.EyeOff16;
                oldPass.Password = showOldPass.Text;
                oldPass.Visibility = Visibility.Visible;
                showOldPass.Visibility = Visibility.Collapsed;
            }
        }

        private void shownewPassword(object sender, RoutedEventArgs e)
        {
            if (IsShowNew)
                IsShowNew = false;
            else
                IsShowNew = true;

            if (IsShowNew)
            {
                showNewBut.Icon = Wpf.Ui.Common.SymbolRegular.Eye16;
                showNewPass.Text = oldPass.Password;
                newPass.Visibility = Visibility.Collapsed;
                showNewPass.Visibility = Visibility.Visible;
            }
            else
            {
                showNewBut.Icon = Wpf.Ui.Common.SymbolRegular.EyeOff16;
                newPass.Password = showNewPass.Text;
                newPass.Visibility = Visibility.Visible;
                showNewPass.Visibility = Visibility.Collapsed;
            }
        }

        private void showverifyPassword(object sender, RoutedEventArgs e)
        {
            if (IsShowVerify)
                IsShowVerify = false;
            else
                IsShowVerify = true;

            if (IsShowVerify)
            {
                showPassverifyBut.Icon = Wpf.Ui.Common.SymbolRegular.Eye16;
                showPassverify.Text = Passverify.Password;
                Passverify.Visibility = Visibility.Collapsed;
                showPassverify.Visibility = Visibility.Visible;
            }
            else
            {
                showPassverifyBut.Icon = Wpf.Ui.Common.SymbolRegular.EyeOff16;
                Passverify.Password = showPassverify.Text;
                Passverify.Visibility = Visibility.Visible;
                showPassverify.Visibility = Visibility.Collapsed;
            }
        }
    }
}
