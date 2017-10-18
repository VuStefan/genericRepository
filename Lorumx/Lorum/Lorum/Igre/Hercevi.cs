using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Lorum.Igre
{
    public class Hercevi : Igra
    {
        public Hercevi(Igrac[] igraci, int pocetniIgrac) : base(igraci, pocetniIgrac) { }

        public override void DodeliPoene()
        {
            int rez = 0;

            for (int i = 0; i < odigraneKarte.Length; i++)
            {
                if (odigraneKarte[i].Boja == Boja.Herc)
                {
                    rez ++;
                }
            }

            igraci[pocetniIgrac].TrenutniRezultat += rez;

            if (igraci[pocetniIgrac].TrenutniRezultat == 8)
            {
                igraci[pocetniIgrac].TrenutniRezultat = -8;
            }
        }

        public override bool IgraZavrsena()
        {
            return ProsliHercevi();
        }

        private bool ProsliHercevi()
        {
            for (int i = 0; i < igraci.Length; i++)
            {
                for (int j = 0; j < igraci[trenutniIgrac].Ruka.Count; j++)
                {
                    if (igraci[i].Ruka[j].Boja == Boja.Herc)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
