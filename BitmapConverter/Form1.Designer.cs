namespace BitmapConverter
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.searchButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.asmButton = new System.Windows.Forms.Button();
            this.cButton = new System.Windows.Forms.Button();
            this.resultButton = new System.Windows.Forms.Button();
            this.TargetStyle = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Display = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Pliki graficzne (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(781, 111);
            this.searchButton.Margin = new System.Windows.Forms.Padding(4);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(100, 28);
            this.searchButton.TabIndex = 0;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(480, 114);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(269, 22);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(381, 118);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "File path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.label2.Location = new System.Drawing.Point(192, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(794, 58);
            this.label2.TabIndex = 3;
            this.label2.Text = "IMAGE TO BITMAP CONVERTER";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(529, 164);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(169, 157);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 364);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Display:";
            // 
            // asmButton
            // 
            this.asmButton.Location = new System.Drawing.Point(365, 583);
            this.asmButton.Margin = new System.Windows.Forms.Padding(4);
            this.asmButton.Name = "asmButton";
            this.asmButton.Size = new System.Drawing.Size(151, 28);
            this.asmButton.TabIndex = 11;
            this.asmButton.Text = "Execute in Asembler";
            this.asmButton.UseVisualStyleBackColor = true;
            this.asmButton.Click += new System.EventHandler(this.asmButton_Click);
            // 
            // cButton
            // 
            this.cButton.Location = new System.Drawing.Point(549, 583);
            this.cButton.Margin = new System.Windows.Forms.Padding(4);
            this.cButton.Name = "cButton";
            this.cButton.Size = new System.Drawing.Size(136, 28);
            this.cButton.TabIndex = 12;
            this.cButton.Text = "Execute in C";
            this.cButton.UseVisualStyleBackColor = true;
            this.cButton.Click += new System.EventHandler(this.cButton_Click);
            // 
            // resultButton
            // 
            this.resultButton.Enabled = false;
            this.resultButton.Location = new System.Drawing.Point(719, 583);
            this.resultButton.Margin = new System.Windows.Forms.Padding(4);
            this.resultButton.Name = "resultButton";
            this.resultButton.Size = new System.Drawing.Size(100, 28);
            this.resultButton.TabIndex = 13;
            this.resultButton.Text = "Result";
            this.resultButton.UseVisualStyleBackColor = true;
            this.resultButton.Click += new System.EventHandler(this.resultButton_Click);
            // 
            // TargetStyle
            // 
            this.TargetStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TargetStyle.FormattingEnabled = true;
            this.TargetStyle.Items.AddRange(new object[] {
            "Monochromatic",
            "RBG565"});
            this.TargetStyle.Location = new System.Drawing.Point(529, 434);
            this.TargetStyle.Margin = new System.Windows.Forms.Padding(4);
            this.TargetStyle.Name = "TargetStyle";
            this.TargetStyle.Size = new System.Drawing.Size(169, 24);
            this.TargetStyle.TabIndex = 14;
            this.TargetStyle.TextChanged += new System.EventHandler(this.TargetStyle_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(231, 438);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Target image style:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(984, 583);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 20);
            this.label7.TabIndex = 17;
            this.label7.Text = "Time:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1076, 560);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 16);
            this.label8.TabIndex = 18;
            // 
            // Display
            // 
            this.Display.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Display.FormattingEnabled = true;
            this.Display.Items.AddRange(new object[] {
            "128x64 OLED SSD1306",
            "128x160 TFT ST7735",
            "240x320 TFT ILI9341"});
            this.Display.Location = new System.Drawing.Point(529, 356);
            this.Display.Name = "Display";
            this.Display.Size = new System.Drawing.Size(168, 24);
            this.Display.TabIndex = 19;
            this.Display.SelectedIndexChanged += new System.EventHandler(this.Display_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 639);
            this.Controls.Add(this.Display);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TargetStyle);
            this.Controls.Add(this.resultButton);
            this.Controls.Add(this.cButton);
            this.Controls.Add(this.asmButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.searchButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "BitmapConverter";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button asmButton;
        private System.Windows.Forms.Button cButton;
        private System.Windows.Forms.Button resultButton;
        private System.Windows.Forms.ComboBox TargetStyle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox Display;
    }
}

