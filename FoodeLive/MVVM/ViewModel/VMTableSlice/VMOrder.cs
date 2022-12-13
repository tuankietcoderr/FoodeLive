using IT008_DoAnCuoiKi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodeLive.MVVM.ViewModel.VMTableSlice
{
    public class VMOrder : BaseViewModel
    {
        private ICommand _OrderCommand;

        private string _maBanAn;
        public string MaBanAn { get { return _maBanAn; } set { _maBanAn = value; OnPropertyChanged(); } }
        
        public ICommand OrderCommand { get; set; }

        public VMOrder()
        {
            OrderCommand = new RelayCommand<string>((p) => p != string.Empty, p => OnPopUp(p));
        }

        public VMOrder(string maBanAn)
        {
            MaBanAn = maBanAn;
        }

        void OnPopUp(string p)
        {
            MaBanAn = p;
        }
    }
}
