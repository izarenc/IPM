using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektipm_1._0
{
    class Przechowalnia
    {

        private static Windows.Storage.ApplicationDataContainer LocalSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        private static bool init = false;

        public static void initialize()
        {
            if (!init)
            {
                init = true;
                LocalSettings.Values["RootData"] = "";
                LocalSettings.Values["WalutaTag"] = "";
                LocalSettings.Values["WalutaFrom"] = "";
                LocalSettings.Values["WalutaTo"] = "";
            }
        }

        public static void setRootData(string s)
        {
            LocalSettings.Values["RootData"] = s;
        }
        public static string getRootData()
        {
            return (string)LocalSettings.Values["RootData"];
        }

        public static void setWalutaTag(string s)
        {
            LocalSettings.Values["WalutaTag"] = s;
        }
        public static string getWalutaTag()
        {
            return (string)LocalSettings.Values["WalutaTag"];
        }

        public static void setWalutaFrom(DateTime s)
        {
            LocalSettings.Values["WalutaFrom"] = s.ToString("dd.MM.yyyy"); ;
        }
        public static DateTime getWalutaFrom()
        {
            //if (string.IsNullOrEmpty((string) LocalSettings.Values["WalutaFrom"])) return new DateTime(0001, 1, 1);
            return DateTime.Parse((string)LocalSettings.Values["WalutaFrom"]);
        }

        public static void setWalutaTo(DateTime s)
        {
            LocalSettings.Values["WalutaTo"] = s.ToString("dd.MM.yyyy");
        }
        public static DateTime getWalutaTo()
        {
            // if (string.IsNullOrEmpty((string)LocalSettings.Values["WalutaTo"])) return new DateTime(0001, 1, 1);
            return DateTime.Parse((string)LocalSettings.Values["WalutaTo"]);
        }
    }
}