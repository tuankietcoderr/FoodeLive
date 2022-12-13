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
        private ObservableCollection<BanAn> _ListBanAn;
        public ObservableCollection<BanAn> ListBanAn { get => _ListBanAn; set { _ListBanAn = value; OnPropertyChanged(); } }

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
                    MaBanAn = SelectedItem.MaBanAn;
                    DetailOrderBook detailOrderBook = new DetailOrderBook();
                    detailOrderBook.ShowDialog();
                }
            }
        }
        private string _maBanAn;
        public string MaBanAn { get => _maBanAn; set { _maBanAn = value; OnPropertyChanged();} }
        private string _loai;
        public string Loai { get => _loai; set { _loai = value; OnPropertyChanged(); } }

        public ICommand AddTableCommand { get; set; }
        public ICommand DeleteTableCommand { get; set; }

        public TableViewModel()
        {
            _ListBanAn = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns);

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
                var banAn = new BanAn() { MaBanAn = MaBanAn, Loai = Loai };
                DataProvider.Ins.DB.BanAns.Add(banAn);
                DataProvider.Ins.DB.SaveChanges();
                ListBanAn.Add(banAn);
                MessageBox.Show("Đã thêm!");
                p.Close();
            });

            // Delete table
            DeleteTableCommand = new RelayCommand<Window>((p) => { return true; },
            (p) =>
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
            });
        }
    }
}
