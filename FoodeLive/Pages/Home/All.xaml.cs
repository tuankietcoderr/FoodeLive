using FoodeLive.utils;
using FoodeLive.Windows;
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

        private void all_table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Card card = all_table.SelectedItem as Card;
            if (card == null)
                return;
            Windows.OrderOrBook orderOrBook = new Windows.OrderOrBook();
            orderOrBook.ShowDialog();

        }
    }
}
