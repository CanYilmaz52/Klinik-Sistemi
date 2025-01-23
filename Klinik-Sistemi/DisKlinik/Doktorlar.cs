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
    public partial class Doktorlar : Form
    {
        public Doktorlar()
        {
            InitializeComponent();
        }

        ConnectionString MyCon = new ConnectionString();
        int key = 0;

        void DoktorUyeler()
        {
            DoktorKayit Dr = new DoktorKayit();
            string query = "select * from DoktorlarTbl";
            DataSet dsDoktor = Dr.ShowDoktor(query);
            DoktorlarKayitDGV.DataSource = dsDoktor.Tables[0];
        }

        void ResetDoktorlar()
        {
            DoktorlarAdSoyadTb.Text = string.Empty;
            DoktorlarTelTb.Text = string.Empty;
            DoktorlarEpostaTb.Text = string.Empty;
            DoktorlarDTarihTb.Text = string.Empty;
            DoktorlarTCKNTb.Text = string.Empty;
            DoktorlarCinsiyetCB.Text = string.Empty;
            DoktorlarBransTb.Text = string.Empty;
        }


        private void Doktorlar_Load(object sender, EventArgs e)
        {
            DoktorUyeler();
        }

        private void BtnDoktorKaydet_Click(object sender, EventArgs e)
        {
            string query = "insert into DoktorlarTbl values('" + DoktorlarAdSoyadTb.Text + "', '" + DoktorlarTelTb.Text + "', '" + DoktorlarEpostaTb.Text + "', '" + DoktorlarDTarihTb.Text + "', '" + DoktorlarTCKNTb.Text + "', '" + DoktorlarCinsiyetCB.SelectedItem.ToString() + "', '" + DoktorlarBransTb.Text + "')";
            DoktorKayit Dr = new DoktorKayit();
            try
            {
                Dr.DoktorEkle(query);
                MessageBox.Show("Doktor Başarıyla Eklendi!");
                DoktorUyeler();
                ResetDoktorlar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDoktorDuzenle_Click(object sender, EventArgs e)
        {
            DoktorKayit Dr = new DoktorKayit();
            if (key == 0)
            {
                MessageBox.Show("Düzenlenecek Doktor Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Update DoktorlarTbl set DoktorAd='" + DoktorlarAdSoyadTb.Text + "', DoktorTelefon='" + DoktorlarTelTb.Text + "', DoktorEposta='" + DoktorlarEpostaTb.Text + "', DoktorDTarih='" + DoktorlarDTarihTb.Text + "', DoktorTCKN='" + DoktorlarTCKNTb.Text + "', DoktorCinsiyet='" + DoktorlarCinsiyetCB.SelectedItem.ToString() + "', DoktorBrans='" + DoktorlarBransTb.Text + "' where DoktorId=" + key + " ";
                    Dr.DoktorSil(query);
                    MessageBox.Show("Doktor Bilgileri Başarıyla Düzenlendi!");
                    DoktorUyeler();
                    ResetDoktorlar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnDoktorSil_Click(object sender, EventArgs e)
        {
            DoktorKayit Dr = new DoktorKayit();
            if (key == 0)
            {
                MessageBox.Show("Silinecek Doktor Seçiniz.");
            }
            else
            {
                try
                {
                    string query = "Delete from DoktorlarTbl where DoktorId=" + key + " ";
                    Dr.DoktorSil(query);
                    MessageBox.Show("Doktor Başarıyla Silindi!");
                    DoktorUyeler();
                    ResetDoktorlar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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

        private void DoktorlarKayitDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DoktorlarAdSoyadTb.Text = DoktorlarKayitDGV.SelectedRows[0].Cells[1].Value.ToString();
            DoktorlarTelTb.Text = DoktorlarKayitDGV.SelectedRows[0].Cells[2].Value.ToString();
            DoktorlarEpostaTb.Text = DoktorlarKayitDGV.SelectedRows[0].Cells[3].Value.ToString();
            DoktorlarDTarihTb.Text = DoktorlarKayitDGV.SelectedRows[0].Cells[4].Value.ToString();
            DoktorlarTCKNTb.Text = DoktorlarKayitDGV.SelectedRows[0].Cells[5].Value.ToString();
            DoktorlarCinsiyetCB.SelectedItem = DoktorlarKayitDGV.SelectedRows[0].Cells[6].Value.ToString();
            DoktorlarBransTb.Text = DoktorlarKayitDGV.SelectedRows[0].Cells[7].Value.ToString();

            if (DoktorlarAdSoyadTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(DoktorlarKayitDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
