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
    public partial class IssueDrivingLicense : Form
    {
        private int _LDLA;
        private clsDriversBusinesslayer DriversBusinesslayer;
        private clsLocalDrivingLicenseApplicationBusinessLayer LocalDrivingLicenseApplicationBusinessLayer;
        private clsLicensesBusinessLayer LicensesBusinessLayer;
        private clsLicenseClassesBusinessLayer LicenseClassesBusinessLayer;
        public IssueDrivingLicense(int ldla)
        {
            InitializeComponent();
            _LDLA = ldla;
        }

        private void IssueDrivingLicense_Load(object sender, EventArgs e)
        {
            uctrlDrivingLicenseApplicationInfo1._LoadDrivingLicenseApplicationInfo(_LDLA);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplicationBusinessLayer = clsLocalDrivingLicenseApplicationBusinessLayer.FindByLDLAID(_LDLA);
            int DriverID = clsDriversBusinesslayer.IsHeDriver(LocalDrivingLicenseApplicationBusinessLayer.personInfo.personID);
            if (DriverID == -1)
            {
                DriversBusinesslayer = new clsDriversBusinesslayer();
                DriversBusinesslayer.PersonID = LocalDrivingLicenseApplicationBusinessLayer.ApplicantID;
                DriversBusinesslayer.CreatedByUserID = Global.CurrentUser.UserID;
                DriversBusinesslayer.CreatedDate = DateTime.Now;

                if (DriversBusinesslayer.Save())
                {
                    DriverID = DriversBusinesslayer.DriverID;
                }
                else
                {
                    MessageBox.Show($"Driver Wasn't Saved", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            LicensesBusinessLayer = new clsLicensesBusinessLayer();
            LicensesBusinessLayer.ApplicationID = LocalDrivingLicenseApplicationBusinessLayer.ApplicationID;
            LicensesBusinessLayer.LicenseClass = LocalDrivingLicenseApplicationBusinessLayer.LicenseClassID;
            LicensesBusinessLayer.Notes = tbNotes.Text;
            LicenseClassesBusinessLayer = clsLicenseClassesBusinessLayer.Find(LocalDrivingLicenseApplicationBusinessLayer.LicenseClassID);
            LicensesBusinessLayer.PaidFees = LicenseClassesBusinessLayer.ClassFees;
            LicensesBusinessLayer.CreatedByUserID = Global.CurrentUser.UserID;
            LicensesBusinessLayer.IsActive = true;
            LicensesBusinessLayer.IssueReason = 1;
            LicensesBusinessLayer.IssueDate = DateTime.Now;
            LicensesBusinessLayer.ExpirationDate = DateTime.Now.AddYears((LicenseClassesBusinessLayer.DefaultValidityLength));//كدا هيجمع ال validty Length علي ال التاريخ بتاع النهاردة
            LicensesBusinessLayer.DriverID = DriverID;
            if (LicensesBusinessLayer.Save())
            {
                MessageBox.Show($"License Issued Successfully With License ID={LicensesBusinessLayer.LicenseID}", "Succedded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"License Wasn't Issued", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
