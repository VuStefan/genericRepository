using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lorum.Igre
{
    class ZandarTref : Igra
    {
        public ZandarTref(Igrac[] igraci, int pocetnigrac) : base(igraci, pocetnigrac) { }

        public override void DodeliPoene()
        {
            for (int i = 0; i < odigraneKarte.Length; i++)
            {
                if (odigraneKarte[i].Vrednost == Vrednost.Zandar && odigraneKarte[i].Boja == Boja.Tref)
                {
                    igraci[pocetniIgrac].TrenutniRezultat = 6;

                    int drugi = pocetniIgrac < 2 ? pocetniIgrac + 2 : pocetniIgrac - 2;
                    igraci[drugi].TrenutniRezultat = 2;
                }
            }
        }

        public override bool IgraZavrsena()
        {
            return ProsaoZandarTref();
        }

        private bool ProsaoZandarTref()
        {
            for (int i = 0; i < igraci.Length; i++)
            {
                for (int j = 0; j < igraci[trenutniIgrac].Ruka.Count; j++)
                {
                    if (igraci[i].Ruka[j].Vrednost == Vrednost.Zandar && igraci[i].Ruka[j].Boja == Boja.Tref)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
