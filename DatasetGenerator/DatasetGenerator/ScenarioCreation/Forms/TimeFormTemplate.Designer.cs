namespace DatasetGenerator.ScenarioCreation.Forms
{
    partial class TimeFormTemplate
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
            this.hourTextbox = new System.Windows.Forms.TextBox();
            this.minuteTextbox = new System.Windows.Forms.TextBox();
            this.hourLabel = new System.Windows.Forms.Label();
            this.minuteLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // hourTextbox
            // 
            this.hourTextbox.Location = new System.Drawing.Point(88, 32);
            this.hourTextbox.Name = "hourTextbox";
            this.hourTextbox.Size = new System.Drawing.Size(100, 20);
            this.hourTextbox.TabIndex = 0;
            // 
            // minuteTextbox
            // 
            this.minuteTextbox.Location = new System.Drawing.Point(88, 71);
            this.minuteTextbox.Name = "minuteTextbox";
            this.minuteTextbox.Size = new System.Drawing.Size(100, 20);
            this.minuteTextbox.TabIndex = 1;
            // 
            // hourLabel
            // 
            this.hourLabel.AutoSize = true;
            this.hourLabel.Location = new System.Drawing.Point(85, 16);
            this.hourLabel.Name = "hourLabel";
            this.hourLabel.Size = new System.Drawing.Size(30, 13);
            this.hourLabel.TabIndex = 2;
            this.hourLabel.Text = "Hour";
            // 
            // minuteLabel
            // 
            this.minuteLabel.AutoSize = true;
            this.minuteLabel.Location = new System.Drawing.Point(85, 55);
            this.minuteLabel.Name = "minuteLabel";
            this.minuteLabel.Size = new System.Drawing.Size(44, 13);
            this.minuteLabel.TabIndex = 3;
            this.minuteLabel.Text = "Minutes";
            // 
            // TimeFormTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 134);
            this.Controls.Add(this.minuteLabel);
            this.Controls.Add(this.hourLabel);
            this.Controls.Add(this.minuteTextbox);
            this.Controls.Add(this.hourTextbox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "TimeFormTemplate";
            this.Text = "Time Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox hourTextbox;
        private System.Windows.Forms.TextBox minuteTextbox;
        private System.Windows.Forms.Label hourLabel;
        private System.Windows.Forms.Label minuteLabel;
    }
}