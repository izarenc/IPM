

using System;
using System.ComponentModel;

namespace Projektipm_1._0
{
    public class DataPro
    {
        public string ladna_data; //ładna data
        public string index_data; //raw path xml
        public DateTime data_data;


        public DataPro(string d, string i, DateTime r)
        {
            ladna_data = d;
            index_data = i;
            data_data = r;
        }
    }
}