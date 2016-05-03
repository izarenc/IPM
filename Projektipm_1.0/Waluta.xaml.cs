using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Projektipm_1._0
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Waluta : Page
    {
        public Waluta()
        {
            this.InitializeComponent();
            //this.Loaded += MainChart_Loaded;
            loadData("EUR");
        }

        public async void loadData(string d)
        {
            textBlock.Text = "Historia waluty " + (string) d;
            List<DaneWykres> daneDoWykresu = new List<DaneWykres>();
            WczytaneDane.wczytajKursyWaluta(d);
            daneDoWykresu = new List<DaneWykres>(WczytaneDane.KURSY_WALUTA[d]);
            LoadChartContents(daneDoWykresu);
        }

        //void MainChart_Loaded(object sender, RoutedEventArgs e)
        //{
        //    LoadChartContents();
        //}

        private void LoadChartContents(List<DaneWykres> wyniki)
        {
            Random rand = new Random();
            List<FinancialStuff> financialStuffList = new List<FinancialStuff>();
            var liczba = wyniki.Count;
            var step = Math.Round(liczba/10.0);
            System.Diagnostics.Debug.WriteLine("oto");
            System.Diagnostics.Debug.WriteLine(step);
            int i = 0;
            int z = 0;
            foreach (DaneWykres it in wyniki)
            {
                if (i == 0 || i == liczba)
                {
                    financialStuffList.Add(new FinancialStuff() {Name = z.ToString(), Amount = it.value});
                        //ladnaData(it.data), Amount = it.value });
                    System.Diagnostics.Debug.WriteLine("dodaj");
                }
                else
                {
                    financialStuffList.Add(new FinancialStuff() {Name = z.ToString(), Amount = it.value});
                }
                if (i == step) i = 0;
                i++;
                z++;
            }

            (LineChart.Series[0] as LineSeries).ItemsSource = financialStuffList;
        }

        private string ladnaData(string it)
        {
            //System.Diagnostics.Debug.WriteLine(it);
            //System.Diagnostics.Debug.WriteLine(it.Substring(5));
            //WSZYSTKIE_DATY.Add(it);
            string value = it.Substring(9) + "/" + it.Substring(7, 2) + "/20" + it.Substring(5, 2);
            //System.Diagnostics.Debug.WriteLine(value);
            return value;
        }

        private void LoadChartContents()
        {
            Random rand = new Random();
            List<FinancialStuff> financialStuffList = new List<FinancialStuff>();
            financialStuffList.Add(new FinancialStuff() {Name = "M", Amount = rand.Next(0, 200)});
            financialStuffList.Add(new FinancialStuff() {Name = "A", Amount = rand.Next(0, 200)});
            financialStuffList.Add(new FinancialStuff() {Name = "G", Amount = rand.Next(0, 200)});
            financialStuffList.Add(new FinancialStuff() {Name = "B", Amount = rand.Next(0, 200)});
            (LineChart.Series[0] as LineSeries).ItemsSource = financialStuffList;
            //(LineChart.Series[0] as ISeries)..AxisX Label  = "Times(s)";
            //LineChart.ActualAxes..Axes..ChartAreas[0].Add(area);
            //LineChart.ChartAreas["Default"].AxisY.LabelStyle.Format = "C";
            //LineChart.Series[0].itLegendItems.
            //var chart = new Chart();
            //chart.Series[0].ax.Points[0].AxisLabel = "First Point";
            //chart..ChartAreas.
            //(LineChart.Series[0] as LineSeries).Po
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var tag = e.Parameter as String;
            //System.Diagnostics.Debug.WriteLine("piekny tag");
            //System.Diagnostics.Debug.WriteLine(tag);
            loadData(tag);
        }
    }

    public class FinancialStuff
    {
        public string Name { get; set; }
        public float Amount { get; set; }
    }
}