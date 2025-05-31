using System;
using System.Data.SqlClient;
using System.Configuration;

namespace ZamowieniaApp1
{
    public static class DatabaseHelper
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ZamowieniaDB"].ConnectionString;
        }

        public static int DodajZamowienie(DateTime dataZamowienia)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = "INSERT INTO Zamowienie (DataDodania) VALUES (@DataZamowienia); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DataZamowienia", dataZamowienia);
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());
                    return newId;
                }
            }




        }

        public static void DodajPozycje(int zamowienieId, string kodProduktu, int ilosc)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = "INSERT INTO Pozycja (ZamowienieID, KodProduktu, Ilosc) VALUES (@ZamowienieID, @KodProduktu, @Ilosc)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ZamowienieID", zamowienieId);
                    cmd.Parameters.AddWithValue("@KodProduktu", kodProduktu);
                    cmd.Parameters.AddWithValue("@Ilosc", ilosc);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UsunPozycje(int zamowienieId, string kodProduktu, int ilosc)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = "DELETE FROM Pozycja WHERE ZamowienieID = @ZamowienieID AND KodProduktu = @KodProduktu AND Ilosc = @Ilosc";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ZamowienieID", zamowienieId);
                    cmd.Parameters.AddWithValue("@KodProduktu", kodProduktu);
                    cmd.Parameters.AddWithValue("@Ilosc", ilosc);
                    cmd.ExecuteNonQuery();
                }
            }
        }



    }
}
