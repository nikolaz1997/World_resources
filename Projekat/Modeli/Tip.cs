using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Model
{
    public class Tip
    {
        private string oznaka = "";
        private string naziv = "";
        private string opis = "";
        private string slika = "";

        public ObservableCollection<Resurs> resursi
        {
            get;
            set;
        }

        public Tip() { resursi = new ObservableCollection<Resurs>(); }

        public Tip(string o, string n, string op, string sl)
        {
            oznaka = o;
            naziv = n;
            opis = op;
            slika = sl;
            resursi = new ObservableCollection<Resurs>();
        }

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

        public void setAll(Tip t)
        {
            oznaka = t.oznaka;
            naziv = t.naziv;
            opis = t.opis;
            slika = t.slika;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public static implicit operator Tip(string v)
        {
            throw new NotImplementedException();
        }
    }
}
