using Projekat.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for etiketa.xaml
    /// </summary>
    public partial class etiketa : Window
    {
        private string oznaka;

        public string Oznaka
        {
            get { return oznaka; }
            set { oznaka = value; }
        }


        private System.Drawing.Color boja;

        public System.Drawing.Color Boja
        {
            get { return boja; }
            set { boja = value; }
        }

        private String opis;

        public String Opis
        {
            get { return opis; }
            set { opis = value; }
        }

        private DataBase baza;
        public etiketa()
        {
            baza = new DataBase();
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.DataContext = this;

        }


        private void colorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            System.Windows.Media.Color mediacolor = ColorPicker.SelectedColor.Value;
            boja = System.Drawing.Color.FromArgb(mediacolor.A, mediacolor.R, mediacolor.G, mediacolor.B);


        }

        
      
    
    private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Sacuvaj_Click_1(object sender, RoutedEventArgs e)
        {
            Etiketa etiketa = new Etiketa(oznaka, opis, boja);
            bool passed = baza.novaEtiketa(etiketa);
            if (passed)
            {
                System.Windows.MessageBox.Show("Uspešno dodavanje nove etikete.", "Uspeh!");
                baza.sacuvajEtiketu();
                this.Close();
            }
            else
                System.Windows.MessageBox.Show("Vec postoji etiketa sa tom oznakom!", "Greska!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(null, @"./../../../Pomoc.chm");
        }
    }
}
