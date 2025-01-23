using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisKlinik
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void BtnAnaSyfHastaGiris_Click(object sender, EventArgs e)
        {
            AdminLoginForm adminLoginForm = new AdminLoginForm();
            adminLoginForm.ShowDialog();

            if (adminLoginForm.IsAuthenticated)
            {
                MessageBox.Show("Admin doğrulandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Hasta hst = new Hasta();
                hst.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Admin doğrulanamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnAnaSyfRandevuGiris_Click(object sender, EventArgs e)
        {
            Randevu rnd = new Randevu();
            rnd.Show();
            this.Hide();
        }

        private void BtnAnaSyfTedaviGiris_Click(object sender, EventArgs e)
        {
            AdminLoginForm adminLoginForm = new AdminLoginForm();
            adminLoginForm.ShowDialog();

            if (adminLoginForm.IsAuthenticated)
            {
                MessageBox.Show("Admin doğrulandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Tedavi tdv = new Tedavi();
                tdv.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Admin doğrulanamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAnaSyfReceteGiris_Click(object sender, EventArgs e)
        {
            Receteler rct = new Receteler();
            rct.Show();
            this.Hide();
        }

        private void BtnAnaSyfDoktorSec_Click(object sender, EventArgs e)
        {
            DoktorSecim drSec = new DoktorSecim();
            drSec.Show();
            this.Hide();
        }

        private void lblCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnAnasyfDoktorKayit_Click(object sender, EventArgs e)
        {
            AdminLoginForm adminLoginForm = new AdminLoginForm();
            adminLoginForm.ShowDialog();

            if (adminLoginForm.IsAuthenticated)
            {
                MessageBox.Show("Admin doğrulandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Doktorlar dr = new Doktorlar();
                dr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Admin doğrulanamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnAnaSyfTahlil_Click(object sender, EventArgs e)
        {
            Tahliller tl = new Tahliller();
            tl.Show();
            this.Hide();
        }

        private void BtnAnaSyfDegerlendir_Click(object sender, EventArgs e)
        {
            ZiyaretDegerlendir zDeg = new ZiyaretDegerlendir();
            zDeg.Show();
            this.Hide();
        }
    }
}
