using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Model
{
    class DataBase
    {
        private ObservableCollection<Resurs> resursi = new ObservableCollection<Resurs>();
        private ObservableCollection<Etiketa> etikete = new ObservableCollection<Etiketa>();
        private ObservableCollection<Tip> tipovi = new ObservableCollection<Tip>();

        private string putanjaResursa = null;
        private string putanjaEtiketa = null;
        private string putanjaTipova = null;

        public DataBase()
        {
            putanjaResursa = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resursi.txt");
            putanjaEtiketa = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Etikete.txt");
            putanjaTipova = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tipovi.txt");

            ucitajResurse();
            ucitajEtikete();
            ucitajTipove();
            sacuvajResurs();
        }

        public void ucitajEtikete()
        {
            if (File.Exists(putanjaEtiketa))
            {

                using (StreamReader reader = File.OpenText(putanjaEtiketa))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    etikete = (ObservableCollection<Etiketa>)serializer.Deserialize(reader, typeof(ObservableCollection<Etiketa>));
                }
            }
            else
            {
                etikete = new ObservableCollection<Etiketa>();
            }
        }

        public void ucitajResurse()
        {
            if (File.Exists(putanjaResursa))
            {

                using (StreamReader reader = File.OpenText(putanjaResursa))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    resursi = (ObservableCollection<Resurs>)serializer.Deserialize(reader, typeof(ObservableCollection<Resurs>));
                }

            }
            else
            {
                resursi = new ObservableCollection<Resurs>();
            }
        }

        public void ucitajTipove()
        {
            if (File.Exists(putanjaTipova))
            {

                using (StreamReader reader = File.OpenText(putanjaTipova))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    tipovi = (ObservableCollection<Tip>)serializer.Deserialize(reader, typeof(ObservableCollection<Tip>));
                }
            }
            else
            {
                tipovi = new ObservableCollection<Tip>();
            }
        }

        public void sacuvaj()
        {
            using (StreamWriter writer = File.CreateText(putanjaResursa))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, resursi);
                writer.Close();
            }

            using (StreamWriter writer = File.CreateText(putanjaEtiketa))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, etikete);
                writer.Close();
            }

            using (StreamWriter writer = File.CreateText(putanjaTipova))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, tipovi);
                writer.Close();
            }
        }

        public void sacuvajResurs()
        {
            using (StreamWriter writer = File.CreateText(putanjaResursa))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, resursi);
                writer.Close();
            }
        }

        public void sacuvajEtiketu()
        {
            using (StreamWriter writer = File.CreateText(putanjaEtiketa))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, etikete);
                writer.Close();
            }
        }   

        public void sacuvajTip()
        {
            using (StreamWriter writer = File.CreateText(putanjaTipova))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, tipovi);
                writer.Close();
            }
        }

        public bool noviResurs(Resurs r)
        {
            foreach (Resurs resurs1 in resursi)
            {
                if (resurs1.Oznaka == r.Oznaka)
                {
                    return false;
                }
            }
            resursi.Add(r);
            sacuvajResurs();

            return true;
        }

        public bool novaEtiketa(Etiketa e)
        {
            foreach (Etiketa etik1 in etikete)
            {
                if (etik1.Oznaka == e.Oznaka)
                {
                    return false;
                }
            }
            etikete.Add(e);
            sacuvajEtiketu();
            return true;
        }

        public bool novTip(Tip t)
        {

            foreach (Tip tip1 in tipovi)
            {
                if (tip1.Oznaka == t.Oznaka)
                {

                    return false;
                }
            }
            tipovi.Add(t);
            sacuvajTip();

            return true;
        }

        public bool brisanjeResursa(Resurs r)
        {

            foreach (Resurs r2 in resursi)
            {
                if (r2.Oznaka == r.Oznaka)
                {
                    resursi.Remove(r);
                    sacuvajResurs();

                    return true;
                }
            }

            return false;
        }
        public bool brisanjeEtikete(Etiketa e)
        {

            foreach (Etiketa etik2 in etikete)
            {
                if (etik2.Oznaka == e.Oznaka)
                {
                    etikete.Remove(e);
                    sacuvajEtiketu();
                    return true;
                }
            }

            return false;
        }

        public bool brisanjeTipa(Tip t)
        {

            foreach (Tip tip2 in tipovi)
            {
                if (tip2.Oznaka == t.Oznaka)
                {
                    tipovi.Remove(t);
                    sacuvajTip();
                    return true;
                }
            }

            return false;
        }

        public ObservableCollection<Resurs> Resursi
        {
            get { return resursi; }
            set { resursi = value; }
        }

        public ObservableCollection<Etiketa> Etikete
        {

            get { return etikete; }
            set { etikete = value; }
        }

        public ObservableCollection<Tip> Tipovi
        {
            get { return tipovi; }
            set { tipovi = value; }
        }
    }
}
