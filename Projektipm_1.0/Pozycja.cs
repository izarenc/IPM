using System;

namespace Projektipm_1._0
{
    public class Pozycja
    {
        public string nazwa;
        public float przelicznik;
        public string kod;
        public float kurs;
        public float kurs_oryginalny;
        //public DateTime data;

        public Pozycja(string a, float b, string c, float d)
        {
            nazwa = a;
            przelicznik = b;
            kod = c;
            kurs = d*b;
            kurs_oryginalny = d;
        }
    }
}