using FoodeLive.Auth;
using FoodeLive.Database;
using FoodeLive.Pages.Setting;
using FoodeLive.Windows.Auth;
using FoodeLive.Windows.TableDetail;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodeLive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            home.IsSelected = true;
        }

        private void navbar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem selected = navbar.SelectedItem as ListBoxItem;
            if (selected == null)
                return;
            string name = selected.Name;
            switch (name)
            {
                case "home":
                    navframe.Navigate(new Pages.Home.Container());
                    break;
                case "menu":
                    navframe.Navigate(new Pages.Menu.MenuP());
                    break;
                case "history":
                    navframe.Navigate(new Pages.History.All());
                        break;
                case "report":
                    navframe.Navigate(new Pages.Report.All());
                    break;
                case "setting":
                    navframe.Navigate(new Pages.Setting.SettingP());
                    break;
                default:
                    navframe.Navigate(new Pages.Home.All());
                    break;
            }
        }
    }
}
