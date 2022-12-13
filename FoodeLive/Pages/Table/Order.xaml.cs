using FoodeLive.MVVM.ViewModel.VMTableSlice;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodeLive.Pages.Table
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        public Order()
        {
            InitializeComponent();
        }

        ~Order() { }

        public VMOrder ViewModel
        {
            get;
        }

        public Order(VMOrder viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void AddFoodBtn_Click(object sender, RoutedEventArgs e)
        {
            Windows.TableDetail.TableAddFood tableAddFood = new Windows.TableDetail.TableAddFood();
            tableAddFood.ShowDialog();

        }
    }
}