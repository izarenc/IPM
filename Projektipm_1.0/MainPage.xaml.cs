using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Projektipm_1._0
{
    public sealed partial class MainPage : Page
    {
       
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
            //funkcja();
        }

        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            container.Navigate(typeof(Root), this);
        }

        private void btnWaluta_Click(object sender, RoutedEventArgs e)
        {
            container.Navigate(typeof(Waluta), this);
        }

        private void btnKursy_Click(object sender, RoutedEventArgs e)
        {
            container.Navigate(typeof(Kursy), this);
        }

        private void btnHistoria_Click(object sender, RoutedEventArgs e)
        {
            container.Navigate(typeof(Root),this);
        }

        private void btnShowPane_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
            if (!MySplitView.IsPaneOpen)
            {
                //ZAMKNIETE
                //MySplitView.IsPaneOpen = true;
                btnShowPane.Content = "\uE8A9";
                btnShowPane.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                //OTWARTE
                //MySplitView.IsPaneOpen = false;
                btnShowPane.Content = "\uE00E";
                btnShowPane.HorizontalAlignment = HorizontalAlignment.Center;
            }
        }

        private void btnPobierz_Click(object sender, RoutedEventArgs e)
        {
            WczytaneDane.wczytajWszystkieDane();
        }
    }

    public class AnotherPagePayload
    {
        public MainPage parameter1 { get; set; }
        public string parameter2 { get; set; }
    }

}
