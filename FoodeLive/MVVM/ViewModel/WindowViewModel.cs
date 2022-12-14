using FoodeLive.Windows.Auth;
using IT008_DoAnCuoiKi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace FoodeLive.MVVM.ViewModel
{
    public class WindowViewModel : BaseViewModel
    {
        public ICommand LogOutCommand { get; set; }

        public WindowViewModel()
        {
            LogOutCommand = new RelayCommand<Window>(p =>
            {
                return true;
            },
            p =>
            {
                Login login = new Login();
                p.Close();
                login.ShowDialog();
            }
            );
        }
    }
}
