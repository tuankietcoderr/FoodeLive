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

        ~DetailOrderBook() { }
        public VMDetail DetailViewModel
        {
            get;
        }

        public VMOrder OrderViewModel
        {
            get;
        }

        public VMBook BookViewModel
        {
            get;
        }

        public DetailOrderBook(VMBookDetailOrder vMBookDetailOrder)
        {
            DetailViewModel = vMBookDetailOrder.VMDetail;
            OrderViewModel = vMBookDetailOrder.VMOrder;
            BookViewModel = vMBookDetailOrder.VMBook;
            InitializeComponent();
            detail.IsSelected= true;
        }

        public DetailOrderBook(VMDetail vMDetail, VMOrder vMOrder, VMBook vMBook)
        {
            DetailViewModel = vMDetail;
            OrderViewModel = vMOrder;
            BookViewModel = vMBook;
            InitializeComponent();
            detail.IsSelected = true;
        }

        private void orderOrBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem selected = orderOrBook.SelectedItem as ListBoxItem;
            if (selected == null)
                return;
            string name = selected.Name;

            switch (name)
            {
                case "detail":
                    navframe.Navigate(new Detail(DetailViewModel));
                    break;
                case "order":
                    navframe.Navigate(new Order(OrderViewModel));
                    break;
                case "book":
                    navframe.Navigate(new Book(BookViewModel));
                    break;
                default:
                    navframe.Navigate(new Detail(DetailViewModel));
                    break;
            }
        }
    }
}
