using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace Projektipm_1._0
{

    public sealed partial class Kursy : Page
    {
        public Kursy()
        {
            this.InitializeComponent();  
            funkcja();
            
        }

        public ObservableCollection<DataPro> datyKursow;

        private async void funkcja()
        {
            if (WczytaneDane.daty_kursow)
            {
                datyKursow = new ObservableCollection<DataPro>(WczytaneDane.DATY_KURSOW.Reverse());
                System.Diagnostics.Debug.WriteLine("if");
            }
            else
            {
                await WczytaneDane.wczytajDaneNaglowkow();
               
                System.Diagnostics.Debug.WriteLine("else");
                System.Diagnostics.Debug.WriteLine(WczytaneDane.DATY_KURSOW.Count());
                datyKursow = new ObservableCollection<DataPro>(WczytaneDane.DATY_KURSOW.Reverse());
            }
        }

        private void dalekoJeszcze(object sender, TappedRoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine((sender as TextBlock).Tag.ToString());
            Frame.Navigate(typeof(Root), (sender as TextBlock).Tag.ToString());

        }

        ////override
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    //this.parent = e.Parameter as MainPage;
        //    funkcja(); //??
        //}

    }
}
