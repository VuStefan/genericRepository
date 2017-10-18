using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Lorum
{
    // boja is the suit of the cards in the respective order of clubs, diamond, spades and hearts.
    // vrednost is the value of a card, ordered from seven to ace.
    public enum Boja
    {
        Tref,
        Karo,
        Pik,
        Herc
    }

    public enum Vrednost
    {
        Sedam,
        Osam,
        Devet,
        Deset,
        Zandar,
        Dama,
        Kralj,
        As
    }

    // all posibilites encounterd when comparing two cards.. Bigger, Smaller, Different Suit
    public enum PoredjenjeKarata
    {
        Veca,
        Manja,
        RazlicitaBoja
    }

    // comparable class card
    // each card has a suit, value, a image, bool wheither to show the card
    // and a rectangle later used for checking if the user clicked on it
    public class Karta : IComparable<Karta>
    {
        private Boja boja;
        private Vrednost vrednost;
        private Bitmap slika;
        private bool prikazi;
        private Rectangle rectangle;

        public Karta(Boja boja, Vrednost vrednost, Bitmap slika)
        {
            this.boja = boja;
            this.vrednost = vrednost;
            this.slika = slika;
        }

        public Boja Boja
        {
            get { return boja; }
        }

        public Vrednost Vrednost
        {
            get { return vrednost; }
        }

        public Bitmap Slika
        {
            get { return slika; }
        }

        public bool Prikazi
        {
            get { return prikazi; }
            set { prikazi = value; }
        }

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        // compares the cards
        public static PoredjenjeKarata Uporedi(Karta karta1, Karta karta2)
        {
            if (karta1.boja == karta2.boja)
            {
                if (karta1.Vrednost > karta2.Vrednost)
                {
                    return PoredjenjeKarata.Veca;
                }
                return PoredjenjeKarata.Manja;
            }
            return PoredjenjeKarata.RazlicitaBoja;
        }

        public int CompareTo(Karta other)
        {
            if (this.Boja > other.Boja)
            {
                return 1;
            }
            
            if (this.Boja < other.Boja)
            {
                return -1;
            }

            if (this.Vrednost > other.Vrednost)
            {
                return 1;
            }

            if (this.Vrednost < other.Vrednost)
            {
                return -1;
            }

            return 0;
        }
    }
}
