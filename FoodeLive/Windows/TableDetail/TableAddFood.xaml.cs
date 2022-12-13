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

namespace FoodeLive.Windows.TableDetail
{
    /// <summary>
    /// Interaction logic for TableAddFood.xaml
    /// </summary>
    public partial class TableAddFood : Window
    {
        public TableAddFood()
        {
            InitializeComponent();
            foodsOfTable = GetFood();
            if (foodsOfTable.Count > 0)
            {
                //FoodControl.ItemsSource = foodsOfTable;
            }
        }

        ~TableAddFood() { }


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
    }
}
