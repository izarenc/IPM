using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Projektipm_1._0
{
    public sealed partial class Kursy : Page
    {
        public Kursy()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Funkcja();
        }

        private ObservableCollection<DataPro> DatyKursow = new ObservableCollection<DataPro>();

        private void Funkcja()
        {
            if (!WczytaneDane.daty_kursow)
            {
                WczytaneDane.wczytajDaneNaglowkow();

                System.Diagnostics.Debug.WriteLine("else");
                System.Diagnostics.Debug.WriteLine(WczytaneDane.DATY_KURSOW.Count);
            }
            foreach (var i in WczytaneDane.DATY_KURSOW.Reverse())
            {
                DatyKursow.Add(i);
            }
        }

        private void DalekoJeszcze(object sender, TappedRoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine((sender as TextBlock).Tag.ToString());
            var textBlock = sender as TextBlock;
            if (textBlock != null)
                Frame.Navigate(typeof(Root), textBlock.Tag.ToString());
        }

        ////override
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    //this.parent = e.Parameter as MainPage;
        //    funkcja(); //??
        //}
    }
}