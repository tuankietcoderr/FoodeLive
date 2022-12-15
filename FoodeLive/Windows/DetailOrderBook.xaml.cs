using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using FoodeLive.MVVM.ViewModel;
using FoodeLive.MVVM.ViewModel.VMTableSlice;
using FoodeLive.Pages.Table;

namespace FoodeLive.Windows
{
    /// <summary>
    /// Interaction logic for OrderOrBook.xaml
    /// </summary>
    public partial class DetailOrderBook : Window
    {

        public DetailOrderBook()
        {
            InitializeComponent();
            detail.IsSelected = true;
        }

        public FoodViewModel viewModel
        {
            get;
        }

        public DetailOrderBook(string MaBanAn, int SoHoaDon)
        {
            viewModel = new FoodViewModel(MaBanAn, SoHoaDon);
            InitializeComponent();
            detail.IsSelected = true;
        }

        ~DetailOrderBook() { }

        private void orderOrBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem selected = orderOrBook.SelectedItem as ListBoxItem;
            if (selected == null)
                return;
            string name = selected.Name;

            switch (name)
            {
                case "detail":
                    navframe.Navigate(new Detail());
                    break;
                case "order":
                    navframe.Navigate(new Order());
                    break;
                case "book":
                    navframe.Navigate(new Book());
                    break;
                default:
                    navframe.Navigate(new Detail());
                    break;
            }
        }
    }
}
