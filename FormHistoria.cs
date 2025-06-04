
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace ZamowieniaApp1
{
    public partial class FormHistoria : Form
    {
        public FormHistoria()
        {
            InitializeComponent();
            ZaladujZamowienia();
        }

        private void ZaladujZamowienia()
        {
            // Pobierz zamówienia z bazy (np. id i daty)
            var dt = DatabaseHelper.PobierzListeZamowien();
            listBoxZamowienia.DisplayMember = "Opis"; // np. "Zamówienie 2024-05-01"
            listBoxZamowienia.ValueMember = "Id";
            listBoxZamowienia.DataSource = dt;
        }

        private void btnEksportuj_Click(object sender, EventArgs e)
        {
            if (listBoxZamowienia.SelectedItem == null)
            {
                MessageBox.Show("Wybierz zamówienie!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedId = (int)((DataRowView)listBoxZamowienia.SelectedItem)["Id"];
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Pliki CSV (*.csv)|*.csv",
                FileName = $"Zamowienie_{selectedId}.csv"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                DatabaseHelper.EksportujZamowienieDoCsv(selectedId, sfd.FileName);
                MessageBox.Show("Zamówienie wyeksportowane!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
