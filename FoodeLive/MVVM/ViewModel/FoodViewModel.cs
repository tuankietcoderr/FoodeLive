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
        public ObservableCollection<MonAn> ListMonAn { get; set; }

        private MonAn _selectedItems;
        public MonAn SelectedItems { get; set; }
        public ICommand AnnounceAddFood { get; set; }

        public FoodViewModel()
        {
            ListMonAn = new ObservableCollection<MonAn>(DataProvider.Ins.DB.MonAns);
            AnnounceAddFood = new RelayCommand<object>(p => p != null, p =>
            {
                System.Collections.IList items = (System.Collections.IList)p;
                var collection = items.Cast<MonAn>();
                MessageBox.Show(collection.ToList().First().MaMonAn);
            });
        }
    }
}
