﻿namespace WindowsFormsApp1
{
    partial class EncryptionScreen
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
            this.GenerationButton = new System.Windows.Forms.Button();
            this.Key1label = new System.Windows.Forms.Label();
            this.Key2Label = new System.Windows.Forms.Label();
            this.LoadPathTextBox = new System.Windows.Forms.TextBox();
            this.Image1 = new System.Windows.Forms.PictureBox();
            this.FirstKeyTextBox = new System.Windows.Forms.TextBox();
            this.SecondKeyTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.saveKeysButton = new System.Windows.Forms.Button();
            this.threadCheckBox = new System.Windows.Forms.CheckBox();
            this.threadNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.NumberThreadLabel = new System.Windows.Forms.Label();
            this.numberPiecesLabel = new System.Windows.Forms.Label();
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
            this.EncryptButton.Text = "Encrypt";
            this.EncryptButton.UseVisualStyleBackColor = true;
            this.EncryptButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(123, 69);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(135, 40);
            this.loadButton.TabIndex = 2;
            this.loadButton.Text = "Load Image ";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.button2_Click);
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
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(117, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 31);
            this.label3.TabIndex = 8;
            this.label3.Text = "Encryption";
            // 
            // VisualisationCheckBox
            // 
            this.VisualisationCheckBox.AutoSize = true;
            this.VisualisationCheckBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VisualisationCheckBox.Location = new System.Drawing.Point(5, 358);
            this.VisualisationCheckBox.Name = "VisualisationCheckBox";
            this.VisualisationCheckBox.Size = new System.Drawing.Size(122, 25);
            this.VisualisationCheckBox.TabIndex = 9;
            this.VisualisationCheckBox.Text = "Visualisation";
            this.VisualisationCheckBox.UseVisualStyleBackColor = true;
            this.VisualisationCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
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
            // GenerationButton
            // 
            this.GenerationButton.Location = new System.Drawing.Point(123, 170);
            this.GenerationButton.Name = "GenerationButton";
            this.GenerationButton.Size = new System.Drawing.Size(135, 40);
            this.GenerationButton.TabIndex = 12;
            this.GenerationButton.Text = "Generate Keys";
            this.GenerationButton.UseVisualStyleBackColor = true;
            this.GenerationButton.Click += new System.EventHandler(this.GenerationButton_Click);
            // 
            // Key1label
            // 
            this.Key1label.AutoSize = true;
            this.Key1label.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Key1label.Location = new System.Drawing.Point(-2, 236);
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
            this.Key2Label.Location = new System.Drawing.Point(-2, 267);
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
            this.LoadPathTextBox.Location = new System.Drawing.Point(9, 124);
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
            this.FirstKeyTextBox.Location = new System.Drawing.Point(57, 234);
            this.FirstKeyTextBox.MinimumSize = new System.Drawing.Size(305, 30);
            this.FirstKeyTextBox.Name = "FirstKeyTextBox";
            this.FirstKeyTextBox.ReadOnly = true;
            this.FirstKeyTextBox.Size = new System.Drawing.Size(305, 29);
            this.FirstKeyTextBox.TabIndex = 18;
            // 
            // SecondKeyTextBox
            // 
            this.SecondKeyTextBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SecondKeyTextBox.Location = new System.Drawing.Point(57, 269);
            this.SecondKeyTextBox.MinimumSize = new System.Drawing.Size(305, 30);
            this.SecondKeyTextBox.Name = "SecondKeyTextBox";
            this.SecondKeyTextBox.ReadOnly = true;
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
            // saveKeysButton
            // 
            this.saveKeysButton.Location = new System.Drawing.Point(227, 310);
            this.saveKeysButton.MinimumSize = new System.Drawing.Size(135, 40);
            this.saveKeysButton.Name = "saveKeysButton";
            this.saveKeysButton.Size = new System.Drawing.Size(135, 40);
            this.saveKeysButton.TabIndex = 22;
            this.saveKeysButton.Text = "Save Keys";
            this.saveKeysButton.UseVisualStyleBackColor = true;
            this.saveKeysButton.Click += new System.EventHandler(this.saveKeysButton_Click);
            // 
            // threadCheckBox
            // 
            this.threadCheckBox.AutoSize = true;
            this.threadCheckBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threadCheckBox.Location = new System.Drawing.Point(5, 389);
            this.threadCheckBox.Name = "threadCheckBox";
            this.threadCheckBox.Size = new System.Drawing.Size(147, 25);
            this.threadCheckBox.TabIndex = 23;
            this.threadCheckBox.Text = "Dividing picture";
            this.threadCheckBox.UseVisualStyleBackColor = true;
            this.threadCheckBox.CheckedChanged += new System.EventHandler(this.threadCheckBox_CheckedChanged);
            // 
            // threadNumericUpDown
            // 
            this.threadNumericUpDown.Location = new System.Drawing.Point(272, 447);
            this.threadNumericUpDown.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.threadNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.threadNumericUpDown.Name = "threadNumericUpDown";
            this.threadNumericUpDown.Size = new System.Drawing.Size(90, 20);
            this.threadNumericUpDown.TabIndex = 24;
            this.threadNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.threadNumericUpDown.ValueChanged += new System.EventHandler(this.threadNumericUpDown_ValueChanged);
            // 
            // NumberThreadLabel
            // 
            this.NumberThreadLabel.AutoSize = true;
            this.NumberThreadLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumberThreadLabel.Location = new System.Drawing.Point(16, 447);
            this.NumberThreadLabel.Name = "NumberThreadLabel";
            this.NumberThreadLabel.Size = new System.Drawing.Size(111, 19);
            this.NumberThreadLabel.TabIndex = 25;
            this.NumberThreadLabel.Text = " Number threads";
            // 
            // numberPiecesLabel
            // 
            this.numberPiecesLabel.AutoSize = true;
            this.numberPiecesLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberPiecesLabel.Location = new System.Drawing.Point(16, 422);
            this.numberPiecesLabel.Name = "numberPiecesLabel";
            this.numberPiecesLabel.Size = new System.Drawing.Size(105, 19);
            this.numberPiecesLabel.TabIndex = 26;
            this.numberPiecesLabel.Text = " Number pieces";
            // 
            // EncryptionScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(902, 592);
            this.Controls.Add(this.numberPiecesLabel);
            this.Controls.Add(this.NumberThreadLabel);
            this.Controls.Add(this.threadNumericUpDown);
            this.Controls.Add(this.threadCheckBox);
            this.Controls.Add(this.saveKeysButton);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SecondKeyTextBox);
            this.Controls.Add(this.FirstKeyTextBox);
            this.Controls.Add(this.LoadPathTextBox);
            this.Controls.Add(this.Key2Label);
            this.Controls.Add(this.Key1label);
            this.Controls.Add(this.GenerationButton);
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
            this.Name = "EncryptionScreen";
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
        private System.Windows.Forms.Button GenerationButton;
        private System.Windows.Forms.Label Key1label;
        private System.Windows.Forms.Label Key2Label;
        private System.Windows.Forms.TextBox LoadPathTextBox;
        private System.Windows.Forms.TextBox FirstKeyTextBox;
        private System.Windows.Forms.TextBox SecondKeyTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Button saveKeysButton;
        private System.Windows.Forms.CheckBox threadCheckBox;
        private System.Windows.Forms.NumericUpDown threadNumericUpDown;
        private System.Windows.Forms.Label NumberThreadLabel;
        private System.Windows.Forms.Label numberPiecesLabel;
    }
}

