using System;
using System.Collections.Generic;
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
using FoodeLive.Pages.OrderOrBook;

namespace FoodeLive.Windows
{
    /// <summary>
    /// Interaction logic for OrderOrBook.xaml
    /// </summary>
    public partial class OrderOrBook : Window
    {
        public string MaBanAn { get; set; }

        public VMOrderOrBook ViewModel
        {
            get;
        }
        public OrderOrBook(VMOrderOrBook viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            order.IsSelected= true;
        }

        private void orderOrBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem selected = orderOrBook.SelectedItem as ListBoxItem;
            if (selected == null)
                return;
            string name = selected.Name;

            switch (name)
            {
                case "order":
                    navframe.Navigate(new Order(ViewModel));
                    break;
                case "book":
                    navframe.Navigate(new Book());
                    break;
                default:
                    navframe.Navigate(new Order());
                    break;
            }
        }
    }
}
