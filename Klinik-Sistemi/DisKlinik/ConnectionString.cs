using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DisKlinik
{
    internal class ConnectionString
    {
        public SqlConnection GetCon()
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\cangu\OneDrive\Belgeler\DentalDB.mdf;Integrated Security=True;Connect Timeout=30";
            return baglanti;
        }
    }
}
