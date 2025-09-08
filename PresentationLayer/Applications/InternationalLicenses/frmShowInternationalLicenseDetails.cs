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
    public partial class frmShowInternationalLicenseDetails : Form
    {
        private int _IntLicenseID;
        public frmShowInternationalLicenseDetails(int intLicenseID)
        {
            InitializeComponent();
            _IntLicenseID = intLicenseID;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private clsInternationalDrivingLicenseBusinessLayer internationalDrivingLicenseBusinessLayer;
        private clsPerson person;
        private void _LoadIntLicenseInfo()
        {
           internationalDrivingLicenseBusinessLayer=clsInternationalDrivingLicenseBusinessLayer.FindByIntLicenseId(_IntLicenseID);
            if (internationalDrivingLicenseBusinessLayer != null) { 
              lblDateOfBirth.Text=internationalDrivingLicenseBusinessLayer.ApplicationDate.ToShortDateString();
                lblAppID.Text=internationalDrivingLicenseBusinessLayer.ApplicationID.ToString();
                lblIsActive.Text=(internationalDrivingLicenseBusinessLayer.IsActive==true)?"Yes":"No";
                lblExpirationDate.Text=internationalDrivingLicenseBusinessLayer.ExpirationDate.ToShortDateString();
                lblIssueDate.Text=internationalDrivingLicenseBusinessLayer.IssueDate.ToShortDateString();
                lblLicenseID.Text = clsLicensesBusinessLayer.FindByLicenseID(internationalDrivingLicenseBusinessLayer.LocalLicenseID).LicenseID.ToString();
                person=clsPerson.Find(internationalDrivingLicenseBusinessLayer.ApplicantID);
                lblname.Text = person.FirstName + " " + person.Secondname + " " + person.ThirdName + " " + person.LastName;
                lblDriverID.Text=internationalDrivingLicenseBusinessLayer.DriverID.ToString();
                lblGendor.Text = (person.Gendor == 0) ? "Male" : "Female";
                lblNationalNo.Text=person.NationalNo.ToString();
                lblIntLicenseID.Text = internationalDrivingLicenseBusinessLayer.InternationalLicenseID.ToString();
            }
        }
        private void frmShowInternationalLicenseDetails_Load(object sender, EventArgs e)
        {
            _LoadIntLicenseInfo();
        }
    }
}
