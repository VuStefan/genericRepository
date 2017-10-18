using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lorum.Igre
{
    class StoManje : Igra
    {
        public StoManje(Igrac[] igraci, int pocetniIgrac) : base(igraci, pocetniIgrac) { }

        public override void DodeliPoene()
        {
            igraci[pocetniIgrac].TrenutniRezultat++;
        }

        public override bool IgraZavrsena()
        {
            return odigraneRuke == 8;
        }
    }
}
