using LicenseBusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_VehiclesLicensesDepartmentProject
{
    public partial class uctrlDrivingLicenseApplicationInfo : UserControl
    {
        private clsLocalDrivingLicenseApplicationBusinessLayer localDrivingLicenseApplicationBusinessLayer=new clsLocalDrivingLicenseApplicationBusinessLayer();
         public clsLocalDrivingLicenseApplicationBusinessLayer LocalDrivingLicenseApplicationBusinessLayer { get { return localDrivingLicenseApplicationBusinessLayer; } }
        public uctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        public void _LoadDrivingLicenseApplicationInfo(int LocalDrivingLicenseApplicationID)
        {
          
                localDrivingLicenseApplicationBusinessLayer = clsLocalDrivingLicenseApplicationBusinessLayer.FindByLDLAID(LocalDrivingLicenseApplicationID);
                if(localDrivingLicenseApplicationBusinessLayer != null )
            {
                lblAppliedForLicense.Text = localDrivingLicenseApplicationBusinessLayer.LicenseClassesBusinessLayer.ClassName;
                lblDLAppID.Text=localDrivingLicenseApplicationBusinessLayer.LDLAppID.ToString();
                lblUserName.Text = localDrivingLicenseApplicationBusinessLayer.UserInfo.UserName;
                lblType.Text = localDrivingLicenseApplicationBusinessLayer.ApplicationTypeInfo.ApplicationTypeTitle;
                lblStatusDate.Text = localDrivingLicenseApplicationBusinessLayer.LastStatusDate.ToShortDateString();
                lblFees.Text=localDrivingLicenseApplicationBusinessLayer.PaidFees.ToString();
                lblDate.Text = localDrivingLicenseApplicationBusinessLayer.ApplicationDate.ToShortDateString();
                lblStatus.Text =localDrivingLicenseApplicationBusinessLayer.StatusText;
                clsPerson person=clsPerson.Find(localDrivingLicenseApplicationBusinessLayer.ApplicantID);
                lblApplicant.Text =person.FirstName+' '+person.Secondname+' '+person.ThirdName+' '+person.LastName;
                lblAppID.Text = localDrivingLicenseApplicationBusinessLayer.ApplicationID.ToString();
                lblPassedTests.Text = localDrivingLicenseApplicationBusinessLayer.PassedTests.ToString();
            }
            else
            {
                MessageBox.Show("This LDLAppID Not Found");
            }
        }
        private void uctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }

        private void gbApplicationBAsicInfo_Enter(object sender, EventArgs e)
        {

        }

        private void llPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowDetailsForm showDetails = new ShowDetailsForm(localDrivingLicenseApplicationBusinessLayer.ApplicantID);
            showDetails.Show();
        }
    }
}
