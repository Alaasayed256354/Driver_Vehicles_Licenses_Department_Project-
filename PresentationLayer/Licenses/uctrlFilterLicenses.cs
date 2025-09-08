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
    public partial class uctrlFilterLicenses : UserControl
    {
        public uctrlFilterLicenses()
        {
            InitializeComponent();
        }
        public event Action<int> OnLicenseSelected;
        public event Action OnTextFree;
        public bool IsClear = true;
        public clsLicensesBusinessLayer licensesBusinessLayer;
        //public clsLocalDrivingLicenseApplicationBusinessLayer localDrivingLicenseApplicationBusinessLayer;
        private clsLicenseClassesBusinessLayer licensesClassesBusinessLayer;
        public clsApplicationsBusinesssLayer applicationsBusinesssLayer;
        private clsPerson person;
        //private void tbFilterLicense_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //}
        public void Clear()
        {
            IsClear = true;
            lblPersonName.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblClass.Text = "[???]";
            lblGendor.Text = "[???]";
            lblLicenseID.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblIssueReason.Text = "[???]";
            lblNotes.Text = "[???]";
            lblIsActive.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblDriverID.Text = "[???]";
            lblIsDetained.Text = "[???]";
            lblExpirationDate.Text = "[???]";
        }
        //private void button1_Click(object sender, EventArgs e)
        //{
         
        //}
        private void uctrlFilterLicenses_Load(object sender, EventArgs e)
        {

        }

        private void tbFilterLicense_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }
        }
        public void LoadDriverLicenseInfo(int LicenseID)
        {
            licensesBusinessLayer = clsLicensesBusinessLayer.FindByLicenseID(LicenseID);
            if (licensesBusinessLayer == null)
            {
                MessageBox.Show("No License With This ID", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            IsClear = false;
            applicationsBusinesssLayer = clsApplicationsBusinesssLayer.Find(licensesBusinessLayer.ApplicationID);
            person = clsPerson.Find(applicationsBusinesssLayer.ApplicantID);
            if (person == null)
            {
                MessageBox.Show("Can't Find Person Releated With This ID", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            licensesClassesBusinessLayer = clsLicenseClassesBusinessLayer.Find(licensesBusinessLayer.LicenseClass);
            lblClass.Text = licensesClassesBusinessLayer.ClassName;
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            lblDriverID.Text = licensesBusinessLayer.DriverID.ToString();
            lblExpirationDate.Text = licensesBusinessLayer.ExpirationDate.ToShortDateString().ToString();
            lblGendor.Text = (person.Gendor == 0) ? "Male" : "Female";
            lblIsDetained.Text=(clsDetainedLicenseBusineesLayer.IsLicensedetained(LicenseID))?"Yes":"No";
            lblIssueDate.Text = licensesBusinessLayer.IssueDate.ToShortDateString().ToString();
            lblNationalNo.Text = person.NationalNo.ToString();
            lblNotes.Text = (licensesBusinessLayer.Notes == "") ? "No Notes" : licensesBusinessLayer.Notes;
            lblPersonName.Text = person.FirstName + " " + person.Secondname + " " + person.ThirdName + " " + person.LastName;
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
            OnLicenseSelected?.Invoke(int.Parse(tbFilterLicense.Text));

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFilterLicense.Text) || string.IsNullOrWhiteSpace(tbFilterLicense.Text.Trim())/*||this.ValidateChildren()*/)
            {
                MessageBox.Show("No License ID To Search", "Inform!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        LoadDriverLicenseInfo(int.Parse(tbFilterLicense.Text));
        }

        private void tbFilterLicense_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbFilterLicense.Text))
            {
                OnTextFree?.Invoke();
                Clear();
            }
        }
    }
}
