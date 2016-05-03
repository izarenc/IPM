using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Threading;

namespace Projektipm_1._0
{
    class WczytaneDane
    {
        public static Boolean daty_kursow = false;
        public static Boolean kursy = false;

        public static ObservableCollection<DataPro> DATY_KURSOW = new ObservableCollection<DataPro>();
            //zbindowane w xaml: ładna data + indeks + data data4

        public static Dictionary<DateTime, ObservableCollection<Pozycja>> KURSY_DATA =
            new Dictionary<DateTime, ObservableCollection<Pozycja>>();

        public static Dictionary<string, List<DaneWykres>> KURSY_WALUTA = new Dictionary<string, List<DaneWykres>>();

        public static async void wczytajWszystkieDane()
        {
            if (!daty_kursow) await wczytajDaneNaglowkow();
            if (!kursy) await wczytajKursy();
        }

        public static async Task wczytajDaneNaglowkow()
        {
            System.Diagnostics.Debug.WriteLine("Wczytuje naglowki!");
            daty_kursow = true;
            string page = "http://www.nbp.pl/kursy/xml/dir.txt";

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();

                string[] words = result.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                //splitting at white character
                foreach (string it in words)
                {
                    if ('a'.Equals(it[0]))
                    {
                        DATY_KURSOW.Add(
                            new DataPro(it.Substring(9) + "/" + it.Substring(7, 2) + "/20" + it.Substring(5, 2), it,
                                DateTime.Parse(it.Substring(9) + "." + it.Substring(7, 2) + ".20" + it.Substring(5, 2))));
                    }
                }
                //DATY_KURSOW[0].NotifyPropertyChanged("ladna_data");
            }
            System.Diagnostics.Debug.WriteLine("Wczytano nagłówki plików xml");
        }

        public static async Task wczytajKursy()
        {
            System.Diagnostics.Debug.WriteLine("Wczytuje kursy!");
            kursy = true;

            foreach (var it in WczytaneDane.DATY_KURSOW)
            {
                string page = "http://www.nbp.pl/kursy/xml/" + it.index_data + ".xml";

                using (HttpClient client2 = new HttpClient())

                {
                    var byteData = await client2.GetByteArrayAsync(page);
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    Encoding iso_8859_2 = Encoding.GetEncoding(1252); //"ISO-8859-2");
                    string data = iso_8859_2.GetString(byteData);
                    XDocument loadedData = XDocument.Parse(data);
                    var a = loadedData.Descendants("pozycja").Elements();

                    IEnumerable<InputData> dane = from query in loadedData.Descendants("pozycja")
                        //where (string)query.Element("kod_waluty") == d
                        select new InputData((string) query.Element("kod_waluty"), it.data_data,
                            float.Parse(query.Element("kurs_sredni").Value.Replace(",", ".")),
                            query.Element("nazwa_waluty").Value, float.Parse(query.Element("przelicznik").Value));

                    foreach (InputData item in dane)
                    {
                        if (!KURSY_WALUTA.ContainsKey(item.index)) KURSY_WALUTA.Add(item.index, new List<DaneWykres>());

                        KURSY_WALUTA[item.index].Add(new DaneWykres(item.value, item.data));

                        if (!KURSY_DATA.ContainsKey(item.data))
                            KURSY_DATA.Add(item.data, new ObservableCollection<Pozycja>());

                        KURSY_DATA[item.data].Add(new Pozycja(item.nazwa, item.przelicznik, item.index, item.value));
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("Wczytano wszystkie dane!");
        }

        public static async Task wczytajKursData(string adr)
        {
            DateTime datunia = DateTime.Parse(adr.Substring(9) + "." + adr.Substring(7, 2) + ".20" + adr.Substring(5, 2));

            if (KURSY_DATA.ContainsKey(datunia)) return;
            KURSY_DATA.Add(datunia, new ObservableCollection<Pozycja>());

            System.Diagnostics.Debug.WriteLine("Wczytuje kurs data" + adr);

            string page = "http://www.nbp.pl/kursy/xml/" + adr + ".xml";

            using (HttpClient client2 = new HttpClient())

            {
                var byteData = await client2.GetByteArrayAsync(page);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding iso_8859_2 = Encoding.GetEncoding(1252); //"ISO-8859-2");
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
                KURSY_DATA[datunia] = new ObservableCollection<Pozycja>(dane);
                //foreach (Pozycja item in dane)
                //{
                //    KURSY_DATA[datunia].Add(item);
                //}
            }
            System.Diagnostics.Debug.WriteLine("Wczytano kurs");
        }

        public static async Task wczytajKursyWaluta(string adr)
        {
            if (KURSY_WALUTA.ContainsKey(adr)) return;
            KURSY_WALUTA.Add(adr, new List<DaneWykres>());

            System.Diagnostics.Debug.WriteLine("Wczytuje kurs waluta" + adr.ToString());
            if (!daty_kursow) await wczytajDaneNaglowkow();
            foreach (var it in WczytaneDane.DATY_KURSOW)
            {
                string page = "http://www.nbp.pl/kursy/xml/" + it.index_data + ".xml";

                using (HttpClient client2 = new HttpClient())

                {
                    var byteData = await client2.GetByteArrayAsync(page);
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    Encoding iso_8859_2 = Encoding.GetEncoding(1252); //"ISO-8859-2");
                    string data = iso_8859_2.GetString(byteData);
                    XDocument loadedData = XDocument.Parse(data);
                    var a = loadedData.Descendants("pozycja").Elements();

                    IEnumerable<DaneWykres> dane = from query in loadedData.Descendants("pozycja")
                        where (string) query.Element("kod_waluty") == adr
                        select
                            new DaneWykres(float.Parse(query.Element("kurs_sredni").Value.Replace(",", ".")),
                                it.data_data);

                    KURSY_WALUTA[adr] = new List<DaneWykres>(dane);
                }
            }
            System.Diagnostics.Debug.WriteLine("Wczytano kurs!");
        }

        public static string mojaPierwszaFunkcjaWCudownymJezyku(string s)
        {
            return s.Remove(1).ToUpper() + s.Substring(1);
        }
    }


    public class InputData
    {
        public string index;
        public DateTime data;
        public float value;
        public string nazwa;
        public float przelicznik;

        public InputData(string i, DateTime d, float v, string n, float p)
        {
            index = i;
            data = d;
            value = v;
            nazwa = n;
            przelicznik = p;
        }
    }
}