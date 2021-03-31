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
    /// Interaction logic for resurs.xaml
    /// </summary>
    public partial class resurs : Window
    {
       /* public resurs()
        {
            InitializeComponent();
        }*/

        private bool _myTextBoxChanging = false;

        private bool obnovljiv = false;
        private bool eksploatisan = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        private DataBase baza;
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

        private string cena;
        public string Cena
        {
            get { return cena; }
            set
            {
                if(value != cena)
                {
                    cena = value;
                    OnPropertyChanged("Cena");
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

        private ObservableCollection<Etiketa> etikete;


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

        private ObservableCollection<Resurs> resList;

        public ObservableCollection<Resurs> ResList
        {
            get { return resList; }
            set
            {
                if (resList != value)
                {

                    resList = value;
                    OnPropertyChanged("ResList");
                }
            }
        }

        public resurs()
        {
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
            baza = new DataBase();
            tip = new Tip();

            this.DataContext = this;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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
                slika = ikonica.Source.ToString();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if(naziv == "" || oznaka == "" || tip_textBox.Text == "")
            {
                System.Windows.MessageBox.Show("Niste uneli neophodne informacije!");               
                return;
            }

            DateTime d = datum;
            //bool obnovljiv = false; bool eksploatisan = false;
            if(ObnovljivDa.IsChecked == true)
            {
                obnovljiv = true;
            }
            if(EksploatisanMoguce.IsChecked == true)
            {
                eksploatisan = true;
            }

            String frekvencija_pojavljivanja = "";
            if (frekvencija_comboBox.SelectedIndex.Equals(-1))
                frekvencija_pojavljivanja = "";
            else if (frekvencija_comboBox.SelectedItem.Equals("redak"))
            {
                int idx = FrekvencijaPojavljivanja.IndexOf("redak");
                frekvencija_pojavljivanja = FrekvencijaPojavljivanja[idx];
            }
            else if (frekvencija_comboBox.SelectedItem.Equals("cest"))
            {
                int idx = FrekvencijaPojavljivanja.IndexOf("cest");
                frekvencija_pojavljivanja = FrekvencijaPojavljivanja[idx];
            }
            else
            {
                int idx = FrekvencijaPojavljivanja.IndexOf("univerzalan");
                frekvencija_pojavljivanja = FrekvencijaPojavljivanja[idx];
            }

            String mera = "";
            if (mera_comboBox.SelectedIndex.Equals(-1))
                mera = "";
            else if (mera_comboBox.SelectedItem.Equals("barel"))
            {
                int idx = Mera.IndexOf("barel");
                mera = Mera[idx];
            }
            else if (mera_comboBox.SelectedItem.Equals("tona"))
            {
                int idx = Mera.IndexOf("tona");
                mera = Mera[idx];
            }
            else if (mera_comboBox.SelectedItem.Equals("kilogram"))
            {
                int idx = Mera.IndexOf("kilogram");
                mera = Mera[idx];
            }
            else
            {
                int idx = Mera.IndexOf("merica");
                mera = Mera[idx];
            }

            String strateska_vaznost = "";
            if (strateska_comboBox.SelectedIndex.Equals(-1))
                strateska_vaznost = "";
            else if (strateska_comboBox.SelectedItem.Equals("visoka"))
            {
                int idx = StrateskaVaznost.IndexOf("visoka");
                strateska_vaznost = StrateskaVaznost[idx];
            }
            else if (strateska_comboBox.SelectedItem.Equals("umerena"))
            {
                int idx = StrateskaVaznost.IndexOf("umerena");
                strateska_vaznost = StrateskaVaznost[idx];
            }
            else
            {
                int idx = StrateskaVaznost.IndexOf("niska");
                strateska_vaznost = StrateskaVaznost[idx];
            }

            ObservableCollection<Etiketa> listaEtiketa = etikete;

            Opis = opis_textBox.Text;
            if(slika == null && tip.Slika != null)
            {
                if (tip.Slika != "")
                    slika = tip.Slika;
            }
            else if(slika == null && tip.Slika == null)
            {
                slika = System.IO.Path.GetFullPath(@"..\..\") + "Images\\defLoc.png";
            }
            /*string s = oznaka;
            s = s.Replace(' ', '_');
            oznaka = s;*/
            if(datumPicker.Text != "")
                datum = (DateTime)datumPicker.SelectedDate;
            Resurs m = new Resurs(oznaka, naziv, opis, tip, obnovljiv, eksploatisan, cena, frekvencija_pojavljivanja, mera, strateska_vaznost, slika, datum, etikete);
            bool passed = baza.noviResurs(m);
            if (passed)
            {
                System.Windows.MessageBox.Show("Uspešno ste dodali nov resurs.", "Dodavanje resursa");
                baza.sacuvajResurs();
                resList = baza.Resursi;
                MainWindow.Instance.puniDrvoProvera(m);
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Vec postoji resus sa tom oznakom!", "Dodavanje resursa");
            }
        }

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
