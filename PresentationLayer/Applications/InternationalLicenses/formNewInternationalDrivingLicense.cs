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
    public partial class formNewInternationalDrivingLicense : Form
    {
        //private clsLocalDrivingLicenseApplicationBusinessLayer   localDrivingLicenseApplicationBusinessLayer;
        //private clsLicensesBusinessLayer licensesBusinessLayer;
        private clsLicenseClassesBusinessLayer licenseClassesBusinessLayer;
        private clsInternationalDrivingLicenseBusinessLayer internationalDrivingLicenseBusinessLayer;
        public formNewInternationalDrivingLicense()
        {
            InitializeComponent();
        }
        public void Clear()
        {
            btnIssue.Enabled = false;
            lblILAppID.Text = "[???]";
            lblAppDate.Text = "[???]";
            lblilicenseiD.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblFees.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblLLID.Text = "[???]";
            lblExpirationDate.Text = "[???]";
            lblCreatedBy.Text = "[???]";
        }

        private int _LicenseiD;
        private void _CheckBeforIssue(int LicenseID)
        {
            _LicenseiD = LicenseID;
            //licensesBusinessLayer=clsLicensesBusinessLayer.FindByLicenseID(LicenseID);
            //localDrivingLicenseApplicationBusinessLayer = clsLocalDrivingLicenseApplicationBusinessLayer.FindByAppID(licensesBusinessLayer.ApplicationID);
            licenseClassesBusinessLayer = clsLicenseClassesBusinessLayer.Find(uctrlFilterLicenses1.licensesBusinessLayer.LicenseClass);
            lblCreatedBy.Text=Global.CurrentUser.UserName;
            lblExpirationDate.Text=DateTime.Now.AddYears(1).ToShortDateString();
            lblFees.Text=clsApplicationTypesBusinessLayer.Find(6).ApplicationFees.ToString();
            lblIssueDate.Text=DateTime.Now.ToShortDateString();
            lblLLID.Text=_LicenseiD.ToString();
            lblAppDate.Text= DateTime.Now.ToShortDateString();
            llShowLicenseHistory.Enabled = true;

            if (licenseClassesBusinessLayer.ClsassID != 3)
            {
                MessageBox.Show("You Can't Issue International License Without Having Local Driving License Of Ordinery Llicense Class,Select Another One", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsLicensesBusinessLayer.IsLicenseExpired(LicenseID))
            {
                MessageBox.Show("You Can't Issue International License With Expired Local Driving License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!clsLicensesBusinessLayer.IsLicenseActive(_LicenseiD))
            {
                MessageBox.Show("You Can't Issue International License With an InActive Local Driving License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btnIssue.Enabled = true;
        }
        private void formNewInternationalDrivingLicense_Load(object sender, EventArgs e)
        {
            uctrlFilterLicenses1.OnLicenseSelected += _CheckBeforIssue;
            uctrlFilterLicenses1.OnTextFree += Clear;
            btnIssue.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseDetails.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //private int ApplicantID = clsApplicationsBusinesssLayer.Find(uctrlFilterLicenses1.licensesBusinessLayer.ApplicationID).ApplicantID;
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (clsInternationalDrivingLicenseBusinessLayer.IsHasInternationalLicense(uctrlFilterLicenses1.licensesBusinessLayer.DriverID))
            {
                MessageBox.Show("He Already Has Innternational License","Not Allowed",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            internationalDrivingLicenseBusinessLayer=new clsInternationalDrivingLicenseBusinessLayer();
            internationalDrivingLicenseBusinessLayer.ApplicantID= clsApplicationsBusinesssLayer.Find(uctrlFilterLicenses1.licensesBusinessLayer.ApplicationID).ApplicantID; ;
            internationalDrivingLicenseBusinessLayer.LocalLicenseID=_LicenseiD;
            internationalDrivingLicenseBusinessLayer.ApplicationDate=DateTime.Now;
            internationalDrivingLicenseBusinessLayer.ExpirationDate=DateTime.Now.AddYears(1);
            internationalDrivingLicenseBusinessLayer.IssueDate=DateTime.Now;
            internationalDrivingLicenseBusinessLayer.LastStatusDate=DateTime.Now;
            internationalDrivingLicenseBusinessLayer.ApplicationStatus =clsApplicationsBusinesssLayer.enApplicationStatus.Completed;
            internationalDrivingLicenseBusinessLayer.CreatedByUserID=Global.CurrentUser.UserID;
            internationalDrivingLicenseBusinessLayer.DriverID=uctrlFilterLicenses1.licensesBusinessLayer.DriverID;
            internationalDrivingLicenseBusinessLayer.IsActive=true;
            internationalDrivingLicenseBusinessLayer.ApplicationTypeID = 6;
            internationalDrivingLicenseBusinessLayer.PaidFees=clsApplicationTypesBusinessLayer.Find(6).ApplicationFees;
            if (internationalDrivingLicenseBusinessLayer.Save())
            {
                    btnIssue.Enabled=false;
                uctrlFilterLicenses1.groupBox1.Enabled=false;
                lblilicenseiD.Text = internationalDrivingLicenseBusinessLayer.InternationalLicenseID.ToString();
                    lblILAppID.Text = internationalDrivingLicenseBusinessLayer.ApplicationID.ToString();
                    MessageBox.Show("License Added Successfully", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                llShowLicenseDetails.Enabled = true;
            }
            else
            {
                MessageBox.Show("International Driving License Wasn't Added Successfully", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void uctrlFilterLicenses1_Load(object sender, EventArgs e)
        {

        }

        private void llShowLicenseDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseDetails internationalLicenseDetails = new frmShowInternationalLicenseDetails(internationalDrivingLicenseBusinessLayer.InternationalLicenseID);
             internationalLicenseDetails.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory licenseHistory = new LicenseHistory(clsApplicationsBusinesssLayer.Find(uctrlFilterLicenses1.licensesBusinessLayer.ApplicationID).ApplicantID);
            licenseHistory.ShowDialog();
        }
    }
}
