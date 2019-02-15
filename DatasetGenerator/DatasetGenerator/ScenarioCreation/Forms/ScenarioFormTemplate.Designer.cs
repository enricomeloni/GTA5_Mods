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
            this.scenarioSettingsTabControl = new System.Windows.Forms.TabControl();
            this.cameraSettingsTab = new System.Windows.Forms.TabPage();
            this.pedsSettingsTab = new System.Windows.Forms.TabPage();
            this.placeSettingsTab = new System.Windows.Forms.TabPage();
            this.weatherSettingsTab = new System.Windows.Forms.TabPage();
            this.placeSettings = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.scenarioSettingsTabControl.SuspendLayout();
            this.cameraSettingsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // scenarioSettingsTabControl
            // 
            this.scenarioSettingsTabControl.Controls.Add(this.cameraSettingsTab);
            this.scenarioSettingsTabControl.Controls.Add(this.pedsSettingsTab);
            this.scenarioSettingsTabControl.Controls.Add(this.placeSettingsTab);
            this.scenarioSettingsTabControl.Controls.Add(this.weatherSettingsTab);
            this.scenarioSettingsTabControl.Controls.Add(this.placeSettings);
            this.scenarioSettingsTabControl.Location = new System.Drawing.Point(0, 0);
            this.scenarioSettingsTabControl.Name = "scenarioSettingsTabControl";
            this.scenarioSettingsTabControl.SelectedIndex = 0;
            this.scenarioSettingsTabControl.Size = new System.Drawing.Size(799, 450);
            this.scenarioSettingsTabControl.TabIndex = 0;
            // 
            // cameraSettingsTab
            // 
            this.cameraSettingsTab.Controls.Add(this.button2);
            this.cameraSettingsTab.Controls.Add(this.button1);
            this.cameraSettingsTab.Controls.Add(this.label1);
            this.cameraSettingsTab.Controls.Add(this.listView1);
            this.cameraSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.cameraSettingsTab.Name = "cameraSettingsTab";
            this.cameraSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.cameraSettingsTab.Size = new System.Drawing.Size(791, 424);
            this.cameraSettingsTab.TabIndex = 0;
            this.cameraSettingsTab.Text = "Camera Settings";
            this.cameraSettingsTab.UseVisualStyleBackColor = true;
            // 
            // pedsSettingsTab
            // 
            this.pedsSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.pedsSettingsTab.Name = "pedsSettingsTab";
            this.pedsSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.pedsSettingsTab.Size = new System.Drawing.Size(791, 424);
            this.pedsSettingsTab.TabIndex = 1;
            this.pedsSettingsTab.Text = "Peds Settings";
            this.pedsSettingsTab.UseVisualStyleBackColor = true;
            // 
            // placeSettingsTab
            // 
            this.placeSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.placeSettingsTab.Name = "placeSettingsTab";
            this.placeSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.placeSettingsTab.Size = new System.Drawing.Size(791, 424);
            this.placeSettingsTab.TabIndex = 2;
            this.placeSettingsTab.Text = "Place Settings";
            this.placeSettingsTab.UseVisualStyleBackColor = true;
            // 
            // weatherSettingsTab
            // 
            this.weatherSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.weatherSettingsTab.Name = "weatherSettingsTab";
            this.weatherSettingsTab.Size = new System.Drawing.Size(791, 424);
            this.weatherSettingsTab.TabIndex = 3;
            this.weatherSettingsTab.Text = "Weather Settings";
            this.weatherSettingsTab.UseVisualStyleBackColor = true;
            // 
            // placeSettings
            // 
            this.placeSettings.Location = new System.Drawing.Point(4, 22);
            this.placeSettings.Name = "placeSettings";
            this.placeSettings.Size = new System.Drawing.Size(791, 424);
            this.placeSettings.TabIndex = 4;
            this.placeSettings.Text = "Place Settings";
            this.placeSettings.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(8, 28);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(221, 363);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cameras List";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(248, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 42);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add Current Camera";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(248, 76);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 53);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ScenarioFormTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.scenarioSettingsTabControl);
            this.Name = "ScenarioFormTemplate";
            this.Text = "Scenario Settings";
            this.scenarioSettingsTabControl.ResumeLayout(false);
            this.cameraSettingsTab.ResumeLayout(false);
            this.cameraSettingsTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl scenarioSettingsTabControl;
        private System.Windows.Forms.TabPage cameraSettingsTab;
        private System.Windows.Forms.TabPage pedsSettingsTab;
        private System.Windows.Forms.TabPage placeSettingsTab;
        private System.Windows.Forms.TabPage weatherSettingsTab;
        private System.Windows.Forms.TabPage placeSettings;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
    }
}