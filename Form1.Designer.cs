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
            lblSuma = new Label();
            label1 = new Label();
            label2 = new Label();
            btnPokazPodsumowanie = new Button();
            txtTrescMaila = new TextBox();
            btnPodgladCSV = new Button();
            btnWyslijMaila = new Button();
            cmbEmailOdbiorcy = new ComboBox();
            label3 = new Label();
            pictureBoxBackground = new PictureBox();
            btnOdswiez = new Button();
            btnHistoria = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridPozycje).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBackground).BeginInit();
            SuspendLayout();
            // 
            // labelOCR
            // 
            labelOCR.AutoSize = true;
            labelOCR.Location = new Point(431, 101);
            labelOCR.Name = "labelOCR";
            labelOCR.Size = new Size(150, 15);
            labelOCR.TabIndex = 0;
            labelOCR.Text = "Podgląd tekstu z PDF(OCR)";
            labelOCR.Click += labelOCR_Click;
            // 
            // txtKodProduktu
            // 
            txtKodProduktu.Location = new Point(179, 101);
            txtKodProduktu.Name = "txtKodProduktu";
            txtKodProduktu.Size = new Size(77, 23);
            txtKodProduktu.TabIndex = 1;
            // 
            // btnDodajPozycje
            // 
            btnDodajPozycje.Location = new Point(104, 43);
            btnDodajPozycje.Name = "btnDodajPozycje";
            btnDodajPozycje.Size = new Size(152, 23);
            btnDodajPozycje.TabIndex = 2;
            btnDodajPozycje.Text = "Dodaj pozycję";
            btnDodajPozycje.UseVisualStyleBackColor = true;
            btnDodajPozycje.Click += btnDodajPozycje_Click;
            // 
            // btndodajPDF
            // 
            btndodajPDF.Location = new Point(104, 14);
            btndodajPDF.Name = "btndodajPDF";
            btndodajPDF.Size = new Size(152, 23);
            btndodajPDF.TabIndex = 3;
            btndodajPDF.Text = "Dodaj plik PDF";
            btndodajPDF.UseVisualStyleBackColor = true;
            btndodajPDF.Click += btnDodajPDF_Click;
            // 
            // btnUsunPozycje
            // 
            btnUsunPozycje.Location = new Point(104, 72);
            btnUsunPozycje.Name = "btnUsunPozycje";
            btnUsunPozycje.Size = new Size(152, 23);
            btnUsunPozycje.TabIndex = 4;
            btnUsunPozycje.Text = "Usuń pozycję";
            btnUsunPozycje.UseVisualStyleBackColor = true;
            btnUsunPozycje.Click += btnUsunPozycje_Click;
            // 
            // dataGridPozycje
            // 
            dataGridPozycje.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridPozycje.Columns.AddRange(new DataGridViewColumn[] { KodProduktu, Ilosc });
            dataGridPozycje.Location = new Point(12, 178);
            dataGridPozycje.Name = "dataGridPozycje";
            dataGridPozycje.Size = new Size(244, 476);
            dataGridPozycje.TabIndex = 5;
            dataGridPozycje.CellContentClick += dataGridPozycje_CellContentClick;
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
            txtOCR.ForeColor = SystemColors.ActiveCaptionText;
            txtOCR.Location = new Point(289, 122);
            txtOCR.Multiline = true;
            txtOCR.Name = "txtOCR";
            txtOCR.Size = new Size(425, 203);
            txtOCR.TabIndex = 6;
            txtOCR.TextChanged += txtOCR_TextChanged;
            // 
            // txtilosc
            // 
            txtilosc.Location = new Point(179, 130);
            txtilosc.Name = "txtilosc";
            txtilosc.Size = new Size(77, 23);
            txtilosc.TabIndex = 7;
            txtilosc.TextChanged += textBox1_TextChanged;
            // 
            // lblSuma
            // 
            lblSuma.AutoSize = true;
            lblSuma.Location = new Point(12, 160);
            lblSuma.Name = "lblSuma";
            lblSuma.Size = new Size(141, 15);
            lblSuma.TabIndex = 8;
            lblSuma.Text = "Suma zamówienia: 0,00 zł";
            lblSuma.Click += lblSuma_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 109);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 9;
            label1.Text = "Kod produktu:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 133);
            label2.Name = "label2";
            label2.Size = new Size(34, 15);
            label2.TabIndex = 10;
            label2.Text = "Ilość:";
            // 
            // btnPokazPodsumowanie
            // 
            btnPokazPodsumowanie.Location = new Point(560, 391);
            btnPokazPodsumowanie.Name = "btnPokazPodsumowanie";
            btnPokazPodsumowanie.Size = new Size(150, 23);
            btnPokazPodsumowanie.TabIndex = 11;
            btnPokazPodsumowanie.Text = "Pokaż podsumowanie";
            btnPokazPodsumowanie.UseVisualStyleBackColor = true;
            btnPokazPodsumowanie.Click += btnPokazPodsumowanie_Click;
            // 
            // txtTrescMaila
            // 
            txtTrescMaila.Location = new Point(289, 421);
            txtTrescMaila.Multiline = true;
            txtTrescMaila.Name = "txtTrescMaila";
            txtTrescMaila.ScrollBars = ScrollBars.Vertical;
            txtTrescMaila.Size = new Size(576, 233);
            txtTrescMaila.TabIndex = 13;
            txtTrescMaila.TextChanged += txtTrescMaila_TextChanged;
            // 
            // btnPodgladCSV
            // 
            btnPodgladCSV.Location = new Point(561, 362);
            btnPodgladCSV.Name = "btnPodgladCSV";
            btnPodgladCSV.Size = new Size(149, 23);
            btnPodgladCSV.TabIndex = 14;
            btnPodgladCSV.Text = "Podgląd CSV";
            btnPodgladCSV.UseVisualStyleBackColor = true;
            btnPodgladCSV.Click += btnPodgladCSV_Click;
            // 
            // btnWyslijMaila
            // 
            btnWyslijMaila.Location = new Point(716, 392);
            btnWyslijMaila.Name = "btnWyslijMaila";
            btnWyslijMaila.Size = new Size(149, 23);
            btnWyslijMaila.TabIndex = 15;
            btnWyslijMaila.Text = "Wyślij zamówienie";
            btnWyslijMaila.UseVisualStyleBackColor = true;
            btnWyslijMaila.Click += btnWyslijMaila_Click;
            // 
            // cmbEmailOdbiorcy
            // 
            cmbEmailOdbiorcy.FormattingEnabled = true;
            cmbEmailOdbiorcy.Location = new Point(289, 392);
            cmbEmailOdbiorcy.Name = "cmbEmailOdbiorcy";
            cmbEmailOdbiorcy.Size = new Size(265, 23);
            cmbEmailOdbiorcy.TabIndex = 16;
            cmbEmailOdbiorcy.SelectedIndexChanged += cmbEmailOdbiorcy_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(289, 374);
            label3.Name = "label3";
            label3.Size = new Size(139, 15);
            label3.TabIndex = 17;
            label3.Text = "Wybierz odbiorcę e-mail:";
            label3.Click += label3_Click;
            // 
            // pictureBoxBackground
            // 
            pictureBoxBackground.Dock = DockStyle.Fill;
            pictureBoxBackground.Location = new Point(0, 0);
            pictureBoxBackground.Name = "pictureBoxBackground";
            pictureBoxBackground.Size = new Size(922, 683);
            pictureBoxBackground.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBackground.TabIndex = 18;
            pictureBoxBackground.TabStop = false;
            pictureBoxBackground.Click += pictureBox1_Click;
            // 
            // btnOdswiez
            // 
            btnOdswiez.Location = new Point(742, 23);
            btnOdswiez.Name = "btnOdswiez";
            btnOdswiez.Size = new Size(123, 23);
            btnOdswiez.TabIndex = 19;
            btnOdswiez.Text = "Odśwież aplikację";
            btnOdswiez.UseVisualStyleBackColor = true;
            btnOdswiez.Click += btnOdswiez_Click;
            // 
            // btnHistoria
            // 
            btnHistoria.Location = new Point(742, 160);
            btnHistoria.Name = "btnHistoria";
            btnHistoria.Size = new Size(153, 23);
            btnHistoria.TabIndex = 20;
            btnHistoria.Text = "Historia zamówień";
            btnHistoria.UseVisualStyleBackColor = true;
            btnHistoria.Click += btnHistoria_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(922, 683);
            Controls.Add(btnHistoria);
            Controls.Add(btnOdswiez);
            Controls.Add(label3);
            Controls.Add(cmbEmailOdbiorcy);
            Controls.Add(btnWyslijMaila);
            Controls.Add(btnPodgladCSV);
            Controls.Add(txtTrescMaila);
            Controls.Add(btnPokazPodsumowanie);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblSuma);
            Controls.Add(txtilosc);
            Controls.Add(txtOCR);
            Controls.Add(dataGridPozycje);
            Controls.Add(btnUsunPozycje);
            Controls.Add(btndodajPDF);
            Controls.Add(btnDodajPozycje);
            Controls.Add(txtKodProduktu);
            Controls.Add(labelOCR);
            Controls.Add(pictureBoxBackground);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridPozycje).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBackground).EndInit();
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
        private Label lblSuma;
        private Label label1;
        private Label label2;
        private Button btnPokazPodsumowanie;
        private TextBox txtTrescMaila;
        private Button btnPodgladCSV;
        private Button btnWyslijMaila;
        private ComboBox cmbEmailOdbiorcy;
        private Label label3;
        private PictureBox pictureBoxBackground;
        private Button btnOdswiez;
        private Button btnHistoria;
    }
}
