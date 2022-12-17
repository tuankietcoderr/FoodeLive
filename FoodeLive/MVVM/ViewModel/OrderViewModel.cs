using FoodeLive.MVVM.Model;
using FoodeLive.Windows;
using FoodeLive.Windows.Auth;
using IT008_DoAnCuoiKi.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FoodeLive.MVVM.ViewModel
{
    public class OrderViewModel : BaseViewModel
    {
        //private string _soHoaDon;
        //public string SoHoaDon { get => _soHoaDon; set { _soHoaDon = value; OnPropertyChanged(); } }

        //private string _maBanAn;
        //public string MaBanAn { get => _maBanAn; set { _maBanAn = value; OnPropertyChanged(); } }

        //private string _maNhanVien;
        //public string MaNhanVien { get => _maNhanVien; set { _maNhanVien = value; OnPropertyChanged(); } }

        //private double _triGia;
        //public double TriGia { get => _triGia; set { _triGia = value; OnPropertyChanged(); } }
        //Khong can thiet


        private DateTime _ngayHoaDon = DateTime.Now;
        public DateTime NgayHoaDon
        {
            get => _ngayHoaDon;
            set
            {
                if (_ngayHoaDon.Month != value.Month)
                {
                    _ngayHoaDon = value;
                    OnPropertyChanged();
                    _LisHoaDon = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(b => b.NgayLapHoaDon.Value.Month == _ngayHoaDon.Month)); OnPropertyChanged("ListHoaDon");
                }
            }
        }

        private ObservableCollection<HoaDon> _LisHoaDon;
        public ObservableCollection<HoaDon> ListHoaDon { get => _LisHoaDon; set { _LisHoaDon = value; OnPropertyChanged(); } }

        public ICommand RefreshCommand { get; set; }


        public OrderViewModel()
        {
            _ngayHoaDon = new DateTime();
            _LisHoaDon = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons);
            RefreshCommand = new RelayCommand<object>(p => true, p =>
            {
                _LisHoaDon.Clear();
                _LisHoaDon = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons);
                OnPropertyChanged("ListHoaDon");
            });
        }


    }
}
