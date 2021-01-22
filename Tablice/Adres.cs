using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Tablice
{
    public class Adres : ObservableObject
    {
        public int id { get; set; }
        public string kodPocztowy { get; set; }
        public string miejscowosc { get; set; }
        public string ulica { get; set; }
        public string dom { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public int telefon { get; set; }
        public string uzytkownik { get; set; }

        public Adres(
            int _id,
            string _miejscowosc,
            string _ulica,
            string _dom,
            string _kodPocztowy,
            string _imie,
            string _nazwisko,
            int _telefon,
            string _uzytkownik
            )
        {
            this.id = _id;
            this.kodPocztowy = _kodPocztowy;
            this.miejscowosc = _miejscowosc;
            this.ulica = _ulica;
            this.dom = _dom;
            this.imie = _imie;
            this.nazwisko = _nazwisko;
            this.telefon = _telefon;
            this.uzytkownik = _uzytkownik;
        }
    }
}
