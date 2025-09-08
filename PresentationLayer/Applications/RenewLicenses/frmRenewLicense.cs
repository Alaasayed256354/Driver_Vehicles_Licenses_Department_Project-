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
    public partial class frmRenewLicense : Form
    {
        public frmRenewLicense()
        {
            InitializeComponent();
        }
        private int _licenseID;
        //private clsLicensesBusinessLayer licensesBusinessLayer;
        private clsApplicationTypesBusinessLayer licensesApplicationTypesBusinessLayer;
        private clsApplicationsBusinesssLayer applicationsBusinesssLayer;
        public void Clear()
        {
            btnrenew.Enabled = false;
            llShowLicenseDetails.Enabled = false;
            llShowLicenseHistory.Enabled = false;
            lblOldLicenseID.Text = "[???]";
            lblAppDate.Text = "[???]";
            LicenseFees.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblApplicationFees.Text = "[???]";
            lblTotalFees.Text = "[???]";
            lblExpirationDate.Text = "[???]";
            lblCreatedBy.Text = "[???]";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int ExpirationYears=-1;
        private void _CheckBeforeRenew(int LicenseID)
        {
            _licenseID = LicenseID;
            llShowLicenseHistory.Enabled = true;
            lblAppDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = clsApplicationTypesBusinessLayer.Find(2).ApplicationFees.ToString();
            lblCreatedBy.Text = Global.CurrentUser.UserName;
             ExpirationYears = clsLicenseClassesBusinessLayer.Find(uctrlFilterLicenses1.licensesBusinessLayer.LicenseClass).DefaultValidityLength;
            lblExpirationDate.Text = DateTime.Now.AddYears(ExpirationYears).ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblOldLicenseID.Text = _licenseID.ToString();
            lblLicenseFees.Text = clsLicenseClassesBusinessLayer.Find(uctrlFilterLicenses1.licensesBusinessLayer.LicenseClass).ClassFees.ToString();
            lblTotalFees.Text = (clsLicenseClassesBusinessLayer.Find(uctrlFilterLicenses1.licensesBusinessLayer.LicenseClass).ClassFees + clsApplicationTypesBusinessLayer.Find(2).ApplicationFees).ToString();
            if (!clsLicensesBusinessLayer.IsLicenseActive(_licenseID))
            {
                MessageBox.Show("You Can't Renew Non Active License:", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!clsLicensesBusinessLayer.IsLicenseExpired(LicenseID))
            {
                MessageBox.Show("You Can't Renew License Before ExpirationDate:" + uctrlFilterLicenses1.licensesBusinessLayer.ExpirationDate, "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btnrenew.Enabled = true;
        }
        private void frmRenewLicense_Load(object sender, EventArgs e)
        {
            uctrlFilterLicenses1.OnLicenseSelected += _CheckBeforeRenew;
            llShowLicenseHistory.Enabled = false;
            llShowLicenseDetails.Enabled = false;
            btnrenew.Enabled=false;
        }

        private void btnrenew_Click(object sender, EventArgs e)
        {
            uctrlFilterLicenses1.licensesBusinessLayer.IsActive = false;
            if (uctrlFilterLicenses1.licensesBusinessLayer.Save())
            {
                applicationsBusinesssLayer = new clsApplicationsBusinesssLayer();
                applicationsBusinesssLayer.ApplicantID = uctrlFilterLicenses1.applicationsBusinesssLayer.ApplicantID;
                applicationsBusinesssLayer.ApplicationDate = DateTime.Now;
                applicationsBusinesssLayer.ApplicationStatus =clsApplicationsBusinesssLayer.enApplicationStatus.Completed;
                applicationsBusinesssLayer.CreatedByUserID = Global.CurrentUser.UserID;
                applicationsBusinesssLayer.PaidFees = clsLicenseClassesBusinessLayer.Find(uctrlFilterLicenses1.licensesBusinessLayer.LicenseClass).ClassFees + clsApplicationTypesBusinessLayer.Find(2).ApplicationFees;
                applicationsBusinesssLayer.LastStatusDate = DateTime.Now;
                applicationsBusinesssLayer.ApplicationTypeID = 2;
                if (applicationsBusinesssLayer.Save())
                {
                    uctrlFilterLicenses1.licensesBusinessLayer = new clsLicensesBusinessLayer();
                    uctrlFilterLicenses1.licensesBusinessLayer.ApplicationID = applicationsBusinesssLayer.ApplicationID;
                    uctrlFilterLicenses1.licensesBusinessLayer.IsActive = true;
                    uctrlFilterLicenses1.licensesBusinessLayer.ExpirationDate = DateTime.Now.AddYears(ExpirationYears);
                    uctrlFilterLicenses1.licensesBusinessLayer.IssueDate = DateTime.Now;
                    uctrlFilterLicenses1.licensesBusinessLayer.IssueReason = 2;
                    uctrlFilterLicenses1.licensesBusinessLayer.LicenseClass = clsLicensesBusinessLayer.FindByLicenseID(_licenseID).LicenseClass;
                    uctrlFilterLicenses1.licensesBusinessLayer.CreatedByUserID = Global.CurrentUser.UserID;
                    uctrlFilterLicenses1.licensesBusinessLayer.PaidFees = clsLicenseClassesBusinessLayer.Find(uctrlFilterLicenses1.licensesBusinessLayer.LicenseClass).ClassFees;
                    uctrlFilterLicenses1.licensesBusinessLayer.DriverID = clsLicensesBusinessLayer.FindByLicenseID(_licenseID).DriverID;
                    uctrlFilterLicenses1.licensesBusinessLayer.Notes = tbNotes.Text.ToString();
                    if (uctrlFilterLicenses1.licensesBusinessLayer.Save())
                    {
                        lblRenewApplicationID.Text =applicationsBusinesssLayer.ApplicationID.ToString();
                        lblRenewedLicenseID.Text =uctrlFilterLicenses1.licensesBusinessLayer.LicenseID.ToString();
                        uctrlFilterLicenses1.groupBox1.Enabled=false;
                        llShowLicenseDetails.Enabled = true;
                        btnrenew.Enabled = false;
                        MessageBox.Show("License Renewed Successfully", "Succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("License Wasn't Renewed Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            frmDriverLicenseInfo driverLicenseInfo=new frmDriverLicenseInfo(applicationsBusinesssLayer.ApplicationID);
            driverLicenseInfo.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory licenseHistory=new LicenseHistory(clsApplicationsBusinesssLayer.Find(uctrlFilterLicenses1.licensesBusinessLayer.ApplicationID).ApplicantID);
            licenseHistory.ShowDialog();
        }
    }
    }

