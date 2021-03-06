using Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekat.Dijalozi
{
    /// <summary>
    /// Interaction logic for pregledTipova.xaml
    /// </summary>
    public partial class pregledTipova : Window
    {

        private ObservableCollection<Tip> tipovi;

        public ObservableCollection<Tip> Tipovi
        {
            get { return tipovi; }
            set { tipovi = value; }
        }

        private DataBase baza;
        private string korisnik;
        public pregledTipova()
        {
            
            baza = new DataBase();
            baza.ucitajTipove();
            tipovi = baza.Tipovi;
            this.DataContext = this;
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;


        }

        /*public bool TextFilter(object o)
        {
            Resurs m = (o as Resurs);
            if (m == null)
                return false;

            if (m.Oznaka.Contains(searchBox.Text))
                return true;
            else
                return false;
        }*/

        
        

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textbox = sender as System.Windows.Controls.TextBox;
            string filter = textbox.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(tipovi);
            if (filter == "")
                cv.Filter = null;
            else
            {
                string[] words = filter.Split(' ');
                if (words.Contains(""))
                    words = words.Where(word => word != "").ToArray();
                cv.Filter = o =>
                {
                    Tip tip = o as Tip;
                    return words.Any(word => tip.Oznaka.ToUpper().Contains(word.ToUpper()));
                };
                dgrMain.ItemsSource = tipovi;
            }

        }

        private void ObrisiBtn_Click_1(object sender, RoutedEventArgs e)
        {
            Tip m = null;
            if (dgrMain.SelectedValue is Tip)
            {
                m = (Tip)dgrMain.SelectedValue;
                List<String> manif = new List<string>();
                bool ima = false;
                foreach (Resurs ma in baza.Resursi)
                {
                    if (ma.Tip.Oznaka.Equals(m.Oznaka))
                        manif.Add("Oznaka: " + ma.Oznaka + ", Naziv: " + ma.Naziv + Environment.NewLine);
                }
                if (manif.Count > 0)
                    ima = true;
                MessageBoxResult result;
                if (ima)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("Da li zelite da nastavite sa brisanjem?" + Environment.NewLine + "Tip se trenutno koristi u sledećim resursima: " + Environment.NewLine).AppendLine();
                    foreach (string str in manif)
                    {
                        builder.Append(str.ToString()).AppendLine();
                    }
                    builder.Append("Brisanjem tipa brisu se i resursi koji ga koriste. ").AppendLine();
                    result = System.Windows.MessageBox.Show(builder.ToString(), "Brisanje tipa", MessageBoxButton.YesNo);
                }
                else
                    result = MessageBox.Show("Da li sigurno želite da obrišete odabrani tip?", "Brisanje tipa", MessageBoxButton.YesNo);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        m = (Tip)dgrMain.SelectedValue;
                        MainWindow.Instance.obrisiTip(m);
                        baza.brisanjeTipa(m);
                        if (ima)
                        {
                            List<Resurs> zaBrisanje = new List<Resurs>();
                            foreach (Resurs ma in baza.Resursi)
                            {
                                if (ma.Tip.Oznaka.Equals(m.Oznaka))
                                    zaBrisanje.Add(ma);
                            }
                            foreach (Resurs ma in zaBrisanje)
                            {
                                baza.Resursi.Remove(ma);
                            }
                            baza.sacuvajResurs();

                        }
                        Tipovi = baza.Tipovi;
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }


            }
            else
            {
                System.Windows.MessageBox.Show("Niste odabrali tip za brisanje!", "Brisanje tipa");

            }
        }

        private void IzmeniBtn_Click_1(object sender, RoutedEventArgs e)
        {
            Tip m;
            if (dgrMain.SelectedValue is Tip)
            {
                m = (Tip)dgrMain.SelectedValue;

                var s = new izmeniTip(m);
                s.ShowDialog();
                baza.ucitajEtikete();
                Tipovi = baza.Tipovi;

                if (s.idx != -1)
                {
                    baza.Tipovi[s.idx] = s.izmenjen;
                    MainWindow.Instance.izmeniTip(s.izmenjen);
                    baza.sacuvajTip();
                    Tipovi = baza.Tipovi;
                    foreach (Resurs ma in baza.Resursi)
                    {
                        if (ma.Tip.Oznaka.Equals(s.izmenjen.Oznaka))
                            ma.Tip = s.izmenjen;
                    }
                    baza.sacuvajResurs();
                    baza.ucitajResurse();

                }
                dgrMain.ItemsSource = null;
                baza.ucitajTipove();
                Tipovi = baza.Tipovi;
                dgrMain.ItemsSource = Tipovi;

            }
            else
            {
                System.Windows.MessageBox.Show("Niste odabrali nijedan tip.", "Izmena tipa");

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.tree.Items.Refresh();
            this.Close();
        }
    }
}
