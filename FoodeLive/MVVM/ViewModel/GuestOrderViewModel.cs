using IT008_DoAnCuoiKi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodeLive.MVVM.ViewModel
{
    public class GuestOrderViewModel : BaseViewModel
    {
        public ICommand RefreshNotification { get; set; }
        public GuestOrderViewModel()
        {

        }
    }
}
