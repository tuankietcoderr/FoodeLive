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

namespace FoodeLive.MenuPage
{
    /// <summary>
    /// Interaction logic for MenuP.xaml
    /// </summary>
    public partial class MenuP : Page
    {
        public MenuP()
        {
            InitializeComponent();
            demos = GetFood();
            if (demos.Count > 0)
            {
                FoodControl.ItemsSource = demos;
            }
            
        }
        private List<Demo> demos = new List<Demo>();
        private List<Demo> GetFood()

        {
            return new List<Demo>()
            {
                new Demo("/MenuPage/fooddome.png","Demo1","20,000 VND"),
                new Demo("/MenuPage/fooddome.png","Demo1","20,000 VND"),
                new Demo("/MenuPage/fooddome.png","Demo1","20,000 VND"),
                new Demo("/MenuPage/fooddome.png","Demo1","20,000 VND"),
                new Demo("/MenuPage/fooddome.png","Demo1","20,000 VND"),
                new Demo("/MenuPage/fooddome.png","Demo1","20,000 VND"),
                new Demo("/MenuPage/fooddome.png","Demo1","20,000 VND"),
                new Demo("/MenuPage/fooddome.png","Demo1","20,000 VND")
            };
        }
        bool isClick = false;
        private void all_Click(object sender, RoutedEventArgs e)
        {
            if (isClick)
            {
                isClick = false;
                type.Background = Brushes.White;
                type.Foreground = Brushes.Black; 
                all.Background = Brushes.DarkOrange;
                all.Foreground = Brushes.White;
            }
            else
            {
                type.Background = Brushes.DarkOrange;
                type.Foreground = Brushes.White;
                all.Background = Brushes.White;
                all.Foreground = Brushes.Black;
                isClick = true;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuPage.AddToMenu addToMenu = new MenuPage.AddToMenu();
            addToMenu.Show();
        }
    }
}
