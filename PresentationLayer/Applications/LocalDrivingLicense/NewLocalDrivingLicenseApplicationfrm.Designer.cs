namespace Driver_VehiclesLicensesDepartmentProject
{
    partial class NewLocalDrivingLicenseApplicationfrm
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
            this.tcLocaldrivingLicense = new System.Windows.Forms.TabControl();
            this.tbPersonalInfo = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.tbFilterPeople = new System.Windows.Forms.TextBox();
            this.AddPerson = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cbFilteration = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbApplicationInfo = new System.Windows.Forms.TabPage();
            this.lblapplicationDate = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbLicenseClasses = new System.Windows.Forms.ComboBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblFees = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblDLAplicatIonID = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLDLAppID = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.ctrlPersonDetails1 = new Driver_VehiclesLicensesDepartmentProject.ctrlPersonDetails();
            this.tcLocaldrivingLicense.SuspendLayout();
            this.tbPersonalInfo.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.tbApplicationInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tcLocaldrivingLicense
            // 
            this.tcLocaldrivingLicense.Controls.Add(this.tbPersonalInfo);
            this.tcLocaldrivingLicense.Controls.Add(this.tbApplicationInfo);
            this.tcLocaldrivingLicense.Location = new System.Drawing.Point(4, 49);
            this.tcLocaldrivingLicense.Name = "tcLocaldrivingLicense";
            this.tcLocaldrivingLicense.SelectedIndex = 0;
            this.tcLocaldrivingLicense.Size = new System.Drawing.Size(1239, 606);
            this.tcLocaldrivingLicense.TabIndex = 0;
            // 
            // tbPersonalInfo
            // 
            this.tbPersonalInfo.Controls.Add(this.btnNext);
            this.tbPersonalInfo.Controls.Add(this.ctrlPersonDetails1);
            this.tbPersonalInfo.Controls.Add(this.gbFilter);
            this.tbPersonalInfo.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPersonalInfo.Location = new System.Drawing.Point(4, 25);
            this.tbPersonalInfo.Name = "tbPersonalInfo";
            this.tbPersonalInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tbPersonalInfo.Size = new System.Drawing.Size(1231, 577);
            this.tbPersonalInfo.TabIndex = 0;
            this.tbPersonalInfo.Text = "Personal Info";
            this.tbPersonalInfo.UseVisualStyleBackColor = true;
            this.tbPersonalInfo.Click += new System.EventHandler(this.tbPersonalInfo_Click);
            // 
            // btnNext
            // 
            this.btnNext.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.next__1_;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(1101, 523);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(125, 43);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.tbFilterPeople);
            this.gbFilter.Controls.Add(this.AddPerson);
            this.gbFilter.Controls.Add(this.button1);
            this.gbFilter.Controls.Add(this.cbFilteration);
            this.gbFilter.Controls.Add(this.label2);
            this.gbFilter.Location = new System.Drawing.Point(44, 16);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(918, 104);
            this.gbFilter.TabIndex = 2;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // tbFilterPeople
            // 
            this.tbFilterPeople.Location = new System.Drawing.Point(344, 36);
            this.tbFilterPeople.Multiline = true;
            this.tbFilterPeople.Name = "tbFilterPeople";
            this.tbFilterPeople.Size = new System.Drawing.Size(287, 28);
            this.tbFilterPeople.TabIndex = 7;
            this.tbFilterPeople.TextChanged += new System.EventHandler(this.tbFilterPeople_TextChanged);
            this.tbFilterPeople.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFilterPeople_KeyPress);
            // 
            // AddPerson
            // 
            this.AddPerson.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.person_boy__6_;
            this.AddPerson.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.AddPerson.Location = new System.Drawing.Point(779, 15);
            this.AddPerson.Name = "AddPerson";
            this.AddPerson.Size = new System.Drawing.Size(103, 84);
            this.AddPerson.TabIndex = 6;
            this.AddPerson.UseVisualStyleBackColor = true;
            this.AddPerson.Click += new System.EventHandler(this.AddPerson_Click);
            // 
            // button1
            // 
            this.button1.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.person_boy__8_;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.Location = new System.Drawing.Point(655, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 84);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbFilteration
            // 
            this.cbFilteration.FormattingEnabled = true;
            this.cbFilteration.Items.AddRange(new object[] {
            "National No",
            "Person ID"});
            this.cbFilteration.Location = new System.Drawing.Point(92, 36);
            this.cbFilteration.Name = "cbFilteration";
            this.cbFilteration.Size = new System.Drawing.Size(239, 29);
            this.cbFilteration.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Find By:";
            // 
            // tbApplicationInfo
            // 
            this.tbApplicationInfo.Controls.Add(this.lblapplicationDate);
            this.tbApplicationInfo.Controls.Add(this.pictureBox5);
            this.tbApplicationInfo.Controls.Add(this.pictureBox4);
            this.tbApplicationInfo.Controls.Add(this.pictureBox3);
            this.tbApplicationInfo.Controls.Add(this.pictureBox2);
            this.tbApplicationInfo.Controls.Add(this.pictureBox1);
            this.tbApplicationInfo.Controls.Add(this.cbLicenseClasses);
            this.tbApplicationInfo.Controls.Add(this.lblCreatedBy);
            this.tbApplicationInfo.Controls.Add(this.lblFees);
            this.tbApplicationInfo.Controls.Add(this.label7);
            this.tbApplicationInfo.Controls.Add(this.lblDLAplicatIonID);
            this.tbApplicationInfo.Controls.Add(this.label6);
            this.tbApplicationInfo.Controls.Add(this.label5);
            this.tbApplicationInfo.Controls.Add(this.label4);
            this.tbApplicationInfo.Controls.Add(this.label3);
            this.tbApplicationInfo.Font = new System.Drawing.Font("Tahoma", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbApplicationInfo.Location = new System.Drawing.Point(4, 25);
            this.tbApplicationInfo.Name = "tbApplicationInfo";
            this.tbApplicationInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tbApplicationInfo.Size = new System.Drawing.Size(1231, 577);
            this.tbApplicationInfo.TabIndex = 1;
            this.tbApplicationInfo.Text = "Application Info";
            this.tbApplicationInfo.UseVisualStyleBackColor = true;
            this.tbApplicationInfo.Click += new System.EventHandler(this.tbApplicationInfo_Click);
            // 
            // lblapplicationDate
            // 
            this.lblapplicationDate.AutoSize = true;
            this.lblapplicationDate.Location = new System.Drawing.Point(369, 125);
            this.lblapplicationDate.Name = "lblapplicationDate";
            this.lblapplicationDate.Size = new System.Drawing.Size(91, 29);
            this.lblapplicationDate.TabIndex = 15;
            this.lblapplicationDate.Text = "[????]";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.id1;
            this.pictureBox5.Location = new System.Drawing.Point(275, 205);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(57, 41);
            this.pictureBox5.TabIndex = 14;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.date_field1;
            this.pictureBox4.Location = new System.Drawing.Point(275, 109);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(67, 67);
            this.pictureBox4.TabIndex = 13;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.money;
            this.pictureBox3.Location = new System.Drawing.Point(275, 272);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(67, 56);
            this.pictureBox3.TabIndex = 12;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.user__3_1;
            this.pictureBox2.Location = new System.Drawing.Point(275, 369);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(67, 67);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.id1;
            this.pictureBox1.Location = new System.Drawing.Point(275, 56);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(57, 39);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // cbLicenseClasses
            // 
            this.cbLicenseClasses.FormattingEnabled = true;
            this.cbLicenseClasses.Location = new System.Drawing.Point(374, 198);
            this.cbLicenseClasses.Name = "cbLicenseClasses";
            this.cbLicenseClasses.Size = new System.Drawing.Size(531, 36);
            this.cbLicenseClasses.TabIndex = 8;
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Location = new System.Drawing.Point(369, 376);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(91, 29);
            this.lblCreatedBy.TabIndex = 7;
            this.lblCreatedBy.Text = "[????]";
            // 
            // lblFees
            // 
            this.lblFees.AutoSize = true;
            this.lblFees.Location = new System.Drawing.Point(369, 279);
            this.lblFees.Name = "lblFees";
            this.lblFees.Size = new System.Drawing.Size(91, 29);
            this.lblFees.TabIndex = 6;
            this.lblFees.Text = "[????]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 376);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(152, 29);
            this.label7.TabIndex = 5;
            this.label7.Text = "Created By:";
            // 
            // lblDLAplicatIonID
            // 
            this.lblDLAplicatIonID.AutoSize = true;
            this.lblDLAplicatIonID.Location = new System.Drawing.Point(369, 56);
            this.lblDLAplicatIonID.Name = "lblDLAplicatIonID";
            this.lblDLAplicatIonID.Size = new System.Drawing.Size(91, 29);
            this.lblDLAplicatIonID.TabIndex = 4;
            this.lblDLAplicatIonID.Text = "[????]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(219, 29);
            this.label6.TabIndex = 3;
            this.label6.Text = "Application Date:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 29);
            this.label5.TabIndex = 2;
            this.label5.Text = "License Class:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 291);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(217, 29);
            this.label4.TabIndex = 1;
            this.label4.Text = "Application Fees:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(240, 29);
            this.label3.TabIndex = 0;
            this.label3.Text = "D.L Application ID:";
            // 
            // lblLDLAppID
            // 
            this.lblLDLAppID.AutoSize = true;
            this.lblLDLAppID.Font = new System.Drawing.Font("Times New Roman", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLDLAppID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblLDLAppID.Location = new System.Drawing.Point(188, 8);
            this.lblLDLAppID.Name = "lblLDLAppID";
            this.lblLDLAppID.Size = new System.Drawing.Size(569, 39);
            this.lblLDLAppID.TabIndex = 0;
            this.lblLDLAppID.Text = "New Local Driving License Application";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.close;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(971, 657);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 43);
            this.button3.TabIndex = 5;
            this.button3.Text = "   Close";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Driver_VehiclesLicensesDepartmentProject.Properties.Resources.diskette;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(1117, 657);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 43);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "   Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ctrlPersonDetails1
            // 
            this.ctrlPersonDetails1.Location = new System.Drawing.Point(-4, 150);
            this.ctrlPersonDetails1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlPersonDetails1.Name = "ctrlPersonDetails1";
            this.ctrlPersonDetails1.Size = new System.Drawing.Size(1124, 378);
            this.ctrlPersonDetails1.TabIndex = 7;
            // 
            // NewLocalDrivingLicenseApplicationfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 701);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblLDLAppID);
            this.Controls.Add(this.tcLocaldrivingLicense);
            this.Name = "NewLocalDrivingLicenseApplicationfrm";
            this.Text = "NewLocalDrivingLicenseApplicationfrm";
            this.Activated += new System.EventHandler(this.NewLocalDrivingLicenseApplicationfrm_Activated);
            this.Load += new System.EventHandler(this.NewLocalDrivingLicenseApplicationfrm_Load);
            this.tcLocaldrivingLicense.ResumeLayout(false);
            this.tbPersonalInfo.ResumeLayout(false);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.tbApplicationInfo.ResumeLayout(false);
            this.tbApplicationInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcLocaldrivingLicense;
        private System.Windows.Forms.TabPage tbPersonalInfo;
        private System.Windows.Forms.TabPage tbApplicationInfo;
        private System.Windows.Forms.Label lblLDLAppID;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Button AddPerson;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbFilteration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFilterPeople;
        private ctrlPersonDetails ctrlPersonDetails1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblDLAplicatIonID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbLicenseClasses;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblFees;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label lblapplicationDate;
    }
}