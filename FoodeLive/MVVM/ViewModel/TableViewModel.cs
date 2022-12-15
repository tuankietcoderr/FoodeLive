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
        public string Loai { get { return _loai; } set { _loai = value; OnPropertyChanged(); } }

        private int _soHoaDon;
        public int SoHoaDon { get => _soHoaDon; set { _soHoaDon = value; OnPropertyChanged(); } }

        private ObservableCollection<BanAn> _ListBanAn;
        public ObservableCollection<BanAn> ListBanAn { get => _ListBanAn; set { _ListBanAn = value; OnPropertyChanged(); } }

        public ICommand AddTableCommand { get; set; }
        public ICommand DeleteTableCommand { get; set; }
        public ICommand RefreshCommand { get; set; }


        public TableViewModel()
        {
            _ListBanAn = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns);

            // Add table
            AddTableCommand = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(_maBanAn) || _loai == "Chọn loại bàn" || _maBanAn[0] != 'B' || _maBanAn.Length > 4)
                    return false;
                var ListMaBanAn = DataProvider.Ins.DB.BanAns.Where(b => b.MaBanAn == MaBanAn).ToList();
                if (ListMaBanAn == null || ListMaBanAn.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                try
                {
                    MessageBox.Show(_maBanAn);
                    var banAn = new BanAn() { MaBanAn = MaBanAn, Loai = Loai, TrangThai = "Trống" };
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
            DeleteTableCommand = new RelayCommand<Window>((p) =>
            {
                return _maBanAn != string.Empty;
            },
            (p) =>
            {
                try
                {
                    MessageBox.Show(_maBanAn);
                    foreach (BanAn item in DataProvider.Ins.DB.BanAns)
                    {
                        if (item.MaBanAn == _maBanAn)
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
