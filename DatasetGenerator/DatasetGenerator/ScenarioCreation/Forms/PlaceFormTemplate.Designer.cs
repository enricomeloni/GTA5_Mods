namespace DatasetGenerator.ScenarioCreation.Forms
{
    partial class PlaceFormTemplate
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
            this.placeComboBox = new System.Windows.Forms.ComboBox();
            this.teleportButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.positionXtextBox = new System.Windows.Forms.TextBox();
            this.positionYtextBox = new System.Windows.Forms.TextBox();
            this.positionZtextBox = new System.Windows.Forms.TextBox();
            this.basePositionLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.currentPositionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // placeComboBox
            // 
            this.placeComboBox.FormattingEnabled = true;
            this.placeComboBox.Location = new System.Drawing.Point(85, 191);
            this.placeComboBox.Name = "placeComboBox";
            this.placeComboBox.Size = new System.Drawing.Size(209, 21);
            this.placeComboBox.TabIndex = 0;
            // 
            // teleportButton
            // 
            this.teleportButton.Location = new System.Drawing.Point(153, 218);
            this.teleportButton.Name = "teleportButton";
            this.teleportButton.Size = new System.Drawing.Size(75, 23);
            this.teleportButton.TabIndex = 1;
            this.teleportButton.Text = "Teleport";
            this.teleportButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Premade Locations";
            // 
            // positionXtextBox
            // 
            this.positionXtextBox.Location = new System.Drawing.Point(47, 79);
            this.positionXtextBox.Name = "positionXtextBox";
            this.positionXtextBox.Size = new System.Drawing.Size(100, 20);
            this.positionXtextBox.TabIndex = 3;
            // 
            // positionYtextBox
            // 
            this.positionYtextBox.Location = new System.Drawing.Point(153, 79);
            this.positionYtextBox.Name = "positionYtextBox";
            this.positionYtextBox.Size = new System.Drawing.Size(100, 20);
            this.positionYtextBox.TabIndex = 4;
            // 
            // positionZtextBox
            // 
            this.positionZtextBox.Location = new System.Drawing.Point(257, 79);
            this.positionZtextBox.Name = "positionZtextBox";
            this.positionZtextBox.Size = new System.Drawing.Size(100, 20);
            this.positionZtextBox.TabIndex = 5;
            // 
            // basePositionLabel
            // 
            this.basePositionLabel.AutoSize = true;
            this.basePositionLabel.Location = new System.Drawing.Point(44, 40);
            this.basePositionLabel.Name = "basePositionLabel";
            this.basePositionLabel.Size = new System.Drawing.Size(70, 13);
            this.basePositionLabel.TabIndex = 6;
            this.basePositionLabel.Text = "Base position";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(194, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(303, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Z";
            // 
            // currentPositionButton
            // 
            this.currentPositionButton.Location = new System.Drawing.Point(128, 105);
            this.currentPositionButton.Name = "currentPositionButton";
            this.currentPositionButton.Size = new System.Drawing.Size(143, 23);
            this.currentPositionButton.TabIndex = 10;
            this.currentPositionButton.Text = "Current Position";
            this.currentPositionButton.UseVisualStyleBackColor = true;
            // 
            // PlaceFormTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 338);
            this.Controls.Add(this.currentPositionButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.basePositionLabel);
            this.Controls.Add(this.positionZtextBox);
            this.Controls.Add(this.positionYtextBox);
            this.Controls.Add(this.positionXtextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.teleportButton);
            this.Controls.Add(this.placeComboBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "PlaceFormTemplate";
            this.Text = "PlaceFormTemplate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox placeComboBox;
        private System.Windows.Forms.Button teleportButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox positionXtextBox;
        private System.Windows.Forms.TextBox positionYtextBox;
        private System.Windows.Forms.TextBox positionZtextBox;
        private System.Windows.Forms.Label basePositionLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button currentPositionButton;
    }
}