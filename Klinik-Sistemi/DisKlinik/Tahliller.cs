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
    public partial class Tahliller : Form
    {
        public Tahliller()
        {
            InitializeComponent();
        }
        int key = 0;
        ConnectionString MyCon = new ConnectionString();

        private void fillTahlil()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select HAd from HastaTbl ", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            DataTable dtTahlil = new DataTable();
            dtTahlil.Columns.Add("HAd", typeof(string));
            dtTahlil.Load(reader);
            HastaTahlilCB.ValueMember = "HAd";
            HastaTahlilCB.DataSource = dtTahlil;
            baglanti.Close();
        }
        void TahlilUygulamalari()
        {
            Randevular rD = new Randevular();
            string query = "select * from TahlilTbl";
            DataSet dtTahlil = rD.ShowRandevu(query);
            TahlillerDGV.DataSource = dtTahlil.Tables[0];
        }

        private void BtnTahlilKaydet_Click(object sender, EventArgs e)
        {
            string query = "insert into TahlilTbl values('" + HastaTahlilCB.SelectedValue.ToString() + "', '" + KanTahlilTb.Text + "', '" + IdrarTahlilTb.Text + "', '" + VitaminTahlilTb.Text + "', '" + HemoglobinTahlilTb.Text + "', '" + KolestrolTahlilTb.Text + "')";
            Tahlil th = new Tahlil();
            try
            {
                th.TahlilEkle(query);
                MessageBox.Show("Tahlil Başarıyla Eklendi!");
                TahlilUygulamalari();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void BtnTahlilDuzenle_Click(object sender, EventArgs e)
        {
            Tahlil th = new Tahlil();
            if (key == 0)
            {
                MessageBox.Show("Düzenlenecek Tahlilili Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Update TahlilTbl set HastaTahlil= '" + HastaTahlilCB.SelectedValue.ToString() + "', KanTahlil= '" + KanTahlilTb.Text + "', IdrarTahlil= '" + IdrarTahlilTb.Text + "', VitaminTahlil= '" + VitaminTahlilTb.Text + "', KolestrolTahlil= '" + KolestrolTahlilTb.Text + "'  where TahlilId= " + key + " ";
                    th.TahlilSil(query);
                    MessageBox.Show("Tahlil Bilgileri Başarıyla Düzenlendi!");
                    TahlilUygulamalari();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnTahlilSil_Click(object sender, EventArgs e)
        {
            Tahlil th = new Tahlil();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Tahlili Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Delete from TahlilTbl where TahlilId=" + key + " ";
                    th.TahlilSil(query);
                    MessageBox.Show("Tahlil Başarıyla Silindi!");
                    TahlilUygulamalari();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

        private void Tahliller_Load(object sender, EventArgs e)
        {
            fillTahlil();
            TahlilUygulamalari();
        }

        private void TahlillerDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HastaTahlilCB.SelectedValue = TahlillerDGV.SelectedRows[0].Cells[1].Value.ToString();
            KanTahlilTb.Text = TahlillerDGV.SelectedRows[0].Cells[2].Value.ToString();
            IdrarTahlilTb.Text = TahlillerDGV.SelectedRows[0].Cells[3].Value.ToString();
            VitaminTahlilTb.Text = TahlillerDGV.SelectedRows[0].Cells[4].Value.ToString();
            HemoglobinTahlilTb.Text = TahlillerDGV.SelectedRows[0].Cells[5].Value.ToString();
            KolestrolTahlilTb.Text = TahlillerDGV.SelectedRows[0].Cells[6].Value.ToString();


            if (HastaTahlilCB.SelectedIndex == -1)
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(TahlillerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
