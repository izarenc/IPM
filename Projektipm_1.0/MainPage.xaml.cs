using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Projektipm_1._0
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            funkcja();
        }

        private async void funkcja()
        {
            string page = "http://www.nbp.pl/kursy/xml/dir.txt";

            // ... Use HttpClient.
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                // ... Read the string.
                string result = await content.ReadAsStringAsync();
                List<string> nazwyPlikow = new List<string>();

                //if (result != null && result.Length >= 50)
                //{
                //    nazwyPlikow.Add(result);
                //    System.Diagnostics.Debug.WriteLine(result);
                //}
                //System.Diagnostics.Debug.WriteLine(nazwyPlikow.Count);
                //result = result.Substring(0, 1000);
                string[] words = result.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);    //splitting at white character
                System.Diagnostics.Debug.WriteLine(result);
                foreach(string it in words)
                {
                    //System.Diagnostics.Debug.WriteLine(it[0]);
                    //System.Diagnostics.Debug.WriteLine('a'.Equals(it[0]));
                    if ('a'.Equals(it[0]) ){
                        System.Diagnostics.Debug.WriteLine(it);
                        nazwyPlikow.Add(it);
                    }
                }
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding iso_8859_2 = Encoding.GetEncoding("ISO-8859-2");
                System.Diagnostics.Debug.WriteLine(nazwyPlikow.Count);
                string path = "http://www.nbp.pl/kursy/xml/LastA.xml";
                HttpClient client2 = new HttpClient();
                HttpResponseMessage response2 = await client.GetAsync(path);
                HttpContent content2 = response2.Content;
                {
                    var byteData = await client.GetByteArrayAsync(path);
                    string data = iso_8859_2.GetString(byteData);

                    //string result2 = iso_8859_2.GetBytes(await content2.ReadAsByteArrayAsync()).ToString();
                    //string result2 = await content2.ReadAsStringAsync();

                    XDocument loadedData = XDocument.Load(data);
                    
                }
            }
        }
    }
}//Windows 10 Universal Apps - Sliding menu tutorial in UWP
