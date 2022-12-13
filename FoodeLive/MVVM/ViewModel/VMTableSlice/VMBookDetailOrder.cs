using IT008_DoAnCuoiKi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FoodeLive.MVVM.ViewModel.VMTableSlice
{
    public class VMBookDetailOrder : BaseViewModel
    {
        private ICommand _BookDetailOrder;

        private string _MaBanAn;
        private VMDetail _vMDetail;
        private VMOrder _vMOrder;
        private VMBook _vMBook;

        public string MaBanAn { get { return _MaBanAn; } set { _MaBanAn = value; OnPropertyChanged(); } }
        public VMDetail VMDetail { get { return _vMDetail; } set { _vMDetail = value; OnPropertyChanged(); } }
        public VMOrder VMOrder { get { return _vMOrder; } set { _vMOrder = value; OnPropertyChanged(); } }
        public VMBook VMBook { get { return _vMBook; } set { _vMBook = value; OnPropertyChanged(); } }

        public ICommand BookDetailOrderCommand { get; set; }

        public VMBookDetailOrder()
        {
            BookDetailOrderCommand = new RelayCommand<string>((p) => p != null, p => OnPopUp(p));
        }

        public VMBookDetailOrder(string maBanAn)
        {
            MaBanAn = maBanAn;
            VMDetail= new VMDetail(MaBanAn);
            VMOrder = new VMOrder(MaBanAn);
            VMBook = new VMBook(MaBanAn);
        }

        void OnPopUp(string p)
        {
            _MaBanAn= p;
        }
    }
}
