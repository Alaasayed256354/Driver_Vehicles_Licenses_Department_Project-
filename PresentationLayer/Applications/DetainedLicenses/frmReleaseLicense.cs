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
    public partial class frmReleaseLicense : Form
    {
        public frmReleaseLicense(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }
             private int _LicenseID;
             private clsDetainedLicenseBusineesLayer detainedLicenseBusineesLayer;
             private clsApplicationsBusinesssLayer applicationsBusinesssLayer;
        private void _CheckBeforeRelease(int LicenseID)
        {
            _LicenseID = LicenseID;
            llShowLicenseHistory.Enabled = true;
           llShowLicenseDetails.Enabled = true;
            detainedLicenseBusineesLayer=clsDetainedLicenseBusineesLayer.FindByLicenseID(_LicenseID);
            if (detainedLicenseBusineesLayer != null) {
                btnRelease.Enabled = true;
                lblDetainDate.Text =detainedLicenseBusineesLayer.DetainDate.ToShortDateString();
                lblAppFees.Text = clsApplicationTypesBusinessLayer.Find(5).ApplicationFees.ToString();
                lblCreatedBy.Text=detainedLicenseBusineesLayer.CreatedByUserID.ToString();
                lblFineFees.Text = detainedLicenseBusineesLayer.FineFees.ToString();
                lblilicenseiD.Text=_LicenseID.ToString();
                lblDetainID.Text = detainedLicenseBusineesLayer.DetainID.ToString();
                lblTotalFees.Text=(detainedLicenseBusineesLayer.FineFees+ clsApplicationTypesBusinessLayer.Find(5).ApplicationFees).ToString();
                //if (!clsDetainedLicenseBusineesLayer.IsLicensedetained(detainedLicenseBusineesLayer.DetainID)) { 
                //  MessageBox.Show("License Not Detained to Release","Not Allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                //    return;
                //}
            }
            else
            {
                MessageBox.Show("No License Detained With This ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmReleaseLicense_Load(object sender, EventArgs e)
        {
            if (_LicenseID != -1)
            {
                uctrlFilterLicenses1.groupBox1.Enabled = false;
                uctrlFilterLicenses1.tbFilterLicense.Text = _LicenseID.ToString();
                uctrlFilterLicenses1.LoadDriverLicenseInfo(_LicenseID);
                _CheckBeforeRelease(_LicenseID);
            }
            else
            {
                uctrlFilterLicenses1.OnLicenseSelected += _CheckBeforeRelease;
                llShowLicenseHistory.Enabled = false;
                llShowLicenseDetails.Enabled = false;
                btnRelease.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            applicationsBusinesssLayer=new clsApplicationsBusinesssLayer();
            applicationsBusinesssLayer.ApplicantID=clsApplicationsBusinesssLayer.Find(uctrlFilterLicenses1.licensesBusinessLayer.ApplicationID).ApplicantID;
            applicationsBusinesssLayer.ApplicationStatus = clsApplicationsBusinesssLayer.enApplicationStatus.Completed;
            applicationsBusinesssLayer.ApplicationDate = DateTime.Now;
            applicationsBusinesssLayer.LastStatusDate = DateTime.Now;
            applicationsBusinesssLayer.PaidFees = clsApplicationTypesBusinessLayer.Find(5).ApplicationFees;
            applicationsBusinesssLayer.CreatedByUserID=Global.CurrentUser.UserID;
            applicationsBusinesssLayer.ApplicationTypeID = 5;
            if (applicationsBusinesssLayer.Save())
            {
                uctrlFilterLicenses1.licensesBusinessLayer.IsActive = true;
                if (uctrlFilterLicenses1.licensesBusinessLayer.Save())
                {
                    detainedLicenseBusineesLayer.IsReleased = true;
                    detainedLicenseBusineesLayer.ReleaseDate = DateTime.Now;
                    detainedLicenseBusineesLayer.ReleasedByUserID = Global.CurrentUser.UserID;
                    detainedLicenseBusineesLayer.ReleaseApplicationID = applicationsBusinesssLayer.ApplicationID;
                    if (detainedLicenseBusineesLayer.Save())
                    {
                        lblApplicationID.Text = applicationsBusinesssLayer.ApplicationID.ToString();
                        btnRelease.Enabled = false;
                        MessageBox.Show("License Released Successfully", "Released!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            //        else
            //        {
            //            MessageBox.Show("License Wasn't Released Successfully", "Wasn't Released!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("License Wasn't Updated Successfully", "Wasn't Updated!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Application Wasn't Saved", "Wasn't Saved!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void llShowLicenseDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseInfo driverLicenseInfo=new frmDriverLicenseInfo(uctrlFilterLicenses1.licensesBusinessLayer.ApplicationID);
            driverLicenseInfo.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory licenseHistory = new LicenseHistory(clsApplicationsBusinesssLayer.Find(uctrlFilterLicenses1.licensesBusinessLayer.ApplicationID).ApplicantID);
            licenseHistory.ShowDialog();
        }
    }
}
