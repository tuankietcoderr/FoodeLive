using FoodeLive.MVVM.Model;
using FoodeLive.Windows;
using FoodeLive.Windows.Auth;
using IT008_DoAnCuoiKi.ViewModel;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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

        private bool _forceRerender = false;
        public bool ForceRerender { get => _foodViewModel.Ordered || _tableViewModel.IsBooked; set { _forceRerender = value; OnPropertyChanged(); } }

        private ObservableCollection<HoaDon> _ListHoaDon;
        public ObservableCollection<HoaDon> ListHoaDon { get => _ListHoaDon; set { _ListHoaDon = value; OnPropertyChanged(); } }


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
                    if (DataProvider.Ins.DB.BanAns.ToList().Find(t => t.MaBanAn == _maBanAn).TrangThai == "Có khách")
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

        private ComboBoxItem _reportMonth;
        public ComboBoxItem ReportMonth { get => _reportMonth; set { _reportMonth = value; OnPropertyChanged(); } }

        private ComboBoxItem _reportYear;
        public ComboBoxItem ReportYear { get => _reportYear; set { _reportYear = value; OnPropertyChanged(); } }

        public ICommand RefreshCommand { get; set; }
        public ICommand AnalyzeReportCommand { get; set; }


        public MainViewModel()
        {
            _tableViewModel = new TableViewModel();
            _foodViewModel = new FoodViewModel();
            _tableViewModel.ListBanAn = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns);
            _tableViewModel.EmptyTables = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns.Where(b => b.TrangThai == "Trống"));
            _tableViewModel.UsingTables = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns.Where(b => b.TrangThai == "Có khách"));
            _tableViewModel.BookedTables = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns.Where(b => b.TrangThai == "Đã đặt"));
            _foodViewModel.ListMonAn = new ObservableCollection<MonAn>(DataProvider.Ins.DB.MonAns);
            _ListHoaDon = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons);


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
            AnalyzeReportCommand = new RelayCommand<WpfPlot>(p =>
            {
                string monthContent = ReportMonth.Content.ToString();
                string yearContent = ReportYear.Content.ToString();
                return monthContent != "Tháng" && yearContent != "Năm";
            }, p =>
            {
                
            });
        }
    }
}
