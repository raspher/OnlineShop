using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Tablice
{
    public class Rola
    {
        public string rola { get; set; }
        public bool adresy_o { get; set; }
        public bool adresy_z { get; set; }
        public bool konta_o { get; set; }
        public bool konta_z { get; set; }
        public bool oceny_o { get; set; }
        public bool oceny_z { get; set; }
        public bool produkty_o { get; set; }
        public bool produkty_z { get; set; }
        public bool role_o { get; set; }
        public bool role_z { get; set; }
        public bool transakcje_o { get; set; }
        public bool transakcje_z { get; set; }
        public bool zamowienia_o { get; set; }
        public bool zamowienia_z { get; set; }

        public Rola(
            string _rola,
            bool _adresy_o,
            bool _adresy_z,
            bool _konta_o,
            bool _konta_z,
            bool _oceny_o,
            bool _oceny_z,
            bool _produkty_o,
            bool _produkty_z,
            bool _role_o,
            bool _role_z,
            bool _transakcje_o,
            bool _transakcje_z,
            bool _zamowienia_o,
            bool _zamowienia_z
            )
        {
            this.rola = _rola;
            this.adresy_o = _adresy_o;
            this.adresy_z = _adresy_z;
            this.konta_o = _konta_o;
            this.konta_z = _konta_z;
            this.oceny_o = _oceny_o;
            this.oceny_z = _oceny_z;
            this.produkty_o = _produkty_o;
            this.produkty_z = _produkty_z;
            this.role_o = _role_o;
            this.role_z = _role_z;
            this.transakcje_o = _transakcje_o;
            this.transakcje_z = _transakcje_z;
            this.zamowienia_o = _zamowienia_o;
            this.zamowienia_z = _zamowienia_z;
        }

    }
}
