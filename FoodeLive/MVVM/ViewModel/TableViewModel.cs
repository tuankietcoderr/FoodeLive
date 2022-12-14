using FoodeLive.MVVM.Model;
using FoodeLive.Windows;
using IT008_DoAnCuoiKi.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace FoodeLive.MVVM.ViewModel
{
    public class TableViewModel : BaseViewModel
    {

        private string _maBanAn;
        public string MaBanAn { get => _maBanAn; set { _maBanAn = value; OnPropertyChanged(); } }
        private string _loai;
        public string Loai { get => _loai; set { _loai = value; OnPropertyChanged(); } }

        private int _soHoaDon;
        public int SoHoaDon { get => _soHoaDon; set { _soHoaDon = value; OnPropertyChanged(); } }

        private ObservableCollection<BanAn> _ListBanAn;
        public ObservableCollection<BanAn> ListBanAn { get => _ListBanAn; set { _ListBanAn = value; OnPropertyChanged(); } }

        public ObservableCollection<HoaDon> ListHoaDon { get; set; }

        public ObservableCollection<ChiTietHoaDon> ListChiTietHoaDon { get; set; }

        public ObservableCollection<ChiTietHoaDon> SelectedChiTietHoaDons { get; set; }

        public ObservableCollection<MonAn> SelectedMonAns { get; set; }

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
                    SelectedMonAns.Clear();
                    _maBanAn = SelectedItem.MaBanAn;
                    foreach (var item in ListChiTietHoaDon)
                        if (item.HoaDon.MaBanAn == _maBanAn)
                        {
                            SelectedChiTietHoaDons.Add(new ChiTietHoaDon(item.SoHoaDon, item.MaMonAn, item.SoLuong, item.MonAn, item.HoaDon));
                            SelectedMonAns.Add(new MonAn(item.MonAn.MaMonAn, item.MonAn.TenMonAn, item.MonAn.Gia, item.MonAn.ImgExtension));
                        }
                    DetailOrderBook detailOrderBook = new DetailOrderBook();
                    detailOrderBook.ShowDialog();
                }
            }
        }

        public ICommand AddTableCommand { get; set; }
        public ICommand DeleteTableCommand { get; set; }
        public ICommand RefreshCommand { get; set; }


        public TableViewModel()
        {
            _ListBanAn = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns);
            ListHoaDon = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons);
            ListChiTietHoaDon = new ObservableCollection<ChiTietHoaDon>();
            SelectedChiTietHoaDons = new ObservableCollection<ChiTietHoaDon>();
            SelectedMonAns = new ObservableCollection<MonAn>();
            var query = from ChiTietHoaDons in DataProvider.Ins.DB.ChiTietHoaDons
                        select new
                        {
                            ChiTietHoaDons.MaMonAn,
                            ChiTietHoaDons.SoLuong,
                            ChiTietHoaDons.SoHoaDon,
                            ChiTietHoaDons.MonAn,
                            ChiTietHoaDons.HoaDon
                        };
            foreach (var cthd in query)
                ListChiTietHoaDon.Add(new ChiTietHoaDon(cthd.SoHoaDon, cthd.MaMonAn, cthd.SoLuong, cthd.MonAn, cthd.HoaDon));

            // Add table
            AddTableCommand = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(MaBanAn) || Loai == "Chọn loại bàn")
                    return false;
                var ListMaBanAn = DataProvider.Ins.DB.BanAns.Where(b => b.MaBanAn == MaBanAn).ToList();
                if (ListMaBanAn == null || ListMaBanAn.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                try
                {
                    var banAn = new BanAn() { MaBanAn = MaBanAn, Loai = Loai };
                    DataProvider.Ins.DB.BanAns.Add(banAn);
                    DataProvider.Ins.DB.SaveChanges();
                    ListBanAn.Add(banAn);
                    MessageBox.Show("Đã thêm!");
                    p.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            });

            // Delete table
            DeleteTableCommand = new RelayCommand<Window>((p) => { return true; },
            (p) =>
            {
                try
                {
                    foreach (BanAn item in DataProvider.Ins.DB.BanAns)
                    {
                        if (item.MaBanAn == MaBanAn)
                        {
                            DataProvider.Ins.DB.BanAns.Remove(item);
                            ListBanAn.Remove(item);
                            break;
                        }
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Đã xóa!");
                    p.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.InnerException.Message);
                }
            });

            RefreshCommand = new RelayCommand<object>(p => true, p =>
            {
                //ObservableCollection<BanAn>  _RefListBanAn = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns);
                //_ListBanAn = _RefListBanAn;
                //OnPropertyChanged("ListBanAn");
                
            });
        }
    }
}
