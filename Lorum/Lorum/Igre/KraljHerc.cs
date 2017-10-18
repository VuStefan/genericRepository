using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lorum.Igre
{
    class KraljHerc : Igra
    {
        public KraljHerc(Igrac[] igraci, int pocetnigrac) : base(igraci, pocetnigrac) { }

        public override void DodeliPoene()
        {
            int rez = 0;

            for (int i = 0; i < odigraneKarte.Length; i++)
            {
                if (odigraneKarte[i].Vrednost == Vrednost.Kralj && odigraneKarte[i].Boja == Boja.Herc)
                {
                    rez += 4;
                }
            }

            if (odigraneRuke == 7)
            {
                rez += 4;
            }

            igraci[pocetniIgrac].TrenutniRezultat += rez;
        }

        public override bool IgraZavrsena()
        {
            return odigraneRuke == 8;
        }
    }
    
}
