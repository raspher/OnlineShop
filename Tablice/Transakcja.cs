using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Tablice
{
    public class Transakcja : ObservableObject
    {
        public string numer_paragonu { get; set; }
        public string uzytkownik { get; set; }
        public float kwota { get; set; }

        public Transakcja(
            string _nr_paragonu,
            string _uzytkownik,
            float _kwota
            )
        {
            this.numer_paragonu = _nr_paragonu;
            this.uzytkownik = _uzytkownik;
            this.kwota = _kwota;
        }
    }
}
