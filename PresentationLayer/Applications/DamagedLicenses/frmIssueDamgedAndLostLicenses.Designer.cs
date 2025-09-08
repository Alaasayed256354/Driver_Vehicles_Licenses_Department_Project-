namespace Driver_VehiclesLicensesDepartmentProject
{
    partial class frmIssueDamgedAndLostLicenses
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
            this.lblDamagedOrLostLicense = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbLostlicense = new System.Windows.Forms.RadioButton();
            this.rbDamagedLicense = new System.Windows.Forms.RadioButton();
            this.llShowLicenseDetails = new System.Windows.Forms.LinkLabel();
            this.llShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.btnIssue = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.CreatedBy = new System.Windows.Forms.Label();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.LocalLicenseID = new System.Windows.Forms.Label();
            this.lblOldLicense = new System.Windows.Forms.Label();
            this.lblReplacedLicenseiD = new System.Windows.Forms.Label();
            this.lblilicenseiD = new System.Windows.Forms.Label();
            this.lblFees = new System.Windows.Forms.Label();
            this.Fees = new System.Windows.Forms.Label();
            this.lblAppDate = new System.Windows.Forms.Label();
            this.lblILAppID = new System.Windows.Forms.Label();
            this.AppDate = new System.Windows.Forms.Label();
            this.lblReplacmentapplicationID = new System.Windows.Forms.Label();
            this.uctrlFilterLicenses1 = new Driver_VehiclesLicensesDepartmentProject.uctrlFilterLicenses();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDamagedOrLostLicense
            // 
            this.lblDamagedOrLostLicense.AutoSize = true;
            this.lblDamagedOrLostLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDamagedOrLostLicense.ForeColor = System.Drawing.Color.Red;
            this.lblDamagedOrLostLicense.Location = new System.Drawing.Point(163, 9);
            this.lblDamagedOrLostLicense.Name = "lblDamagedOrLostLicense";
            this.lblDamagedOrLostLicense.Size = new System.Drawing.Size(460, 38);
            this.lblDamagedOrLostLicense.TabIndex = 1;
            this.lblDamagedOrLostLicense.Text = "Replacement For Lost License";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbLostlicense);
            this.groupBox1.Controls.Add(this.rbDamagedLicense);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(665, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Replacment For";
            // 
            // rbLostlicense
            // 
            this.rbLostlicense.AutoSize = true;
            this.rbLostlicense.Location = new System.Drawing.Point(6, 70);
            this.rbLostlicense.Name = "rbLostlicense";
            this.rbLostlicense.Size = new System.Drawing.Size(143, 29);
            this.rbLostlicense.TabIndex = 1;
            this.rbLostlicense.TabStop = true;
            this.rbLostlicense.Text = "Lost License";
            this.rbLostlicense.UseVisualStyleBackColor = true;
            this.rbLostlicense.CheckedChanged += new System.EventHandler(this.rbLostlicense_CheckedChanged);
            // 
            // rbDamagedLicense
            // 
            this.rbDamagedLicense.AutoSize = true;
            this.rbDamagedLicense.Location = new System.Drawing.Point(6, 26);
            this.rbDamagedLicense.Name = "rbDamagedLicense";
            this.rbDamagedLicense.Size = new System.Drawing.Size(191, 29);
            this.rbDamagedLicense.TabIndex = 0;
            this.rbDamagedLicense.TabStop = true;
            this.rbDamagedLicense.Text = "Damaged License";
            this.rbDamagedLicense.UseVisualStyleBackColor = true;
            this.rbDamagedLicense.CheckedChanged += new System.EventHandler(this.rbDamagedLicense_CheckedChanged);
            // 
            // llShowLicenseDetails
            // 
            this.llShowLicenseDetails.AutoSize = true;
            this.llShowLicenseDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llShowLicenseDetails.Location = new System.Drawing.Point(268, 806);
            this.llShowLicenseDetails.Name = "llShowLicenseDetails";
            this.llShowLicenseDetails.Size = new System.Drawing.Size(199, 25);
            this.llShowLicenseDetails.TabIndex = 11;
            this.llShowLicenseDetails.TabStop = true;
            this.llShowLicenseDetails.Text = "Show License Details";
            this.llShowLicenseDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicenseDetails_LinkClicked);
            // 
            // llShowLicenseHistory
            // 
            this.llShowLicenseHistory.AutoSize = true;
            this.llShowLicenseHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llShowLicenseHistory.Location = new System.Drawing.Point(7, 806);
            this.llShowLicenseHistory.Name = "llShowLicenseHistory";
            this.llShowLicenseHistory.Size = new System.Drawing.Size(200, 25);
            this.llShowLicenseHistory.TabIndex = 10;
            this.llShowLicenseHistory.TabStop = true;
            this.llShowLicenseHistory.Text = "Show License History";
            this.llShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicenseHistory_LinkClicked);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.close;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(518, 788);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 43);
            this.button2.TabIndex = 9;
            this.button2.Text = "    Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnIssue
            // 
            this.btnIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIssue.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.world_south_america;
            this.btnIssue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssue.Location = new System.Drawing.Point(665, 788);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(223, 43);
            this.btnIssue.TabIndex = 8;
            this.btnIssue.Text = "    Issue Replacment";
            this.btnIssue.UseVisualStyleBackColor = true;
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.pictureBox4);
            this.groupBox2.Controls.Add(this.lblCreatedBy);
            this.groupBox2.Controls.Add(this.CreatedBy);
            this.groupBox2.Controls.Add(this.pictureBox9);
            this.groupBox2.Controls.Add(this.pictureBox8);
            this.groupBox2.Controls.Add(this.pictureBox2);
            this.groupBox2.Controls.Add(this.pictureBox10);
            this.groupBox2.Controls.Add(this.LocalLicenseID);
            this.groupBox2.Controls.Add(this.lblOldLicense);
            this.groupBox2.Controls.Add(this.lblReplacedLicenseiD);
            this.groupBox2.Controls.Add(this.lblilicenseiD);
            this.groupBox2.Controls.Add(this.lblFees);
            this.groupBox2.Controls.Add(this.Fees);
            this.groupBox2.Controls.Add(this.lblAppDate);
            this.groupBox2.Controls.Add(this.lblILAppID);
            this.groupBox2.Controls.Add(this.AppDate);
            this.groupBox2.Controls.Add(this.lblReplacmentapplicationID);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 613);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(826, 172);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Application Info For License Replacment";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.user__2_;
            this.pictureBox1.Location = new System.Drawing.Point(587, 121);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 101;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.card;
            this.pictureBox4.Location = new System.Drawing.Point(587, 78);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(37, 30);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 99;
            this.pictureBox4.TabStop = false;
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedBy.Location = new System.Drawing.Point(646, 118);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(56, 21);
            this.lblCreatedBy.TabIndex = 98;
            this.lblCreatedBy.Text = "[???]";
            // 
            // CreatedBy
            // 
            this.CreatedBy.AutoSize = true;
            this.CreatedBy.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreatedBy.Location = new System.Drawing.Point(462, 118);
            this.CreatedBy.Name = "CreatedBy";
            this.CreatedBy.Size = new System.Drawing.Size(109, 21);
            this.CreatedBy.TabIndex = 96;
            this.CreatedBy.Text = "Created By:";
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackgroundImage = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.card;
            this.pictureBox9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox9.Location = new System.Drawing.Point(587, 42);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(37, 30);
            this.pictureBox9.TabIndex = 94;
            this.pictureBox9.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox8.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.money;
            this.pictureBox8.Location = new System.Drawing.Point(178, 130);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(50, 30);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox8.TabIndex = 93;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.date_field;
            this.pictureBox2.Location = new System.Drawing.Point(179, 78);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 42);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 91;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox10
            // 
            this.pictureBox10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox10.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.id;
            this.pictureBox10.Location = new System.Drawing.Point(179, 29);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(50, 43);
            this.pictureBox10.TabIndex = 90;
            this.pictureBox10.TabStop = false;
            // 
            // LocalLicenseID
            // 
            this.LocalLicenseID.AutoSize = true;
            this.LocalLicenseID.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocalLicenseID.Location = new System.Drawing.Point(454, 78);
            this.LocalLicenseID.Name = "LocalLicenseID";
            this.LocalLicenseID.Size = new System.Drawing.Size(117, 21);
            this.LocalLicenseID.TabIndex = 89;
            this.LocalLicenseID.Text = "Old LIcense:";
            // 
            // lblOldLicense
            // 
            this.lblOldLicense.AutoSize = true;
            this.lblOldLicense.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOldLicense.Location = new System.Drawing.Point(646, 78);
            this.lblOldLicense.Name = "lblOldLicense";
            this.lblOldLicense.Size = new System.Drawing.Size(56, 21);
            this.lblOldLicense.TabIndex = 88;
            this.lblOldLicense.Text = "[???]";
            // 
            // lblReplacedLicenseiD
            // 
            this.lblReplacedLicenseiD.AutoSize = true;
            this.lblReplacedLicenseiD.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReplacedLicenseiD.Location = new System.Drawing.Point(382, 42);
            this.lblReplacedLicenseiD.Name = "lblReplacedLicenseiD";
            this.lblReplacedLicenseiD.Size = new System.Drawing.Size(189, 21);
            this.lblReplacedLicenseiD.TabIndex = 87;
            this.lblReplacedLicenseiD.Text = "Replaced License ID:";
            // 
            // lblilicenseiD
            // 
            this.lblilicenseiD.AutoSize = true;
            this.lblilicenseiD.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblilicenseiD.Location = new System.Drawing.Point(646, 38);
            this.lblilicenseiD.Name = "lblilicenseiD";
            this.lblilicenseiD.Size = new System.Drawing.Size(56, 21);
            this.lblilicenseiD.TabIndex = 86;
            this.lblilicenseiD.Text = "[???]";
            // 
            // lblFees
            // 
            this.lblFees.AutoSize = true;
            this.lblFees.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFees.Location = new System.Drawing.Point(243, 130);
            this.lblFees.Name = "lblFees";
            this.lblFees.Size = new System.Drawing.Size(56, 21);
            this.lblFees.TabIndex = 85;
            this.lblFees.Text = "[???]";
            // 
            // Fees
            // 
            this.Fees.AutoSize = true;
            this.Fees.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fees.Location = new System.Drawing.Point(108, 130);
            this.Fees.Name = "Fees";
            this.Fees.Size = new System.Drawing.Size(55, 21);
            this.Fees.TabIndex = 84;
            this.Fees.Text = "Fees:";
            // 
            // lblAppDate
            // 
            this.lblAppDate.AutoSize = true;
            this.lblAppDate.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppDate.Location = new System.Drawing.Point(244, 78);
            this.lblAppDate.Name = "lblAppDate";
            this.lblAppDate.Size = new System.Drawing.Size(56, 21);
            this.lblAppDate.TabIndex = 81;
            this.lblAppDate.Text = "[???]";
            // 
            // lblILAppID
            // 
            this.lblILAppID.AutoSize = true;
            this.lblILAppID.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblILAppID.Location = new System.Drawing.Point(244, 38);
            this.lblILAppID.Name = "lblILAppID";
            this.lblILAppID.Size = new System.Drawing.Size(56, 21);
            this.lblILAppID.TabIndex = 80;
            this.lblILAppID.Text = "[???]";
            // 
            // AppDate
            // 
            this.AppDate.AutoSize = true;
            this.AppDate.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppDate.Location = new System.Drawing.Point(10, 78);
            this.AppDate.Name = "AppDate";
            this.AppDate.Size = new System.Drawing.Size(163, 21);
            this.AppDate.TabIndex = 79;
            this.AppDate.Text = " Application Date:";
            // 
            // lblReplacmentapplicationID
            // 
            this.lblReplacmentapplicationID.AutoSize = true;
            this.lblReplacmentapplicationID.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReplacmentapplicationID.Location = new System.Drawing.Point(6, 38);
            this.lblReplacmentapplicationID.Name = "lblReplacmentapplicationID";
            this.lblReplacmentapplicationID.Size = new System.Drawing.Size(171, 21);
            this.lblReplacmentapplicationID.TabIndex = 78;
            this.lblReplacmentapplicationID.Text = "L.R Application ID:";
            // 
            // uctrlFilterLicenses1
            // 
            this.uctrlFilterLicenses1.Location = new System.Drawing.Point(15, 51);
            this.uctrlFilterLicenses1.Name = "uctrlFilterLicenses1";
            this.uctrlFilterLicenses1.Size = new System.Drawing.Size(799, 566);
            this.uctrlFilterLicenses1.TabIndex = 0;
            // 
            // frmIssueDamgedAndLostLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 836);
            this.Controls.Add(this.llShowLicenseDetails);
            this.Controls.Add(this.llShowLicenseHistory);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnIssue);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblDamagedOrLostLicense);
            this.Controls.Add(this.uctrlFilterLicenses1);
            this.Name = "frmIssueDamgedAndLostLicenses";
            this.Text = "frmIssueDamgedAndLostLicenses";
            this.Load += new System.EventHandler(this.frmIssueDamgedAndLostLicenses_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private uctrlFilterLicenses uctrlFilterLicenses1;
        private System.Windows.Forms.Label lblDamagedOrLostLicense;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbLostlicense;
        private System.Windows.Forms.RadioButton rbDamagedLicense;
        private System.Windows.Forms.LinkLabel llShowLicenseDetails;
        private System.Windows.Forms.LinkLabel llShowLicenseHistory;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnIssue;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label CreatedBy;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Label LocalLicenseID;
        private System.Windows.Forms.Label lblOldLicense;
        private System.Windows.Forms.Label lblReplacedLicenseiD;
        private System.Windows.Forms.Label lblilicenseiD;
        private System.Windows.Forms.Label lblFees;
        private System.Windows.Forms.Label Fees;
        private System.Windows.Forms.Label lblAppDate;
        public System.Windows.Forms.Label lblILAppID;
        private System.Windows.Forms.Label AppDate;
        private System.Windows.Forms.Label lblReplacmentapplicationID;
    }
}