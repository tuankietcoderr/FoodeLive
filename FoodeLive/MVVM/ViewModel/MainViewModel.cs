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
    public class MainViewModel : BaseViewModel
    {
        private TableViewModel _tableViewModel;
        public TableViewModel TableViewModel { get => _tableViewModel; set { _tableViewModel = value; OnPropertyChanged(); } }
        private FoodViewModel _foodViewModel;
        public FoodViewModel FoodViewModel { get => _foodViewModel; set { _foodViewModel = value; OnPropertyChanged(); } }

        private string _maBanAn;
        public string MaBanAn { get => _maBanAn; set { _maBanAn = value; OnPropertyChanged(); } }

        private int _soHoaDon;
        public int SoHoaDon { get => _soHoaDon; set { _soHoaDon = value; OnPropertyChanged(); } }


        private BanAn _selectedItem;
        public BanAn SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    _maBanAn = _selectedItem.MaBanAn;
                    if (DataProvider.Ins.DB.BanAns.ToList().Find(t => t.MaBanAn == _maBanAn).TrangThai != "Trống")
                        _soHoaDon = DataProvider.Ins.DB.HoaDons.ToList().FindLast(t => t.MaBanAn == _maBanAn).SoHoaDon;
                    else
                        _soHoaDon = 0;
                    _tableViewModel.MaBanAn = _maBanAn;
                    _foodViewModel.MaBanAn = _maBanAn;
                    _foodViewModel.SoHoaDon = _soHoaDon;
                    DetailOrderBook detailOrderBook = new DetailOrderBook();
                    detailOrderBook.ShowDialog();
                }
            }
        }

        public ICommand RefreshCommand { get; set; }


        public MainViewModel()
        {
            _tableViewModel = new TableViewModel();
            _foodViewModel = new FoodViewModel();
            _tableViewModel.ListBanAn = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns);
            _tableViewModel.EmptyTables = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns.Where(b => b.TrangThai == "Trống"));
            _tableViewModel.UsingTables = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns.Where(b => b.TrangThai == "Có khách"));
            _tableViewModel.BookedTables = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns.Where(b => b.TrangThai == "Đã đặt"));
            _foodViewModel.ListMonAn = new ObservableCollection<MonAn>(DataProvider.Ins.DB.MonAns);
            _tableViewModel.ChiTietDatBan = new ChiTietDatBan();

            // khi bam them ban, ban moi se duoc push vao trong trong
            // neu ban duoc order, 

            RefreshCommand = new RelayCommand<object>(p => true, p =>
            {
                _tableViewModel.ListBanAn.Clear();
                _tableViewModel.ListBanAn = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns);
                _tableViewModel.EmptyTables.Clear();
                _tableViewModel.EmptyTables = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns.Where(b => b.TrangThai == "Trống"));
                _tableViewModel.UsingTables.Clear();
                _tableViewModel.UsingTables = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns.Where(b => b.TrangThai == "Có khách"));
                _tableViewModel.BookedTables.Clear();
                _tableViewModel.BookedTables = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns.Where(b => b.TrangThai == "Đã đặt"));
                _foodViewModel.ListMonAn.Clear();
                _foodViewModel.ListMonAn = new ObservableCollection<MonAn>(DataProvider.Ins.DB.MonAns);

            });
        }
    }
}
