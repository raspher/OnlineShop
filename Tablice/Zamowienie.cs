using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Tablice
{
    public class Zamowienie : ObservableObject
    {
        public int id { get; set; }
        public string paragon { get; set; }
        public string produkt { get; set; }
        public int adres { get; set; }

        public Zamowienie(
            int _id,
            string _paragon,
            string _produkt,
            int _adres
            )
        {
            this.id = _id;
            this.paragon = _paragon;
            this.produkt = _produkt;
            this.adres = _adres;
            
        }
    }
}
