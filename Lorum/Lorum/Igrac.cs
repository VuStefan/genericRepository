using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lorum
{
    public class Igrac
    {
        /* class player
        each player has a list of cards/his hand
        a name and his total and current score.
        */
        private List<Karta> ruka;
        private string ime;                
        private int ukupniRezultat;
        private int trenutniRezultat;

        public Igrac(string ime)
        {
            ruka = new List<Karta>(8);
            this.ime = ime;
        }

        // add a card
        public void DodajKartu(Karta karta)
        {
            ruka.Add(karta);
        }

        // throws a card
        public Karta BaciKartu(int index)
        {
            Karta k = ruka[index];
            ruka.RemoveAt(index);
            return k;
        }

        // players hand
        public List<Karta> Ruka
        {
            get { return ruka; }
        }

        // player name
        public string Ime
        {
            get { return ime; }
        }

        // current score
        public int TrenutniRezultat
        {
            get { return trenutniRezultat; }
            set { trenutniRezultat = value; }
         }

        // total score
        public int UkupniRezultat
        {
            get { return ukupniRezultat; }
            set { ukupniRezultat = value; }
        }
        
        // sorts his hand for a cleaner view at your hand
        public void Sortiraj()
        {
            ruka.Sort();
        }
    }
}
