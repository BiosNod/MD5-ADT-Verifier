namespace HashVerifyer
{
    partial class MainForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.labelDirectory = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.loggerRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.badFilesTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.goodFilesTextBox = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioHybridMode = new System.Windows.Forms.RadioButton();
            this.radioHashAddOnly = new System.Windows.Forms.RadioButton();
            this.radioHashCheckOnly = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.newFileTextBox = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.changedFileTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(137, 299);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(260, 70);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonStartClick);
            // 
            // labelDirectory
            // 
            this.labelDirectory.AutoSize = true;
            this.labelDirectory.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelDirectory.Location = new System.Drawing.Point(217, 56);
            this.labelDirectory.Name = "labelDirectory";
            this.labelDirectory.Size = new System.Drawing.Size(228, 30);
            this.labelDirectory.TabIndex = 1;
            this.labelDirectory.Text = "Please select directory";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(22, 44);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(174, 54);
            this.button2.TabIndex = 2;
            this.button2.Text = "Select";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.selectDirClick);
            // 
            // loggerRichTextBox
            // 
            this.loggerRichTextBox.Location = new System.Drawing.Point(22, 1075);
            this.loggerRichTextBox.Name = "loggerRichTextBox";
            this.loggerRichTextBox.Size = new System.Drawing.Size(1410, 194);
            this.loggerRichTextBox.TabIndex = 3;
            this.loggerRichTextBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(22, 886);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(521, 90);
            this.label2.TabIndex = 4;
            this.label2.Text = "This program calculate MD5 hash and write it to ADS\r\n(alternative data steam) int" +
    "o MFT (main file table)\r\nfor NTFS file system only (Windows)";
            // 
            // badFilesTextBox
            // 
            this.badFilesTextBox.Location = new System.Drawing.Point(589, 330);
            this.badFilesTextBox.Name = "badFilesTextBox";
            this.badFilesTextBox.Size = new System.Drawing.Size(843, 194);
            this.badFilesTextBox.TabIndex = 5;
            this.badFilesTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(589, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(423, 30);
            this.label1.TabIndex = 6;
            this.label1.Text = "Bad (MD5 mismatch - verify fail) files log:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(22, 1033);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 30);
            this.label4.TabIndex = 8;
            this.label4.Text = "Global log:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(589, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(389, 30);
            this.label5.TabIndex = 10;
            this.label5.Text = "Good (MD5 match - verified) files log:";
            // 
            // goodFilesTextBox
            // 
            this.goodFilesTextBox.Location = new System.Drawing.Point(589, 81);
            this.goodFilesTextBox.Name = "goodFilesTextBox";
            this.goodFilesTextBox.Size = new System.Drawing.Size(843, 194);
            this.goodFilesTextBox.TabIndex = 9;
            this.goodFilesTextBox.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HashVerifyer.Properties.Resources._2manga_eyes_looking_from_paper_tear_black_white_color_anime_girl_peeps_out_isolated_380711_443;
            this.pictureBox1.InitialImage = global::HashVerifyer.Properties.Resources._2manga_eyes_looking_from_paper_tear_black_white_color_anime_girl_peeps_out_isolated_380711_4431;
            this.pictureBox1.Location = new System.Drawing.Point(22, 420);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(518, 445);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // radioHybridMode
            // 
            this.radioHybridMode.AutoSize = true;
            this.radioHybridMode.Checked = true;
            this.radioHybridMode.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioHybridMode.Location = new System.Drawing.Point(22, 144);
            this.radioHybridMode.Name = "radioHybridMode";
            this.radioHybridMode.Size = new System.Drawing.Size(404, 34);
            this.radioHybridMode.TabIndex = 12;
            this.radioHybridMode.TabStop = true;
            this.radioHybridMode.Text = "Hybrid - add/check hash to/from ADT";
            this.radioHybridMode.UseVisualStyleBackColor = true;
            // 
            // radioHashAddOnly
            // 
            this.radioHashAddOnly.AutoSize = true;
            this.radioHashAddOnly.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioHashAddOnly.Location = new System.Drawing.Point(22, 190);
            this.radioHashAddOnly.Name = "radioHashAddOnly";
            this.radioHashAddOnly.Size = new System.Drawing.Size(530, 34);
            this.radioHashAddOnly.TabIndex = 13;
            this.radioHashAddOnly.Text = "Skip check, only add hash to ADT if hash don\'t exist";
            this.radioHashAddOnly.UseVisualStyleBackColor = true;
            // 
            // radioHashCheckOnly
            // 
            this.radioHashCheckOnly.AutoSize = true;
            this.radioHashCheckOnly.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioHashCheckOnly.Location = new System.Drawing.Point(22, 241);
            this.radioHashCheckOnly.Name = "radioHashCheckOnly";
            this.radioHashCheckOnly.Size = new System.Drawing.Size(462, 34);
            this.radioHashCheckOnly.TabIndex = 14;
            this.radioHashCheckOnly.Text = "Skip add, only check hash from ADT is exists";
            this.radioHashCheckOnly.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(589, 545);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(375, 30);
            this.label3.TabIndex = 16;
            this.label3.Text = "New files (without MD5 in ADS) log:";
            // 
            // newFileTextBox
            // 
            this.newFileTextBox.Location = new System.Drawing.Point(589, 587);
            this.newFileTextBox.Name = "newFileTextBox";
            this.newFileTextBox.Size = new System.Drawing.Size(843, 194);
            this.newFileTextBox.TabIndex = 15;
            this.newFileTextBox.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(589, 811);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(421, 30);
            this.label6.TabIndex = 18;
            this.label6.Text = "Changed (and must recalc MD5) files log:";
            // 
            // changedFileTextBox
            // 
            this.changedFileTextBox.Location = new System.Drawing.Point(589, 853);
            this.changedFileTextBox.Name = "changedFileTextBox";
            this.changedFileTextBox.Size = new System.Drawing.Size(843, 194);
            this.changedFileTextBox.TabIndex = 17;
            this.changedFileTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1461, 1298);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.changedFileTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.newFileTextBox);
            this.Controls.Add(this.radioHashCheckOnly);
            this.Controls.Add(this.radioHashAddOnly);
            this.Controls.Add(this.radioHybridMode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.goodFilesTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.badFilesTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.loggerRichTextBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelDirectory);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MainForm";
            this.Text = "MD5 ADT Verifier";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private Label labelDirectory;
        private Button button2;
        private Label label2;
        public RichTextBox loggerRichTextBox;
        public RichTextBox badFilesTextBox;
        private Label label1;
        private Label label4;
        private Label label5;
        public RichTextBox goodFilesTextBox;
        private PictureBox pictureBox1;
        private RadioButton radioHybridMode;
        private RadioButton radioHashAddOnly;
        private RadioButton radioHashCheckOnly;
        private Label label3;
        public RichTextBox newFileTextBox;
        private Label label6;
        public RichTextBox changedFileTextBox;
    }
}