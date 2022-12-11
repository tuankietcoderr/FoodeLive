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

namespace FoodeLive.MenuPage
{
    /// <summary>
    /// Interaction logic for AddToMenu.xaml
    /// </summary>
    public partial class AddToMenu : Window
    {
        public AddToMenu()
        {
            InitializeComponent();
        }

        private void click_open(object sender, MouseButtonEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
