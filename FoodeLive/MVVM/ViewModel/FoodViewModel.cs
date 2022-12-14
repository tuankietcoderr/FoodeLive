using FoodeLive.MVVM.Model;
using IT008_DoAnCuoiKi.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodeLive.MVVM.ViewModel
{
    public class FoodViewModel : BaseViewModel
    {

        private ObservableCollection<MonAn> _ListMonAn;

        public ObservableCollection<MonAn> ListMonAn { get; set; }

        public FoodViewModel()
        {
            _ListMonAn = new ObservableCollection<MonAn>(DataProvider.Ins.DB.MonAns);
        }
    }
}
