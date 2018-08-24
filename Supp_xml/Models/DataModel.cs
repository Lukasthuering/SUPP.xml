using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp_xml.Models
{
    public class DataModel
    {
        private string text;
        private DateTime datumZeit;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public DateTime DatumZeit
        {
            get { return datumZeit; }
            set { datumZeit = value; }
        }

    }
}
