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

        private Brush brush { get; set; }
        public Order()
        {
            InitializeComponent();
        }

        ~Order() { }

        public Order(VMOrder viewModel)
        {
            InitializeComponent();
        }

        private void AddFoodBtn_Click(object sender, RoutedEventArgs e)
        {
            Windows.TableDetail.TableAddFood tableAddFood = new Windows.TableDetail.TableAddFood();
            tableAddFood.ShowDialog();
        }

        private void ListViewItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                MessageBox.Show(item.ToString());
            }
        }

        private void card_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Wpf.Ui.Controls.CardColor item = sender as Wpf.Ui.Controls.CardColor;
            if (item != null)
            {
                if (item.BorderBrush == Brushes.LightGreen)
                {
                    item.BorderBrush = brush;
                }
                else
                {
                    brush = item.BorderBrush;
                    item.BorderBrush = Brushes.LightGreen;
                }
            }
        }
    }
}