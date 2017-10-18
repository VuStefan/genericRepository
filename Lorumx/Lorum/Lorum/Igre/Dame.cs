using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lorum.Igre
{
    public class Dame : Igra
    {
        public Dame(Igrac[] igraci, int pocetnigrac) : base(igraci, pocetnigrac) { }

        public override void DodeliPoene()
        {
            int rez = 0;

            for (int i = 0; i < odigraneKarte.Length; i++)
            {
                if (odigraneKarte[i].Vrednost == Vrednost.Dama)
                {
                    rez += 2;
                }
            }

            igraci[pocetniIgrac].TrenutniRezultat += rez;
        }

        public override bool IgraZavrsena()
        {
            return ProsleDame();
        }

        private bool ProsleDame()
        {
            for (int i = 0; i < igraci.Length; i++)
            {
                for (int j = 0; j < igraci[trenutniIgrac].Ruka.Count; j++)
                {
                    if (igraci[i].Ruka[j].Vrednost == Vrednost.Dama)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        //public override int TrenutniigracBacaKartu()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
