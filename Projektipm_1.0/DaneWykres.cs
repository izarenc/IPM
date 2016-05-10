using System;

namespace Projektipm_1._0
{
    class DaneWykres
    {
        public float value { get; set; } //wartość
        public DateTime data { get; set; } //etykieta

        //public string Name { get; set; }
        //public float Amount { get; set; }

        public DaneWykres(float v, DateTime d)
        {
            value = v;
            data = d;
        }
    }
}