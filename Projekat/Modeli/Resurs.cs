using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Model
{
    public class Resurs
    {
        private bool obnovljiv = false;
        private bool eksploatisan = false;
        private string frekvencija_pojavljivanja = "";
        private string mera = "";
        private string strateska_vaznost = "";
        private string cena = "";
        private DateTime datum = DateTime.Today;
        private string oznaka = "";
        private string naziv = "";
        private string opis = "";
        private Tip tip = new Tip();
        private ObservableCollection<Etiketa> etikete = new ObservableCollection<Etiketa>();
        private string slika = "";
        private double x = -1;
        private double y = -1;

        public string Oznaka
        {
            get
            {
                return oznaka;
            }
            set
            {
                if (value != oznaka)
                {
                    oznaka = value;
                    OnPropertyChanged("Oznaka");
                }
            }
        }

        public string Naziv
        {
            get
            {
                return naziv;
            }
            set
            {
                if (value != naziv)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }

        public string Opis
        {
            get
            {
                return opis;
            }
            set
            {
                if (value != opis)
                {
                    opis = value;
                    OnPropertyChanged("Opis");
                }
            }
        }

        public Tip Tip
        {
            get
            {
                return tip;
            }
            set
            {
                if (value != tip)
                {
                    tip = value;
                    OnPropertyChanged("Tip");
                }
            }
        }

        public bool Obnovljiv
        {
            get
            {
                return obnovljiv;
            }
            set
            {
                if(value != obnovljiv)
                {
                    obnovljiv = value;
                    OnPropertyChanged("Obnovljiv");
                }
            }
        }

        public bool Eksploatisan
        {
            get
            {
                return eksploatisan;
            }
            set
            {
                if (value != eksploatisan)
                {
                    eksploatisan = value;
                    OnPropertyChanged("Eksploatisan");
                }
            }
        }

        public string FrekvencijaPojavljivanja
        {
            get
            {
                return frekvencija_pojavljivanja;
            }
            set
            {
                if(value != frekvencija_pojavljivanja)
                {
                    frekvencija_pojavljivanja = value;
                    OnPropertyChanged("FrekvencijaPojavljivanja");
                }
            }
        }

        public string Mera
        {
            get
            {
                return mera;
            }
            set
            {
                if (value != mera)
                {
                    mera = value;
                    OnPropertyChanged("Mera");
                }
            }
        }

        public string StrateskaVaznost
        {
            get
            {
                return strateska_vaznost;
            }
            set
            {
                if (value != strateska_vaznost)
                {
                    strateska_vaznost = value;
                    OnPropertyChanged("StrateskaVaznost");
                }
            }
        }

        public string Cena
        {
            get
            {
                return cena;
            }
            set
            {
                if (value != cena)
                {
                    cena = value;
                    OnPropertyChanged("Cena");
                }
            }
        }

        public DateTime Datum
        {
            get
            {
                return datum;
            }
            set
            {
                if (value != datum)
                {
                    datum = value;
                    OnPropertyChanged("Datum");
                }
            }
        }

        public ObservableCollection<Etiketa> Etikete
        {
            get
            {
                return etikete;
            }
            set
            {
                if (value != etikete)
                {
                    etikete = value;
                    OnPropertyChanged("Etikete");
                }
            }
        }

        public string Slika
        {
            get
            {
                return slika;
            }
            set
            {
                if (value != slika)
                {
                    slika = value;
                    OnPropertyChanged("Slika");
                }
            }
        }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                if (value != x)
                {
                    x = value;
                    OnPropertyChanged("X");
                }
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                if (value != y)
                {
                    y = value;
                    OnPropertyChanged("Y");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Resurs() { }
        public Resurs(string o, string n, string op, Tip t, bool obnv, bool ekspl, string c, string frekv, string m, string strvaz, string sl, DateTime dat, ObservableCollection<Etiketa> etk)
        {
            this.oznaka = o;
            this.naziv = n;
            this.opis = op;
            this.tip = t;
            this.obnovljiv = obnv;
            this.eksploatisan = ekspl;
            this.cena = c;
            this.frekvencija_pojavljivanja = frekv;
            this.mera = m;
            this.strateska_vaznost = strvaz;
            this.slika = sl;
            this.datum = dat;
            this.etikete = etk;
        }

        public void setAll(Resurs res)
        {
            oznaka = res.oznaka;
            naziv = res.naziv;
            opis = res.opis;
            tip = res.tip;
            obnovljiv = res.obnovljiv;
            eksploatisan = res.eksploatisan;
            cena = res.cena;
            frekvencija_pojavljivanja = res.frekvencija_pojavljivanja;
            mera = res.mera;
            strateska_vaznost = res.strateska_vaznost;
            slika = res.slika;
            datum = res.datum;
            etikete = res.etikete;
            x = res.x;
            y = res.y;
        }

        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public bool addEtiketa(Etiketa e)
        {
            foreach (Etiketa e1 in etikete)
            {
                if (e1.Oznaka == e.Oznaka)
                {
                    etikete.Remove(e);
                    return true;
                }
            }

            return false;
        }
    }
}
