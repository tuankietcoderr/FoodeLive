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
using Wpf.Ui.Controls;

namespace FoodeLive.MVVM.View.Windows.CRUD.Setting
{
    /// <summary>
    /// Interaction logic for StaffManager.xaml
    /// </summary>
    public partial class StaffManager : Window
    {
        public StaffManager()
        {
            InitializeComponent();
            datagrid.BeginningEdit += (s, ss) => ss.Cancel = true;
        }
    }
}
