using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Tablice
{
    public class Transakcja : ObservableObject
    {
        string numer_paragonu { get; set; }
        string uzytkownik { get; set; }
        float kwota { get; set; }

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
