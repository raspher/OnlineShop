using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Tablice
{
    public class Adres : ObservableObject
    {
        int id { get; set; }
        string kodPocztowy { get; set; }
        string miejscowosc { get; set; }
        string ulica { get; set; }
        string dom { get; set; }
        string imie { get; set; }
        string nazwisko { get; set; }
        int telefon { get; set; }
        string uzytkownik { get; set; }

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
