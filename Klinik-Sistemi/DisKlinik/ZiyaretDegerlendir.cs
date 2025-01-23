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
    public partial class ZiyaretDegerlendir : Form
    {
        public ZiyaretDegerlendir()
        {
            InitializeComponent();
        }
        int key = 0;
        ConnectionString MyCon = new ConnectionString();

        void ZiyaretDegGosterim()
        {
            Degerlendir rD = new Degerlendir();
            string query = "select * from ZiyaretDegTbl";
            DataSet dsRandevu = rD.ShowZiyaret(query);
            ZiyaretDegerlendirDGV.DataSource = dsRandevu.Tables[0];
        }

        private void fillZiyaret()
        {
            SqlConnection baglanti = MyCon.GetCon();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select HAd from HastaTbl ", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            DataTable dtRandevu = new DataTable();
            dtRandevu.Columns.Add("HAd", typeof(string));
            dtRandevu.Load(reader);
            ZiyaretCB.ValueMember = "HAd";
            ZiyaretCB.DataSource = dtRandevu;
            baglanti.Close();
        }

        private void BtnZiyaretKaydet_Click(object sender, EventArgs e)
        {
            string query = "insert into ZiyaretDegTbl values('" + ZiyaretCB.SelectedValue.ToString() + "', '" + DoktorDegTb.Text + "', '" + HastaneDegTb.Text + "', '" + OnlineRandevuDegTb.Text + "')";
            Degerlendir deg = new Degerlendir();
            try
            {
                deg.ZiyaretEkle(query);
                MessageBox.Show("Ziyaret Başarıyla Kaydedildi!");
                ZiyaretDegGosterim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnZiyaretDuzenle_Click(object sender, EventArgs e)
        {
            Degerlendir deg = new Degerlendir();
            if (key == 0)
            {
                MessageBox.Show("Düzenlenecek Ziyareti Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Update ZiyaretDegTbl set HastaAdDeg= '" + ZiyaretCB.SelectedValue.ToString() + "', DoktorDeg= '" + DoktorDegTb.Text + "', ZiyaretDeg= '" + HastaneDegTb.Text + "', OnlineDeg= '" + OnlineRandevuDegTb.Text + "'  where ZiyaretDegId= " + key + " ";
                    deg.ZiyaretSil(query);
                    MessageBox.Show("Ziyaret Bilgileri Başarıyla Düzenlendi!");
                    ZiyaretDegGosterim();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnZiyaretSil_Click(object sender, EventArgs e)
        {
            Degerlendir deg = new Degerlendir();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Ziyaret Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Delete from ZiyaretDegTbl where ZiyaretDegId=" + key + " ";
                    deg.ZiyaretSil(query);
                    MessageBox.Show("Ziyaret Başarıyla Silindi!");
                    ZiyaretDegGosterim();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ZiyaretDegerlendir_Load(object sender, EventArgs e)
        {
            ZiyaretDegGosterim();
            fillZiyaret();
        }

        private void ZiyaretDegerlendirDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ZiyaretCB.SelectedValue = ZiyaretDegerlendirDGV.SelectedRows[0].Cells[1].Value.ToString();
            DoktorDegTb.Text = ZiyaretDegerlendirDGV.SelectedRows[0].Cells[2].Value.ToString();
            HastaneDegTb.Text = ZiyaretDegerlendirDGV.SelectedRows[0].Cells[3].Value.ToString();
            OnlineRandevuDegTb.Text = ZiyaretDegerlendirDGV.SelectedRows[0].Cells[4].Value.ToString();

            if (ZiyaretCB.SelectedIndex == -1)
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ZiyaretDegerlendirDGV.SelectedRows[0].Cells[0].Value.ToString());
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
