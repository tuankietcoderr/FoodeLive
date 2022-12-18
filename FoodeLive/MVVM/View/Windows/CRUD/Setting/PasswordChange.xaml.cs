using FoodeLive.Auth;
using FoodeLive.MVVM.Model;
using FoodeLive.MVVM.ViewModel;
using ScottPlot.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Mvvm.Interfaces;

namespace FoodeLive.View.Windows.CRUD.Setting
{
    /// <summary>
    /// Interaction logic for PasswordChange.xaml
    /// </summary>
    public partial class PasswordChange : Window
    {
        MainViewModel viewModel
        {
            get;
        }
        Brush Gray { get; set; }
        public PasswordChange()
        {
            InitializeComponent();
            viewModel = this.DataContext as MainViewModel;
            Gray = password_length.Foreground;
        }

        ~PasswordChange() { }

        private void HandleChange_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSuccessValidate(password_case) && CheckSuccessValidate(password_length) && CheckSuccessValidate(password_match) && CheckSuccessValidate(password_specha))
            {
                if (viewModel.NguoiQuanLy != null)
                    DataProvider.Ins.DB.NguoiQuanLies.ToList().Find(ql => ql.MaQuanLy == viewModel.NguoiQuanLy.MaQuanLy).MatKhau = signup_confirm_password.Password;
                else
                    DataProvider.Ins.DB.NhanViens.ToList().Find(nv => nv.MaNV == viewModel.NhanVienHoatDong.MaNV).MatKhau = signup_confirm_password.Password;
                DataProvider.Ins.DB.SaveChanges();
                System.Windows.MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
                System.Windows.MessageBox.Show("Vui lòng điền phù hợp", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        bool CheckSuccessValidate(TextBlock tb)
        {
            return tb.Foreground == Brushes.ForestGreen;
        }

        void OnValidateSuccessful(ref SymbolIcon symbolIcon, ref TextBlock textBlock)
        {
            symbolIcon.Symbol = SymbolRegular.FoodGrains20;
            symbolIcon.Foreground = Brushes.ForestGreen;
            textBlock.Foreground = Brushes.ForestGreen;
            textBlock.FontWeight = FontWeights.SemiBold;
        }

        void OnValidateFail(ref SymbolIcon symbolIcon, ref TextBlock textBlock)
        {
            symbolIcon.Symbol = SymbolRegular.ErrorCircle12;
            symbolIcon.Foreground = Gray;
            textBlock.Foreground = Gray;
            textBlock.FontWeight = FontWeights.Regular;
        }


        private void signup_password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string password = signup_password.Password;
            string confirmPassword = signup_confirm_password.Password;

            if (password.Length > 8)
                OnValidateSuccessful(ref password_length_icon, ref password_length);
            else
                OnValidateFail(ref password_length_icon, ref password_length);

            Regex lowerCase = new Regex(@"[A-Z][a-z]{0}");
            if (lowerCase.IsMatch(password))
                OnValidateSuccessful(ref password_case_icon, ref password_case);
            else
                OnValidateFail(ref password_case_icon, ref password_case);

            Regex specialCharaters = new Regex(@"[!@#$%^&*(),.?"":{}|<>]");
            if (specialCharaters.IsMatch(password))
                OnValidateSuccessful(ref password_specha_icon, ref password_specha);
            else
                OnValidateFail(ref password_specha_icon, ref password_specha);

            if (password.CompareTo(confirmPassword) == 0)
                OnValidateSuccessful(ref password_match_icon, ref password_match);
            else
                OnValidateFail(ref password_match_icon, ref password_match);
        }

        private void signup_confirm_password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string password = signup_password.Password;
            string confirmPassword = signup_confirm_password.Password;

            if (confirmPassword.CompareTo(password) == 0)
                OnValidateSuccessful(ref password_match_icon, ref password_match);
            else
                OnValidateFail(ref password_match_icon, ref password_match);
        }
    }
}
