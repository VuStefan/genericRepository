using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Drawing;

namespace Lorum
{
    public class Spil
    {
        // deck
        // each deck had a list of cards and their indexes
        // the random is used to shuffle their indexes, or rather shuffling the deck
        private List<int> indeksi;
        private List<Karta> karte;
        private Random rnd = new Random();
        private Bitmap pozadina;

        public Spil()
        {
            karte = new List<Karta>(32);

            ResourceManager rm = new ResourceManager("Lorum.Resource1", typeof(Resource1).Assembly);
            pozadina = (Bitmap)rm.GetObject("Pozadina");

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    string naziv = Enum.GetName(typeof(Boja), i) + Enum.GetName(typeof(Vrednost), j);
                    Bitmap slika = (Bitmap)rm.GetObject(naziv);
                    karte.Add(new Karta((Boja)i, (Vrednost)j, slika));
                }
            }

            ResetujIndekse();
        }

        public Bitmap Pozadina
        {
            get { return pozadina; }
        }

        //shuffle
        public void Promesaj()
        {
            ResetujIndekse();
        }
        //resets the indexs
        private void ResetujIndekse()
        {
            indeksi = new List<int>(32);

            for (int i = 0; i < 32; i++)
            {
                indeksi.Add(i);
                karte[i].Prikazi = false;
            }
        }
        // deals a card
        public Karta PodeliKartu()
        {
            if (indeksi.Count == 0)
            {
                return null;
            }

            int sindex = indeksi[rnd.Next(indeksi.Count)];
            Karta pkarta = karte[sindex];
            indeksi.Remove(sindex);
            return pkarta;
        }
    }
}
