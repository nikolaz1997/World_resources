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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow instance;
        private DataBase baza;
        private Tip tip;
        private ObservableCollection<Etiketa> etikete;
        private string listaEtiketa;
        private Resurs m;
        private Point startPoint;
        private ObservableCollection<Resurs> mfList;
        private Resurs lastSelected;

        public static MainWindow Instance
        {
            get { return instance; }
        }

        public ObservableCollection<Resurs> MfList
        {
            get { return mfList; }
            set
            {
                if (mfList != value)
                {
                    mfList = value;
                    OnPropertyChanged("MfList");
                }
            }
        }

        public ObservableCollection<Tip> tipovi
        {
            get;
            set;
        }

        public Tip Tip
        {
            get { return tip; }
            set { tip = value; }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void ucitajSve()
        {
            foreach (Resurs m in baza.Resursi)
            {
                bool result = canvasMap.Children.Cast<Image>().Any(x => x.Tag != null && x.Tag.ToString() == m.Oznaka);

                if (result)
                    continue;
                if (m.X != -1 || m.Y != -1)
                {
                    Image img = new Image();
                    if (!m.Slika.Equals(""))
                        img.Source = new BitmapImage(new Uri(m.Slika));
                    else
                        img.Source = new BitmapImage(new Uri(m.Tip.Slika));

                    img.Width = 50;
                    img.Height = 50;
                    img.Tag = m.Oznaka;
                    WrapPanel wp = new WrapPanel();
                    wp.Orientation = Orientation.Vertical;

                    TextBox oznaka = new TextBox();
                    oznaka.IsEnabled = false;
                    oznaka.Text = "Oznaka: " + m.Oznaka;
                    wp.Children.Add(oznaka);

                    TextBox naziv = new TextBox();
                    naziv.IsEnabled = false;
                    naziv.Text = "Naziv: " + m.Naziv;
                    wp.Children.Add(naziv);


                    TextBox tip = new TextBox();
                    tip.IsEnabled = false;
                    tip.Text = "Tip: " + m.Tip.Oznaka;
                    wp.Children.Add(tip);


                    TextBox opis = new TextBox();
                    opis.IsEnabled = false;
                    opis.Text = "Opis: " + m.Opis;
                    wp.Children.Add(opis);

                    TextBox datum = new TextBox();
                    datum.IsEnabled = false;
                    datum.Text = "Datum: " + m.Datum.ToShortDateString();
                    wp.Children.Add(datum);

                    TextBox obnovljiv = new TextBox();
                    obnovljiv.IsEnabled = false;
                    if (m.Obnovljiv)
                        obnovljiv.Text = "Obnovljiv: Da";
                    else
                        obnovljiv.Text = "Obnovljiv: Ne";
                    wp.Children.Add(obnovljiv);

                    TextBox eksploatisan = new TextBox();
                    eksploatisan.IsEnabled = false;
                    if (m.Eksploatisan)
                        eksploatisan.Text = "Eksploatisanje: Moguce";
                    else
                        eksploatisan.Text = "Eksploatisan: Nemoguce";
                    wp.Children.Add(eksploatisan);

                    TextBox frekvencija = new TextBox();
                    frekvencija.IsEnabled = false;
                    frekvencija.Text = "Frekvencija pojavljivanja: " + m.FrekvencijaPojavljivanja;
                    wp.Children.Add(frekvencija);

                    TextBox mera = new TextBox();
                    mera.IsEnabled = false;
                    mera.Text = "Mera: " + m.Mera;
                    wp.Children.Add(mera);

                    TextBox strateska = new TextBox();
                    strateska.IsEnabled = false;
                    strateska.Text = "Strateska vaznost: " + m.StrateskaVaznost;
                    wp.Children.Add(strateska);

                    TextBox cena = new TextBox();
                    cena.IsEnabled = false;
                    cena.Text = "Cena: " + m.Cena;
                    wp.Children.Add(cena);

                    TextBox etikete = new TextBox();
                    etikete.IsEnabled = false;
                    etikete.Text = "Etikete:" + System.Environment.NewLine;
                    ListaEtiketa = "";
                    StringBuilder sb = new StringBuilder(ListaEtiketa);
                    ObservableCollection<Etiketa> list = m.Etikete;
                    foreach (Etiketa et in list)
                    {
                        sb.Append(et.Oznaka);
                        sb.Append(System.Environment.NewLine);
                    }
                    ListaEtiketa = sb.ToString();
                    etikete.Text += ListaEtiketa;
                    ListaEtiketa = "";
                    wp.Children.Add(etikete);

                    ToolTip tt = new ToolTip();
                    tt.Content = wp;
                    img.ToolTip = tt;
                    img.PreviewMouseLeftButtonDown += DraggedImagePreviewMouseLeftButtonDown;
                    img.MouseMove += DraggedImageMouseMove;

                    canvasMap.Children.Add(img);
                    Canvas.SetLeft(img, m.X - 20);
                    Canvas.SetTop(img, m.Y - 20);
                }
            }
        }


        public MainWindow()
        {
            instance = this;
            etikete = new ObservableCollection<Etiketa>();
            tipovi = new ObservableCollection<Tip>();
            m = new Resurs();
            baza = new DataBase();
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.DataContext = this;

            tree.Items.Refresh();
            baza.ucitajResurse();
            ucitajSve();
            mfList = baza.Resursi;
            baza.ucitajEtikete();
            baza.ucitajTipove();

            tipovi = baza.Tipovi;
            //tree.ItemsSource = tipovi;
            puniDrvo();
        }

        public void puniDrvo()
        {
            tree.Items.Refresh();

            foreach (Tip t in tipovi)
            {
                foreach (Resurs ma in baza.Resursi)
                {
                    if (ma.Tip.Oznaka.Equals(t.Oznaka))
                    {
                        t.resursi.Add(ma);
                        /* if(t.resursi.Count == 0)
                         {
                             Console.WriteLine("Prazan");
                         }
                         else
                         {
                             Console.WriteLine("Nije");
                         }*/
                    }
                }
            }
        }

        public void izbrisiResu(Resurs m)
        {
            foreach (Tip t in tipovi)
            {
                if (m.Tip.Oznaka.Equals(t.Oznaka))
                {
                    for (int i = 0; i < t.resursi.Count; i++)
                    {
                        if (m.Oznaka == t.resursi[i].Oznaka)
                        {
                            Image zaBrisanje = null;
                            foreach (Image img in canvasMap.Children)
                            {
                                if (t.resursi[i].Oznaka.Equals(img.Tag))
                                {
                                    zaBrisanje = img;
                                }
                            }
                            if (zaBrisanje != null)
                                canvasMap.Children.Remove(zaBrisanje);

                            t.resursi.Remove(t.resursi[i]);
                            tree.Items.Refresh();
                            break;
                        }
                    }
                }
            }
        }

        public void izmeniResu(Resurs m)
        {
            foreach (Tip t in tipovi)
            {
                if (m.Tip.Oznaka.Equals(t.Oznaka))
                {
                    for (int i = 0; i < t.resursi.Count; i++)
                    {
                        if (m.Oznaka == t.resursi[i].Oznaka)
                        {
                            t.resursi[i].setAll(m);

                            Image zaMenjanje = null;
                            int idx = 0;
                            foreach (Image img in canvasMap.Children)
                            {
                                if (t.resursi[i].Oznaka.Equals(img.Tag))
                                {
                                    zaMenjanje = img;
                                    break;
                                }
                                idx++;
                            }
                            if (zaMenjanje != null)
                            {
                                WrapPanel wp = new WrapPanel();
                                wp.Orientation = Orientation.Vertical;

                                TextBox oznaka = new TextBox();
                                oznaka.IsEnabled = false;
                                oznaka.Text = "Oznaka: " + m.Oznaka;
                                wp.Children.Add(oznaka);

                                TextBox naziv = new TextBox();
                                naziv.IsEnabled = false;
                                naziv.Text = "Naziv: " + m.Naziv;
                                wp.Children.Add(naziv);


                                TextBox tip = new TextBox();
                                tip.IsEnabled = false;
                                tip.Text = "Tip: " + m.Tip.Oznaka;
                                wp.Children.Add(tip);


                                TextBox opis = new TextBox();
                                opis.IsEnabled = false;
                                opis.Text = "Opis: " + m.Opis;
                                wp.Children.Add(opis);

                                TextBox datum = new TextBox();
                                datum.IsEnabled = false;
                                datum.Text = "Datum: " + m.Datum.ToShortDateString();
                                wp.Children.Add(datum);

                                TextBox obnovljiv = new TextBox();
                                obnovljiv.IsEnabled = false;
                                if (m.Obnovljiv)
                                    obnovljiv.Text = "Obnovljiv: Da";
                                else
                                    obnovljiv.Text = "Obnovljiv: Ne";
                                wp.Children.Add(obnovljiv);

                                TextBox eksploatisan = new TextBox();
                                eksploatisan.IsEnabled = false;
                                if (m.Eksploatisan)
                                    eksploatisan.Text = "Eksploatisanje: Moguce";
                                else
                                    eksploatisan.Text = "Eksploatisan: Nemoguce";
                                wp.Children.Add(eksploatisan);

                                TextBox frekvencija = new TextBox();
                                frekvencija.IsEnabled = false;
                                frekvencija.Text = "Frekvencija pojavljivanja: " + m.FrekvencijaPojavljivanja;
                                wp.Children.Add(frekvencija);

                                TextBox mera = new TextBox();
                                mera.IsEnabled = false;
                                mera.Text = "Mera: " + m.Mera;
                                wp.Children.Add(mera);

                                TextBox strateska = new TextBox();
                                strateska.IsEnabled = false;
                                strateska.Text = "Strateska vaznost: " + m.StrateskaVaznost;
                                wp.Children.Add(strateska);

                                TextBox cena = new TextBox();
                                cena.IsEnabled = false;
                                cena.Text = "Cena: " + m.Cena;
                                wp.Children.Add(cena);

                                TextBox etikete = new TextBox();
                                etikete.IsEnabled = false;
                                etikete.Text = "Etikete:" + System.Environment.NewLine;
                                ListaEtiketa = "";
                                StringBuilder sb = new StringBuilder(ListaEtiketa);
                                ObservableCollection<Etiketa> list = m.Etikete;
                                foreach (Etiketa et in list)
                                {
                                    sb.Append(et.Oznaka);
                                    sb.Append(System.Environment.NewLine);
                                }
                                ListaEtiketa = sb.ToString();
                                etikete.Text += ListaEtiketa;
                                ListaEtiketa = "";
                                wp.Children.Add(etikete);

                                ToolTip tt = new ToolTip();
                                tt.Content = wp;
                                zaMenjanje.ToolTip = tt;

                                if (!m.Slika.Equals(""))
                                    zaMenjanje.Source = new BitmapImage(new Uri(m.Slika));
                                else
                                    zaMenjanje.Source = new BitmapImage(new Uri(m.Tip.Slika));

                                canvasMap.Children[idx] = zaMenjanje;
                            }
                        }
                    }
                }
            }
        }

        public void obrisiTip(Tip t)
        {
            foreach (Tip t1 in tipovi)
            {

                if (t1.Oznaka == t.Oznaka)
                {
                    Image zaBrisanje = null;
                    foreach (Image img in canvasMap.Children)
                    {
                        for (int i = 0; i < t1.resursi.Count; i++)
                        {
                            if (t1.resursi[i].Oznaka.Equals(img.Tag))
                            {
                                zaBrisanje = img;
                            }

                            if (zaBrisanje != null)
                                canvasMap.Children.Remove(zaBrisanje);
                        }

                        tipovi.Remove(t1);
                        tree.Items.Refresh();
                        break;
                    }
                    tipovi.Remove(t1);
                    tree.Items.Refresh();
                    break;
                }


            }
        }

        public void izmeniTip(Tip t)
        {
            foreach (Tip t1 in tipovi)
            {
                if (t1.Oznaka == t.Oznaka)
                {
                    t1.setAll(t);
                    tree.Items.Refresh();
                    break;
                }
            }
        }

        public void puniDrvoProvera(Resurs m)
        {
            foreach (Tip t in tipovi)
            {
                if (m.Tip.Oznaka.Equals(t.Oznaka))
                {
                    if (t.resursi.Count > 0)
                    {
                        for (int i = 0; i < t.resursi.Count; i++)
                        {
                            if (m.Oznaka != t.resursi[i].Oznaka)
                            {

                                t.resursi.Add(m);
                                mfList.Add(m);
                                tree.Items.Refresh();
                                break;
                            }
                        }
                    }
                    else
                    {
                        t.resursi.Add(m);
                        mfList.Add(m);
                    }
                }
            }
        }

        private void dropOnMe_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("resurs"))
            {
                Resurs m = e.Data.GetData("resurs") as Resurs;

                bool result = canvasMap.Children.Cast<Image>()
                            .Any(x => x.Tag != null && x.Tag.ToString() == m.Oznaka);
                //bool preklapanja = false
                System.Windows.Point d0 = e.GetPosition(canvasMap);

                foreach (Resurs r0 in baza.Resursi)
                {
                    if (m.Oznaka != r0.Oznaka) //ako hoce da pomeri manifesatciju jako malo da ne okida dole
                    {
                        if (r0.X != -1 && r0.Y != -1)
                        {
                            if (Math.Abs(r0.X - d0.X) <= 30 && Math.Abs(r0.Y - d0.Y) <= 30)
                            {
                                System.Windows.MessageBox.Show("Resurs sa ovom lokacijom već postoji na mapi! Ponovo unesite resurs na mapu.", "Premeštanje resursa na mapi");
                                ucitajSve();
                                return;
                            }
                        }
                    }
                }

                if (result == false)
                {
                    Image img = new Image();
                    if (!m.Slika.Equals(""))
                        img.Source = new BitmapImage(new Uri(m.Slika));
                    else
                        img.Source = new BitmapImage(new Uri(m.Tip.Slika));

                    img.Width = 50;
                    img.Height = 50;
                    img.Tag = m.Oznaka;

                    var positionX = e.GetPosition(canvasMap).X;
                    var positionY = e.GetPosition(canvasMap).Y;
                    m.X = positionX;
                    m.Y = positionY;

                    WrapPanel wp = new WrapPanel();
                    wp.Orientation = Orientation.Vertical;

                    TextBox oznaka = new TextBox();
                    oznaka.IsEnabled = false;
                    oznaka.Text = "Oznaka: " + m.Oznaka;
                    wp.Children.Add(oznaka);

                    TextBox naziv = new TextBox();
                    naziv.IsEnabled = false;
                    naziv.Text = "Naziv: " + m.Naziv;
                    wp.Children.Add(naziv);


                    TextBox tip = new TextBox();
                    tip.IsEnabled = false;
                    tip.Text = "Tip: " + m.Tip.Oznaka;
                    wp.Children.Add(tip);

                    TextBox opis = new TextBox();
                    opis.IsEnabled = false;
                    opis.Text = "Opis: " + m.Opis;
                    wp.Children.Add(opis);


                    TextBox datum = new TextBox();
                    datum.IsEnabled = false;
                    datum.Text = "Datum: " + m.Datum.ToShortDateString();
                    wp.Children.Add(datum);

                    TextBox obnovljiv = new TextBox();
                    obnovljiv.IsEnabled = false;
                    if (m.Obnovljiv)
                        obnovljiv.Text = "Obnovljiv: Da";
                    else
                        obnovljiv.Text = "Obnovljiv: Ne";
                    wp.Children.Add(obnovljiv);

                    TextBox eksploatisan = new TextBox();
                    eksploatisan.IsEnabled = false;
                    if (m.Eksploatisan)
                        eksploatisan.Text = "Eksploatisanje: Moguce";
                    else
                        eksploatisan.Text = "Eksploatisan: Nemoguce";
                    wp.Children.Add(eksploatisan);

                    TextBox frekvencija = new TextBox();
                    frekvencija.IsEnabled = false;
                    frekvencija.Text = "Frekvencija pojavljivanja: " + m.FrekvencijaPojavljivanja;
                    wp.Children.Add(frekvencija);

                    TextBox mera = new TextBox();
                    mera.IsEnabled = false;
                    mera.Text = "Mera: " + m.Mera;
                    wp.Children.Add(mera);

                    TextBox strateska = new TextBox();
                    strateska.IsEnabled = false;
                    strateska.Text = "Strateska vaznost: " + m.StrateskaVaznost;
                    wp.Children.Add(strateska);

                    TextBox cena = new TextBox();
                    cena.IsEnabled = false;
                    cena.Text = "Cena: " + m.Cena;
                    wp.Children.Add(cena);

                    TextBox etikete = new TextBox();
                    etikete.IsEnabled = false;
                    etikete.Text = "Etikete:" + System.Environment.NewLine;
                    ListaEtiketa = "";
                    StringBuilder sb = new StringBuilder(ListaEtiketa);
                    ObservableCollection<Etiketa> list = m.Etikete;
                    foreach (Etiketa et in list)
                    {
                        sb.Append(et.Oznaka);
                        sb.Append(System.Environment.NewLine);
                    }
                    ListaEtiketa = sb.ToString();
                    etikete.Text += ListaEtiketa;
                    ListaEtiketa = "";
                    wp.Children.Add(etikete);

                    ToolTip tt = new ToolTip();
                    tt.Content = wp;
                    img.ToolTip = tt;
                    img.PreviewMouseLeftButtonDown += DraggedImagePreviewMouseLeftButtonDown;
                    img.MouseMove += DraggedImageMouseMove;

                    baza.ucitajResurse();
                    ObservableCollection<Resurs> mfLista = baza.Resursi;
                    int idx = 0;
                    foreach (Resurs ma in mfLista)
                    {
                        if (ma.Oznaka.Equals(m.Oznaka))
                            break;

                        idx++;
                    }

                    mfLista[idx] = m;
                    baza.Resursi = mfLista;
                    baza.sacuvajResurs();

                    canvasMap.Children.Add(img);
                    Canvas.SetLeft(img, m.X - 20);
                    Canvas.SetTop(img, m.Y - 20);
                }
                else
                {
                    System.Windows.MessageBox.Show("Resurs sa ovom oznakom već postoji na mapi!", "Dodavanje resursa na mapu");
                }
            }
        }

        private void DraggedImageMouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed &&
               (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
               Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                Resurs selektovana = (Resurs)tree.SelectedValue;

                if (selektovana != null)
                {
                    Image img = sender as Image;
                    canvasMap.Children.Remove(img);
                    DataObject dragData = new DataObject("resurs", selektovana);
                    DragDrop.DoDragDrop(img, dragData, DragDropEffects.Move);
                }
            }
        }

        private void DropList_DragEnter(object sender, DragEventArgs e)
        {

            if (!e.Data.GetDataPresent("manifestacija") || sender == e.Source)
            {

                e.Effects = DragDropEffects.None;
            }
        }

        private void PrikazIkonice_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void DraggedImagePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
            Image img = sender as Image;

            foreach (Resurs man in MfList)
            {
                if (man.Oznaka.Equals(img.Tag))
                {
                    var tvi = FindTviFromObjectRecursive(tree, man);
                    if (tvi != null) tvi.IsSelected = true;
                    if (!man.Slika.Equals(""))
                        PrikazIkonice.Source = new BitmapImage(new Uri(man.Slika));
                    else
                        PrikazIkonice.Source = new BitmapImage(new Uri(man.Tip.Slika));

                }
            }
            if (e.LeftButton == MouseButtonState.Released)
                e.Handled = true;
        }

        private void DraggedImagePreviewMouseRightButtonDown(object sender, MouseEventArgs e)
        {
            startPoint = e.GetPosition(null);
            Image img = sender as Image;
            int i = 0;
            foreach (Resurs man in mfList)
            {
                if (man.Oznaka.Equals(img.Tag))
                {
                    var tvi = FindTviFromObjectRecursive(tree, man);
                    if (tvi != null) tvi.IsSelected = true;
                }
            }
        }

        public static TreeViewItem FindTviFromObjectRecursive(ItemsControl ic, object o)
        {
            try
            {
                //Search for the object model in first level children (recursively)
                TreeViewItem tvi = ic.ItemContainerGenerator.ContainerFromItem(o) as TreeViewItem;
                if (tvi != null) return tvi;
                //Loop through user object models
                foreach (object i in ic.Items)
                {
                    //Get the TreeViewItem associated with the iterated object model
                    TreeViewItem tvi2 = ic.ItemContainerGenerator.ContainerFromItem(i) as TreeViewItem;
                    tvi = FindTviFromObjectRecursive(tvi2, o);
                    if (tvi != null) return tvi;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void PrikazIkonice_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed &&
               (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
               Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                try
                {
                    Resurs selektovana = (Resurs)tree.SelectedValue;
                    if (selektovana != null)
                    {
                        DataObject dragData = new DataObject("resurs", selektovana);
                        DragDrop.DoDragDrop(PrikazIkonice, dragData, DragDropEffects.Move);
                    }
                }
                catch
                {
                    return;
                }
            }
        }

        private void tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tree.SelectedValue is Resurs)
            {
                Resurs m = (Resurs)tree.SelectedValue;
                if (!m.Slika.Equals(""))
                    PrikazIkonice.Source = new BitmapImage(new Uri(m.Slika));
                else
                    PrikazIkonice.Source = new BitmapImage(new Uri(m.Tip.Slika));

            }
            else { PrikazIkonice.Source = null; }

        }

        private void Ukloni_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Resurs selektovana = (Resurs)tree.SelectedItem;
                if (selektovana != null)
                {
                    bool postoji = canvasMap.Children.Cast<Image>()
                           .Any(x => x.Tag != null && x.Tag.ToString() == selektovana.Oznaka);
                    if (postoji)
                    {
                        Image img = null;
                        foreach (Image i in canvasMap.Children)
                        {
                            if (i.Tag.Equals(selektovana.Oznaka))
                                img = i;
                        }
                        canvasMap.Children.Remove(img);
                        int idx = 0;
                        foreach (Resurs m in mfList)
                        {
                            if (selektovana.Oznaka.Equals(m.Oznaka))
                                break;
                            idx++;
                        }


                        baza.Resursi[idx].X = -1;
                        baza.Resursi[idx].Y = -1;
                        baza.sacuvajResurs();
                        baza.ucitajResurse();
                        mfList = baza.Resursi;
                        ucitajSve();
                        tree.Items.Refresh();
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var resurs = new Dijalozi.resurs();
            resurs.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var tip = new Dijalozi.tip();
            tip.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var etiketa = new Dijalozi.etiketa();
            etiketa.ShowDialog();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            var izm_r = new Dijalozi.pregledResursa();
            izm_r.ShowDialog();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            var izm_t = new Dijalozi.pregledTipova();
            izm_t.ShowDialog();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            var izm_e = new Dijalozi.PregledEtiketa();
            izm_e.ShowDialog();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(null, @"./../../../Pomoc.chm");
        }

        private void Tutorijal_btn(object sender, RoutedEventArgs e)
        {
            var tuto = new Dijalozi.Tutorijal();
            tuto.ShowDialog();
        }

        
    }
}
