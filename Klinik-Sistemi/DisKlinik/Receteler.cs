using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisKlinik
{
    public partial class Receteler : Form
    {

        int key = 0;

        public Receteler()
        {
            InitializeComponent();
        }
        ConnectionString MyCon = new ConnectionString();


        private void fillHasta()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select HAd from HastaTbl ", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            DataTable dtRandevu = new DataTable();
            dtRandevu.Columns.Add("HAd", typeof(string));
            dtRandevu.Load(reader);
            ReceteAdSoyadCB.ValueMember = "HAd";
            ReceteAdSoyadCB.DataSource = dtRandevu;
            baglanti.Close();
        }

        private void fillTedavi()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from RandevuTbl where RHasta= '"+ReceteAdSoyadCB.SelectedValue.ToString()+"' ", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                ReceteTedaviAdTb.Text= dr["RTedavi"].ToString();
            }
            baglanti.Close();
        }

        private void fillPrice()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TedaviTbl where TAd= '" + ReceteTedaviAdTb.Text + "' ", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                ReceteUcretTb.Text = dr["TUcret"].ToString();
            }
            baglanti.Close();
        }


        private void Receteler_Load(object sender, EventArgs e)
        {
            fillHasta();
            GirilenReceteler();
        }

        private void ReceteAdSoyadCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fillTedavi();
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
        void GirilenReceteler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from ReceteTbl";
            DataSet ds = Hs.ShowHasta(query);
            ReceteDGV.DataSource = ds.Tables[0];
        }

        void FilterReceteler() // buraya hastatbl değil recetetbl den al diğer tablolard da yaparken dikkat et
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from ReceteTbl where HasAd like '%"+txtBoxAramaRecete.Text+"%'";
            DataSet ds = Hs.ShowHasta(query);
            ReceteDGV.DataSource = ds.Tables[0];
        }

        private void BtnReceteKaydet_Click(object sender, EventArgs e)
        {
            string query = "insert into ReceteTbl values('" + ReceteAdSoyadCB.SelectedValue.ToString() + "', '" + ReceteTedaviAdTb.Text + "', '" + ReceteUcretTb.Text + "', '" + ReceteIlacTb.Text + "', '" + ReceteMiktarTb.Text + "')";
            Hastalar Hs = new Hastalar();
            try
            {
                Hs.HastaEkle(query);
                MessageBox.Show("Reçete Başarıyla Eklendi!");
                GirilenReceteler();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnReceteDuzenle_Click(object sender, EventArgs e)
        {
            Randevular Hs = new Randevular();
            if (key == 0)
            {
                MessageBox.Show("Düzenlenecek Reçeteyi Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Update ReceteTbl set HasAd= '" + ReceteAdSoyadCB.SelectedValue.ToString() + "', TedaviAd= '" + ReceteTedaviAdTb.Text + "', TedaviUcret= '" + ReceteUcretTb.Text + "', Ilac= '" + ReceteIlacTb.Text + "', IlacMiktar= '"+ReceteMiktarTb.Text+"'  where RecId= " + key + " ";
                    Hs.RandevuSil(query);
                    MessageBox.Show("Reçete Bilgileri Başarıyla Düzenlendi!");
                    GirilenReceteler();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnReceteSil_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Reçeteyi Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Delete from ReceteTbl where RecId=" + key + " ";
                    Hs.HastaSil(query);
                    MessageBox.Show("Reçete Başarıyla Silindi!");
                    GirilenReceteler();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ReceteDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ReceteAdSoyadCB.SelectedValue = ReceteDGV.SelectedRows[0].Cells[1].Value.ToString();
            ReceteTedaviAdTb.Text = ReceteDGV.SelectedRows[0].Cells[2].Value.ToString();
            ReceteUcretTb.Text = ReceteDGV.SelectedRows[0].Cells[3].Value.ToString();
            ReceteIlacTb.Text = ReceteDGV.SelectedRows[0].Cells[4].Value.ToString();
            ReceteMiktarTb.Text = ReceteDGV.SelectedRows[0].Cells[5].Value.ToString();


            if (ReceteAdSoyadCB.SelectedIndex == -1)
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ReceteDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void ReceteUcretTb_TextChanged(object sender, EventArgs e)
        {
            fillPrice();
        }

        private void ReceteTedaviAdTb_TextChanged(object sender, EventArgs e)
        {
            fillPrice();
        }

        private void txtBoxAramaRecete_TextChanged(object sender, EventArgs e)
        {
            FilterReceteler();
        }

        Bitmap bitmap;

        private void BtnReceteYazdir_Click(object sender, EventArgs e)
        {
            int height = ReceteDGV.Height;
            ReceteDGV.Height = ReceteDGV.RowCount * ReceteDGV.RowTemplate.Height * 2;
            bitmap = new Bitmap(ReceteDGV.Width, ReceteDGV.Height);
            ReceteDGV.DrawToBitmap(bitmap, new Rectangle(0, 10, ReceteDGV.Width, ReceteDGV.Height));
            ReceteDGV.Height = height;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
    }
}
