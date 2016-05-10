using System;

namespace Projektipm_1._0
{
    //Klasa przechowująca informacje o rekordzie odpowiadającym określonej dacie
    public class DataPro
    {
        public string LadnaData; //data w ładnym formacie
        public string IndexData; //raw path of file in xml
        public DateTime DataData;//data w formacie DataTime

        public DataPro(string d, string i, DateTime r)
        {
            LadnaData = d;
            IndexData = i;
            DataData = r;
        }
    }
}