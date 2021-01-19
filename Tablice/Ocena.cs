using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Tablice
{
    public class Ocena : ObservableObject
    {
        public int id { get; set; }
        public int ocena { get; set; }
        public string komentarz { get; set; }
        public string uzytkownik { get; set; }
        public string produkt { get; set; }

        public Ocena(
            int _id,
            int _ocena,
            string _komentarz,
            string _uzytkownik,
            string _produkt
            )
        {
            this.id = _id;
            this.ocena = _ocena;
            this.komentarz = _komentarz;
            this.uzytkownik = _uzytkownik;
            this.produkt = _produkt;
        }

    }
}
