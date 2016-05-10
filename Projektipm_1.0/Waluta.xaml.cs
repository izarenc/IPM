using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Syncfusion.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace Projektipm_1._0
{
    public partial class Waluta : Page
    {
        private string aktualnyKurs = "EUR";

        public Waluta()
        {
            InitializeComponent();
        }

        public async void loadData(string d)
        {
            BiezacaWaluta.Text = "Historia waluty " + (string)d;
            await WczytaneDane.wczytajKursyWaluta(d);
            //LoadChartContents(WczytaneDane.KURSY_WALUTA[d]);
            (LineChart.Series[0] as LineSeries).ItemsSource = WczytaneDane.KURSY_WALUTA[d];
        }

        private void LoadChartContents(DateTime f, DateTime t )
        {
            List<DaneWykres> temp = new List<DaneWykres>();
            foreach (DaneWykres it in WczytaneDane.KURSY_WALUTA[aktualnyKurs])
            {
                if (f <= it.data & it.data <= t)
                {
                    temp.Add(it); 
                }  
            }
            (LineChart.Series[0] as LineSeries).ItemsSource = temp;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            var adr = e.Parameter as string;
            System.Diagnostics.Debug.WriteLine("parametr: " + adr);
            if (string.IsNullOrEmpty(adr))
            {
                loadData("EUR");
            }
            else
            {
                WczytaneDane.wczytajKursData(adr);
                loadData(adr);
                aktualnyKurs = adr;
            }

        }

        private void CalendarHandler(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine(CalendarFrom.Date+"paramet" + CalendarTo.Date);
            if (CalendarFrom.Date.Equals(null) | CalendarTo.Date.Equals(null)) return;
            if (CalendarFrom.Date >= CalendarTo.Date)
            {
                MessageText.Text = "Data początkowa musi być wcześniejsza niż końcowa";
                return;
            }
            DateTime s = CalendarFrom.Date.ToDateTime();
            TimeSpan ts = new TimeSpan(0, 0, 0);
            s = s.Date + ts;

            foreach (DataPro it in WczytaneDane.DATY_KURSOW)
            {
                if (it.data_data.Equals(s)) goto spelnionyWarunek1;
            }
            MessageText.Text = "Taka data początkowa nie została wczytana";
            return;

            spelnionyWarunek1:

            s = CalendarTo.Date.ToDateTime();
            s = s.Date + ts;

            foreach (DataPro it in WczytaneDane.DATY_KURSOW)
            {
                if (it.data_data.Equals(s)) goto spelnionyWarunek2;
            }
            MessageText.Text = "Taka data końcowa nie została wczytana";
            return;

            spelnionyWarunek2:

            MessageText.Text = "";
            System.Diagnostics.Debug.WriteLine("Brawo, wybrałeś poprawne daty");
            LoadChartContents(CalendarFrom.Date.ToDateTime(), CalendarTo.Date.ToDateTime());

        }
    }

}