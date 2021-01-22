using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Tablice
{
    public class Konto : ObservableObject
    {
        public string nazwa { get; set; }
        public string haslo { get; set; }
        public string email { get; set; }
        public string rola { get; set; }
        

        public Konto(
            string _nazwa,
            string _email,
            string _haslo,
            string _rola
            )
        {
            this.nazwa = _nazwa;
            this.email = _email;
            this.haslo = _haslo;
            this.rola = _rola;
        }
    }
}
