namespace WindowsFormsApp1
{
    partial class DecryptionScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.EncryptButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.backButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.VisualisationCheckBox = new System.Windows.Forms.CheckBox();
            this.percentageLabel = new System.Windows.Forms.Label();
            this.Key1label = new System.Windows.Forms.Label();
            this.Key2Label = new System.Windows.Forms.Label();
            this.LoadPathTextBox = new System.Windows.Forms.TextBox();
            this.Image1 = new System.Windows.Forms.PictureBox();
            this.FirstKeyTextBox = new System.Windows.Forms.TextBox();
            this.SecondKeyTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.threadCheckBox = new System.Windows.Forms.CheckBox();
            this.threadNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // EncryptButton
            // 
            this.EncryptButton.Location = new System.Drawing.Point(2, 480);
            this.EncryptButton.MinimumSize = new System.Drawing.Size(150, 50);
            this.EncryptButton.Name = "EncryptButton";
            this.EncryptButton.Size = new System.Drawing.Size(150, 50);
            this.EncryptButton.TabIndex = 1;
            this.EncryptButton.Text = "Decrypt";
            this.EncryptButton.UseVisualStyleBackColor = true;
            this.EncryptButton.Click += new System.EventHandler(this.EncryptButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(123, 99);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(135, 40);
            this.loadButton.TabIndex = 2;
            this.loadButton.Text = "Load Image ";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(378, 534);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(512, 52);
            this.progressBar1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(246, 285);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "   ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(2, 536);
            this.backButton.MinimumSize = new System.Drawing.Size(150, 50);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(150, 50);
            this.backButton.TabIndex = 6;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(153, 536);
            this.saveButton.MinimumSize = new System.Drawing.Size(150, 50);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(150, 50);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save Image";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(117, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 31);
            this.label3.TabIndex = 8;
            this.label3.Text = "Decryption";
            // 
            // VisualisationCheckBox
            // 
            this.VisualisationCheckBox.AutoSize = true;
            this.VisualisationCheckBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VisualisationCheckBox.Location = new System.Drawing.Point(2, 382);
            this.VisualisationCheckBox.Name = "VisualisationCheckBox";
            this.VisualisationCheckBox.Size = new System.Drawing.Size(122, 25);
            this.VisualisationCheckBox.TabIndex = 9;
            this.VisualisationCheckBox.Text = "Visualisation";
            this.VisualisationCheckBox.UseVisualStyleBackColor = true;
            this.VisualisationCheckBox.CheckedChanged += new System.EventHandler(this.VisualisationCheckBox_CheckedChanged);
            // 
            // percentageLabel
            // 
            this.percentageLabel.AutoSize = true;
            this.percentageLabel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.percentageLabel.Location = new System.Drawing.Point(319, 536);
            this.percentageLabel.MinimumSize = new System.Drawing.Size(50, 50);
            this.percentageLabel.Name = "percentageLabel";
            this.percentageLabel.Size = new System.Drawing.Size(50, 50);
            this.percentageLabel.TabIndex = 11;
            this.percentageLabel.Text = "0%";
            this.percentageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Key1label
            // 
            this.Key1label.AutoSize = true;
            this.Key1label.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Key1label.Location = new System.Drawing.Point(-2, 242);
            this.Key1label.MinimumSize = new System.Drawing.Size(55, 30);
            this.Key1label.Name = "Key1label";
            this.Key1label.Size = new System.Drawing.Size(55, 30);
            this.Key1label.TabIndex = 15;
            this.Key1label.Text = "Key1 :";
            this.Key1label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Key2Label
            // 
            this.Key2Label.AutoSize = true;
            this.Key2Label.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Key2Label.Location = new System.Drawing.Point(-2, 276);
            this.Key2Label.MinimumSize = new System.Drawing.Size(55, 30);
            this.Key2Label.Name = "Key2Label";
            this.Key2Label.Size = new System.Drawing.Size(55, 30);
            this.Key2Label.TabIndex = 16;
            this.Key2Label.Text = "Key2 :";
            this.Key2Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LoadPathTextBox
            // 
            this.LoadPathTextBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadPathTextBox.Location = new System.Drawing.Point(2, 166);
            this.LoadPathTextBox.MinimumSize = new System.Drawing.Size(360, 40);
            this.LoadPathTextBox.Name = "LoadPathTextBox";
            this.LoadPathTextBox.ReadOnly = true;
            this.LoadPathTextBox.Size = new System.Drawing.Size(360, 29);
            this.LoadPathTextBox.TabIndex = 17;
            // 
            // Image1
            // 
            this.Image1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Image1.Image = global::WindowsFormsApp1.Properties.Resources.No_Image;
            this.Image1.ImageLocation = "";
            this.Image1.Location = new System.Drawing.Point(378, 12);
            this.Image1.Name = "Image1";
            this.Image1.Size = new System.Drawing.Size(512, 512);
            this.Image1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Image1.TabIndex = 3;
            this.Image1.TabStop = false;
            // 
            // FirstKeyTextBox
            // 
            this.FirstKeyTextBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FirstKeyTextBox.Location = new System.Drawing.Point(57, 242);
            this.FirstKeyTextBox.MinimumSize = new System.Drawing.Size(305, 30);
            this.FirstKeyTextBox.Name = "FirstKeyTextBox";
            this.FirstKeyTextBox.Size = new System.Drawing.Size(305, 29);
            this.FirstKeyTextBox.TabIndex = 18;
            // 
            // SecondKeyTextBox
            // 
            this.SecondKeyTextBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SecondKeyTextBox.Location = new System.Drawing.Point(57, 278);
            this.SecondKeyTextBox.MinimumSize = new System.Drawing.Size(305, 30);
            this.SecondKeyTextBox.Name = "SecondKeyTextBox";
            this.SecondKeyTextBox.Size = new System.Drawing.Size(305, 29);
            this.SecondKeyTextBox.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(158, 509);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 21);
            this.label1.TabIndex = 20;
            this.label1.Text = "Time: ";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Location = new System.Drawing.Point(220, 509);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(72, 21);
            this.timeLabel.TabIndex = 21;
            this.timeLabel.Text = "00:00:00";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(123, 326);
            this.button1.MinimumSize = new System.Drawing.Size(135, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 40);
            this.button1.TabIndex = 22;
            this.button1.Text = "Approve Keys";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(69, 214);
            this.label4.MaximumSize = new System.Drawing.Size(340, 35);
            this.label4.MinimumSize = new System.Drawing.Size(300, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(300, 15);
            this.label4.TabIndex = 23;
            this.label4.Text = "Enter the keys used to encrypt the image.";
            // 
            // threadCheckBox
            // 
            this.threadCheckBox.AutoSize = true;
            this.threadCheckBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threadCheckBox.Location = new System.Drawing.Point(2, 427);
            this.threadCheckBox.Name = "threadCheckBox";
            this.threadCheckBox.Size = new System.Drawing.Size(276, 25);
            this.threadCheckBox.TabIndex = 24;
            this.threadCheckBox.Text = "Dividing picture, Number threads";
            this.threadCheckBox.UseVisualStyleBackColor = true;
            this.threadCheckBox.CheckedChanged += new System.EventHandler(this.threadCheckBox_CheckedChanged);
            // 
            // threadNumericUpDown
            // 
            this.threadNumericUpDown.Location = new System.Drawing.Point(293, 427);
            this.threadNumericUpDown.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.threadNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.threadNumericUpDown.Name = "threadNumericUpDown";
            this.threadNumericUpDown.Size = new System.Drawing.Size(69, 20);
            this.threadNumericUpDown.TabIndex = 25;
            this.threadNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // DecryptionScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(902, 592);
            this.Controls.Add(this.threadNumericUpDown);
            this.Controls.Add(this.threadCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SecondKeyTextBox);
            this.Controls.Add(this.FirstKeyTextBox);
            this.Controls.Add(this.LoadPathTextBox);
            this.Controls.Add(this.Key2Label);
            this.Controls.Add(this.Key1label);
            this.Controls.Add(this.percentageLabel);
            this.Controls.Add(this.VisualisationCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Image1);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.EncryptButton);
            this.MaximumSize = new System.Drawing.Size(918, 630);
            this.MinimumSize = new System.Drawing.Size(918, 630);
            this.Name = "DecryptionScreen";
            ((System.ComponentModel.ISupportInitialize)(this.Image1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button EncryptButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.PictureBox Image1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox VisualisationCheckBox;
        private System.Windows.Forms.Label percentageLabel;
        private System.Windows.Forms.Label Key1label;
        private System.Windows.Forms.Label Key2Label;
        private System.Windows.Forms.TextBox LoadPathTextBox;
        private System.Windows.Forms.TextBox FirstKeyTextBox;
        private System.Windows.Forms.TextBox SecondKeyTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox threadCheckBox;
        private System.Windows.Forms.NumericUpDown threadNumericUpDown;
    }
}

