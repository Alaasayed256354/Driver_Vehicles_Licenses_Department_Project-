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
    public partial class frmDriverLicenseInfo : Form
    {
        private int _AppID;
       private clsApplicationsBusinesssLayer applicationsBusinesssLayer;
        private clsLicensesBusinessLayer licensesBusinessLayer;
        private clsLicenseClassesBusinessLayer licenseClassesBusinessLayer;
        private clsPerson person;
        private void _LoadLicenseInfo()
        {
            applicationsBusinesssLayer = clsApplicationsBusinesssLayer.Find(_AppID);
            licensesBusinessLayer = clsLicensesBusinessLayer.FindByApplicationID(_AppID);
            licenseClassesBusinessLayer = clsLicenseClassesBusinessLayer.Find(licensesBusinessLayer.LicenseClass);
            person = clsPerson.Find(applicationsBusinesssLayer.ApplicantID);
            lblClass.Text = licenseClassesBusinessLayer.ClassName;
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            lblDriverID.Text = licensesBusinessLayer.DriverID.ToString();
            lblExpirationDate.Text = licensesBusinessLayer.ExpirationDate.ToShortDateString().ToString();
            lblGendor.Text = (person.Gendor == 0) ? "Male" : "Female";
            lblNationalNo.Text =person.NationalNo.ToString();
            lblIsDetained.Text =(clsDetainedLicenseBusineesLayer.IsLicensedetained(licensesBusinessLayer.LicenseID))?"Yes":"No";
            lblIssueDate.Text = licensesBusinessLayer.IssueDate.ToShortDateString().ToString();
            lblLicenseID.Text = person.NationalNo.ToString();
            lblNotes.Text = (licensesBusinessLayer.Notes == "") ? "No Notes" : licensesBusinessLayer.Notes;
            lblname.Text = person.FirstName + " " + person.Secondname + " " + person.ThirdName + " " + person.LastName;
            lblLicenseID.Text = licensesBusinessLayer.LicenseID.ToString();
            lblIsActive.Text = (licensesBusinessLayer.IsActive == true) ? "Yes" : "No";
            switch (licensesBusinessLayer.IssueReason)
            {
                case 1:
                    lblIssueReason.Text = "FirstTime";
                    break;
                case 2:
                    lblIssueReason.Text = "Renew";
                    break;
                case 3:
                    lblIssueReason.Text = "Replacement for Damaged";
                    break;
                case 4:
                    lblIssueReason.Text = " Replacement for Lost";
                    break;
                    //1 - FirstTime, 2 - Renew, 3 - Replacement for Damaged, 4 - Replacement for Lost.
            }

        }
        public frmDriverLicenseInfo(int AppID)
        {
            InitializeComponent();
            _AppID = AppID;
        }

        private void frmDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            _LoadLicenseInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
