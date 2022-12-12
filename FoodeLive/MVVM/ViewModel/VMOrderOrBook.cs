using IT008_DoAnCuoiKi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodeLive.MVVM.ViewModel
{
    public class VMOrderOrBook : BaseViewModel
    {
        private ICommand _OrderCommand;
        private ICommand _BookCommand;

        private string _MaBanAn;
        public string MaBanAn { get { return _MaBanAn; } set { _MaBanAn = value; OnPropertyChanged(); } }
        
        public ICommand OrderCommand { get; set; }
        public ICommand BookCommand { get; set; }

        public VMOrderOrBook()
        {
            OrderCommand = new RelayCommand<string>((p) => p != string.Empty, p => OnPopUp(p));
        }

        void OnPopUp(string p)
        {
            _MaBanAn = p;
        }
    }
}
