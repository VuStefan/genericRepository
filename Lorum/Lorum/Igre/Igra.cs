using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lorum.Igre
{
    // all game names
    public enum NazivIgre
    {
        StoVise,
        StoManje,
        ZandarTref,
        Dame,
        KraljHerc,
        Hercevi
    }

    // abstract class game
    // each game will have player, starting player, number of played card, played hands, current player,
    // and a list of played cards
    public abstract class Igra
    {
        protected Igrac[] igraci;
        protected int pocetniIgrac;
        protected int brojOdigranjihKarata;
        protected int odigraneRuke;
        protected int trenutniIgrac;
        protected Karta[] odigraneKarte;

        public Karta[] OdigraneKarte
        {
            get { return odigraneKarte; }
        }

        public int PocetniIgrac
        {
            get { return pocetniIgrac; }
        }

        public int TrenutnIgrac
        {
            get { return trenutniIgrac; }
        }

        public Igra(Igrac[] igraci, int pocetnIgrac)
        {
            this.igraci = igraci;
            trenutniIgrac = this.pocetniIgrac = pocetnIgrac;
            brojOdigranjihKarata = 0;
            odigraneKarte = new Karta[4];
            odigraneRuke = 0;
        }

        // validates card
        public string ValidirajKartu(int broj)
        {
            Karta karta = igraci[trenutniIgrac].Ruka[broj];

            if (Validiraj(karta))
            {
                karta.Prikazi = true;
                return string.Empty;
            }

            return "Odigraj " + odigraneKarte[pocetniIgrac].Boja;
        }

        private bool Validiraj(Karta karta)
        {
            if (brojOdigranjihKarata == 0)
            {
                return true;
            }

            if (Karta.Uporedi(odigraneKarte[pocetniIgrac], karta) != PoredjenjeKarata.RazlicitaBoja)
            {
                return true;
            }

            for (int i = 0; i < igraci[trenutniIgrac].Ruka.Count; i++)
            {
                if (Karta.Uporedi(odigraneKarte[pocetniIgrac], igraci[trenutniIgrac].Ruka[i]) != PoredjenjeKarata.RazlicitaBoja)
                {
                    return false;
                }
            }

            return true;
        }

        // plays the card
        public void OdigrajKartu()
        {
            for (int i = 0; i < igraci[trenutniIgrac].Ruka.Count; i++)
            {
                if (igraci[trenutniIgrac].Ruka[i].Prikazi)
                {
                    odigraneKarte[trenutniIgrac] = igraci[trenutniIgrac].BaciKartu(i);
                    brojOdigranjihKarata++;
                    return;
                }
            }
        }

        // checks if the game is over
        public bool Zavrsena()
        {
            odigraneRuke++;

            if (IgraZavrsena())
            {
                odigraneRuke = 0;

                for (int i = 0; i < 4; i++)
                {
                    igraci[i].UkupniRezultat += igraci[i].TrenutniRezultat;
                }

                return true;
            }

            return false;
        }

        public abstract bool IgraZavrsena();

        // current player throws card
        public virtual int TrenutnIgracBacaKartu()
        {
            Karta prvaKarta = odigraneKarte[pocetniIgrac];
            Karta najvecaKarta = NajvecaKartaNaStolu(odigraneKarte, pocetniIgrac);

            if (prvaKarta == null)
            {
                return NajmanjaKarta(igraci[trenutniIgrac]);
            }

            int najvecaManja = NajvecaManjaKarta(igraci[trenutniIgrac], odigraneKarte, pocetniIgrac);

            if (najvecaManja == -1)
            {
                int najveca = NajvecaKartaUBoji(igraci[trenutniIgrac], prvaKarta.Boja);

                if (najveca == -1)
                {
                    return NajvecaKarta(igraci[trenutniIgrac]);
                }
                else
                {
                    return najveca;
                }
            }
            else
            {
                return najvecaManja;
            }
        }

        public abstract void DodeliPoene();

        // check end of hand
        public bool ProveriKrajRuke()
        {
            if (brojOdigranjihKarata == 4)
            {
                trenutniIgrac = pocetniIgrac = ProveriKoNosi();
                DodeliPoene();
                return true;
            }

            trenutniIgrac = trenutniIgrac == 3 ? 0 : trenutniIgrac + 1;
            return false;
        }

        // check who wins
        private int ProveriKoNosi()
        {
            Karta najjaca = odigraneKarte[pocetniIgrac];
            int koNosi = pocetniIgrac;

            for (int i = 0; i < 4; i++)
            {
                if (Karta.Uporedi(odigraneKarte[i], najjaca) == PoredjenjeKarata.Veca)
                {
                    najjaca = odigraneKarte[i];
                    koNosi = i;
                }
            }

            return koNosi;
        }

        // next round
        public void SledecaRuka()
        {
            odigraneKarte = new Karta[4];
            brojOdigranjihKarata = 0;
        }

        // who played the smallest card
        public int NajmanjaKarta(Igrac igrac)
        {
            int index = 0;
            Karta min = igrac.Ruka[index];

            for (int i = 1; i < igrac.Ruka.Count; i++)
            {
                if (igrac.Ruka[i].Vrednost < min.Vrednost)
                {
                    min = igrac.Ruka[i];
                    index = i;
                }
            }

            return index;
        }

        // smallest card in the correct suit
        public int NajmanjaKartaUBoji(Igrac igrac, Boja boja)
        {
            int index = -1;
            Karta min = null;

            for (int i = 0; i < igrac.Ruka.Count; i++)
            {
                if (igrac.Ruka[i].Boja == boja)
                {
                    if (min == null)
                    {
                        min = igrac.Ruka[i];
                        index = i;
                    }
                    else if (igrac.Ruka[i].Vrednost < min.Vrednost)
                    {
                        min = igrac.Ruka[i];
                        index = i;
                    }
                }
            }

            return index;
        }

        // who played the biggest card
        public int NajvecaKarta(Igrac igrac)
        {
            int index = 0;
            Karta max = igrac.Ruka[index];

            for (int i = 1; i < igrac.Ruka.Count; i++)
            {
                if (igrac.Ruka[i].Vrednost > max.Vrednost)
                {
                    max = igrac.Ruka[i];
                    index = i;
                }
            }

            return index;
        }

        // biggest card in the corresponding suit
        public int NajvecaKartaUBoji(Igrac igrac, Boja boja)
        {
            int index = -1;
            Karta max = null;

            for (int i = 0; i < igrac.Ruka.Count; i++)
            {
                if (igrac.Ruka[i].Boja == boja)
                {
                    if (max == null)
                    {
                        max = igrac.Ruka[i];
                        index = i;
                    }
                    else if (igrac.Ruka[i].Vrednost > max.Vrednost)
                    {
                        max = igrac.Ruka[i];
                        index = i;
                    }
                }
            }

            return index;
        }

        public int NajvecaManjaKarta(Igrac igrac, Karta[] odigraneKarte, int prvaBacenaKarta)
        {
            int index = -1;
            Karta max = null;
            Karta highest = NajvecaKartaNaStolu(odigraneKarte, prvaBacenaKarta);

            for (int i = 0; i < igrac.Ruka.Count; i++)
            {
                if (igrac.Ruka[i].Boja == highest.Boja)
                {
                    if (max == null && igrac.Ruka[i].Vrednost < highest.Vrednost)
                    {
                        max = igrac.Ruka[i];
                        index = i;
                    }
                    else if (max != null && igrac.Ruka[i].Vrednost > max.Vrednost && igrac.Ruka[i].Vrednost < highest.Vrednost)
                    {
                        max = igrac.Ruka[i];
                        index = i;
                    }
                }
            }

            return index;
        }

        //number of played cards
        public int BrojKarataNaStolu(Karta[] odigraneKarte)
        {
            int count = 0;

            for (int i = 0; i < odigraneKarte.Length; i++)
            {
                if (odigraneKarte[i] != null)
                {
                    count++;
                }
            }

            return count;
        }

        // Biggest played card
        public Karta NajvecaKartaNaStolu(Karta[] odigraneKarte, int prvaBacenaKarta)
        {
            Karta max = odigraneKarte[prvaBacenaKarta];

            for (int i = 0; i < odigraneKarte.Length; i++)
            {
                Karta card = odigraneKarte[i];

                if (card != null && card.Boja == max.Boja && card.Vrednost > max.Vrednost)
                {
                    max = card;
                }
            }

            return max;
        }
    }
}
