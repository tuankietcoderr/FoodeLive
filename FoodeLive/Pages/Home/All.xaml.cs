using FoodeLive.MVVM.Model;
using FoodeLive.MVVM.ViewModel.VMTableSlice;
using FoodeLive.utils;
using FoodeLive.Windows;
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
    /// Interaction logic for All.xaml
    /// </summary>
    public partial class All : Page
    {
        public All()
        {
            InitializeComponent();
        }
        ~All() { }


        private void table_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Card card = sender as Card;
            string MaBanAn = card.Tag.ToString();
            VMBookDetailOrder vMBookDetailOrder = new VMBookDetailOrder(MaBanAn);
            Windows.DetailOrderBook orderOrBook = new Windows.DetailOrderBook();
            orderOrBook.ShowDialog();
        }
    }
}
