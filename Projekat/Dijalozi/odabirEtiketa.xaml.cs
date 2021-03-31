using Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for odabirEtiketa.xaml
    /// </summary>
    public partial class odabirEtiketa : Window
    {
        private DataBase baza;
        private ObservableCollection<Etiketa> etikete;

        public ObservableCollection<Etiketa> Etikete
        {
            get { return etikete; }
            set { etikete = value; }
        }

        private ObservableCollection<Etiketa> odabrana;

        public ObservableCollection<Etiketa> Odabrana
        {
            get { return odabrana; }
            set { odabrana = value; }
        }
        public bool dodavanje;

        public odabirEtiketa()
        {
            baza = new DataBase();
            baza.ucitajEtikete();
            etikete = baza.Etikete;
            InitializeComponent();
            this.DataContext = this;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

        }


        private void Izaberi_Click_1(object sender, RoutedEventArgs e)
        {
            dodavanje = true;
            odabrana = new ObservableCollection<Etiketa>();
            if (dgrMain.SelectedItems != null)
            {
                foreach (Etiketa et in dgrMain.SelectedItems)
                {
                    odabrana.Add(et);
                }
            }
            else
                odabrana = null;
            this.Close();
        }
    

    private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(null, @"./../../../Pomoc.chm");
        }

        /*   private void Obrisi_Click(object sender, RoutedEventArgs e)
           {
               dodavanje = false;
               odabrana = new ObservableCollection<Etiketa>();
               if (dgrMain.SelectedItems != null)
               {
                   foreach (Etiketa et in dgrMain.SelectedItems)
                   {
                       odabrana.Add(et);
                   }
               }
               else
                   odabrana = null;
               this.Close();
           }*/
    }
}
