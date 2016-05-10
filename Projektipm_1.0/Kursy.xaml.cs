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
            }
            DatyKursow = new ObservableCollection<DataPro>(WczytaneDane.DATY_KURSOW.Reverse());
        }

        private void TappedDateHandler(object sender, TappedRoutedEventArgs e)
        {
            var textBlock = sender as TextBlock;
            if (textBlock != null)
                Frame.Navigate(typeof(Root), textBlock.Tag.ToString());
        }
    }
}