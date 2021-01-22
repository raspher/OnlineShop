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
            baza.WczytajWszystkie();

            // ładowanie do tabelek
            dgAdresy.ItemsSource = baza.adresy;
            dgKonta.ItemsSource = baza.konta;
            dgOceny.ItemsSource = baza.oceny;
            dgProdukty.ItemsSource = baza.produkty;
            dgRole.ItemsSource = baza.role;
            dgTransakcje.ItemsSource = baza.transakcje;
            dgZamowienia.ItemsSource = baza.zamowienia;

            // ładowanie zasobów do panelu
            k_cb.ItemsSource = baza.role.Select(x => x.rola).ToList();
        }

        private void Odswiez_Baze() {
            if (aktualnyUzytkownik.rola == "klient") {
                baza.WczytajWszystkieUzytkownika(aktualnyUzytkownik);
            } 
            else
            {
                baza.WczytajWszystkie();
            }
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

                        aAktualizuj.IsEnabled = rola.adresy_z ? true : false;
                        aDodaj.IsEnabled = rola.adresy_z ? true : false;
                        aUsun.IsEnabled = rola.adresy_z ? true : false;

                        kAktualizuj.IsEnabled = rola.konta_z ? true : false;
                        kDodaj.IsEnabled = rola.konta_z ? true : false;
                        kUsun.IsEnabled = rola.konta_z ? true : false;

                        oAktualizuj.IsEnabled = rola.oceny_z ? true : false;
                        oDodaj.IsEnabled = rola.oceny_z ? true : false;
                        oUsun.IsEnabled = rola.oceny_z ? true : false;

                        pAktualizuj.IsEnabled = rola.produkty_z ? true : false;
                        pDodaj.IsEnabled = rola.produkty_z ? true : false;
                        pUsun.IsEnabled = rola.produkty_z ? true : false;

                        rAktualizuj.IsEnabled = rola.role_z ? true : false;
                        rDodaj.IsEnabled = rola.role_z ? true : false;
                        rUsun.IsEnabled = rola.role_z ? true : false;

                        tAktualizuj.IsEnabled = rola.transakcje_z ? true : false;
                        tDodaj.IsEnabled = rola.transakcje_z ? true : false;
                        tUsun.IsEnabled = rola.transakcje_z ? true : false;

                        zAktualizuj.IsEnabled = rola.zamowienia_z ? true : false;
                        zDodaj.IsEnabled = rola.zamowienia_z ? true : false;
                        zUsun.IsEnabled = rola.zamowienia_z ? true : false;

 

                        // TODO: zapis - użytkownik nie może zmienić swojej nazwy itd...

                        // wczytaj odpowiednie tabele
                        Odswiez_Baze();
                        

                        Baza.Visibility = Visibility.Visible;
                        return;
                    }
                }
            }

            MessageBox.Show("Nie udało się zalogować!");
            
        }

        private void Button_Click_Dodaj_Produkt(object sender, RoutedEventArgs e)
        {
            if (baza.produkty.Exists(x => x.SKU == PSKU.Text))
            {
                MessageBox.Show("Już istnieje element o tym SKU!");
                return;
            }

            var cmd = new OleDbCommand(
                $"INSERT INTO Produkty (SKU, EAN, Nazwa, Opis, Cena, Ilość) " +
                $"VALUES ('{PSKU.Text}', '{PEAN.Text}', '{PNazwa.Text}', '{POpis.Text}', " +
                $"{decimal.Parse(PCena.Text)}, {int.Parse(PIlosc.Text)});",
                baza.conn);

            baza.conn.Open();
            cmd.ExecuteNonQuery();
            baza.conn.Close();
            Odswiez_Baze();
            dgProdukty.Items.Refresh();
        }

        private void Button_Click_Aktualizuj_Produkt(object sender, RoutedEventArgs e)
        {
            if (!baza.produkty.Exists(x => x.SKU == PSKU.Text))
            {
                MessageBox.Show("Brak takiego wpisu!");
                return;
            }

            var cmd = new OleDbCommand(
                $"UPDATE Produkty SET SKU='{PSKU.Text}', EAN='{PEAN.Text}', Nazwa='{PNazwa.Text}', " +
                $"Opis='{POpis.Text}', Cena={decimal.Parse(PCena.Text)}, Ilość={int.Parse(PIlosc.Text)} " +
                $"WHERE SKU='{PSKU.Text}';",
                baza.conn);

            baza.conn.Open();
            cmd.ExecuteNonQuery();
            baza.conn.Close();
            Odswiez_Baze();
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
            Odswiez_Baze();
            dgProdukty.Items.Refresh();
        }

        private void Button_Click_Dodaj_Konto(object sender, RoutedEventArgs e)
        {
            foreach (var konto in baza.konta)
            {
                if (konto.nazwa == k_uzytkownik.Text)
                {
                    MessageBox.Show($"Już istnieje element taki użytkownik {k_uzytkownik.Text}!");
                    return;
                }

            }

            var cmd = new OleDbCommand(
                $"INSERT INTO Konta (Użytkownik, Email, Hasło, Rola) " +
                $"VALUES ('{k_uzytkownik.Text}', '{k_email.Text}', '{k_haslo.Text}', '{k_cb.SelectedValue}' );",
                baza.conn);

            baza.conn.Open();
            cmd.ExecuteNonQuery();
            baza.conn.Close();
            Odswiez_Baze();
            dgKonta.Items.Refresh();
        }

        private void Button_Click_Aktualizuj_Konto(object sender, RoutedEventArgs e)
        {
            if (!baza.konta.Exists(x => x.nazwa.Equals(k_uzytkownik.Text)))
            {
                MessageBox.Show("Błąd nazwy użytkownika!");
                return;
            }

            var cmd = new OleDbCommand(
                $"UPDATE Konta SET Email='{k_email.Text}', " +
                $"Hasło='{k_haslo.Text}', Rola='{k_cb.SelectedItem}' " +
                $"WHERE Użytkownik='{k_uzytkownik.Text}';",
                baza.conn);

            baza.conn.Open();
            cmd.ExecuteNonQuery();
            baza.conn.Close();
            baza.WczytajWszystkie();
            dgKonta.Items.Refresh();
        }

        private void Button_Click_Usun_Konto(object sender, RoutedEventArgs e)
        {
            if (baza.transakcje.Exists(x => x.uzytkownik == k_uzytkownik.Text))
            {
                MessageBox.Show($"Produkt jest połączony relacją z Transakcjami!");
                return;
            }
            if (baza.adresy.Exists(x => x.uzytkownik == k_uzytkownik.Text))
            {
                MessageBox.Show($"Produkt jest połączony relacją z Adresami!");
                return;
            }
            if (baza.oceny.Exists(x => x.uzytkownik == k_uzytkownik.Text))
            {
                MessageBox.Show($"Produkt jest połączony relacją z Ocenami!");
            }

            var cmd = new OleDbCommand(
                $"DELETE FROM Konta WHERE Użytkownik='{k_uzytkownik.Text}'", baza.conn);

            baza.conn.Open();
            cmd.ExecuteNonQuery();
            baza.conn.Close();
            baza.WczytajWszystkie();
            dgKonta.Items.Refresh();
        }
    }
}
