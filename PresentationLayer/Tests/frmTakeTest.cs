using LicenseBusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Driver_VehiclesLicensesDepartmentProject
{
    public partial class frmTakeTest : Form
    {

        //private string TakeTestTrialFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "countTakeTestTrials.txt");
        private int _LDLAppID;
        private int _TestAppointmentID;
        private int _TestTypeID;
        private clsLocalDrivingLicenseApplicationBusinessLayer _LocalDrivingLicenseApplication;
        private clsLicenseClassesBusinessLayer _LicenseClassesBusinessLayer;
        private clsApplicationsBusinesssLayer _ApplicationsBusinesssLayer;
        private clsTestAppointmentsBusinessLayer _TestAppointmentsBusinessLayer;
        private clsPerson _Person;
        private clsTestsBusinessLayer _TestsBusinessLayer;
        private void _LoadScheduledTestInfo()
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplicationBusinessLayer.FindByLDLAID(_LDLAppID);
            _LicenseClassesBusinessLayer = clsLicenseClassesBusinessLayer.Find(_LocalDrivingLicenseApplication.LicenseClassID);
            _Person = clsPerson.Find(_LocalDrivingLicenseApplication.ApplicantID);
            lblDLAppID.Text = _LDLAppID.ToString();
            lblAppliedForLicense.Text = _LicenseClassesBusinessLayer.ClassName.ToString();
            lblName.Text = _Person.FirstName + " " + _Person.Secondname + " " + _Person.ThirdName + " " + _Person.LastName;
            lblDate.Text = DateTime.Now.ToShortDateString();//_TestAppointmentsBusinessLayer.AppointmentDate.ToString();
            if (_TestTypeID == 1)
            {
                lblFees.Text = "10";
                gbTakeTest.Text = "Vision Test";
                pbTakeTest.Image = Properties.Resources.eye;
            }
            else if (_TestTypeID == 2)
            {
                gbTakeTest.Text = "Writing Test";
                lblFees.Text = "20";
                pbTakeTest.Image = Properties.Resources.documents;
            }
            else
            {
                gbTakeTest.Text = "Street Test";
                lblFees.Text = "30";
                pbTakeTest.Image = Properties.Resources.car;
            }
            lblTestID.Text = "Not Taken Yet";
            _TestsBusinessLayer = new clsTestsBusinessLayer();
        }
     
        public frmTakeTest(int lDLAppID, int TestAppointmentID, int TestTypeID)
        {
            InitializeComponent();
            _LDLAppID = lDLAppID;
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _LoadScheduledTestInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!rbPass.Checked && !rbFail.Checked)
            {
                MessageBox.Show("You Have To Select Test Result", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _TestsBusinessLayer.TestAppointmentID = _TestAppointmentID;
            _TestsBusinessLayer.TestResult = (rbPass.Checked) ? true : false;
            _TestsBusinessLayer.Notes = tbNotes.Text.Trim();
            _TestsBusinessLayer.CreatedByUserID = Global.CurrentUser.UserID;
            if (_TestsBusinessLayer.Save())
            {
                _TestAppointmentsBusinessLayer = clsTestAppointmentsBusinessLayer.Find(_TestAppointmentID);
                if (_TestAppointmentsBusinessLayer != null)
                {
                    _TestAppointmentsBusinessLayer.IsLocked = true;
                    _TestAppointmentsBusinessLayer.RetakeTestApplicationID = _TestAppointmentsBusinessLayer.RetakeTestAppInfo?.ApplicationID??-1;
                    if (_TestAppointmentsBusinessLayer.Save())
                    {
                        lblTestID.Text = _TestsBusinessLayer.TestID.ToString();
                        MessageBox.Show("Test Saved Successsfully", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                MessageBox.Show("Test Wasn't Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (_TestTypeID == 3 && clsTestsBusinessLayer.IsPassedTheTest(_LDLAppID, _TestTypeID))
            {
                _LocalDrivingLicenseApplication.ApplicationStatus = clsApplicationsBusinesssLayer.enApplicationStatus.Completed;
                _LocalDrivingLicenseApplication.Save();
            }
            OnTestFinish?.Invoke();
        }
        public event Action OnTestFinish;
        private void frmTakeTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnTestFinish?.Invoke();//Check if there any subscribers
        }
    }
}
