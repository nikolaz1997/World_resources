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
    /// Interaction logic for PregledEtiketa.xaml
    /// </summary>
    public partial class PregledEtiketa : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }


        private ObservableCollection<Etiketa> etikete;

        public ObservableCollection<Etiketa> Etikete
        {
            get { return etikete; }
            set
            {
                if (etikete != value)
                {
                    etikete = value;
                    OnPropertyChanged("Etikete");
                }

            }
        }

        private DataBase baza;
        private string korisnik;
        public PregledEtiketa()
        {
            
            baza = new DataBase();
            baza.ucitajEtikete();
            Etikete = baza.Etikete;
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
            TextBox textbox = sender as TextBox;
            string filter = textbox.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(etikete);
            if (filter == "")
                cv.Filter = null;
            else
            {
                string[] words = filter.Split(' ');
                if (words.Contains(""))
                    words = words.Where(word => word != "").ToArray();
                cv.Filter = o =>
                {
                    Etiketa etiketa = o as Etiketa;
                    return words.Any(word => etiketa.Oznaka.ToUpper().Contains(word.ToUpper()));
                };

                dgrMain.ItemsSource = etikete;
            }
        }

        private void ObrisiBtn_Click_1(object sender, RoutedEventArgs e)
        {
            Etiketa m = null;
            if (dgrMain.SelectedValue is Etiketa)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Da li ste sigurni da želite da obrišete etiketu?", "Brisanje etikete", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        m = (Etiketa)dgrMain.SelectedValue;
                        baza.brisanjeEtikete(m);

                        Etikete = baza.Etikete;
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }


            }
            else
            {
                System.Windows.MessageBox.Show("Niste odabrali etiketu za brisanje!", "Brisanje etikete");

            }
        }

        private void IzmeniBtn_Click_1(object sender, RoutedEventArgs e)
        {
            Etiketa m;
            if (dgrMain.SelectedValue is Etiketa)
            {
                m = (Etiketa)dgrMain.SelectedValue;

                var s = new izmeniEtiketu(m);
                s.ShowDialog();
                baza.ucitajEtikete();
                Etikete = baza.Etikete;
                if (s.idx != -1)
                {
                    baza.Etikete[s.idx] = s.izmenjena;
                    baza.sacuvajEtiketu();
                    Etikete = baza.Etikete;
                }
                dgrMain.ItemsSource = null;
                baza.ucitajEtikete();
                Etikete = baza.Etikete;
                dgrMain.ItemsSource = Etikete;

            }
            else
            {
                System.Windows.MessageBox.Show("Niste odabrali nijednu etiketu.", "Izmena etikete");

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
