using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;


namespace Projektipm_1._0
{
    public sealed partial class Root : Page
    {
        public ObservableCollection<Pozycja> pozycje = new ObservableCollection<Pozycja>();

        public Root()
        {
            this.InitializeComponent();
            funkcja();
        }

        public static string mojaPierwszaFunkcjaWCudownymJezyku(string s)
        {
            return s.Remove(1).ToUpper() + s.Substring(1);
        }

        private async void funkcja()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string path = "http://www.nbp.pl/kursy/xml/LastA.xml";
            HttpClient client = new HttpClient();
            HttpResponseMessage response2 = await client.GetAsync(path);
            HttpContent content2 = response2.Content;
            {
                var byteData = await client.GetByteArrayAsync(path);
                Encoding iso_8859_2 = Encoding.GetEncoding("ISO-8859-2");
                string data = iso_8859_2.GetString(byteData);
                XDocument loadedData = XDocument.Parse(data);
                var a = loadedData.Descendants("pozycja").Elements();

                IEnumerable<Pozycja> dane = from query in loadedData.Descendants("pozycja")
                    select new Pozycja(
                        mojaPierwszaFunkcjaWCudownymJezyku(query.Element("nazwa_waluty").Value),
                        float.Parse(query.Element("przelicznik").Value),
                        query.Element("kod_waluty").Value,
                        float.Parse(query.Element("kurs_sredni").Value.Replace(",", "."))
                        );
                pozycje = new ObservableCollection<Pozycja>(dane);
                //var temp=new Pozycja("a", 0.5F, "b", 8.0F);
                //pozycje.Add(temp);
                //pozycje.Remove(temp);
            }
        }

        private async void funkcja(string adr)
        {
            DateTime datunia = DateTime.Parse(adr.Substring(9) + "." + adr.Substring(7, 2) + ".20" + adr.Substring(5, 2));
            WczytaneDane.wczytajKursData(adr);
            pozycje = WczytaneDane.KURSY_DATA[datunia];

            //var temp = new Pozycja("a", 0.5F, "b", 8.0F);
            //pozycje.Add(temp);
            //pozycje.Remove(temp);
            // wczytaj kursy
        }


        private void NieWiemCoRobie(object sender, TappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("dzialaj!!");
            System.Diagnostics.Debug.WriteLine((sender as TextBlock).Tag.ToString());
            Frame.Navigate(typeof(Waluta), (sender as TextBlock).Tag.ToString());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var adr = e.Parameter as string;
            System.Diagnostics.Debug.WriteLine("parametr: " + adr);
            if (string.IsNullOrEmpty(adr))
            {
                funkcja();
            }
            else
            {
                WczytaneDane.wczytajKursData(adr);
                funkcja(adr);
            }
        }
    }
}