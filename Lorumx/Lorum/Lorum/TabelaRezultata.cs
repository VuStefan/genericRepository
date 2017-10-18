using Lorum.Igre;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lorum
{
    public partial class TabelaRezultata : Form
    {
        Igrac[] igraci;

        public TabelaRezultata(Igrac[] igraci)
        {
            InitializeComponent();

            this.igraci = igraci;
            NapraviTabelu();
        }

        private void NapraviTabelu()
        {
            dataGridView1.Columns.Add("Igra", "Igra");
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

            for (int i = 0; i < 4; i++)
            {
                dataGridView1.Columns.Add(igraci[i].Ime, igraci[i].Ime);
                dataGridView1.Rows.Add(6);
                dataGridView1.Columns[i + 1].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int i = 0; i < 6; i++)
            {
                dataGridView1.Rows[6 + i].DefaultCellStyle.BackColor = Color.LightGray;
                dataGridView1.Rows[18 + i].DefaultCellStyle.BackColor = Color.LightGray;

                string naziv = ((NazivIgre)i).ToString();
                dataGridView1.Rows[i].Cells[0].Value = naziv;
                dataGridView1.Rows[6 + i].Cells[0].Value = naziv;
                dataGridView1.Rows[12 + i].Cells[0].Value = naziv;
                dataGridView1.Rows[18 + i].Cells[0].Value = naziv;
            }

            dataGridView1.Rows.Add("", 0, 0, 0, 0);
            dataGridView1.Rows[24].DefaultCellStyle.BackColor = Color.LawnGreen;
        }

        private void TabelaRezultata_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        public void Ispisi(string nazivIgre, int igra)
        {
            for (int i = 1; i < 5; i++)
            {
                dataGridView1.Rows[igra].Cells[i].Value = igraci[i - 1].TrenutniRezultat;
                dataGridView1.Rows[24].Cells[i].Value = igraci[i - 1].UkupniRezultat;
            }
            dataGridView1[0, 0].Selected = false;
        }

    }
}
