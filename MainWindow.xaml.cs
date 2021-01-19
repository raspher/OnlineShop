using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OleDb;

namespace OnlineShop
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AccDB baza = new AccDB();
        public Tablice.Konto aktualnyUzytkownik;

        public MainWindow()
        {
            InitializeComponent();

            dgAdresy.ItemsSource = baza.adresy;
            dgKonta.ItemsSource = baza.konta;
            dgOceny.ItemsSource = baza.oceny;
            dgProdukty.ItemsSource = baza.produkty;
            dgRole.ItemsSource = baza.role;
            dgTransakcje.ItemsSource = baza.transakcje;
            dgZamowienia.ItemsSource = baza.zamowienia;

            /*
            Baza.Visibility = Visibility.Hidden;
            
            ListaProduktow.DataContext = OnlineShop.DBConn.Read("SELECT * FROM Produkty", "Produkty");
            
            ListaTransakcji.DataContext = OnlineShop.DBConn.Read(
                "SELECT Transakcje.ID AS ID, Konta.Nazwa AS Nazwa_Konta, Produkty.Nazwa_Produktu AS Produkt, Transakcje.Numer_Paragonu AS Numer_Paragonu, Transakcje.Kwota AS Kwota " +
                "FROM Produkty INNER JOIN((Konta INNER JOIN Transakcje ON Konta.[ID] = Transakcje.[ID_Konta]) " +
                "INNER JOIN Zamówienia ON Transakcje.[ID] = Zamówienia.[ID_Transakcji]) ON Produkty.[ID] = Zamówienia.[ID_Produktu]; ", "Transakcje");

            ListaAdresy.DataContext = OnlineShop.DBConn.Read(
                "SELECT ID, Kod_Pocztowy AS KP, Miejscowość AS MIASTO, Ulica, Dom_Mieszkanie AS DOM, Imie, Nazwisko, Telefon FROM Adresy", "Adresy");

            ListaKonta.DataContext = OnlineShop.DBConn.Read(
                "SELECT Konta.ID AS ID, Konta.Hasło AS Hasło, Konta.Email AS Email, Konta.Nazwa AS Nazwa, Role.Nazwa AS Rola " +
                "FROM Role INNER JOIN Konta ON Role.[ID] = Konta.[ID_Rola]", "Konta");

            ListaRole.DataContext = OnlineShop.DBConn.Read("SELECT * FROM Role", "Role");

            ListaOceny.DataContext = OnlineShop.DBConn.Read(
                "SELECT Oceny.ID AS ID, Oceny.Ocena AS Ocena, Oceny.Komentarz AS Komentarz, Produkty.Nazwa_Produktu AS Produkt, Konta.Nazwa AS Konto " +
                "FROM Produkty INNER JOIN(Konta INNER JOIN Oceny ON Konta.[ID] = Oceny.[ID_Użytkownika]) " +
                "ON Produkty.[ID] = Oceny.[ID_Produktu]; ", "Oceny");

            ListaZamowienia.DataContext = OnlineShop.DBConn.Read(
                "SELECT Zamówienia.ID AS ID, Zamówienia.ID_Transakcji AS ID_TR, Produkty.Nazwa_Produktu AS P_Nazwa, Produkty.Ilość AS Ilość, " +
                "Produkty.EAN AS EAN, Produkty.SKU AS SKU " +
                "FROM Produkty INNER JOIN Zamówienia ON Produkty.[ID] = Zamówienia.[ID_Produktu];", "Zamówienia");
            */


        }

        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {            
            foreach(var konto in baza.konta)
            {
                if(konto.nazwa == Login.Text)
                {
                    if(konto.haslo == Haslo.Text)
                    {
                        aktualnyUzytkownik = konto;
                        Tablice.Rola rola = baza.role.Find(x => x.rola == aktualnyUzytkownik.rola);

                        // ładuj uprawnienia
                        //odczyt
                        TabAdresy.Visibility = rola.adresy_o ? Visibility.Visible : Visibility.Hidden;
                        TabKonta.Visibility = rola.konta_o ? Visibility.Visible : Visibility.Hidden;
                        TabOceny.Visibility = rola.oceny_o ? Visibility.Visible : Visibility.Hidden;
                        TabProdukty.Visibility = rola.produkty_o ? Visibility.Visible : Visibility.Hidden;
                        TabRole.Visibility = rola.role_o ? Visibility.Visible : Visibility.Hidden;
                        TabTransakcje.Visibility = rola.transakcje_o ? Visibility.Visible : Visibility.Hidden;
                        TabZamowienia.Visibility = rola.zamowienia_o ? Visibility.Visible : Visibility.Hidden;

                        // TODO: zapis - nazwij i wyłącz guziki

                        // TODO: w guzikach trzeba sprawdzić czy zalogowany to klient 
                        // -> if (aktualnyUzytkownik.rola == "Klient")

                        // TODO: zapis - użytkownik nie może zmienić swojej nazwy itd...

                        // TODO: wczytaj odpowiednie tabele
                        if (rola.rola == "Klient")
                            baza.WczytajWszystkieUzytkownika(konto);
                        

                        Baza.Visibility = Visibility.Visible;
                        return;
                    }
                }
            }

            MessageBox.Show("Nie udało się zalogować!");
            
        }

        private void Button_Click_Dodaj_Produkt(object sender, RoutedEventArgs e)
        {
            var cmd = new OleDbCommand(
                $"INSERT INTO Produkty (SKU, EAN, Nazwa, Opis, Cena, Ilość) " +
                $"VALUES ('{PSKU.Text}', '{PEAN.Text}', '{PNazwa.Text}', '{POpis.Text}', " +
                $"{decimal.Parse(PCena.Text)}, {int.Parse(PIlosc.Text)});",
                baza.conn);

            baza.conn.Open();
            cmd.ExecuteNonQuery();
            baza.conn.Close();
            baza.WczytajWszystkie();
            dgProdukty.Items.Refresh();
        }

        private void Button_Click_Aktualizuj_Produkt(object sender, RoutedEventArgs e)
        {
            var cmd = new OleDbCommand(
                $"UPDATE Produkty SET SKU='{PSKU.Text}', EAN='{PEAN.Text}', Nazwa='{PNazwa.Text}', " +
                $"Opis='{POpis.Text}', Cena={decimal.Parse(PCena.Text)}, Ilość={int.Parse(PIlosc.Text)} " +
                $"WHERE SKU='{PSKU.Text}';",
                baza.conn);

            baza.conn.Open();
            cmd.ExecuteNonQuery();
            baza.conn.Close();
            baza.WczytajWszystkie();
            dgProdukty.Items.Refresh();
        }

        private void Button_Click_Usun_Produkt(object sender, RoutedEventArgs e)
        {
            int test = baza.oceny.FindIndex(x => x.produkt == PSKU.Text);
            if (test >= 0)
            {
                MessageBox.Show($"Produkt jest połączony relacją z oceną o id: {test}");
                return;
            }

            test = baza.zamowienia.FindIndex(x => x.produkt == PSKU.Text);
            if (test >= 0)
            {
                MessageBox.Show($"Produkt jest połączony relacją z zamówieniem o id: {test}");
                return;
            }

            var cmd = new OleDbCommand(
                $"DELETE FROM Produkty WHERE SKU='{PSKU.Text}'", baza.conn);

            baza.conn.Open();
            cmd.ExecuteNonQuery();
            baza.conn.Close();
            baza.WczytajWszystkie();
            dgProdukty.Items.Refresh();
        }
    }
}
