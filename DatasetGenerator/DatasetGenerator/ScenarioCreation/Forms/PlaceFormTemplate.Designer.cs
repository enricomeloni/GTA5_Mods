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
            this.SuspendLayout();
            // 
            // placeComboBox
            // 
            this.placeComboBox.FormattingEnabled = true;
            this.placeComboBox.Location = new System.Drawing.Point(47, 23);
            this.placeComboBox.Name = "placeComboBox";
            this.placeComboBox.Size = new System.Drawing.Size(209, 21);
            this.placeComboBox.TabIndex = 0;
            // 
            // teleportButton
            // 
            this.teleportButton.Location = new System.Drawing.Point(117, 50);
            this.teleportButton.Name = "teleportButton";
            this.teleportButton.Size = new System.Drawing.Size(75, 23);
            this.teleportButton.TabIndex = 1;
            this.teleportButton.Text = "Teleport";
            this.teleportButton.UseVisualStyleBackColor = true;
            // 
            // PlaceFormTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 122);
            this.Controls.Add(this.teleportButton);
            this.Controls.Add(this.placeComboBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "PlaceFormTemplate";
            this.Text = "PlaceFormTemplate";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox placeComboBox;
        private System.Windows.Forms.Button teleportButton;
    }
}