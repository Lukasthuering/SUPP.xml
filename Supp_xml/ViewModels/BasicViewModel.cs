using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp_xml.ViewModels
{
    public class BasicViewModel : INotifyPropertyChanged
    {
        private string commandParameter;
        public event PropertyChangedEventHandler PropertyChanged;

        public string CommandParameter
        {
            get { return commandParameter; }
            set { commandParameter = value; }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
 
    }
}
