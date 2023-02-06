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

namespace FoodeLive.View.Pages.Menu
{
    /// <summary>
    /// Interaction logic for MenuP.xaml
    /// </summary>
    public partial class MenuP : Page
    {

        public MenuP()
        {
            InitializeComponent();
            /*
             ListBoxItem selected = container_nav.SelectedItem as ListBoxItem;
            if (selected == null)
            {
                return;
            }
            string name = selected.Name;
            switch (name)
            {
                case "notification_dh":
                    notification_navframe.Navigate(new MVVM.View.Pages.Notification.DonHang());
                    break;
                case "notification_db":
                    notification_navframe.Navigate(new DatBan());
                    break;
                default:
                    notification_navframe.Navigate(new MVVM.View.Pages.Notification.DonHang());
                    break;
            }
             */
        }

        ~MenuP() { }
    }
}
