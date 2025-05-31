using System;
using System.Windows.Forms;
using PdfiumViewer;
using Tesseract;

namespace ZamowieniaApp1
{
    public partial class Form1 : Form
    {
        private int bie��ceZamowienieId = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDodajPozycje_Click(object sender, EventArgs e)
        {
            string kodProduktu = txtKodProduktu.Text.Trim();
            string ilosc = txtilosc.Text.Trim();

            if (!string.IsNullOrEmpty(kodProduktu) && !string.IsNullOrEmpty(ilosc) && int.TryParse(ilosc, out int iloscInt))
            {
                
                dataGridPozycje.Rows.Add(kodProduktu, ilosc);

                
                if (bie��ceZamowienieId == 0)
                    bie��ceZamowienieId = DatabaseHelper.DodajZamowienie(DateTime.Now);

               
                try
                {
                    DatabaseHelper.DodajPozycje(bie��ceZamowienieId, kodProduktu, iloscInt);
                    MessageBox.Show("Dodano pozycj� do bazy danych.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("B��d zapisu do bazy: " + ex.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                txtKodProduktu.Clear();
                txtilosc.Clear();
            }
            else
            {
                MessageBox.Show("Uzupe�nij oba pola: Kod produktu i Ilo��.", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void btnUsunPozycje_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridPozycje.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                   
                    string kodProduktu = row.Cells["KodProduktu"].Value?.ToString();
                    int ilosc = 0;
                    int.TryParse(row.Cells["Ilosc"].Value?.ToString(), out ilosc);

                    
                    try
                    {
                        DatabaseHelper.UsunPozycje(bie��ceZamowienieId, kodProduktu, ilosc);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("B��d przy usuwaniu z bazy: " + ex.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    
                    dataGridPozycje.Rows.Remove(row);
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
                            using (var image = document.Render(0, 300, 300, true))
                            {
                                using (var engine = new Tesseract.TesseractEngine(@"./tessdata", "pol", Tesseract.EngineMode.Default))
                                {
                                    using (var bitmap = new Bitmap(image))
                                    using (var pix = PixHelper.ConvertBitmapToPix(bitmap))
                                    {
                                        using (var page = engine.Process(pix))
                                        {
                                            string text = page.GetText();
                                            txtOCR.Text = text;

                                           
                                            bie��ceZamowienieId = DatabaseHelper.DodajZamowienie(DateTime.Now);

                                            dataGridPozycje.Rows.Clear();

                                            string[] lines = text.Split('\n');
                                            string kod = "";
                                            string ilosc = "";

                                            foreach (var line in lines)
                                            {
                                               
                                                if (line.ToLower().Contains("kod produktu") || line.ToLower().Contains("testowy kod produktu"))
                                                {
                                                    int idx = line.IndexOf(":");
                                                    if (idx >= 0 && idx + 1 < line.Length)
                                                        kod = line.Substring(idx + 1).Trim();
                                                    else
                                                        kod = "";
                                                }

                                                
                                                if (line.ToLower().Contains("no��") || line.ToLower().Contains("ilo��") || line.ToLower().Contains("ilosc"))
                                                {
                                                    int idx = line.IndexOf(":");
                                                    if (idx >= 0 && idx + 1 < line.Length)
                                                    {
                                                        ilosc = line.Substring(idx + 1).Trim();

                                                        if (!string.IsNullOrWhiteSpace(kod) && !string.IsNullOrWhiteSpace(ilosc))
                                                        {
                                                            if (int.TryParse(ilosc, out int iloscInt))
                                                            {
                                                               
                                                                DatabaseHelper.DodajPozycje(bie��ceZamowienieId, kod, iloscInt);
                                                                dataGridPozycje.Rows.Add(kod, iloscInt);
                                                            }
                                                            kod = "";
                                                            ilosc = "";
                                                        }
                                                    }
                                                }
                                            }

                                            MessageBox.Show("Zam�wienie i pozycje zosta�y dodane do bazy!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Wyst�pi� b��d: " + ex.Message);
                    }
                }
            }
        }





        private void txtOCR_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
