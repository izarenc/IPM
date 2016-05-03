

using System;
using System.ComponentModel;

namespace Projektipm_1._0
{
    public class DataPro : INotifyPropertyChanged
    {
        public string ladna_data; //ładna data
        public string index_data; //raw path xml
        public DateTime data_data;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
            System.Diagnostics.Debug.WriteLine("changed!!!!!!!!");
        }

        public DataPro(string d, string i, DateTime r)
        {
            ladna_data = d;
            index_data = i;
            data_data = r;
        }
    }
}