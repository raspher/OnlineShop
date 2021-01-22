using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using OnlineShop.Tablice;

namespace OnlineShop
{
    public class AccDB : ObservableObject
    {
        public AccDB() {
            produkty = new List<Produkt>();
            adresy = new List<Adres>();
            konta = new List<Konto>();
            oceny = new List<Ocena>();
            role = new List<Rola>();
            transakcje = new List<Transakcja>();
            zamowienia = new List<Zamowienie>();

        }

        ~AccDB() {
            produkty.Clear();
            adresy.Clear();
            konta.Clear();
            oceny.Clear();
            role.Clear();
            transakcje.Clear();
            zamowienia.Clear();
        }

        // Konfiguracja połączenia z bazą danych 
        public OleDbConnection conn = new OleDbConnection(
                ConfigurationManager.ConnectionStrings["OnlineShop.Properties.Settings.OnlineShopConnectionString"].ConnectionString
                );

        // Tworzenie list obiektów (tablic)
        public List<Produkt> produkty { get; set; }
        public List<Adres> adresy { get; set; }
        public List<Ocena> oceny { get; set; }
        public List<Konto> konta { get; set; }
        public List<Rola> role { get; set; }
        public List<Transakcja> transakcje { get; set; }
        public List<Zamowienie> zamowienia { get; set; }

        // Ładowanie danych
        public void WczytajWszystkie()
        {
            
            WczytajAdresy();
            WczytajKonta();
            WczytajOceny();
            WczytajProdukty();
            WczytajRole();
            WczytajTransakcje();
            WczytajZamowienia();
        }

        public void WczytajWszystkieUzytkownika(Konto konto)
        {
            WczytajAdresyU(konto);
            WczytajKontoU(konto);
            WczytajOcenyU(konto);
            WczytajProdukty();
            WczytajTransakcjeU(konto);
            WczytajZamowieniaU(konto);
        }

        public void WczytajAdresy()
        {
            conn.Open();

            var zapytanie = "Select * from Adresy";
            OleDbCommand komenda = new OleDbCommand(zapytanie, conn);
            OleDbDataReader reader = komenda.ExecuteReader();

            adresy.Clear();
            while (reader.Read())
            {
                Adres adres = new Adres(
                    reader.GetInt32(0),     // id
                    reader.GetString(1),    // miejscowosc
                    reader.GetString(2),    // ulica
                    reader.GetString(3),    // dom
                    reader.GetString(4),    // kod pocztowy
                    reader.GetString(5),    // imie
                    reader.GetString(6),    // nazwisko
                    reader.GetInt32(7),     // telefon
                    reader.GetString(8)     // użytkownik
                    );
                adresy.Add(adres);
            }

            conn.Close();
        }

        public void WczytajAdresyU(Konto konto)
        {
            conn.Open();

            var zapytanie = $"Select * from Adresy WHERE Użytkownik='{konto.nazwa}'";
            OleDbCommand komenda = new OleDbCommand(zapytanie, conn);
            OleDbDataReader reader = komenda.ExecuteReader();

            adresy.Clear();
            while (reader.Read())
            {
                Adres adres = new Adres(
                    reader.GetInt32(0),     // id
                    reader.GetString(1),    // miejscowosc
                    reader.GetString(2),    // ulica
                    reader.GetString(3),    // dom
                    reader.GetString(4),    // kod pocztowy
                    reader.GetString(5),    // imie
                    reader.GetString(6),    // nazwisko
                    reader.GetInt32(7),     // telefon
                    reader.GetString(8)     // użytkownik
                    );
                adresy.Add(adres);
            }

            conn.Close();
        }

        public void WczytajKonta()
        {
            conn.Open();

            var zapytanie = "Select * from Konta";
            OleDbCommand komenda = new OleDbCommand(zapytanie, conn);
            OleDbDataReader reader = komenda.ExecuteReader();

            konta.Clear();
            while (reader.Read())
            {
                Konto konto = new Konto(
                    reader.GetString(0),    // uzytkownik
                    reader.GetString(1),    // email
                    reader.GetString(2),    // haslo
                    reader.GetString(3)     // rola
                    );
                konta.Add(konto);
            }

            conn.Close();
        }

        public void WczytajKontoU(Konto kontou)
        {
            conn.Open();

            var zapytanie = $"Select * from Konta WHERE Użytkownik='{kontou.nazwa}'";
            OleDbCommand komenda = new OleDbCommand(zapytanie, conn);
            OleDbDataReader reader = komenda.ExecuteReader();

            konta.Clear();
            while (reader.Read())
            {
                Konto konto = new Konto(
                    reader.GetString(0),    // uzytkownik
                    reader.GetString(1),    // email
                    reader.GetString(2),    // haslo
                    reader.GetString(3)     // rola
                    );
                konta.Add(konto);
            }

            conn.Close();
        }

        public void WczytajOceny()
        {
            conn.Open();

            var zapytanie = "Select * from Oceny";
            OleDbCommand komenda = new OleDbCommand(zapytanie, conn);
            OleDbDataReader reader = komenda.ExecuteReader();

            oceny.Clear();
            while (reader.Read())
            {
                Ocena ocena = new Ocena(
                    reader.GetInt32(0),     // id
                    reader.GetInt32(1),     // ocena
                    reader.GetString(2),    // komentarz
                    reader.GetString(3),    // uzytkownik
                    reader.GetString(4)     // produkt
                    );
                oceny.Add(ocena);
            }

            conn.Close();
        }

        public void WczytajOcenyU(Konto konto)
        {
            conn.Open();

            var zapytanie = $"Select * from Oceny Użytkownik='{konto.nazwa}'";
            OleDbCommand komenda = new OleDbCommand(zapytanie, conn);
            OleDbDataReader reader = komenda.ExecuteReader();

            oceny.Clear();
            while (reader.Read())
            {
                Ocena ocena = new Ocena(
                    reader.GetInt32(0),     // id
                    reader.GetInt32(1),     // ocena
                    reader.GetString(2),    // komentarz
                    reader.GetString(3),    // uzytkownik
                    reader.GetString(4)     // produkt
                    );
                oceny.Add(ocena);
            }

            conn.Close();
        }

        public void WczytajProdukty()
        {
            conn.Open();

            var zapytanie = "Select * from Produkty";
            OleDbCommand komenda = new OleDbCommand(zapytanie, conn);
            OleDbDataReader reader = komenda.ExecuteReader();

            produkty.Clear();
            while (reader.Read())
            {
                Produkt produkt = new Produkt(
                    reader.GetString(0),    // sku
                    reader.GetString(1),    // ean
                    reader.GetString(2),    // nazwa
                    reader.GetString(3),    // opis
                    reader.GetDecimal(4),     // cena
                    reader.GetInt32(5)      // ilosc
                    );
                produkty.Add(produkt);
            }

            conn.Close();
        }

        public void WczytajRole()
        {
            conn.Open();

            var zapytanie = "Select * from Role";
            OleDbCommand komenda = new OleDbCommand(zapytanie, conn);
            OleDbDataReader reader = komenda.ExecuteReader();

            role.Clear();
            while (reader.Read())
            {
                Rola rola = new Rola(
                    reader.GetString(0),        // rola
                    reader.GetBoolean(1),       // adresy odczyt
                    reader.GetBoolean(2),       // adresy zapis
                    reader.GetBoolean(3),       // konta odczyt
                    reader.GetBoolean(4),       // konta zapis
                    reader.GetBoolean(5),       // oceny odczyt
                    reader.GetBoolean(6),       // oceny zapis
                    reader.GetBoolean(7),       // produkty odczyt
                    reader.GetBoolean(8),       // produkty zapis
                    reader.GetBoolean(9),       // role odczyt
                    reader.GetBoolean(10),      // role zapis
                    reader.GetBoolean(11),      // transakcje odczyt
                    reader.GetBoolean(12),      // transakcje zapis
                    reader.GetBoolean(13),      // zamowienia odczyt
                    reader.GetBoolean(14)       // zamowienia zapis
                    );
                role.Add(rola);
            }

            conn.Close();
        }

        public void WczytajRole(Konto konto)
        {
            conn.Open();

            var zapytanie = $"Select * from Role WHERE Rola='{konto.rola}'";
            OleDbCommand komenda = new OleDbCommand(zapytanie, conn);
            OleDbDataReader reader = komenda.ExecuteReader();

            role.Clear();
            while (reader.Read())
            {
                Rola rola = new Rola(
                    reader.GetString(0),        // rola
                    reader.GetBoolean(1),       // adresy odczyt
                    reader.GetBoolean(2),       // adresy zapis
                    reader.GetBoolean(3),       // konta odczyt
                    reader.GetBoolean(4),       // konta zapis
                    reader.GetBoolean(5),       // oceny odczyt
                    reader.GetBoolean(6),       // oceny zapis
                    reader.GetBoolean(7),       // produkty odczyt
                    reader.GetBoolean(8),       // produkty zapis
                    reader.GetBoolean(9),       // role odczyt
                    reader.GetBoolean(10),      // role zapis
                    reader.GetBoolean(11),      // transakcje odczyt
                    reader.GetBoolean(12),      // transakcje zapis
                    reader.GetBoolean(13),      // zamowienia odczyt
                    reader.GetBoolean(14)       // zamowienia zapis
                    );
                role.Add(rola);
            }

            conn.Close();
        }

        public void WczytajTransakcje()
        {
            conn.Open();

            var zapytanie = "Select * from Transakcje";
            OleDbCommand komenda = new OleDbCommand(zapytanie, conn);
            OleDbDataReader reader = komenda.ExecuteReader();

            transakcje.Clear();
            while (reader.Read())
            {
                Transakcja transakcja = new Transakcja(
                    reader.GetString(0),     // paragon
                    reader.GetString(1),    // uzytkownik  
                    reader.GetFloat(2)    // kwota
                    );
                transakcje.Add(transakcja);
            }

            conn.Close();
        }

        public void WczytajTransakcjeU(Konto konto)
        {
            conn.Open();

            var zapytanie = $"Select * from Transakcje WHERE Użytkownik='{konto.nazwa}'";
            OleDbCommand komenda = new OleDbCommand(zapytanie, conn);
            OleDbDataReader reader = komenda.ExecuteReader();

            transakcje.Clear();
            while (reader.Read())
            {
                Transakcja transakcja = new Transakcja(
                    reader.GetString(0),     // paragon
                    reader.GetString(1),    // uzytkownik  
                    reader.GetFloat(2)    // kwota
                    );
                transakcje.Add(transakcja);
            }

            conn.Close();
        }

        public void WczytajZamowienia()
        {
            conn.Open();

            var zapytanie = "Select * from Zamówienia";
            OleDbCommand komenda = new OleDbCommand(zapytanie, conn);
            OleDbDataReader reader = komenda.ExecuteReader();

            zamowienia.Clear();
            while (reader.Read())
            {
                Zamowienie zamowienie = new Zamowienie(
                    reader.GetInt32(0),     // id
                    reader.GetString(1),    // paragon
                    reader.GetString(2),    // produkt
                    reader.GetInt32(3)      // adres
                    );
                zamowienia.Add(zamowienie);
            }

            conn.Close();
        }

        public void WczytajZamowieniaU(Konto konto)
        {
            conn.Open();

            var zapytanie = $"Select * from Zamówienia WHERE Użytkownik='{konto.nazwa}'";
            OleDbCommand komenda = new OleDbCommand(zapytanie, conn);
            OleDbDataReader reader = komenda.ExecuteReader();

            zamowienia.Clear();
            while (reader.Read())
            {
                Zamowienie zamowienie = new Zamowienie(
                    reader.GetInt32(0),     // id
                    reader.GetString(1),    // paragon
                    reader.GetString(2),    // produkt
                    reader.GetInt32(3)      // adres
                    );
                zamowienia.Add(zamowienie);
            }

            conn.Close();
        }

    }
}
