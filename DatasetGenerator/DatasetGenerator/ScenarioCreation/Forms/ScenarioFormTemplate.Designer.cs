namespace DatasetGenerator.ScenarioCreation.Forms
{
    partial class ScenarioFormTemplate
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
            this.scenariosPathTextbox = new System.Windows.Forms.TextBox();
            this.saveScenarioButton = new System.Windows.Forms.Button();
            this.loadScenarioButton = new System.Windows.Forms.Button();
            this.scenariosPathLabel = new System.Windows.Forms.Label();
            this.confirmationLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // scenariosPathTextbox
            // 
            this.scenariosPathTextbox.Location = new System.Drawing.Point(12, 69);
            this.scenariosPathTextbox.Name = "scenariosPathTextbox";
            this.scenariosPathTextbox.Size = new System.Drawing.Size(266, 20);
            this.scenariosPathTextbox.TabIndex = 1;
            // 
            // saveScenarioButton
            // 
            this.saveScenarioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.saveScenarioButton.Location = new System.Drawing.Point(12, 95);
            this.saveScenarioButton.Name = "saveScenarioButton";
            this.saveScenarioButton.Size = new System.Drawing.Size(127, 33);
            this.saveScenarioButton.TabIndex = 2;
            this.saveScenarioButton.Text = "Save Scenario";
            this.saveScenarioButton.UseVisualStyleBackColor = true;
            // 
            // loadScenarioButton
            // 
            this.loadScenarioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.loadScenarioButton.Location = new System.Drawing.Point(145, 95);
            this.loadScenarioButton.Name = "loadScenarioButton";
            this.loadScenarioButton.Size = new System.Drawing.Size(133, 33);
            this.loadScenarioButton.TabIndex = 3;
            this.loadScenarioButton.Text = "Load Scenario";
            this.loadScenarioButton.UseVisualStyleBackColor = true;
            // 
            // scenariosPathLabel
            // 
            this.scenariosPathLabel.AutoSize = true;
            this.scenariosPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.scenariosPathLabel.Location = new System.Drawing.Point(12, 51);
            this.scenariosPathLabel.Name = "scenariosPathLabel";
            this.scenariosPathLabel.Size = new System.Drawing.Size(83, 15);
            this.scenariosPathLabel.TabIndex = 4;
            this.scenariosPathLabel.Text = "Scenario path";
            // 
            // confirmationLabel
            // 
            this.confirmationLabel.AutoSize = true;
            this.confirmationLabel.Location = new System.Drawing.Point(131, 169);
            this.confirmationLabel.Name = "confirmationLabel";
            this.confirmationLabel.Size = new System.Drawing.Size(38, 13);
            this.confirmationLabel.TabIndex = 5;
            this.confirmationLabel.Text = "Saved";
            // 
            // ScenarioFormTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 191);
            this.Controls.Add(this.confirmationLabel);
            this.Controls.Add(this.scenariosPathLabel);
            this.Controls.Add(this.loadScenarioButton);
            this.Controls.Add(this.saveScenarioButton);
            this.Controls.Add(this.scenariosPathTextbox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "ScenarioFormTemplate";
            this.Text = "Scenario Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox scenariosPathTextbox;
        private System.Windows.Forms.Button saveScenarioButton;
        private System.Windows.Forms.Button loadScenarioButton;
        private System.Windows.Forms.Label scenariosPathLabel;
        private System.Windows.Forms.Label confirmationLabel;
    }
}