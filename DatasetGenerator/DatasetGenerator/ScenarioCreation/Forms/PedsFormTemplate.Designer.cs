namespace DatasetGenerator.ScenarioCreation.Forms
{
    partial class PedsFormTemplate
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
            this.pedsBehaviorCombobox = new System.Windows.Forms.ComboBox();
            this.pedsNumberLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pedsGroupCheckbox = new System.Windows.Forms.CheckBox();
            this.pedsNumberTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pedsBehaviorCombobox
            // 
            this.pedsBehaviorCombobox.FormattingEnabled = true;
            this.pedsBehaviorCombobox.Location = new System.Drawing.Point(71, 86);
            this.pedsBehaviorCombobox.Name = "pedsBehaviorCombobox";
            this.pedsBehaviorCombobox.Size = new System.Drawing.Size(121, 21);
            this.pedsBehaviorCombobox.TabIndex = 0;
            // 
            // pedsNumberLabel
            // 
            this.pedsNumberLabel.AutoSize = true;
            this.pedsNumberLabel.Location = new System.Drawing.Point(68, 20);
            this.pedsNumberLabel.Name = "pedsNumberLabel";
            this.pedsNumberLabel.Size = new System.Drawing.Size(83, 13);
            this.pedsNumberLabel.TabIndex = 2;
            this.pedsNumberLabel.Text = "Number of Peds";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(68, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Peds Behavior";
            // 
            // pedsGroupCheckbox
            // 
            this.pedsGroupCheckbox.AutoSize = true;
            this.pedsGroupCheckbox.Location = new System.Drawing.Point(71, 125);
            this.pedsGroupCheckbox.Name = "pedsGroupCheckbox";
            this.pedsGroupCheckbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pedsGroupCheckbox.Size = new System.Drawing.Size(15, 14);
            this.pedsGroupCheckbox.TabIndex = 3;
            this.pedsGroupCheckbox.UseVisualStyleBackColor = true;
            // 
            // pedsNumberTextbox
            // 
            this.pedsNumberTextbox.Location = new System.Drawing.Point(71, 36);
            this.pedsNumberTextbox.Name = "pedsNumberTextbox";
            this.pedsNumberTextbox.Size = new System.Drawing.Size(100, 20);
            this.pedsNumberTextbox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Should Peds group?";
            // 
            // PedsFormTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 167);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pedsNumberTextbox);
            this.Controls.Add(this.pedsGroupCheckbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pedsNumberLabel);
            this.Controls.Add(this.pedsBehaviorCombobox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "PedsFormTemplate";
            this.Text = "Peds Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox pedsBehaviorCombobox;
        private System.Windows.Forms.Label pedsNumberLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox pedsGroupCheckbox;
        private System.Windows.Forms.TextBox pedsNumberTextbox;
        private System.Windows.Forms.Label label3;
    }
}