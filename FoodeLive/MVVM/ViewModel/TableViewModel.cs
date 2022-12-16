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

        private ObservableCollection<BanAn> _EmptyTables;
        public ObservableCollection<BanAn> EmptyTables { get => _EmptyTables; set { _EmptyTables = value; OnPropertyChanged(); } }

        private ObservableCollection<BanAn> _UsingTables;
        public ObservableCollection<BanAn> UsingTables { get => _UsingTables; set { _UsingTables = value; OnPropertyChanged(); } }

        private ObservableCollection<BanAn> _BookedTables;
        public ObservableCollection<BanAn> BookedTables { get => _BookedTables; set { _BookedTables = value; OnPropertyChanged(); } }

        public ICommand AddTableCommand { get; set; }
        public ICommand DeleteTableCommand { get; set; }
        public ICommand BookTableCommand { get; set; }

        private ChiTietDatBan _chiTietDatBan;
        public ChiTietDatBan ChiTietDatBan { get => _chiTietDatBan; set { _chiTietDatBan = value; OnPropertyChanged(); } }



        public TableViewModel()
        {
            // Add table
            AddTableCommand = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(_maBanAn) || _loai == "Chọn loại bàn" || _maBanAn[0] != 'B' || _maBanAn.Length != 4)
                    return false;
                var ListMaBanAn = DataProvider.Ins.DB.BanAns.Where(b => b.MaBanAn == MaBanAn).ToList();
                if (ListMaBanAn == null || ListMaBanAn.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                try
                {
                    var banAn = new BanAn() { MaBanAn = _maBanAn, Loai = Loai, TrangThai = "Trống" };
                    DataProvider.Ins.DB.BanAns.Add(banAn);
                    DataProvider.Ins.DB.SaveChanges();
                    _ListBanAn.Add(banAn);
                    _EmptyTables.Add(banAn);
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
                    foreach (BanAn item in DataProvider.Ins.DB.BanAns)
                    {
                        if (item.MaBanAn == _maBanAn)
                        {
                            DataProvider.Ins.DB.BanAns.Remove(item);
                            _ListBanAn.Remove(item);
                            _UsingTables.Remove(item);
                            _EmptyTables.Remove(item);
                            _BookedTables.Remove(item);
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

            BookTableCommand = new RelayCommand<object>(p =>
            {
                MessageBox.Show(_chiTietDatBan.NguoiDat);
                
                return false;
            }, p =>
            {
                MessageBox.Show(ChiTietDatBan.NguoiDat);
                //DataProvider.Ins.DB.BanAns.ToList().Find(b => b.MaBanAn == _maBanAn).TrangThai = "Đã đặt";
                //DataProvider.Ins.DB.SaveChanges();
            });
        }
    }
}
