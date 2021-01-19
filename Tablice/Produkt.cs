using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Tablice
{
    public class Produkt : ObservableObject
    {
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public decimal Cena { get; set; }
        public int Ilosc { get; set; }
        public string EAN { get; set; }
        public string SKU { get; set; }

        public Produkt(
            string _SKU,
            string _EAN,
            string _nazwa, 
            string _opis, 
            decimal _cena,
            int _ilosc
            )
        {
            this.SKU = _SKU;
            this.EAN = _EAN;
            this.Nazwa = _nazwa;
            this.Opis = _opis;
            this.Cena = _cena;
            this.Ilosc = _ilosc;
        }
    }
}
