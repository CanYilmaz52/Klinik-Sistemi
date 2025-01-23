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
    public partial class DoktorSecim : Form
    {
        public DoktorSecim()
        {
            InitializeComponent();
        }
        ConnectionString MyCon = new ConnectionString();
        int key = 0;
        private void fillDoktorAdSoyad(string brans)
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select DoktorAd from DoktorlarTbl where DoktorBrans=@DoktorBrans", baglanti);
            komut.Parameters.AddWithValue("@DoktorBrans", brans);
            SqlDataReader reader = komut.ExecuteReader();
            DataTable dtDoktorAdSoyad = new DataTable();
            dtDoktorAdSoyad.Columns.Add("DoktorAd", typeof(string));
            dtDoktorAdSoyad.Load(reader);
            DoktorSecimCB.ValueMember = "DoktorAd";
            DoktorSecimCB.DataSource = dtDoktorAdSoyad;
            baglanti.Close();
        }

        // Parametre almayan versiyon
        private void fillDoktorAdSoyad()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select DoktorAd from DoktorlarTbl", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            DataTable dtDoktorAdSoyad = new DataTable();
            dtDoktorAdSoyad.Columns.Add("DoktorAd", typeof(string));
            dtDoktorAdSoyad.Load(reader);
            DoktorSecimCB.ValueMember = "DoktorAd";
            DoktorSecimCB.DataSource = dtDoktorAdSoyad;
            baglanti.Close();
        }

        private void fillDoktorBrans()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select DISTINCT DoktorBrans from DoktorlarTbl", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            DataTable dtDoktorBrans = new DataTable();
            dtDoktorBrans.Columns.Add("DoktorBrans", typeof(string));
            dtDoktorBrans.Load(reader);
            DoktorBransSecimCB.ValueMember = "DoktorBrans";
            DoktorBransSecimCB.DataSource = dtDoktorBrans;
            baglanti.Close();
        }


        private void lblCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblGeri_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void DoktorSecim_Load(object sender, EventArgs e)
        {
            fillDoktorBrans();
            fillDoktorAdSoyad(); // Parametre almayan versiyonu çağır
            // ComboBox değişikliğinde çalışacak olay işleyiciyi ekliyoruz.
            DoktorBransSecimCB.SelectedIndexChanged += new EventHandler(DoktorBransSecimCB_SelectedIndexChanged);
            DoktorSecUyeler();
        }

        private void DoktorBransSecimCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedBrans = DoktorBransSecimCB.SelectedValue.ToString();
            fillDoktorAdSoyad(selectedBrans);
        }

        void DoktorSecUyeler()
        {
            Randevular rD = new Randevular();
            string query = "select * from DoktorSecTbl";
            DataSet dsRandevu = rD.ShowRandevu(query);
            DoktorSecDGV.DataSource = dsRandevu.Tables[0];
        }

        private void BtnDoktorSecKaydet_Click(object sender, EventArgs e)
        {
            string query = "insert into DoktorSecTbl values('" + DoktorBransSecimCB.SelectedValue.ToString() + "', '" + DoktorSecimCB.SelectedValue.ToString() + "', '" + DoktorSecRandevuSaatCB.SelectedItem.ToString() + "')";
            DoktorlarSecim DrSec = new DoktorlarSecim();
            try
            {
                DrSec.DoktorSecEkle(query);
                MessageBox.Show("Doktor Başarıyla Kaydedildi!");
                DoktorSecUyeler();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDoktorSecDuzenle_Click(object sender, EventArgs e)
        {
            DoktorlarSecim DrSec = new DoktorlarSecim();
            if (key == 0)
            {
                MessageBox.Show("Düzenlenecek Doktor Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Update DoktorSecTbl set DrSecBrans='" + DoktorBransSecimCB.SelectedValue.ToString() + "', DrSecAdSoyad='" + DoktorSecimCB.SelectedValue.ToString() + "', DrSecSaat='" + DoktorSecRandevuSaatCB.SelectedItem.ToString() + "' where DrSecId=" + key + " ";
                    DrSec.DoktorSecSil(query);
                    MessageBox.Show("Doktor Kayıtları Başarıyla Düzenlendi!");
                    DoktorSecUyeler();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnDoktorSecSil_Click(object sender, EventArgs e)
        {
            DoktorlarSecim DrSec = new DoktorlarSecim();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Doktor Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Delete from DoktorSecTbl where DrSecId=" + key + " ";
                    DrSec.DoktorSecSil(query);
                    MessageBox.Show("Doktor Kaydı Silindi!");
                    DoktorSecUyeler();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DoktorSecDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DoktorBransSecimCB.SelectedValue = DoktorSecDGV.SelectedRows[0].Cells[1].Value.ToString();
            DoktorSecimCB.SelectedValue = DoktorSecDGV.SelectedRows[0].Cells[2].Value.ToString();
            DoktorSecRandevuSaatCB.Text = DoktorSecDGV.SelectedRows[0].Cells[3].Value.ToString();

            if (DoktorBransSecimCB.SelectedIndex == -1)
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(DoktorSecDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
