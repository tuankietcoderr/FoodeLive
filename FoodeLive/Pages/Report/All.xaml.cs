using FoodeLive.MVVM.Model;
using FoodeLive.MVVM.ViewModel;
using IT008_DoAnCuoiKi.ViewModel;
using Microsoft.Win32;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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


namespace FoodeLive.Pages.Report
{
    /// <summary>
    /// Interaction logic for All.xaml
    /// </summary>
    public partial class All : Page
    {

        MainViewModel ViewModel { get; }
        public All()
        {
            InitializeComponent();
            ViewModel = this.DataContext as MainViewModel;
            ReportAnalyze.Plot.SetAxisLimits(yMin: 0, xMin: 0, yMax: 3000000, xMax: 31);
            ReportAnalyze.Plot.XLabel("Ngày");
            ReportAnalyze.Plot.YLabel("Doanh số");
            PieAnalyze.Plot.XLabel("Ngày");
            PieAnalyze.Plot.XLabel("Doanh số");

            int currMonth = DateTime.Now.Month;
            int currYear = DateTime.Now.Year;
            AnalyzeData(currMonth, currYear);
        }


        ~All() { }

        private void analyze_Click(object sender, RoutedEventArgs e)
        {
            string monthContent = ViewModel.ReportMonth.Content.ToString();
            string yearContent = ViewModel.ReportYear.Content.ToString();
            if (monthContent == "Tháng" || yearContent == "Năm")
                return;
            int month = Convert.ToInt32(monthContent);
            int year = Convert.ToInt32(yearContent);
            AnalyzeData(month, year);
        }

        private void AnalyzeData(int month, int year)
        {
            ReportAnalyze.Reset();
            PieAnalyze.Reset();
            ReportAnalyze.Plot.SetAxisLimits(yMin: 0, xMin: 0, yMax: 3000000, xMax: 32);
            ReportAnalyze.Plot.XLabel("Ngày");
            ReportAnalyze.Plot.YLabel("Doanh số");
            PieAnalyze.Plot.XLabel("Ngày");
            PieAnalyze.Plot.YLabel("Doanh số");

            int days = DateTime.DaysInMonth(year, month);

            double[] values = new double[days];
            double[] positions = new double[days];
            string[] labels = new string[days];

            ObservableCollection<HoaDon> hoaDons = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(hd => hd.NgayLapHoaDon.Value.Month == month && hd.NgayLapHoaDon.Value.Year == year));
            if (hoaDons.Count == 0)
            {
                for (int i = 0; i < days; i++)
                {
                    values[i] = 0;
                    labels[i] = (i + 1).ToString();
                    positions[i] = i + 1;
                }
            }
            else
            {
                int j = 0;
                for (int i = 0; i < days; i++)
                {
                    if (hoaDons[j].NgayLapHoaDon.Value.Day == i + 1)
                    {
                        values[i] = Convert.ToDouble(DataProvider.Ins.DB.HoaDons.Where(hd => hd.NgayLapHoaDon.Value.Month == month && hd.NgayLapHoaDon.Value.Year == year && hd.NgayLapHoaDon.Value.Day == i + 1).Sum(t => t.TriGia));
                        j++;
                    }
                    else
                        values[i] = 0;
                    positions[i] = i + 1;
                    labels[i] = (i + 1).ToString();
                }
            }

            var bar = ReportAnalyze.Plot.AddBar(values, positions);
            bar.ShowValuesAboveBars = true;
            ReportAnalyze.Plot.XTicks(positions, labels);
            ReportAnalyze.Render();

            var ScatterList = PieAnalyze.Plot.AddScatterList();
            ScatterList.AddRange(positions, values);
            PieAnalyze.Render();

        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "Pic (.png)| *.png*";
            dialog.RestoreDirectory = true;

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                PieAnalyze.Plot.SaveFig(dialog.FileName + ".png");
            }
        }
    }
}
