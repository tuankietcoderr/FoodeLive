using IT008_DoAnCuoiKi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodeLive.MVVM.ViewModel.VMTableSlice
{
    public class VMBook : BaseViewModel
    {
        private ICommand _BookCommand;

        private string _MaBanAn;
        public string MaBanAn { get { return _MaBanAn; } set { _MaBanAn = value; OnPropertyChanged(); } }

        public ICommand BookCommand { get; set; }

        public VMBook()
        {
            BookCommand = new RelayCommand<string>((p) => p != string.Empty, p => OnPopUp(p));
        }

        public VMBook(string maBanAn)
        {
            MaBanAn = maBanAn;
        }

        void OnPopUp(string p)
        {
            _MaBanAn = p;
        }
    }
}
