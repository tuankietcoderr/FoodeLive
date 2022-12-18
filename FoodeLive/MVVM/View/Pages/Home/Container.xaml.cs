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

namespace FoodeLive.View.Pages.Home
{
    /// <summary>
    /// Interaction logic for Container.xaml
    /// </summary>
    public partial class Container : Page
    {
        public Container()
        {
            InitializeComponent();
            home_container_nav_all.IsSelected = true;
        }

        ~Container() { }

        private void container_nav_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem selected = container_nav.SelectedItem as ListBoxItem;
            if (selected == null)
            {
                return;
            }
            string name = selected.Name;
            switch (name)
            {
                case "home_container_nav_all":
                    home_navframe.Navigate(new All());
                    break;
                case "home_container_nav_using":
                    home_navframe.Navigate(new Using());
                    break;
                case "home_container_nav_empty":
                    home_navframe.Navigate(new Empty());
                    break;
                case "home_container_nav_booked":
                    home_navframe.Navigate(new Booked());
                    break;
                default:
                    home_navframe.Navigate(new All());
                    break;
            }
        }
    }
}
