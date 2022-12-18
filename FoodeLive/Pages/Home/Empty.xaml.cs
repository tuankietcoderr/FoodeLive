using FoodeLive.MVVM.Model;
using FoodeLive.MVVM.ViewModel.VMTableSlice;
using IT008_DoAnCuoiKi.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace FoodeLive.Pages.Home
{
    /// <summary>
    /// Interaction logic for Empty.xaml
    /// </summary>
    public partial class Empty : Page
    {
        public Empty()
        {
            InitializeComponent();
        }

        ~Empty() { }

        private void table_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Card card = sender as Card;
            string MaBanAn = card.Tag.ToString();
            VMBookDetailOrder vMOrderOrBook = new VMBookDetailOrder(MaBanAn);
            Windows.DetailOrderBook orderOrBook = new Windows.DetailOrderBook();
            orderOrBook.ShowDialog();
        }

        class EmptyTable: BaseViewModel
        {
            private ObservableCollection<BanAn> _EmptyTables;
            public ObservableCollection<BanAn> EmptyTables { get => _EmptyTables; set { _EmptyTables = value; OnPropertyChanged(); } }

            public EmptyTable()
            {
                _EmptyTables = new ObservableCollection<BanAn>(DataProvider.Ins.DB.BanAns.Where(b => b.TrangThai == "Trống"));

            }
        }



    }
}
