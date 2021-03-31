using Microsoft.Win32;
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
    /// Interaction logic for izmeniResurs.xaml
    /// </summary>
    public partial class izmeniResurs : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        private string oznaka;
        public string Oznaka
        {
            get { return oznaka; }
            set
            {
                if (value != oznaka)
                {
                    oznaka = value;
                    OnPropertyChanged("Oznaka");
                }
            }
        }

        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set
            {
                if (value != naziv)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }

        private Tip tip;

        public Tip Tip
        {
            get { return tip; }
            set
            {
                if (value != tip)
                {
                    tip = value;
                    OnPropertyChanged("Tip");
                }
            }
        }

        private string opis;

        public string Opis
        {
            get { return opis; }
            set
            {
                if (value != opis)
                {
                    opis = value;
                    OnPropertyChanged("Opis");
                }
            }
        }

        private string oznakaTipa;

        public string OznakaTipa
        {
            get { return oznakaTipa; }
            set
            {
                if (value != oznakaTipa)
                {
                    oznakaTipa = value;
                    OnPropertyChanged("OznakaTipa");
                }
            }
        }

        private string rFrekvencijaPojavljivanja;
        public string RFrekvencijaPojavljivanja
        {
            get { return rFrekvencijaPojavljivanja; }
            set
            {
                if(value != rFrekvencijaPojavljivanja)
                {
                    rFrekvencijaPojavljivanja = value;
                    OnPropertyChanged("RFrekvencijaPojavljivanja");
                }
            }
        }

        private string rMera;
        public string RMera
        {
            get { return rMera; }
            set
            {
                if(value != rMera)
                {
                    rMera = value;
                    OnPropertyChanged("RMera");
                }
            }
        }

        private string rStrateskaVaznost;
        public string RStrateskaVaznost
        {
            get { return rStrateskaVaznost; }
            set
            {
                if(value != rStrateskaVaznost)
                {
                    rStrateskaVaznost = value;
                    OnPropertyChanged("RStrateskaVaznost");
                }
            }
        }

        private string rCena;
        public string RCena
        {
            get { return rCena; }
            set
            {
                if(value != rCena)
                {
                    rCena = value;
                    OnPropertyChanged("RCena");
                }
            }
        }

        private bool bObnovljivDa;
        public bool BObnovljivDa
        {
            get { return bObnovljivDa; }
            set
            {
                if(value != bObnovljivDa)
                {
                    bObnovljivDa = value;
                    OnPropertyChanged("BObnovljivDa");
                }
            }
        }

        private bool bObnovljivNe;
        public bool BObnovljivNe
        {
            get { return bObnovljivNe; }
            set
            {
                if (value != bObnovljivNe)
                {
                    bObnovljivNe = value;
                    OnPropertyChanged("BObnovljivNe");
                }
            }
        }

        private bool bMoguc;
        public bool BMoguc
        {
            get { return bMoguc; }
            set
            {
                if(value != bMoguc)
                {
                    bMoguc = value;
                    OnPropertyChanged("BMoguc");
                }
            }
        }

        private bool bNemoguc;
        public bool BNemoguc
        {
            get { return bNemoguc; }
            set
            {
                if (value != bNemoguc){
                    bNemoguc = value;
                    OnPropertyChanged("BNemoguc");
                }
            }
        }

        private DateTime datum;
        public DateTime Datum
        {
            get { return datum; }
            set
            {
                if (value != datum)
                {
                    datum = value;
                    OnPropertyChanged("Datum");
                }
            }
        }

        private string slika;
        public string Slika
        {
            get { return slika; }
            set
            {
                if (value != slika)
                {
                    slika = value;
                    OnPropertyChanged("Slika");
                }

            }
        }

        private string listaEtiketa;
        public string ListaEtiketa
        {
            get { return listaEtiketa; }
            set
            {
                if (value != listaEtiketa)
                {
                    listaEtiketa = value;
                    OnPropertyChanged("ListaEtiketa");
                }
            }
        }

        private ObservableCollection<Etiketa> etikete;

        public ObservableCollection<Etiketa> Etikete
        {
            get { return etikete; }
            set { etikete = value; }
        }

        private DataBase baza;
        private string korisnik;
        private Resurs selektovana;

        public izmeniResurs(Resurs m)
        {
            baza = new DataBase();
            FrekvencijaPojavljivanja = new ObservableCollection<string>();
            FrekvencijaPojavljivanja.Add("redak");
            FrekvencijaPojavljivanja.Add("cest");
            FrekvencijaPojavljivanja.Add("univerzalan");

            Mera = new ObservableCollection<string>();
            Mera.Add("barel");
            Mera.Add("tona");
            Mera.Add("kilogram");
            Mera.Add("merica");

            StrateskaVaznost = new ObservableCollection<string>();
            StrateskaVaznost.Add("visoka");
            StrateskaVaznost.Add("umerena");
            StrateskaVaznost.Add("niska");

            etikete = new ObservableCollection<Etiketa>();

            tip = new Tip();
           // InitializeComponent();
          //  WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.DataContext = this;
            selektovana = m;
            Naziv = m.Naziv;
            Oznaka = m.Oznaka;
            Tip = m.Tip;
            OznakaTipa = Tip.Oznaka;
            Opis = m.Opis;
            RFrekvencijaPojavljivanja = m.FrekvencijaPojavljivanja;
            RMera = m.Mera;
            RStrateskaVaznost = m.StrateskaVaznost;
            RCena = m.Cena;
            Datum = m.Datum;
            bool bObnovljiv = m.Obnovljiv;
            if (bObnovljiv)
            {
                BObnovljivDa = true;
                BObnovljivNe = false;
            }
            else
            {
                BObnovljivDa = false;
                BObnovljivNe = true;
            }
            bool bEksploatisan = m.Eksploatisan;
            if (bEksploatisan)
            {
                BMoguc = true;
                BNemoguc = false;
            }
            else
            {
                BMoguc = false;
                BNemoguc = true;
            }
            Slika = m.Slika;
            Etikete = m.Etikete;
            if(Etikete.Count > 0)
            {
                StringBuilder sb = new StringBuilder(ListaEtiketa);
                foreach (Etiketa et in Etikete)
                {
                    sb.Append(et.Oznaka);
                    sb.Append(System.Environment.NewLine);
                }

                ListaEtiketa = sb.ToString();
            }

            InitializeComponent();
            
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public ObservableCollection<string> FrekvencijaPojavljivanja
        {
            get;
            set;
        }
        public ObservableCollection<string> Mera
        {
            get;
            set;
        }
        public ObservableCollection<string> StrateskaVaznost
        {
            get;
            set;
        }

        private Resurs izmenjena;
        public Resurs Izmenjena
        {
            get { return izmenjena; }
            set { izmenjena = value; }
        }
        public int idx = -1;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(naziv_textBox.Text == "" || oznaka_textBox.Text == "")
            {
                System.Windows.MessageBox.Show("Niste popunili obavezna polja!", "Izmena resursa");
            }
            else
            {
                bool bObnovljiv = false;
                if (BObnovljivDa)
                    bObnovljiv = true;
                bool bEksploatisan = false;
                if (BMoguc)
                    bEksploatisan = true;
                Izmenjena = new Resurs(Oznaka, Naziv, Opis, Tip, bObnovljiv, bEksploatisan, RCena, RFrekvencijaPojavljivanja, RMera, RStrateskaVaznost, Slika, Datum, etikete);
                DataBase b = new DataBase();
                idx = 0;
                foreach(Resurs man in b.Resursi)
                {
                    if (man.Oznaka == izmenjena.Oznaka)
                        break;
                    idx++;
                }
                izmenjena.X = selektovana.X;
                izmenjena.Y = selektovana.Y;
                b.Resursi.RemoveAt(idx);
                b.Resursi.Add(Izmenjena);
                b.sacuvajResurs();
                

                System.Windows.MessageBox.Show("Uspešna izmena resursa!", "Izmena resursa");
                MainWindow.Instance.izmeniResu(izmenjena);
                pregledResursa.Instance.dgrMain.Items.Refresh();

                this.Close();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            odabirEtiketa s = new odabirEtiketa();
            s.ShowDialog();
            ObservableCollection<Etiketa> temp = s.Odabrana;
            if (s.dodavanje)
            {
                ObservableCollection<Etiketa> pomocna = new ObservableCollection<Etiketa>();
                if (temp != null)
                {
                    bool b = false;
                    listaEtiketa = "";
                    baza.ucitajEtikete();
                    etikete = baza.Etikete;
                    StringBuilder sb = new StringBuilder(ListaEtiketa);

                    foreach (Etiketa et in etikete)
                    {
                        foreach (Etiketa et2 in temp)
                        {
                            if (et.Oznaka.Equals(et2.Oznaka))
                            {
                                b = true;
                            }
                            else b = false;
                            if (b)
                            {
                                pomocna.Add(et2);

                                sb.Append(et2.Oznaka);
                                sb.Append(System.Environment.NewLine);
                            }

                        }
                    }
                    etikete = pomocna;
                    ListaEtiketa = sb.ToString();

                    etikete_textBox.Text = ListaEtiketa;
                }
            }
            else
            {

                if (temp != null && temp.Count > 0)
                {

                    listaEtiketa = "";
                    baza.ucitajEtikete();
                    etikete = baza.Etikete;
                    ObservableCollection<Etiketa> pomocna2 = etikete;
                    StringBuilder sb = new StringBuilder(ListaEtiketa);
                    int idx = -1;
                    for (int i = 0; i < temp.Count; i++)
                    {
                        idx = 0;
                        foreach (Etiketa et in etikete)
                        {
                            if (et.Oznaka.Equals(temp.ElementAt(i).Oznaka))
                                break;
                            idx++;
                        }
                        if (idx != -1)
                        {
                            etikete.RemoveAt(idx);
                        }
                    }
                    foreach (Etiketa et in temp)
                    {
                        etikete.Remove(et);
                    }

                    foreach (Etiketa et in etikete)
                    {
                        sb.Append(et.Oznaka);
                        sb.Append(System.Environment.NewLine);
                    }
                    ListaEtiketa = sb.ToString();

                    etikete_textBox.Text = ListaEtiketa;
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();


            fileDialog.Title = "Izaberi ikonicu";
            fileDialog.Filter = "Images|*.jpg;*.jpeg;*.png|" +
                                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                "Portable Network Graphic (*.png)|*.png";
            if (fileDialog.ShowDialog() == true)
            {
                ikonica.Source = new BitmapImage(new Uri(fileDialog.FileName));
                Slika = ikonica.Source.ToString();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            odabirTipa s = new odabirTipa();
            s.ShowDialog();

            Tip temp = s.Odabran;
            if (temp != null)
            {
                tip = temp;
                OznakaTipa = tip.Oznaka;
                tip_textBox.Text = OznakaTipa;
            }
        }

        private bool _myTextBoxChanging = false;
        private void Brojevi(object sender, TextChangedEventArgs e)
        {

            validateText(cene_br);
        }
        private void validateText(TextBox box)
        {
            if (_myTextBoxChanging)
                return;
            _myTextBoxChanging = true;

            string text = box.Text;
            if (text == "")
                return;
            string validText = "";
            int pos = box.SelectionStart;
            for (int i = 0; i < text.Length; i++)
            {
                char s = text[i];
                if (s < '0' || s > '9')
                {
                    if (i <= pos)
                        pos--;
                }
                else
                    validText += s;
            }

            // trim starting 00s 
            while (validText.Length >= 2 && validText.StartsWith("00"))
            {
                validText = validText.Substring(1);
                if (pos < 2)
                    pos--;
            }

            if (pos > validText.Length)
                pos = validText.Length;
            box.Text = validText;
            box.SelectionStart = pos;
            _myTextBoxChanging = false;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(null, @"./../../../Pomoc.chm");
        }
    }
}
