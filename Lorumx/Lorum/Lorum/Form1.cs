using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using Lorum.Igre;

namespace Lorum
{
    public partial class Form1 : Form
    {
        // deck, player, game name, current game, list of played cards, point of cards that are played
        // number of steps, game, first game, result
        private Spil spil;
        private Igrac[] igraci;
        private NazivIgre nazivIgre;
        private Igra trenutnaIgra;
        private Karta[] odigraneKarte;
        private Point odigraneKarte0;
        private Point odigraneKarte1;
        private Point odigraneKarte2;
        private Point odigraneKarte3;
        private int brojKoraka;
        private int igra;
        private int prviIgra = 0;
        private TabelaRezultata rezultat;

        //used for testing, ubrzanje is the speed of the cards moving
        private bool testMod = false;
        private int ubrzanje = 10;

        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(800, 560);
            this.DoubleBuffered = true;

            Inicijalizuj();

            if (testMod)
            {
                Ubrzaj();
            }            

            rezultat = new TabelaRezultata(igraci);
            nazivIgre = NazivIgre.StoVise;
            PocniIgru(nazivIgre);
        }

        private void Ubrzaj()
        {
            timer1.Interval /= ubrzanje;
            timer2.Interval /= ubrzanje;
            timer3.Interval /= ubrzanje;
            timer4.Interval /= ubrzanje;
        }

        // initilizes a new deck, new game, new players
        private void Inicijalizuj()
        {
            spil = new Spil();
            igra = 0;

            igraci = new Igrac[4];
            igraci[0] = new Igrac("Ja");
            igraci[1] = new Igrac("Pera");
            igraci[2] = new Igrac("Mika");
            igraci[3] = new Igrac("Laza");
        }

        // starts a game
        private bool PocniIgru(NazivIgre naziv)
        {
            if (ProveriKraj())
            {
                return false;
            }

            spil.Promesaj();

            for (int i = 0; i < 4; i++)
            {
                igraci[i].Ruka.Clear();

                for (int j = 0; j < 8; j++)
                {
                    igraci[i].DodajKartu(spil.PodeliKartu());
                }

                igraci[i].Sortiraj();
            }

            // sets the current game
            switch (naziv)
            {
                case NazivIgre.StoVise:
                    trenutnaIgra = new StoVise(igraci, prviIgra);
                    break;
                case NazivIgre.StoManje:
                    trenutnaIgra = new StoManje(igraci, prviIgra);
                    break;
                case NazivIgre.ZandarTref:
                    trenutnaIgra = new ZandarTref(igraci, prviIgra);
                    break;
                case NazivIgre.Dame:
                    trenutnaIgra = new Dame(igraci, prviIgra);
                    break;
                case NazivIgre.KraljHerc:
                    trenutnaIgra = new KraljHerc(igraci, prviIgra);
                    break;
                case NazivIgre.Hercevi:
                    trenutnaIgra = new Hercevi(igraci, prviIgra);
                    prviIgra++;
                    break;
            }

            return true;
        }

        // checks if its the end of all games and gives the player the option to play anew or exit
        private bool ProveriKraj()
        {
            if (prviIgra == 4)
            {
                DialogResult result = MessageBox.Show(this, "Kraj igre. Zelite li da igrate novu?", "Lorum", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                this.rezultat.Close();
                this.Close();

                if (result == DialogResult.No)
                {
                    Application.Exit();
                }
                else
                {
                    Application.Restart();
                }

                return true;
            }

            return false;
        }

        // player can check the result table at any time pressing the key r
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'r' || e.KeyChar == 'R')
            {
                PrikaziRezultat();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        // all the card drawing goes here
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int clientSizeHeight = this.ClientSize.Height - statusStrip1.Height;

            e.Graphics.DrawString(nazivIgre.ToString(), SystemFonts.DefaultFont, Brushes.White, 20, 20);

            e.Graphics.DrawString(igraci[0].Ime, SystemFonts.DefaultFont, Brushes.White, (this.ClientSize.Width - 277) / 2 - igraci[0].Ime.Length * 10, clientSizeHeight - 115);
            e.Graphics.DrawString(igraci[1].Ime, SystemFonts.DefaultFont, Brushes.White, this.ClientSize.Width - 87, (clientSizeHeight - 305) / 2 - 20);
            e.Graphics.DrawString(igraci[2].Ime, SystemFonts.DefaultFont, Brushes.White, (this.ClientSize.Width - 277) / 2 - igraci[2].Ime.Length * 10, 20);
            e.Graphics.DrawString(igraci[3].Ime, SystemFonts.DefaultFont, Brushes.White, 20, (clientSizeHeight - 305) / 2 - 20);

            for (int j = 0; j < igraci[0].Ruka.Count; j++)
            {
                int x = (this.ClientSize.Width - 277) / 2 + j * 30;
                int y = clientSizeHeight - 115;
                int width = 30;

                e.Graphics.DrawImage(igraci[0].Ruka[j].Slika, x, y);

                if (j == igraci[0].Ruka.Count - 1)
                {
                    width = igraci[0].Ruka[j].Slika.Width;
                }

                igraci[0].Ruka[j].Rectangle = new Rectangle(x, y, width, igraci[0].Ruka[j].Slika.Height);
            }

            for (int j = 0; j < igraci[1].Ruka.Count; j++)
            {
                if (igraci[1].Ruka[j].Prikazi)
                {
                    e.Graphics.DrawImage(igraci[1].Ruka[j].Slika, this.ClientSize.Width - 87, (clientSizeHeight - 305) / 2 + j * 30);
                }
                else
                {
                    e.Graphics.DrawImage(spil.Pozadina, this.ClientSize.Width - 87, (clientSizeHeight - 305) / 2 + j * 30);
                }
            }

            for (int j = 0; j < igraci[2].Ruka.Count; j++)
            {
                if (igraci[2].Ruka[j].Prikazi)
                {
                    e.Graphics.DrawImage(igraci[2].Ruka[j].Slika, (this.ClientSize.Width - 277) / 2 + j * 30, 20);
                }
                else
                {
                    e.Graphics.DrawImage(spil.Pozadina, (this.ClientSize.Width - 277) / 2 + j * 30, 20);
                }
            }

            for (int j = 0; j < igraci[3].Ruka.Count; j++)
            {
                if (igraci[3].Ruka[j].Prikazi)
                {
                    e.Graphics.DrawImage(igraci[3].Ruka[j].Slika, 20, (clientSizeHeight - 305) / 2 + j * 30);
                }
                else
                {
                    e.Graphics.DrawImage(spil.Pozadina, 20, (clientSizeHeight - 305) / 2 + j * 30);
                }
            }

            if (timer2.Enabled)
            {
                e.Graphics.DrawImage(odigraneKarte[0].Slika, odigraneKarte0);
                e.Graphics.DrawImage(odigraneKarte[1].Slika, odigraneKarte1);
                e.Graphics.DrawImage(odigraneKarte[2].Slika, odigraneKarte2);
                e.Graphics.DrawImage(odigraneKarte[3].Slika, odigraneKarte3);
            }
            else
            {
                //1
                Karta karta = trenutnaIgra.OdigraneKarte[0];
                if (karta != null)
                {
                    e.Graphics.DrawImage(karta.Slika, (this.ClientSize.Width - 67) / 2, clientSizeHeight / 2 + 5);
                }

                //2
                karta = trenutnaIgra.OdigraneKarte[1];
                if (karta != null)
                {
                    e.Graphics.DrawImage(karta.Slika, this.ClientSize.Width / 2 + 23, (clientSizeHeight - 95) / 2);
                }

                //3
                karta = trenutnaIgra.OdigraneKarte[2];
                if (karta != null)
                {
                    e.Graphics.DrawImage(karta.Slika, (this.ClientSize.Width - 67) / 2, clientSizeHeight / 2 - 100);
                }

                //4
                karta = trenutnaIgra.OdigraneKarte[3];
                if (karta != null)
                {
                    e.Graphics.DrawImage(karta.Slika, this.ClientSize.Width / 2 - 90, (clientSizeHeight - 95) / 2);
                }
            }
        }

        // all card movement starts here
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            PocetnePozicije();
            brojKoraka = 20;
            timer2.Start();
            this.Invalidate();
        }

        private void PocetnePozicije()
        {
            int clientSizeHeight = this.ClientSize.Height - statusStrip1.Height;

            odigraneKarte0 = new Point((this.ClientSize.Width - 67) / 2, clientSizeHeight / 2 + 5);
            odigraneKarte1 = new Point(this.ClientSize.Width / 2 + 23, (clientSizeHeight - 95) / 2);
            odigraneKarte2 = new Point((this.ClientSize.Width - 67) / 2, clientSizeHeight / 2 - 100);
            odigraneKarte3 = new Point(this.ClientSize.Width / 2 - 90, (clientSizeHeight - 95) / 2);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int stepX = ClientRectangle.Width / 20 + 2;
            int stepY = ClientRectangle.Height / 20 + 2;

            switch (trenutnaIgra.TrenutnIgrac)
            {
                case 0:
                    odigraneKarte0.Y += stepY;
                    odigraneKarte1.Y += stepY;
                    odigraneKarte2.Y += stepY;
                    odigraneKarte3.Y += stepY;
                    break;
                case 1:
                    odigraneKarte0.X += stepX;
                    odigraneKarte1.X += stepX;
                    odigraneKarte2.X += stepX;
                    odigraneKarte3.X += stepX;
                    break;
                case 2:
                    odigraneKarte0.Y -= stepY;
                    odigraneKarte1.Y -= stepY;
                    odigraneKarte2.Y -= stepY;
                    odigraneKarte3.Y -= stepY;
                    break;
                case 3:
                    odigraneKarte0.X -= stepX;
                    odigraneKarte1.X -= stepX;
                    odigraneKarte2.X -= stepX;
                    odigraneKarte3.X -= stepX;
                    break;
            }

            Invalidate();

            if (brojKoraka-- == 0)
            {
                timer2.Stop();

                trenutnaIgra.SledecaRuka();

                if (trenutnaIgra.Zavrsena())
                {
                    rezultat.Ispisi(nazivIgre.ToString(), igra);
                    PrikaziRezultat();

                    for (int i = 0; i < 4; i++)
                    {
                        igraci[i].TrenutniRezultat = 0;
                    }

                    if (nazivIgre == NazivIgre.Hercevi)
                    {
                        nazivIgre = NazivIgre.StoVise;
                    }
                    else
                    {
                        nazivIgre++;
                    }

                    if (!PocniIgru(nazivIgre))
                    {
                        return;
                    }

                    igra++;

                    Invalidate();
                }

                if (trenutnaIgra.TrenutnIgrac == 0 && !testMod)
                {
                    return;
                }

                int brojKarte = trenutnaIgra.TrenutnIgracBacaKartu();
                this.Odigraj(brojKarte);
            }
        }

        // table of results
        private void PrikaziRezultat()
        {
            rezultat.Visible = false;
            rezultat.Visible = true;
            rezultat.Location = new Point(this.Location.X + this.Width, this.Location.Y);
        }

        // plays a card you have clicked
        // limited to your hand only
        // if any of the timers are running/animations of card moving it doesnt allow the click to go through
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (timer1.Enabled || timer2.Enabled)
            {
                return;
            }

            for (int i = 0; i < igraci[0].Ruka.Count; i++)
            {
                if (igraci[0].Ruka[i].Rectangle.Contains(e.X, e.Y))
                {
                    this.Odigraj(i);
                }
            }
        }

        // plays your card
        private void Odigraj(int redniBrojKarte)
        {
            string s = trenutnaIgra.ValidirajKartu(redniBrojKarte);
            statusLabel1.Text = s;
            if (s != string.Empty)
            {
                return;
            }

            if (trenutnaIgra.TrenutnIgrac == 0)
            {
                BaciKartu();
            }
            else
            {
                timer3.Start();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Stop();
            BaciKartu();
        }

        private void BaciKartu()
        {
            trenutnaIgra.OdigrajKartu();
            this.odigraneKarte = trenutnaIgra.OdigraneKarte;
            this.Invalidate();

            if (trenutnaIgra.ProveriKrajRuke())
            {
                timer1.Start();
                return;
            }

            if (trenutnaIgra.TrenutnIgrac == 0 && !testMod)
            {
                return;
            }

            timer4.Start();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Stop();
            int brojKarte = trenutnaIgra.TrenutnIgracBacaKartu();
            this.Odigraj(brojKarte);
            this.Invalidate();
        }
    }
}
