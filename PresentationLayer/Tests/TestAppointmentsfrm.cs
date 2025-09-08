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
    public partial class TestAppointmentsfrm : Form
    {
        
       public enum enTestMode { VisionMode=1,WritingMode=2,StreetMode=3};
         public enTestMode _TestMode;
        private int _LDLAppID;
        private DataTable dataTable;
        private int _TestTypeID;

        //public TestAppointmentsfrm()
        //{
        //    InitializeComponent();
        //}
        public TestAppointmentsfrm(int lDLAppID, enTestMode TestMode)
        {
            InitializeComponent();
            _LDLAppID = lDLAppID;
            _TestMode = TestMode;
            _TestTypeID =Convert.ToInt32(_TestMode);
        }
        private void _FillDGVWithAppointments()
        {
            dataTable = clsTestAppointmentsBusinessLayer.GetAppointmentsOFGivenPersonOfGivenTestType(_LDLAppID,Math.Abs(_TestTypeID));
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
        }
        private void _LoadTestAppointmentsInfo()
        {
            switch (_TestMode)
            {
                case enTestMode.VisionMode:
                    lblTest.Text = "Vision Test Appointments";
                    this.Text = "Vision Test Appointments";
                    pbTestAppointment.Image = Properties.Resources.eye;
                    _TestTypeID = 1;
                    break;
                case enTestMode.WritingMode:
                    this.Text = "Writingn Test Appointments";
                    lblTest.Text = "Writingn Test Appointments";
                    _TestTypeID = 2;
                    pbTestAppointment.Image = Properties.Resources.documents;
                    break;
                case enTestMode.StreetMode:
                    this.Text = "Street Test Appointments";
                    lblTest.Text = "Street Test Appointments";
                    _TestTypeID = 3;
                    pbTestAppointment.Image = Properties.Resources.car;
                    break;
            }
            uctrlDrivingLicenseApplicationInfo1._LoadDrivingLicenseApplicationInfo(_LDLAppID);
        }
        private void VisionTestAppointmentsfrm_Load(object sender, EventArgs e)
        {
            _LoadTestAppointmentsInfo();
            _FillDGVWithAppointments();
        }
        private void uctrlDrivingLicenseApplicationInfo1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int TestTypeID=-1;
            if (_TestMode == enTestMode.VisionMode) {
                if (clsTestAppointmentsBusinessLayer.IsHasPreviousOpenAppointment(_LDLAppID, 1)) {
                    MessageBox.Show("Person Already have an Active Appointment For Vision Test,you can't add new appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (clsTestsBusinessLayer.IsPassedTheTest(_LDLAppID,1))
                {
                    MessageBox.Show("Person Already Passed The Vision Test,you can't add new appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (clsTestsBusinessLayer.IsHeRetakeTest(_LDLAppID, 1)) {
                    TestTypeID = -1;
                }
                else
                {
                    TestTypeID = 1;
                }
            }
          else  if (_TestMode == enTestMode.WritingMode)
            {
                if (clsTestAppointmentsBusinessLayer.IsHasPreviousOpenAppointment(_LDLAppID,2))
                {
                    MessageBox.Show("Person Already have an Active Appointment For Writing Test,you can't add new appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (clsTestsBusinessLayer.IsPassedTheTest(_LDLAppID,2))
                {
                    MessageBox.Show("Person Already Passed The Writing Test,you can't add new appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (clsTestsBusinessLayer.IsHeRetakeTest(_LDLAppID,2))
                {
                    TestTypeID = -2;
                }
                else
                {
                    TestTypeID =2;
                }
            }
       else     if (_TestMode == enTestMode.StreetMode)
            {
                if (clsTestAppointmentsBusinessLayer.IsHasPreviousOpenAppointment(_LDLAppID, 3))
                {
                    MessageBox.Show("Person Already have an Active Appointment For Street Test,you can't add new appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (clsTestsBusinessLayer.IsPassedTheTest(_LDLAppID, 3))
                {
                    MessageBox.Show("Person Already Passed The Street Test,you can't add new appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (clsTestsBusinessLayer.IsHeRetakeTest(_LDLAppID, 3))
                {
                    TestTypeID = -3;
                }
                else
                {
                    TestTypeID = 3;
                }
            }
               _TestTypeID = TestTypeID;
                SheduleTestfrm shedule = new SheduleTestfrm(-1,TestTypeID, /*uctrlDrivingLicenseApplicationInfo1.LocalDrivingLicenseApplicationBusinessLayer.*/_LDLAppID);
                shedule.OnFormclosing += _FillDGVWithAppointments;
                shedule.ShowDialog();
                _FillDGVWithAppointments();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SheduleTestfrm sheduleTestfrm = new SheduleTestfrm((int)dataGridView1.CurrentRow.Cells[0].Value, (int)dataGridView1.CurrentRow.Cells[1].Value,uctrlDrivingLicenseApplicationInfo1.LocalDrivingLicenseApplicationBusinessLayer.LDLAppID);
            sheduleTestfrm.ShowDialog();
            _FillDGVWithAppointments();
        }
        public event Action OnTestCompleted;
        private void _RefreshDGVOfLocaldrivingLicenses()
        {
            OnTestCompleted?.Invoke();
        }
        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime AppointmentDate = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;
            if (DateTime.Compare(AppointmentDate, DateTime.Now)==1)//Compare function return 1 if appointmentdate greater than datetime.now and return -1 if apointmnentdate less than datetime.now and 0 if they are equal
            {
                MessageBox.Show($"Cannot Take The Test Before The Scheduled Date {AppointmentDate}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //if (clsTestsBusinessLayer.IsPassedTheTest(_LDLAppID, _TestTypeID))
            //{
            //    takeTestToolStripMenuItem.Enabled =false;
            //    return;
            //}
            frmTakeTest takeTest = new frmTakeTest((int)dataGridView1.CurrentRow.Cells[2].Value, (int)dataGridView1.CurrentRow.Cells[0].Value, (int)dataGridView1.CurrentRow.Cells[1].Value);
            takeTest.OnTestFinish += _FillDGVWithAppointments;
            takeTest.OnTestFinish += _LoadTestAppointmentsInfo;
            takeTest.OnTestFinish += _RefreshDGVOfLocaldrivingLicenses;
            takeTest.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                takeTestToolStripMenuItem.Enabled = false;
                editToolStripMenuItem.Enabled = false;
                return;
            }
            editToolStripMenuItem.Enabled = true;
            if (clsTestAppointmentsBusinessLayer.IsTestClosed((int)dataGridView1.CurrentRow.Cells[0].Value)) 
            { 
                 takeTestToolStripMenuItem.Enabled=false;
            }
            else
            {
                takeTestToolStripMenuItem.Enabled=true; 
            }
          
        }
    }
}
