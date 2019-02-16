namespace DatasetGenerator.ScenarioCreation.Forms
{
    partial class CameraFormTemplate
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
            this.camerasListBox = new System.Windows.Forms.ListBox();
            this.addCameraButton = new System.Windows.Forms.Button();
            this.deleteCameraButton = new System.Windows.Forms.Button();
            this.clearCamerasButton = new System.Windows.Forms.Button();
            this.viewCameraButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // camerasListBox
            // 
            this.camerasListBox.FormattingEnabled = true;
            this.camerasListBox.Location = new System.Drawing.Point(12, 12);
            this.camerasListBox.Name = "camerasListBox";
            this.camerasListBox.Size = new System.Drawing.Size(247, 303);
            this.camerasListBox.TabIndex = 0;
            // 
            // addCameraButton
            // 
            this.addCameraButton.Location = new System.Drawing.Point(12, 357);
            this.addCameraButton.Name = "addCameraButton";
            this.addCameraButton.Size = new System.Drawing.Size(247, 23);
            this.addCameraButton.TabIndex = 1;
            this.addCameraButton.Text = "Add new camera";
            this.addCameraButton.UseVisualStyleBackColor = true;
            // 
            // deleteCameraButton
            // 
            this.deleteCameraButton.Location = new System.Drawing.Point(12, 386);
            this.deleteCameraButton.Name = "deleteCameraButton";
            this.deleteCameraButton.Size = new System.Drawing.Size(247, 23);
            this.deleteCameraButton.TabIndex = 2;
            this.deleteCameraButton.Text = "Delete selected camera";
            this.deleteCameraButton.UseVisualStyleBackColor = true;
            // 
            // clearCamerasButton
            // 
            this.clearCamerasButton.Location = new System.Drawing.Point(12, 415);
            this.clearCamerasButton.Name = "clearCamerasButton";
            this.clearCamerasButton.Size = new System.Drawing.Size(247, 23);
            this.clearCamerasButton.TabIndex = 3;
            this.clearCamerasButton.Text = "Clear all cameras";
            this.clearCamerasButton.UseVisualStyleBackColor = true;
            // 
            // viewCameraButton
            // 
            this.viewCameraButton.Location = new System.Drawing.Point(12, 328);
            this.viewCameraButton.Name = "viewCameraButton";
            this.viewCameraButton.Size = new System.Drawing.Size(247, 23);
            this.viewCameraButton.TabIndex = 4;
            this.viewCameraButton.Text = "View Selected Camera";
            this.viewCameraButton.UseVisualStyleBackColor = true;
            // 
            // CameraFormTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 450);
            this.Controls.Add(this.viewCameraButton);
            this.Controls.Add(this.clearCamerasButton);
            this.Controls.Add(this.deleteCameraButton);
            this.Controls.Add(this.addCameraButton);
            this.Controls.Add(this.camerasListBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "CameraFormTemplate";
            this.Text = "Camera Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox camerasListBox;
        private System.Windows.Forms.Button addCameraButton;
        private System.Windows.Forms.Button deleteCameraButton;
        private System.Windows.Forms.Button clearCamerasButton;
        private System.Windows.Forms.Button viewCameraButton;
    }
}