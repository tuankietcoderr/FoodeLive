using FoodeLive.MVVM.Model;
using IT008_DoAnCuoiKi.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FoodeLive.MVVM.ViewModel
{
    public class TableViewModel : BaseViewModel
    {
        private ObservableCollection<BanAn> _ListBanAn;
        public ObservableCollection<BanAn> ListBanAn { get => _ListBanAn; set { _ListBanAn = value; OnPropertyChanged(); } }

        private string _maBanAn;
        public string MaBanAn { get => _maBanAn; set { _maBanAn = value; OnPropertyChanged(); } }
        private string _loai;
        public string Loai { get => _loai; set { _loai = value; OnPropertyChanged(); } }

        public ICommand AddTableCommand { get; set; }

        public TableViewModel()
        {
            _ListBanAn = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns);
            AddTableCommand = new RelayCommand<Window>((p) => {
                if (string.IsNullOrEmpty(MaBanAn) || Loai == "Chọn loại bàn")
                    return false;
                var ListMaBanAn = DataProvider.Ins.DB.BanAns.Where(b => b.MaBanAn == MaBanAn).ToList();
                if (ListMaBanAn == null || ListMaBanAn.Count() != 0)
                    return false;
                return true;
            }, (p) => {
                var banAn = new BanAn() { MaBanAn= MaBanAn, Loai = Loai };
                DataProvider.Ins.DB.BanAns.Add(banAn);
                DataProvider.Ins.DB.SaveChanges();
                ListBanAn.Add(banAn);
                MessageBox.Show("Đã thêm!");
                p.Close();
            });
        }
    }
}
