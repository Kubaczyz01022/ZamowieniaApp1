namespace ZamowieniaApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelOCR = new Label();
            txtKodProduktu = new TextBox();
            btnDodajPozycje = new Button();
            btndodajPDF = new Button();
            btnUsunPozycje = new Button();
            dataGridPozycje = new DataGridView();
            KodProduktu = new DataGridViewTextBoxColumn();
            Ilosc = new DataGridViewTextBoxColumn();
            txtOCR = new TextBox();
            txtilosc = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridPozycje).BeginInit();
            SuspendLayout();
            // 
            // labelOCR
            // 
            labelOCR.AutoSize = true;
            labelOCR.Location = new Point(585, 176);
            labelOCR.Name = "labelOCR";
            labelOCR.Size = new Size(150, 15);
            labelOCR.TabIndex = 0;
            labelOCR.Text = "Podgląd tekstu z PDF(OCR)";
            // 
            // txtKodProduktu
            // 
            txtKodProduktu.Location = new Point(25, 48);
            txtKodProduktu.Name = "txtKodProduktu";
            txtKodProduktu.Size = new Size(321, 23);
            txtKodProduktu.TabIndex = 1;
            txtKodProduktu.Text = "Kod produktu:";
            // 
            // btnDodajPozycje
            // 
            btnDodajPozycje.Location = new Point(585, 47);
            btnDodajPozycje.Name = "btnDodajPozycje";
            btnDodajPozycje.Size = new Size(152, 23);
            btnDodajPozycje.TabIndex = 2;
            btnDodajPozycje.Text = "Dodaj pozycję";
            btnDodajPozycje.UseVisualStyleBackColor = true;
            btnDodajPozycje.Click += btnDodajPozycje_Click;
            // 
            // btndodajPDF
            // 
            btndodajPDF.Location = new Point(25, 147);
            btndodajPDF.Name = "btndodajPDF";
            btndodajPDF.Size = new Size(100, 23);
            btndodajPDF.TabIndex = 3;
            btndodajPDF.Text = "Dodaj plik PDF";
            btndodajPDF.UseVisualStyleBackColor = true;
            btndodajPDF.Click += btnDodajPDF_Click;
            // 
            // btnUsunPozycje
            // 
            btnUsunPozycje.Location = new Point(165, 147);
            btnUsunPozycje.Name = "btnUsunPozycje";
            btnUsunPozycje.Size = new Size(105, 23);
            btnUsunPozycje.TabIndex = 4;
            btnUsunPozycje.Text = "Usuń pozycję";
            btnUsunPozycje.UseVisualStyleBackColor = true;
            btnUsunPozycje.Click += btnUsunPozycje_Click;
            // 
            // dataGridPozycje
            // 
            dataGridPozycje.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridPozycje.Columns.AddRange(new DataGridViewColumn[] { KodProduktu, Ilosc });
            dataGridPozycje.Location = new Point(25, 194);
            dataGridPozycje.Name = "dataGridPozycje";
            dataGridPozycje.Size = new Size(245, 209);
            dataGridPozycje.TabIndex = 5;
            // 
            // KodProduktu
            // 
            KodProduktu.HeaderText = "Kod produktu";
            KodProduktu.Name = "KodProduktu";
            // 
            // Ilosc
            // 
            Ilosc.HeaderText = "Ilość";
            Ilosc.Name = "Ilosc";
            // 
            // txtOCR
            // 
            txtOCR.Location = new Point(541, 194);
            txtOCR.Multiline = true;
            txtOCR.Name = "txtOCR";
            txtOCR.Size = new Size(247, 219);
            txtOCR.TabIndex = 6;
            txtOCR.TextChanged += txtOCR_TextChanged;
            // 
            // txtilosc
            // 
            txtilosc.Location = new Point(25, 93);
            txtilosc.Name = "txtilosc";
            txtilosc.Size = new Size(321, 23);
            txtilosc.TabIndex = 7;
            txtilosc.Text = "Ilość:";
            txtilosc.TextChanged += textBox1_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(800, 450);
            Controls.Add(txtilosc);
            Controls.Add(txtOCR);
            Controls.Add(dataGridPozycje);
            Controls.Add(btnUsunPozycje);
            Controls.Add(btndodajPDF);
            Controls.Add(btnDodajPozycje);
            Controls.Add(txtKodProduktu);
            Controls.Add(labelOCR);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridPozycje).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelOCR;
        private TextBox txtKodProduktu;
        private Button btnDodajPozycje;
        private Button btndodajPDF;
        private Button btnUsunPozycje;
        private DataGridView dataGridPozycje;
        private DataGridViewTextBoxColumn KodProduktu;
        private DataGridViewTextBoxColumn Ilosc;
        private TextBox txtOCR;
        private TextBox txtilosc;
    }
}
