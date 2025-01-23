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
    public partial class AdminLoginForm : Form
    {

        public bool IsAuthenticated { get; private set; }

        public AdminLoginForm()
        {
            InitializeComponent();
            IsAuthenticated = false;
        }

        private void BtnAdminGiris_Click(object sender, EventArgs e)
        {
            string username = AdminLoginAdTb.Text;
            string password = AdminLoginSifreTb.Text;

            if (username == "admin" && password == "admin")
            {
                IsAuthenticated = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
