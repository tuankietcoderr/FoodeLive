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

namespace FoodeLive.Windows.Setting
{
    /// <summary>
    /// Interaction logic for UpdateForm.xaml
    /// </summary>
    public partial class UpdateForm : Window
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        private void chose_pic(object sender, MouseButtonEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.Filter = "Pic (.jpg)| *.jpg*";
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                pic.Text = filename;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
