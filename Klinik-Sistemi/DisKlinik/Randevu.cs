using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DisKlinik
{
    public partial class Randevu : Form
    {
        int key = 0;

        public Randevu()
        {
            InitializeComponent();
        }

        ConnectionString MyCon = new ConnectionString();

        void RandevuUygulamalari()
        {
            Randevular rD = new Randevular();
            string query = "select * from RandevuTbl";
            DataSet dsRandevu = rD.ShowRandevu(query);
            RandevuDGV.DataSource = dsRandevu.Tables[0];
        }

        private void fillHasta()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select HAd from HastaTbl ", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            DataTable dtRandevu = new DataTable();
            dtRandevu.Columns.Add("HAd", typeof(string));
            dtRandevu.Load(reader);
            RandevuAdSoyadCB.ValueMember = "HAd";
            RandevuAdSoyadCB.DataSource = dtRandevu;
            baglanti.Close();
        }
        private void fillTedavi()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select TAd from TedaviTbl ", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            DataTable dtRandevu = new DataTable();
            dtRandevu.Columns.Add("TAd", typeof(string));
            dtRandevu.Load(reader);
            RandevuTedaviSecCB.ValueMember = "TAd";
            RandevuTedaviSecCB.DataSource = dtRandevu;
            baglanti.Close();
        }



        private void Randevu_Load(object sender, EventArgs e)
        {
            fillHasta();
            fillTedavi();
            RandevuUygulamalari();
        }

        private void BtnRandevuKaydet_Click(object sender, EventArgs e)
        {
            string query = "insert into RandevuTbl values('" + RandevuAdSoyadCB.SelectedValue.ToString() + "', '"+RandevuTedaviSecCB.SelectedValue.ToString()+"', '" + RandevuTarih.Text + "', '" + RandevuSaatCB.Text + "')";
            Randevular Hs = new Randevular();
            try
            {
                Hs.RandevuEkle(query);
                MessageBox.Show("Randevu Başarıyla Eklendi!");
                RandevuUygulamalari();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnRandevuDuzenle_Click(object sender, EventArgs e)
        {
            Randevular Hs = new Randevular();
            if (key == 0)
            {
                MessageBox.Show("Düzenlenecek Randevuyu Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Update RandevuTbl set RHasta= '" + RandevuAdSoyadCB.SelectedValue.ToString() + "', RTedavi= '" + RandevuTedaviSecCB.SelectedValue.ToString() + "', RTarih= '" + RandevuTarih.Text + "', RSaat= '"+ RandevuSaatCB.Text+"'  where RId= " + key + " ";
                    Hs.RandevuSil(query);
                    MessageBox.Show("Randevu Bilgileri Başarıyla Düzenlendi!");
                    RandevuUygulamalari();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnRandevuSil_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Randevuyu Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Delete from RandevuTbl where RId=" + key + " ";
                    Hs.HastaSil(query);
                    MessageBox.Show("Randevu Başarıyla Silindi!");
                    RandevuUygulamalari();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void RandevuDGV_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            RandevuAdSoyadCB.SelectedValue = RandevuDGV.SelectedRows[0].Cells[1].Value.ToString();
            RandevuTedaviSecCB.SelectedValue = RandevuDGV.SelectedRows[0].Cells[2].Value.ToString();
            RandevuTarih.Text = RandevuDGV.SelectedRows[0].Cells[3].Value.ToString();
            RandevuSaatCB.Text = RandevuDGV.SelectedRows[0].Cells[4].Value.ToString();

            if (RandevuAdSoyadCB.SelectedIndex == -1)
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(RandevuDGV.SelectedRows[0].Cells[0].Value.ToString());
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
