using FoodeLive.Auth;
using FoodeLive.MVVM.Model;
using FoodeLive.utils;
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
using FoodeLive.View.Pages;

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

        ~MainWindow() { }

        private void navbar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem selected = navbar.SelectedItem as ListBoxItem;
            if (selected == null)
                return;
            string name = selected.Name;
            switch (name)
            {
                case "home":
                    navframe.Navigate(new View.Pages.Home.Container());
                    break;
                case "menu":
                    navframe.Navigate(new View.Pages.Menu.MenuP());
                    break;
                case "history":
                    navframe.Navigate(new View.Pages.History.All());
                    break;
                case "report":
                    navframe.Navigate(new View.Pages.Report.All());
                    break;
                case "notification":
                    navframe.Navigate(new View.Pages.Notification.All());
                    break;
                case "setting":
                    navframe.Navigate(new View.Pages.Setting.SettingP());
                    break;
                default:
                    navframe.Navigate(new View.Pages.Home.All());
                    break;
            }
        }
    }
}
