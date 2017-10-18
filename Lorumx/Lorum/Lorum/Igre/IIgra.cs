using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaturskiRad.Igre
{
    public enum NazivIgre
    {
        StoVise,
        StoManje,
        ZandarTref,
        Dame,
        KraljHerc,
        Hercevi
    }

    public interface IIgra
    {
        string ValidirajKartu(int broj);

        void OdigrajKartu();

        Karta[] OdigraneKarte { get; }

        bool Zavrsena();

        bool ProveriKrajRuke();

        int TrenutniIgrac { get; }

        void SledecaRuka();

        int TrenutniIgracBacaKartu();
    }
}
