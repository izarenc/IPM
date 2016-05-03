using System;

namespace Projektipm_1._0
{
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
