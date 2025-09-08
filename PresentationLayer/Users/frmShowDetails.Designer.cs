namespace Driver_VehiclesLicensesDepartmentProject
{
    partial class frmShowDetails
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
            this.uctrlShowDetails1 = new Driver_VehiclesLicensesDepartmentProject.uctrlShowDetails();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uctrlShowDetails1
            // 
            this.uctrlShowDetails1.Location = new System.Drawing.Point(0, 0);
            this.uctrlShowDetails1.Name = "uctrlShowDetails1";
            this.uctrlShowDetails1.Size = new System.Drawing.Size(1130, 514);
            this.uctrlShowDetails1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.close;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1004, 531);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 52);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "   Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmShowDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 588);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.uctrlShowDetails1);
            this.Name = "frmShowDetails";
            this.Text = "frmShowDetails";
            this.Load += new System.EventHandler(this.frmShowDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private uctrlShowDetails uctrlShowDetails1;
        private System.Windows.Forms.Button btnClose;
    }
}