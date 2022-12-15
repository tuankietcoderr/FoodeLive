using FoodeLive.MVVM.Model;
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
using System.Windows.Media;

namespace FoodeLive.MVVM.ViewModel
{
    public class FoodViewModel : BaseViewModel
    {
        private string _maBanAn;
        public string MaBanAn { get => _maBanAn; set { _maBanAn = value; OnPropertyChanged(); } }

        private int _soHoaDon;
        public int SoHoaDon { get => _soHoaDon; set { _soHoaDon = value; OnPropertyChanged(); } }


        public ObservableCollection<MonAn> ListMonAn { get; set; }

        private ObservableCollection<MonAn> _selectedItems;
        public ObservableCollection<MonAn> SelectedItems { get => _selectedItems; set { _selectedItems = value; OnPropertyChanged(); } }

        public ICommand AnnounceAddFood { get; set; }

        public FoodViewModel(string _maBanAn)
        {
            this._maBanAn = _maBanAn;
            //this._soHoaDon = _soHoaDon;
            ListMonAn = new ObservableCollection<MonAn>(DataProvider.Ins.DB.MonAns);
            _selectedItems = new ObservableCollection<MonAn>();

            List<MonAn> selectedCollection = new List<MonAn>();
            AnnounceAddFood = new RelayCommand<object>(p =>
            {
                System.Collections.IList items = (System.Collections.IList)p;
                selectedCollection = items.Cast<MonAn>().ToList();
                return selectedCollection.Count > 0;
            }, p =>
            {
                try
                {
                    _selectedItems = new ObservableCollection<MonAn>(selectedCollection);
                    // Di insert monan da chon vao ban an
                    //_ = DataProvider.Ins.DB.HoaDons.ToList().Find(u => u.MaBanAn == _maBanAn).ChiTietHoaDons.Where(t => t.SoHoaDon == Convert.ToInt32(_soHoaDon)).A;
                    foreach (MonAn item in _selectedItems)
                    {
                        //ChiTietHoaDon cthd = new ChiTietHoaDon() { SoHoaDon = Convert.ToInt32(_soHoaDon), MonAn = item, MaMonAn = item.MaMonAn, SoLuong = item. };
                    }
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.InnerException.InnerException.Message);
                }
            });
        }
    }
}
