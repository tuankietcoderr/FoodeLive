using FoodeLive.MVVM.ViewModel.VMTableSlice;
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

namespace FoodeLive.Pages.OrderOrBook
{
    /// <summary>
    /// Interaction logic for Detail.xaml
    /// </summary>
    public partial class Detail : Page
    {
        public Detail()
        {
            InitializeComponent();
        }

        public VMDetail ViewModel
        {
            get;
        }

        public Detail(VMDetail viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            this.DataContext = ViewModel;
        }
    }
}
