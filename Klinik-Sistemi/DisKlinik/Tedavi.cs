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
    public partial class Tedavi : Form
    {
        int key = 0;

        public Tedavi()
        {
            InitializeComponent();
        }

        void TedaviUygulamalari()
        {
            Tedaviler tD = new Tedaviler();
            string query = "select * from TedaviTbl";
            DataSet dsTedavi = tD.ShowTedavi(query);
            TedaviDGV.DataSource = dsTedavi.Tables[0];
        }

        void ResetTedavi()
        {
            TedaviAdiTb.Text = string.Empty;
            TedaviUcretTb.Text = string.Empty;
            TedaviAciklamaTb.Text = string.Empty;
        }

        private void BtnTedaviKaydet_Click(object sender, EventArgs e)
        {
            string query = "insert into TedaviTbl values('" + TedaviAdiTb.Text + "', '" + TedaviUcretTb.Text + "', '" + TedaviAciklamaTb.Text + "')";
            Hastalar Hs = new Hastalar();
            try
            {
                Hs.HastaEkle(query);
                MessageBox.Show("Tedavi Başarıyla Eklendi!");
                TedaviUygulamalari();
                ResetTedavi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnTedaviDuzenle_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Düzenlenecek Tedaviyi Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Update TedaviTbl set TAd= '" + TedaviAdiTb.Text + "', TUcret= '" + TedaviUcretTb.Text + "', TAciklama= '" + TedaviAciklamaTb.Text + "' where TId= " + key + " ";
                    Hs.HastaSil(query);
                    MessageBox.Show("Tedavi Bilgileri Başarıyla Düzenlendi!");
                    TedaviUygulamalari();
                    ResetTedavi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnTedaviSil_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Tedaviyi Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Delete from TedaviTbl where TId=" + key + " ";
                    Hs.HastaSil(query);
                    MessageBox.Show("Tedavi Başarıyla Silindi!");
                    TedaviUygulamalari();
                    ResetTedavi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Tedavi_Load(object sender, EventArgs e)
        {
            TedaviUygulamalari();
        }

        private void TedaviDTG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TedaviAdiTb.Text = TedaviDGV.SelectedRows[0].Cells[1].Value.ToString();
            TedaviUcretTb.Text = TedaviDGV.SelectedRows[0].Cells[2].Value.ToString();
            TedaviAciklamaTb.Text = TedaviDGV.SelectedRows[0].Cells[3].Value.ToString();

            if (TedaviAdiTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(TedaviDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void lblGeri_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void lblCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
