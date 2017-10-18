using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Lorum.Igre
{
    public class StoVise : Igra
    {
        public StoVise(Igrac[] igraci, int pocetniIgrac) : base(igraci, pocetniIgrac) { }

        public override void DodeliPoene()
        {
            igraci[pocetniIgrac].TrenutniRezultat--;
        }

        public override bool IgraZavrsena()
        {
            return odigraneRuke == 8;
        }

        public override int TrenutnIgracBacaKartu()
        {
            Karta prvaKarta = odigraneKarte[pocetniIgrac];
            Karta najvecaKarta = NajvecaKartaNaStolu(odigraneKarte, pocetniIgrac);

            if (prvaKarta == null)
            {
                return NajvecaKarta(igraci[trenutniIgrac]);
            }

            int najveca = NajvecaKartaUBoji(igraci[trenutniIgrac], prvaKarta.Boja);

            if (najveca == -1)
            {
                return NajmanjaKarta(igraci[trenutniIgrac]);
            }
            else
            {
                if (igraci[trenutniIgrac].Ruka[najveca].Vrednost < najvecaKarta.Vrednost)
                {
                    return NajmanjaKartaUBoji(igraci[trenutniIgrac], prvaKarta.Boja);
                }

                return najveca;
            }
        }

    }
}
