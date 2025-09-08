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
    public partial class frmDetainLicense : Form
    {
        public frmDetainLicense(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }
        private int _LicenseID;
        private clsDetainedLicenseBusineesLayer detainedLicenseBusineesLayer;
        private void _CheckBeforeDetain(int LicenseID)
        {
            _LicenseID = LicenseID;
            llShowLicenseHistory.Enabled = true;
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text =Global.CurrentUser.UserName;
            lblLicenseID.Text=LicenseID.ToString();
            //detainedLicenseBusineesLayer=clsDetainedLicenseBusineesLayer.FindByLicenseID(LicenseID);
          
            if (clsDetainedLicenseBusineesLayer.FindByLicenseID(LicenseID)!=null) {
                MessageBox.Show("You Can't Detain This License Because It's Already Detained","Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!clsLicensesBusinessLayer.IsLicenseActive(LicenseID))
            {
                MessageBox.Show("You Can't Detain This License Because It's Not Active", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btnDetain.Enabled = true;
        }
        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            tbFineFees.Text = string.Empty;
            if (_LicenseID != -1)
            {
                uctrlFilterLicenses1.groupBox1.Enabled = false;
                uctrlFilterLicenses1.tbFilterLicense.Text=_LicenseID.ToString();
                uctrlFilterLicenses1.LoadDriverLicenseInfo(_LicenseID);
                _CheckBeforeDetain(_LicenseID);
            }
            else{
                uctrlFilterLicenses1.OnLicenseSelected += _CheckBeforeDetain;
            }
            btnDetain.Enabled=false;
            llShowLicenseDetails.Enabled = false;
            llShowLicenseHistory.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFineFees.Text.Trim())) {
                MessageBox.Show("Select Fine Fees Firstly", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                 uctrlFilterLicenses1.licensesBusinessLayer.IsActive=false;
            if (uctrlFilterLicenses1.licensesBusinessLayer.Save())
            {
                    detainedLicenseBusineesLayer = new clsDetainedLicenseBusineesLayer();
                    detainedLicenseBusineesLayer.LicenseID = _LicenseID;
                    detainedLicenseBusineesLayer.IsReleased = false;
                    detainedLicenseBusineesLayer.DetainDate = DateTime.Now;
                    detainedLicenseBusineesLayer.CreatedByUserID = Global.CurrentUser.UserID;
                    detainedLicenseBusineesLayer.FineFees = decimal.Parse(tbFineFees.Text);
                if (detainedLicenseBusineesLayer.Save())
                    {
                       uctrlFilterLicenses1.groupBox1.Enabled = false; lblDetainID.Text = detainedLicenseBusineesLayer.DetainID.ToString();
                       llShowLicenseDetails.Enabled = true;
                       btnDetain.Enabled = false;
                    MessageBox.Show("License Detained Successfully", "Succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                else
                {
                    MessageBox.Show("License Wasn't Detained Successfully", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("License Wasn't Updated Successfully", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbFineFees_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFineFees.Text)||string.IsNullOrWhiteSpace(tbFineFees.Text))
            {
                btnDetain.Enabled=false;
            }
            else
            {
                btnDetain.Enabled=true;
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           int ApplicantID=clsApplicationsBusinesssLayer.Find(clsLicensesBusinessLayer.FindByLicenseID(_LicenseID).ApplicationID).ApplicantID;
            LicenseHistory licenseHistory=new LicenseHistory(ApplicantID);
            licenseHistory.ShowDialog();
        }

        private void llShowLicenseDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int AppID = clsLicensesBusinessLayer.FindByLicenseID(_LicenseID).ApplicationID;
            frmDriverLicenseInfo driverLicenseInfo = new frmDriverLicenseInfo(AppID);
            driverLicenseInfo.ShowDialog();
        }
    }
}
