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
    /// Interaction logic for pregledResursa.xaml
    /// </summary>
    /// 
    
    public partial class pregledResursa : Window
    {
        private DataBase baza;
        private ObservableCollection<Resurs> res;
        private static pregledResursa instance;

        public static pregledResursa Instance
        {
            get { return instance; }
        }

        public ObservableCollection<Resurs> Res
        {
            get { return res; }
            set { res = value; }
        }
 
        public pregledResursa()
        {
            
            Res = new ObservableCollection<Resurs>();
            baza = new DataBase();
            baza.ucitajResurse();
            res = baza.Resursi;
            this.DataContext = this;
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            instance = this;
        }

        public int idx = -1;
        public Resurs izmenjena;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMain.SelectedValue is Resurs)
            {
                Resurs m = (Resurs)dgrMain.SelectedValue;


                var s = new izmeniResurs(m);
                s.ShowDialog();
                if (s.idx != -1)
                {
                    baza.Resursi[s.idx] = s.Izmenjena;
                    baza.sacuvajResurs();
                    baza.ucitajResurse();
                    Res = baza.Resursi;
                    idx = s.idx;
                    izmenjena = s.Izmenjena;
                    
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Niste odabrali nijedan resurs.", "Izmena resursa");

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Resurs m = null;
            if (dgrMain.SelectedValue is Resurs)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Da li ste sigurni da želite da obrišete resurs?", "Brisanje resursa", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        m = (Resurs)dgrMain.SelectedValue;
                        baza.brisanjeResursa(m);
                        MainWindow.Instance.izbrisiResu(m);
                        MainWindow.Instance.tree.Items.Refresh();
                        Res = baza.Resursi;
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }


            }
            else
            {
                System.Windows.MessageBox.Show("Niste odabrali resurs za brisanje!", "Brisanje resursa");

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.tree.Items.Refresh();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textbox = sender as System.Windows.Controls.TextBox;
            string filter = textbox.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(res);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    Resurs man = o as Resurs;
                    string[] words = filter.Split(' ');
                    if (words.Contains(""))
                        words = words.Where(word => word != "").ToArray();
                    return words.Any(word => man.Oznaka.ToUpper().Contains(word.ToUpper()));
                };

                dgrMain.ItemsSource = res;
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textbox = sender as System.Windows.Controls.TextBox;
            string filter = textbox.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(res);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    Resurs man = o as Resurs;
                    string[] words = filter.Split(' ');
                    if (words.Contains(""))
                        words = words.Where(word => word != "").ToArray();
                    return words.Any(word => man.Naziv.ToUpper().Contains(word.ToUpper()));
                };

                dgrMain.ItemsSource = res;
            }
        }
    }
}
