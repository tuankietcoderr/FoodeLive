using FoodeLive.Windows.TableDetail;
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

namespace FoodeLive.Pages.TableDetail
{
    /// <summary>
    /// Interaction logic for Table_FoodDetail.xaml
    /// </summary>
    public partial class Table_FoodDetail : Page
    {
        public Table_FoodDetail()
        {
            InitializeComponent();
            foodsOfTable = GetFood();
            if (foodsOfTable.Count > 0)
            {
                FoodControl.ItemsSource = foodsOfTable;
            }
        }

        private List<Table_Foods> foodsOfTable = new List<Table_Foods>();
        private List<Table_Foods> GetFood()
        {
            return new List<Table_Foods>
            {
                new Table_Foods("/Windows/TableDetail/hamburgers.jpg", "Demo1", "20,000 VND"),
                new Table_Foods("/Windows/TableDetail/hamburgers.jpg", "Demo1", "20,000 VND"),
                new Table_Foods("/Windows/TableDetail/hamburgers.jpg", "Demo1", "20,000 VND"),
                new Table_Foods("/Windows/TableDetail/hamburgers.jpg", "Demo1", "20,000 VND"),
                new Table_Foods("/Windows/TableDetail/hamburgers.jpg", "Demo1", "20,000 VND"),
                new Table_Foods("/Windows/TableDetail/hamburgers.jpg", "Demo1", "20,000 VND"),
                new Table_Foods("/Windows/TableDetail/hamburgers.jpg", "Demo1", "20,000 VND"),
                new Table_Foods("/Windows/TableDetail/hamburgers.jpg", "Demo1", "20,000 VND"),
                new Table_Foods("/Windows/TableDetail/hamburgers.jpg", "Demo1", "20,000 VND"),
            };
        }

        private void AddFoodBtn_Click(object sender, RoutedEventArgs e)
        {
            Table_AddFood addFoodForm = new Table_AddFood();
            addFoodForm.Show();
        }
    }
}