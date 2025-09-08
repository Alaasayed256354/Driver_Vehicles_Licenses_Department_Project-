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
    public partial class SheduleTestfrm : Form
    {
        public enum enScheduleTestMode { VisionMode=0,WritingMode=1,StreetMode=2,RetakeTest=3,Editmode=4};
        private enScheduleTestMode _scheduleTestMode;
         private  int _TestTypeID;
        private int _AppointmentID;
        private int _LDLAppID;
        //private int _ApplicationID;
        private clsApplicationsBusinesssLayer applicationsBusinesssLayer;
        private clsTestAppointmentsBusinessLayer testAppointmentsBusinessLayer;
        private clsLocalDrivingLicenseApplicationBusinessLayer localDrivingLicenseApplicationBusinessLayer;
        public SheduleTestfrm(int AppointmentID,int testTypeID,int LDLAppID)
        {
            InitializeComponent();
            _TestTypeID = testTypeID;
            _AppointmentID = AppointmentID;
            _LDLAppID = LDLAppID;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
      //  private TestAppointmentsfrm appointmentsfrm = new TestAppointmentsfrm();
        private void _LoadScheduleTestInfo()
        {
            
          localDrivingLicenseApplicationBusinessLayer=clsLocalDrivingLicenseApplicationBusinessLayer.FindByLDLAID(_LDLAppID);

            lblTrial.Text =clsLocalDrivingLicenseApplicationBusinessLayer.TotalTrialPerTest(_LDLAppID, Math.Abs(_TestTypeID)).ToString();
            lblDLAppID.Text = localDrivingLicenseApplicationBusinessLayer.LDLAppID.ToString();
            lblAppliedForLicense.Text =clsLicenseClassesBusinessLayer.Find(localDrivingLicenseApplicationBusinessLayer.LicenseClassID).ClassName;
            clsPerson person=clsPerson.Find(localDrivingLicenseApplicationBusinessLayer.ApplicantID);
            lblName.Text = person.FirstName + ' ' + person.Secondname + ' ' + person.ThirdName + ' ' + person.LastName;
            if (_AppointmentID == -1)
            {
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker1.MinDate = DateTime.Now;
                if (_TestTypeID == 1)
                {
                    lblFees.Text = clsApplicationTestsBusinessLayer.Find(_TestTypeID).TestTypeFees.ToString();
                    pbTestAppointment.Image = Properties.Resources.eye;
                    lblScheduleTest.Text = "Schedule Test";
                    uctrlRetakeTestInfo1.Enabled = false;
                    _scheduleTestMode = enScheduleTestMode.VisionMode;
                }
                else if (_TestTypeID == 2)
                {
                    lblFees.Text = clsApplicationTestsBusinessLayer.Find(_TestTypeID).TestTypeFees.ToString();

                    pbTestAppointment.Image = Properties.Resources.documents;
                    lblScheduleTest.Text = "Schedule Test";
                    uctrlRetakeTestInfo1.Enabled = false;
                    _scheduleTestMode = enScheduleTestMode.WritingMode;
                }
                else if (_TestTypeID == 3) { 
                    lblFees.Text = clsApplicationTestsBusinessLayer.Find(_TestTypeID).TestTypeFees.ToString();
                    pbTestAppointment.Image = Properties.Resources.car;
                    lblScheduleTest.Text = "Schedule Test";
                    uctrlRetakeTestInfo1.Enabled = false;
                    _scheduleTestMode = enScheduleTestMode.StreetMode;
                }
                else
                {
                    if (_TestTypeID == -1)
                    {
                        lblFees.Text = clsApplicationTestsBusinessLayer.Find(Math.Abs(_TestTypeID)).TestTypeFees.ToString();
                        this.Text = "Vision Test Appointments";
                        pbTestAppointment.Image = Properties.Resources.eye;
                    }
                    else if (_TestTypeID == -2)
                    {
                        lblFees.Text = clsApplicationTestsBusinessLayer.Find(Math.Abs(_TestTypeID)).TestTypeFees.ToString();
                        this.Text = "Writing Test Appointments";
                        pbTestAppointment.Image = Properties.Resources.documents;
                    }
                    else if (_TestTypeID == -3)
                    {
                        lblFees.Text = clsApplicationTestsBusinessLayer.Find(Math.Abs(_TestTypeID)).TestTypeFees.ToString();
                        this.Text = "Street Test Appointments";
                        pbTestAppointment.Image = Properties.Resources.car;
                    }
                    lblScheduleTest.Text = "Schedule Retake Test";
                    uctrlRetakeTestInfo1.Enabled = true;
                    DateTime? AppointmentDate = clsTestAppointmentsBusinessLayer.GetLastAppointmentOfPersonOfAGivenTestType(_LDLAppID,Math.Abs(_TestTypeID));
                    if (AppointmentDate == null)
                    {
                        dateTimePicker1.Value = DateTime.Now;
                        dateTimePicker1.MinDate = DateTime.Now;
                    }
                    else
                    {
                        dateTimePicker1.MinDate = AppointmentDate.Value;
                    }
                    _scheduleTestMode = enScheduleTestMode.RetakeTest;
                    uctrlRetakeTestInfo1._LoadRetakeInfo(Math.Abs(_TestTypeID),-1);
                }
                testAppointmentsBusinessLayer = new clsTestAppointmentsBusinessLayer();
            }
            else
            {
                testAppointmentsBusinessLayer = clsTestAppointmentsBusinessLayer.Find(_AppointmentID);
                if(testAppointmentsBusinessLayer == null )
                {
                    MessageBox.Show("Appointment ID Not Found");
                    return;
                }
                dateTimePicker1.MinDate =DateTime.Now;
                //dateTimePicker1.Value=testAppointmentsBusinessLayer.AppointmentDate;
                if (clsTestsBusinessLayer.IsThisTestWasARetakeTest(_AppointmentID, Math.Abs(_TestTypeID)))
                {
                    uctrlRetakeTestInfo1._LoadRetakeInfo(Math.Abs(_TestTypeID), clsTestAppointmentsBusinessLayer.Find(_AppointmentID).RetakeTestApplicationID);
                }
                 if (clsTestAppointmentsBusinessLayer.IsTestClosed(_AppointmentID)) 
                {
                    uctrlRetakeTestInfo1.Enabled = false;
                    btnSave.Enabled = false;
                    dateTimePicker1.Enabled = false;
                    lblScheduleTest.Text = "Edit Schedule Test";
                    lblCanotEdit.Text = "Cannot set This Appointment,it is locked";
                    return;
                }
                uctrlRetakeTestInfo1.Enabled = false;
                if (_TestTypeID ==1)
                    {
                        pbTestAppointment.Image = Properties.Resources.eye;
                    }
                    else if (_TestTypeID == 2)
                    {
                        pbTestAppointment.Image = Properties.Resources.documents;
                    }
                    else if (_TestTypeID == 3)
                    {
                        pbTestAppointment.Image = Properties.Resources.car;
                    }
                    btnSave.Enabled = true;
                    dateTimePicker1.Enabled = true;
                    lblScheduleTest.Text = "Edit Schedule Test";
                    lblFees.Text = clsApplicationTestsBusinessLayer.Find(_TestTypeID).TestTypeFees.ToString();
                    _scheduleTestMode = enScheduleTestMode.Editmode;
                }
            }
        private void SheduleTestfrm_Load(object sender, EventArgs e)
        {
            _LoadScheduleTestInfo();
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            testAppointmentsBusinessLayer.CreatedByUserID = Global.CurrentUser.UserID;
            testAppointmentsBusinessLayer.IsLocked =false;
            testAppointmentsBusinessLayer.PaidFees = clsApplicationTestsBusinessLayer.Find(Math.Abs(_TestTypeID)).TestTypeFees;
            testAppointmentsBusinessLayer.AppointmentDate = dateTimePicker1.Value;
            testAppointmentsBusinessLayer.TestTypeID =Math.Abs(_TestTypeID);
            testAppointmentsBusinessLayer.LocalDrivingLicenseAppID = _LDLAppID;
            switch (_scheduleTestMode)
            {
                case enScheduleTestMode.RetakeTest:
                    applicationsBusinesssLayer = new clsApplicationsBusinesssLayer();
                    applicationsBusinesssLayer.ApplicantID =localDrivingLicenseApplicationBusinessLayer.personInfo.personID;
                    applicationsBusinesssLayer.ApplicationDate = dateTimePicker1.Value;
                    applicationsBusinesssLayer.ApplicationTypeID =7;
                    applicationsBusinesssLayer.ApplicationStatus =clsApplicationsBusinesssLayer.enApplicationStatus.Completed;
                    applicationsBusinesssLayer.LastStatusDate = dateTimePicker1.Value;
                    applicationsBusinesssLayer.PaidFees=clsApplicationTypesBusinessLayer.Find(7).ApplicationFees;
                    applicationsBusinesssLayer.CreatedByUserID = Global.CurrentUser.UserID ;
                    if (applicationsBusinesssLayer.Save())
                    {
                        testAppointmentsBusinessLayer.RetakeTestApplicationID=applicationsBusinesssLayer.ApplicationID;
                        if (testAppointmentsBusinessLayer.Save())
                        {
                            uctrlRetakeTestInfo1._LoadRetakeInfo(Math.Abs(_TestTypeID),applicationsBusinesssLayer.ApplicationID);
                            MessageBox.Show("Test Appointment Saved");
                        }
                        else
                        {
                            MessageBox.Show("Test Appointment Wasn't Saved");
                        }
                    }
                    else { 
                        MessageBox.Show("Retake Application Wasn't Saved");
                    }
                    break;
                case enScheduleTestMode.VisionMode:
                case enScheduleTestMode.WritingMode:
                case enScheduleTestMode.StreetMode:
                case enScheduleTestMode.Editmode:
                    if (testAppointmentsBusinessLayer.Save()){
                        MessageBox.Show("Test Appointment Saved");
                    }
                    else
                    {
                        MessageBox.Show("Test Appointment Wasn't Saved");
                    }
                    break;
            }
        }
        public event Action OnFormclosing;
        private void SheduleTestfrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //OnFormclosing?.Invoke();
        }

        private void SheduleTestfrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnFormclosing?.Invoke();
        }
    }
}
