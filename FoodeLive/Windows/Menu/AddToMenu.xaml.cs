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

namespace FoodeLive.Windows.Menu
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

        ~AddToMenu() { }
        string filename;
        private void click_open(object sender, MouseButtonEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.Filter = "Pic (.jpg, .jpeg, .png,)|*.jpg;*.png;*.jpeg"; ;
            bool ? result = dialog.ShowDialog();

            if(result == true)
            {
                filename = dialog.FileName;
                MessageBox.Show(filename);    
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void chose_pic(object sender, MouseButtonEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.Filter = "Picture File (.jpg, .jpeg, .png,)|*.jpg;*.png;*.jpeg";
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                pic.Text = filename;
                //MessageBox.Show(filename.Substring(filename.Length - 4));
            }
        }

    }
}
