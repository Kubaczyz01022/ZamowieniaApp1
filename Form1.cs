using PdfiumViewer;
using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tesseract;


namespace ZamowieniaApp1
{
    public partial class Form1 : Form
    {
        private int bieżąceZamowienieId = 0;
        private string sciezkaCsv = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void OdswiezListeOdbiorcow()
        {
            var listaAdresow = DatabaseHelper.PobierzWszystkieAdresyEmail();
            cmbEmailOdbiorcy.Items.Clear();
            cmbEmailOdbiorcy.Items.AddRange(listaAdresow.ToArray());
        }



        private void btnDodajPozycje_Click(object sender, EventArgs e)
        {
            string kodProduktu = txtKodProduktu.Text.Trim();
            string ilosc = txtilosc.Text.Trim();

            if (!string.IsNullOrEmpty(kodProduktu) && !string.IsNullOrEmpty(ilosc) && int.TryParse(ilosc, out int iloscInt))
            {
                // Dodaj do tabeli na formie
                dataGridPozycje.Rows.Add(kodProduktu, ilosc);

                // Jeśli nie ma jeszcze zamówienia, utwórz je
                if (bieżąceZamowienieId == 0)
                    bieżąceZamowienieId = DatabaseHelper.DodajZamowienie(DateTime.Now);

                try
                {
                    // Dodaj do bazy danych
                    DatabaseHelper.DodajPozycje(bieżąceZamowienieId, kodProduktu, iloscInt);
                    MessageBox.Show("Dodano pozycję do bazy danych.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Pobierz i wyświetl sumę zamówienia
                    decimal suma = DatabaseHelper.PobierzSumeZamowienia(bieżąceZamowienieId);
                    lblSuma.Text = $"Suma zamówienia: {suma:C}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd zapisu do bazy: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Wyczyść pola
                txtKodProduktu.Clear();
                txtilosc.Clear();
            }
            else
            {
                MessageBox.Show("Uzupełnij oba pola: Kod produktu i Ilość.", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private void btnUsunPozycje_Click(object sender, EventArgs e)
        {
            bool usunieto = false;

            foreach (DataGridViewRow row in dataGridPozycje.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    string kodProduktu = row.Cells["KodProduktu"].Value?.ToString();
                    int ilosc = 0;
                    int.TryParse(row.Cells["Ilosc"].Value?.ToString(), out ilosc);

                    try
                    {
                        DatabaseHelper.UsunPozycje(bieżąceZamowienieId, kodProduktu, ilosc);
                        usunieto = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Błąd przy usuwaniu z bazy: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    dataGridPozycje.Rows.Remove(row);
                }
            }

            // Odśwież sumę zamówienia, jeśli coś usunąłeś
            if (usunieto)
            {
                try
                {
                    decimal suma = DatabaseHelper.PobierzSumeZamowienia(bieżąceZamowienieId);
                    lblSuma.Text = $"Suma zamówienia: {suma:C}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd przy pobieraniu sumy zamówienia: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // --- AKTUALIZUJ CSV ---
                sciezkaCsv = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Zamowienie_{bieżąceZamowienieId}.csv");
                var dt = DatabaseHelper.PobierzPozycjeZamowienia(bieżąceZamowienieId);
                if (dt.Rows.Count > 0)
                {
                    // Zaktualizuj CSV (nadpisz)
                    DatabaseHelper.EksportujZamowienieDoCsv(bieżąceZamowienieId, sciezkaCsv);
                }
                else
                {
                    // Jeśli nie ma już żadnych pozycji, usuń plik CSV
                    if (File.Exists(sciezkaCsv))
                        File.Delete(sciezkaCsv);
                }
            }
        }


        private void btnDodajPDF_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                openFileDialog.Title = "Wybierz plik PDF";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var document = PdfiumViewer.PdfDocument.Load(openFileDialog.FileName))
                        {
                            StringBuilder allText = new StringBuilder();

                            for (int pageNum = 0; pageNum < document.PageCount; pageNum++)
                            {
                                using (var image = document.Render(pageNum, 300, 300, true))
                                {
                                    using (var engine = new Tesseract.TesseractEngine(@"./tessdata", "pol", Tesseract.EngineMode.Default))
                                    {
                                        using (var bitmap = new Bitmap(image))
                                        using (var pix = PixHelper.ConvertBitmapToPix(bitmap))
                                        {
                                            using (var page = engine.Process(pix))
                                            {
                                                string text = page.GetText();
                                                allText.AppendLine(text);
                                            }
                                        }
                                    }
                                }
                            }

                            txtOCR.Text = allText.ToString();
                            bieżąceZamowienieId = DatabaseHelper.DodajZamowienie(DateTime.Now);
                            dataGridPozycje.Rows.Clear();

                            // REGEX na całym tekście
                            var matches = Regex.Matches(
                                allText.ToString(),
                                @"([A-Z0-9]{5,})\s*;\s*([0-9]+)",
                                RegexOptions.IgnoreCase | RegexOptions.Multiline
                            );

                            HashSet<string> dodane = new HashSet<string>(); // Unikalne

                            foreach (Match match in matches)
                            {
                                string kod = match.Groups[1].Value.Trim();
                                string ilosc = match.Groups[2].Value.Trim();
                                string ident = $"{kod};{ilosc}";
                                if (!string.IsNullOrWhiteSpace(kod) && !string.IsNullOrWhiteSpace(ilosc)
                                    && !dodane.Contains(ident))
                                {
                                    if (int.TryParse(ilosc, out int iloscInt))
                                    {
                                        DatabaseHelper.DodajPozycje(bieżąceZamowienieId, kod, iloscInt);
                                        dataGridPozycje.Rows.Add(kod, iloscInt);
                                        dodane.Add(ident);
                                    }
                                }
                            }

                            // Fallback: linia po linii (gdyby coś umknęło)
                            var lines = allText.ToString().Split('\n');
                            foreach (var l in lines)
                            {
                                var line = l.Trim();
                                if (!string.IsNullOrEmpty(line) && !line.Contains(";"))
                                    continue; // nie próbuj jeśli nie ma średnika
                                var parts = line.Split(';');
                                if (parts.Length == 2)
                                {
                                    string kod = parts[0].Trim();
                                    string ilosc = parts[1].Trim();
                                    string ident = $"{kod};{ilosc}";
                                    if (!dodane.Contains(ident)
                                        && !string.IsNullOrWhiteSpace(kod)
                                        && int.TryParse(ilosc, out int iloscInt))
                                    {
                                        DatabaseHelper.DodajPozycje(bieżąceZamowienieId, kod, iloscInt);
                                        dataGridPozycje.Rows.Add(kod, iloscInt);
                                        dodane.Add(ident);
                                    }
                                }
                            }

                            // --- ODŚWIEŻ SUMĘ ZAMÓWIENIA ---
                            decimal suma = DatabaseHelper.PobierzSumeZamowienia(bieżąceZamowienieId);
                            lblSuma.Text = $"Suma zamówienia: {suma:C}";

                            MessageBox.Show("Zamówienie i pozycje zostały dodane do bazy!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Wystąpił błąd: " + ex.Message);
                    }
                }
            }
        }














        //private void btnDodajPDF_Click(object sender, EventArgs e)
        //{
        //    using (OpenFileDialog openFileDialog = new OpenFileDialog())
        //    {
        //        openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
        //        openFileDialog.Title = "Wybierz plik PDF";

        //        if (openFileDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            try
        //            {
        //                using (var document = PdfiumViewer.PdfDocument.Load(openFileDialog.FileName))
        //                {
        //                    using (var image = document.Render(0, 300, 300, true))
        //                    {
        //                        using (var engine = new Tesseract.TesseractEngine(@"./tessdata", "pol", Tesseract.EngineMode.Default))
        //                        {
        //                            using (var bitmap = new Bitmap(image))
        //                            using (var pix = PixHelper.ConvertBitmapToPix(bitmap))
        //                            {
        //                                using (var page = engine.Process(pix))
        //                                {
        //                                    string text = page.GetText();
        //                                    txtOCR.Text = text;


        //                                    bieżąceZamowienieId = DatabaseHelper.DodajZamowienie(DateTime.Now);

        //                                    dataGridPozycje.Rows.Clear();

        //                                    string[] lines = text.Split('\n');
        //                                    string kod = "";
        //                                    string ilosc = "";

        //                                    foreach (var line in lines)
        //                                    {

        //                                        if (line.ToLower().Contains("kod produktu") || line.ToLower().Contains("testowy kod produktu"))
        //                                        {
        //                                            int idx = line.IndexOf(":");
        //                                            if (idx >= 0 && idx + 1 < line.Length)
        //                                                kod = line.Substring(idx + 1).Trim();
        //                                            else
        //                                                kod = "";
        //                                        }


        //                                        if (line.ToLower().Contains("ność") || line.ToLower().Contains("ilość") || line.ToLower().Contains("ilosc"))
        //                                        {
        //                                            int idx = line.IndexOf(":");
        //                                            if (idx >= 0 && idx + 1 < line.Length)
        //                                            {
        //                                                ilosc = line.Substring(idx + 1).Trim();

        //                                                if (!string.IsNullOrWhiteSpace(kod) && !string.IsNullOrWhiteSpace(ilosc))
        //                                                {
        //                                                    if (int.TryParse(ilosc, out int iloscInt))
        //                                                    {

        //                                                        DatabaseHelper.DodajPozycje(bieżąceZamowienieId, kod, iloscInt);
        //                                                        dataGridPozycje.Rows.Add(kod, iloscInt);
        //                                                    }
        //                                                    kod = "";
        //                                                    ilosc = "";
        //                                                }
        //                                            }
        //                                        }
        //                                    }

        //                                    MessageBox.Show("Zamówienie i pozycje zostały dodane do bazy!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Wystąpił błąd: " + ex.Message);
        //            }
        //        }
        //    }
        //}





        private void txtOCR_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    OdswiezListeOdbiorcow();
        //}

        private void btnPokazPodsumowanie_Click(object sender, EventArgs e)
        {
            if (bieżąceZamowienieId == 0)
            {
                MessageBox.Show("Nie wybrano zamówienia!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Eksport CSV na pulpit i zapamiętaj ścieżkę
            sciezkaCsv = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Zamowienie_{bieżąceZamowienieId}.csv");
            DatabaseHelper.EksportujZamowienieDoCsv(bieżąceZamowienieId, sciezkaCsv);

            // 2. Pobierz treść maila (bez CSV w treści)
            var podsumowanie = DatabaseHelper.PobierzPodsumowanieEmail(bieżąceZamowienieId);
            txtTrescMaila.Text = podsumowanie.tytul + Environment.NewLine + Environment.NewLine + podsumowanie.tresc
                + Environment.NewLine + Environment.NewLine +
                "W załączeniu przesyłam plik CSV z zamówieniem.";
        }

        private void btnPodgladCSV_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sciezkaCsv) && File.Exists(sciezkaCsv))
            {
                var psi = new ProcessStartInfo
                {
                    FileName = sciezkaCsv,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            else
            {
                MessageBox.Show("Plik CSV nie został jeszcze wygenerowany lub nie istnieje!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridPodsumowanie_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnWyslijMaila_Click(object sender, EventArgs e)
        {
            string adres = cmbEmailOdbiorcy.Text.Trim();  // ← tylko raz deklarujesz zmienną!
            DatabaseHelper.DodajAdresEmail(adres);
            string temat = "Zamówienie z aplikacji";
            string tresc = txtTrescMaila.Text;
            // Treść maila z TextBoxa (cały mail)
            // sciezkaCsv – ścieżka do pliku CSV z zamówieniem, z podsumowania

            try
            {
                DatabaseHelper.WyslijEmail(adres, temat, tresc, sciezkaCsv);
                MessageBox.Show("Wiadomość została wysłana!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas wysyłki maila: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            OdswiezListeOdbiorcow();
        }


        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelOCR_Click(object sender, EventArgs e)
        {

        }

        private void dataGridPozycje_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTrescMaila_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbEmailOdbiorcy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

            OdswiezListeOdbiorcow();


            string sciezkaObrazka = @"Resources\93a89ad1119d8e5e9b2dbe81416145ba.jpg";
            pictureBoxBackground.Image = Image.FromFile(sciezkaObrazka);
        }

        private void lblSuma_Click(object sender, EventArgs e)
        {

        }

        private void btnOdswiez_Click(object sender, EventArgs e)
        {

            dataGridPozycje.Rows.Clear();
            txtKodProduktu.Clear();
            txtilosc.Clear();
            txtOCR.Clear();
            txtTrescMaila.Clear();
            lblSuma.Text = "Suma zamówienia: 0,00 zł";
            bieżąceZamowienieId = 0;
            sciezkaCsv = "";
            OdswiezListeOdbiorcow();

        }

        private void btnHistoria_Click(object sender, EventArgs e)
        {
            FormHistoria historiaForm = new FormHistoria();
            historiaForm.ShowDialog();
        }

    }
}
