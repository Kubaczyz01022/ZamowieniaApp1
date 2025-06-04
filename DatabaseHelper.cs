using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;

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

        public static decimal PobierzSumeZamowienia(int zamowienieId)
        {
            decimal suma = 0;
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("PobierzSumeZamowienia", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ZamowienieId", zamowienieId);

                    object wynik = cmd.ExecuteScalar();
                    if (wynik != DBNull.Value && wynik != null)
                        suma = Convert.ToDecimal(wynik);
                }
            }
            return suma;
        }
        public static (string tytul, string tresc) PobierzPodsumowanieEmail(int zamowienieId)
        {
            string tytul = "";
            string tresc = "";
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("PobierzPodsumowanieZamowieniaEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ZamowienieId", zamowienieId);

                    var tytulParam = new SqlParameter("@Tytul", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                    var trescParam = new SqlParameter("@Tresc", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(tytulParam);
                    cmd.Parameters.Add(trescParam);

                    cmd.ExecuteNonQuery();

                    tytul = tytulParam.Value.ToString();
                    tresc = trescParam.Value.ToString();
                }
            }
            return (tytul, tresc);
        }

        public static DataTable PobierzPozycjeZamowienia(int zamowienieId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = @"SELECT p.KodProduktu AS [Kod produktu], 
                                pr.NazwaProduktu AS [Nazwa produktu], 
                                p.Ilosc AS [Ilość], 
                                pr.Cena AS [Cena (szt.)], 
                                (p.Ilosc * pr.Cena) AS [Wartość] 
                         FROM Pozycja p
                         JOIN Produkty pr ON p.KodProduktu = pr.KodProduktu
                         WHERE p.ZamowienieID = @ZamowienieID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ZamowienieID", zamowienieId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
        public static string GenerujZamowienieCsvJakoString(int zamowienieId)
        {
            DataTable dt = PobierzPozycjeZamowienia(zamowienieId);
            StringBuilder sb = new StringBuilder();

            // Nagłówki
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(dt.Columns[i]);
                if (i < dt.Columns.Count - 1)
                    sb.Append(";");
            }
            sb.AppendLine();

            // Wiersze
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.Append(row[i]);
                    if (i < dt.Columns.Count - 1)
                        sb.Append(";");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public static void EksportujZamowienieDoCsv(int zamowienieId, string sciezkaPliku)
        {
            DataTable dt = PobierzPozycjeZamowienia(zamowienieId);

            using (var sw = new StreamWriter(sciezkaPliku, false, Encoding.UTF8))
            {
                // Nagłówki
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dt.Columns[i]);
                    if (i < dt.Columns.Count - 1)
                        sw.Write(";");
                }
                sw.WriteLine();

                // Wiersze
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sw.Write(row[i]);
                        if (i < dt.Columns.Count - 1)
                            sw.Write(";");
                    }
                    sw.WriteLine();
                }
            }
        }

        public static void WyslijEmail(string adresOdbiorcy, string temat, string tresc, string sciezkaZalacznika)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress("jakub.firmowy2001@gmail.com");    // <-- Twój adres Gmail
            mail.To.Add(adresOdbiorcy);                             // Adres odbiorcy
            mail.Subject = temat;
            mail.Body = tresc;
            mail.IsBodyHtml = false;                                // Nie wysyłaj HTML

            // Dodaj CSV jako załącznik (jeśli jest)
            if (!string.IsNullOrEmpty(sciezkaZalacznika) && File.Exists(sciezkaZalacznika))
                mail.Attachments.Add(new Attachment(sciezkaZalacznika));

            var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("jakub.firmowy2001@gmail.com", "skvn wtpd rhru shch"); // <-- wklej hasło aplikacji

            smtp.Send(mail);
        }
        public static List<string> PobierzWszystkieAdresyEmail()
        {
            List<string> adresy = new List<string>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT Adres FROM OdbiorcyEmail";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        adresy.Add(reader.GetString(0));
                    }
                }
            }
            return adresy;
        }

        public static void DodajAdresEmail(string adres)
        {
            if (string.IsNullOrWhiteSpace(adres))
                return;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                // Wstaw tylko jeśli nie istnieje już taki adres
                string query = @"IF NOT EXISTS (SELECT 1 FROM OdbiorcyEmail WHERE Adres = @Adres)
                         INSERT INTO OdbiorcyEmail (Adres) VALUES (@Adres)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Adres", adres);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static DataTable PobierzListeZamowien()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                // Pobieramy ID i datę z tabeli Zamowienie
                string query = "SELECT Id AS Id, CONCAT('Zamówienie ', FORMAT(DataDodania, 'yyyy-MM-dd')) AS Opis FROM Zamowienie ORDER BY DataDodania DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }




    }
}
