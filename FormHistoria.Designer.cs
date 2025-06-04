namespace ZamowieniaApp1
{
    partial class FormHistoria
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ListBox listBoxZamowienia;
        private System.Windows.Forms.Button btnEksportuj;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxZamowienia = new System.Windows.Forms.ListBox();
            this.btnEksportuj = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxZamowienia
            // 
            this.listBoxZamowienia.FormattingEnabled = true;
            this.listBoxZamowienia.ItemHeight = 15;
            this.listBoxZamowienia.Location = new System.Drawing.Point(20, 20);
            this.listBoxZamowienia.Name = "listBoxZamowienia";
            this.listBoxZamowienia.Size = new System.Drawing.Size(250, 124);
            this.listBoxZamowienia.TabIndex = 0;
            // 
            // btnEksportuj
            // 
            this.btnEksportuj.Location = new System.Drawing.Point(170, 160);
            this.btnEksportuj.Name = "btnEksportuj";
            this.btnEksportuj.Size = new System.Drawing.Size(100, 30);
            this.btnEksportuj.TabIndex = 1;
            this.btnEksportuj.Text = "Eksportuj";
            this.btnEksportuj.UseVisualStyleBackColor = true;
            this.btnEksportuj.Click += new System.EventHandler(this.btnEksportuj_Click);
            // 
            // FormHistoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 211);
            this.Controls.Add(this.btnEksportuj);
            this.Controls.Add(this.listBoxZamowienia);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormHistoria";
            this.Text = "Eksport historii zamówień";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
