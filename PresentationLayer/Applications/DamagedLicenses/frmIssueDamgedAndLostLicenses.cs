using LicenseBusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_VehiclesLicensesDepartmentProject
{
    public partial class frmIssueDamgedAndLostLicenses : Form
    {
        public frmIssueDamgedAndLostLicenses()
        {
            InitializeComponent();
        }
        private int _LicenseiD;
        private clsApplicationsBusinesssLayer applicationsBusinesssLayer;
        //private void _CheckApplicationType()
        //{
        //    if (rbDamagedLicense.Checked) {
        //        lblDamagedOrLostLicense.Text = "Replacement For Damaged License";
        //    }
        //    else
        //    {
        //        lblDamagedOrLostLicense.Text = "Replacement For Lost License";
        //    }
        //}
        private void _CheckKBeforeIssue(int LicenseID)
        {
            _LicenseiD = LicenseID;
            llShowLicenseHistory.Enabled = true;
            if (!rbDamagedLicense.Checked && !rbLostlicense.Checked) {
                MessageBox.Show("Select Replacment First", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblAppDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = Global.CurrentUser.UserName;
            if (rbDamagedLicense.Checked) {
                lblFees.Text=clsApplicationTypesBusinessLayer.Find(4).ApplicationFees.ToString();
            }
            else
            {
                lblFees.Text = clsApplicationTypesBusinessLayer.Find(3).ApplicationFees.ToString();
            }
            lblOldLicense.Text = LicenseID.ToString();
            if (!clsLicensesBusinessLayer.IsLicenseActive(LicenseID))
            {
                MessageBox.Show("Selected License Is Not Active", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsLicensesBusinessLayer.IsLicenseExpired(LicenseID)) {
                MessageBox.Show("Cannot Replace An Expired License", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btnIssue.Enabled = true;
        }
        private void frmIssueDamgedAndLostLicenses_Load(object sender, EventArgs e)
        {
            rbDamagedLicense.Select();
            lblDamagedOrLostLicense.Text = "Replacement For Damaged License";
            uctrlFilterLicenses1.OnLicenseSelected += _CheckKBeforeIssue;
            llShowLicenseDetails.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            btnIssue.Enabled = false;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            uctrlFilterLicenses1.licensesBusinessLayer.IsActive = false;
            if (uctrlFilterLicenses1.licensesBusinessLayer.Save()) { 
                applicationsBusinesssLayer=new clsApplicationsBusinesssLayer();
                applicationsBusinesssLayer.ApplicantID=clsApplicationsBusinesssLayer.Find(uctrlFilterLicenses1.licensesBusinessLayer.ApplicationID).ApplicantID;
                applicationsBusinesssLayer.ApplicationStatus =clsApplicationsBusinesssLayer.enApplicationStatus.Completed;
                applicationsBusinesssLayer.ApplicationDate = DateTime.Now;
                applicationsBusinesssLayer.LastStatusDate = DateTime.Now;
                applicationsBusinesssLayer.PaidFees = decimal.Parse(lblFees.Text);
                applicationsBusinesssLayer.ApplicationTypeID=(rbDamagedLicense.Checked)?4:3;//if i made enum it would be better than this
                applicationsBusinesssLayer.CreatedByUserID=Global.CurrentUser.UserID;
                if (applicationsBusinesssLayer.Save()) { 
                    uctrlFilterLicenses1.licensesBusinessLayer=new clsLicensesBusinessLayer();
                    uctrlFilterLicenses1.licensesBusinessLayer.ApplicationID=applicationsBusinesssLayer.ApplicationID;
                    uctrlFilterLicenses1.licensesBusinessLayer.DriverID=clsLicensesBusinessLayer.FindByLicenseID(_LicenseiD).DriverID;
                    uctrlFilterLicenses1.licensesBusinessLayer.IsActive=true;
                    uctrlFilterLicenses1.licensesBusinessLayer.IssueDate=DateTime.Now;
                    uctrlFilterLicenses1.licensesBusinessLayer.ExpirationDate=DateTime.Now;
                    uctrlFilterLicenses1.licensesBusinessLayer.IssueReason =(rbDamagedLicense.Checked) ?(byte)3 :(byte)4; //3 - Replacement for Damaged, 4 - Replacement for Lost.;
                    uctrlFilterLicenses1.licensesBusinessLayer.LicenseClass = clsLicensesBusinessLayer.FindByLicenseID(_LicenseiD).LicenseClass;
                    uctrlFilterLicenses1.licensesBusinessLayer.CreatedByUserID = Global.CurrentUser.UserID;
                    uctrlFilterLicenses1.licensesBusinessLayer.PaidFees = clsLicenseClassesBusinessLayer.Find(uctrlFilterLicenses1.licensesBusinessLayer.LicenseClass).ClassFees;
                    uctrlFilterLicenses1.licensesBusinessLayer.Notes ="";
                    if (uctrlFilterLicenses1.licensesBusinessLayer.Save())
                    {

                        lblILAppID.Text = applicationsBusinesssLayer.ApplicationID.ToString();
                        lblilicenseiD.Text = uctrlFilterLicenses1.licensesBusinessLayer.LicenseID.ToString();
                        groupBox1.Enabled=false;
                        uctrlFilterLicenses1.groupBox1.Enabled=false;
                        llShowLicenseDetails.Enabled = true;
                        btnIssue.Enabled = false;
                        MessageBox.Show("License Replaced Successfully", "Succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("License Wasn't Replaced Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Application Wasn't Saved Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("License Wasn't updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llShowLicenseDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseInfo driverLicenseInfo = new frmDriverLicenseInfo(applicationsBusinesssLayer.ApplicationID);
            driverLicenseInfo.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory licenseHistory = new LicenseHistory(clsApplicationsBusinesssLayer.Find(uctrlFilterLicenses1.licensesBusinessLayer.ApplicationID).ApplicantID);
            licenseHistory.ShowDialog();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblDamagedOrLostLicense.Text = "Replacement For Damaged License";
                lblFees.Text=clsApplicationTypesBusinessLayer.Find(4).ApplicationFees.ToString();
        }

        private void rbLostlicense_CheckedChanged(object sender, EventArgs e)
        {
            lblDamagedOrLostLicense.Text = "Replacement For Lost License";
            lblFees.Text = clsApplicationTypesBusinessLayer.Find(3).ApplicationFees.ToString();//i Had To Make An Enum in clsApplicationBusinessLayer for ApplicationTypes
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
