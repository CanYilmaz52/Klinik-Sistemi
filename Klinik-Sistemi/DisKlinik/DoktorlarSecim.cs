using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisKlinik
{
    internal class DoktorlarSecim
    {

        public void DoktorSecEkle(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection baglanti = MyConnection.GetCon();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglanti;
            baglanti.Open();
            komut.CommandText = query;
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        public void DoktorSecSil(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection baglanti = MyConnection.GetCon();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglanti;
            baglanti.Open();
            komut.CommandText = query;
            komut.ExecuteNonQuery();
            baglanti.Close();
        }




        public DataSet ShowSecDoktor(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection baglanti = MyConnection.GetCon();

            // Ensure the connection is open before using it
            baglanti.Open();

            SqlCommand komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = query;

            // Since we are fetching data, we should use SqlDataAdapter instead of ExecuteNonQuery
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            DataSet dsDoktorSec = new DataSet();

            // Fill the DataSet with the data retrieved from the database
            sda.Fill(dsDoktorSec);

            // Close the connection after use
            baglanti.Close();

            // Return the filled DataSet
            return dsDoktorSec;
        }
    }
}
