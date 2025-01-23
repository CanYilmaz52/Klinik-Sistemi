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
    public partial class Hasta : Form
    {

        int key = 0;

        public Hasta()
        {
            InitializeComponent();
        }

        private void BtnHastaKaydet_Click(object sender, EventArgs e)
        {
            string query = "insert into HastaTbl values('"+HAdSoyadTb.Text+"', '"+HTelefonTb.Text+"', '"+AdresTb.Text+"', '"+HDogumTarih.Text+"', '"+HCinsiyetCb.SelectedItem.ToString()+"', '"+AlerjiTb.Text+"', '"+HTCKNTb.Text+"','"+HEpostaTb.Text+"')";
            Hastalar Hs = new Hastalar();
            try
            {
                Hs.HastaEkle(query);
                MessageBox.Show("Hasta Başarıyla Eklendi!");
                Uyeler();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void Uyeler()
        {
            Hastalar Hs = new Hastalar();
            string query = "select * from HastaTbl";
            DataSet ds = Hs.ShowHasta(query);
            HastaDGV.DataSource = ds.Tables[0];
        }

        void Reset()
        {
            HAdSoyadTb.Text = string.Empty;
            HTelefonTb.Text = string.Empty;
            AdresTb.Text = string.Empty;
            HDogumTarih.Text = string.Empty;
            HCinsiyetCb.Text = string.Empty;
            AlerjiTb.Text = string.Empty;
            HTCKNTb.Text = string.Empty;
            HEpostaTb.Text = string.Empty;
        }

        private void Hasta_Load(object sender, EventArgs e)
        {
            Uyeler();
        }
        
        private void HastaDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HAdSoyadTb.Text = HastaDGV.SelectedRows[0].Cells[1].Value.ToString();
            HTelefonTb.Text = HastaDGV.SelectedRows[0].Cells[2].Value.ToString();
            AdresTb.Text = HastaDGV.SelectedRows[0].Cells[3].Value.ToString();
            HDogumTarih.Text = HastaDGV.SelectedRows[0].Cells[4].Value.ToString();
            HCinsiyetCb.SelectedItem = HastaDGV.SelectedRows[0].Cells[5].Value.ToString();
            AlerjiTb.Text = HastaDGV.SelectedRows[0].Cells[6].Value.ToString();
            HTCKNTb.Text = HastaDGV.SelectedRows[0].Cells[7].Value.ToString();
            HEpostaTb.Text = HastaDGV.SelectedRows[0].Cells[8].Value.ToString();

            if(HAdSoyadTb.Text =="")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(HastaDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void BtnHastaSil_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if(key == 0)
            {
                MessageBox.Show("Silinecek Hastayı Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Delete from HastaTbl where HId=" + key + " ";
                    Hs.HastaSil(query);
                    MessageBox.Show("Hasta Başarıyla Silindi!");
                    Uyeler();
                    Reset();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        
        private void BtnHastaDuzenle_Click(object sender, EventArgs e)
        {
            Hastalar Hs = new Hastalar();
            if(key == 0)
            {
                MessageBox.Show("Düzenlenecek Hastayı Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Update HastaTbl set HAd= '"+HAdSoyadTb.Text+"', HTelefon= '"+HTelefonTb.Text+"', HAdres= '"+AdresTb.Text+"', HDTarih= '"+HDogumTarih.Text+"', HCinsiyet= '"+HCinsiyetCb.SelectedItem.ToString()+"', HAlerji= '"+AlerjiTb.Text+"', HTCKN= '"+HTCKNTb.Text+"', HEposta= '"+HEpostaTb.Text+"' where HId= "+key+" ";
                    Hs.HastaSil(query);
                    MessageBox.Show("Hasta Bilgileri Başarıyla Düzenlendi!");
                    Uyeler();
                    Reset();
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

    }
}
