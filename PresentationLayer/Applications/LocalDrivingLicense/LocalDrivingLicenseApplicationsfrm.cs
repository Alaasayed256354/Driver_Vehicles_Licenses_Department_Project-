using LicenseBusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Driver_VehiclesLicensesDepartmentProject
{
    public partial class LocalDrivingLicenseApplicationsfrm : Form
    {
        public LocalDrivingLicenseApplicationsfrm()
        {
            InitializeComponent();
        }
        DataTable datatable;
        private void _FiilDGVWithLocalDLApplications()
        {
            datatable = clsLocalDrivingLicenseApplicationBusinessLayer.GetAlllocalDrivingLicense();
            dataGridView1.DataSource = datatable;
            dataGridView1.Refresh();
        }
        private void _FilteringLocalDrivingAccessApplications()
        {
            DataView dataView = new DataView(datatable);
            string filter = "";
            string FilterText = tbFiltering.Text.ToString();
            if (string.IsNullOrEmpty(FilterText) || string.IsNullOrEmpty(cbFilter.SelectedItem.ToString()) || cbFilter.SelectedItem.ToString() == "None" || string.IsNullOrWhiteSpace(FilterText))
            {
                dataGridView1.DataSource = datatable;
                return;
            }
            switch (cbFilter.SelectedItem.ToString())
            {
                case "Full Name":
                    filter = $"FullName LIKE '{FilterText}%'";
                    break;
                case "L D L AppID":
                    filter = $"LocalDrivingLicenseApplicationID='{FilterText}'";
                    break;
                case "Status":
                    filter = $"Status LIKE '{FilterText}%'";
                    break;
                case "National No":
                    filter = $"NationalNo LIKE '{FilterText}%'";
                    break;
            }
            dataView.RowFilter = filter;
            dataGridView1.DataSource = dataView;
        }

        private void LocalDrivingLicenseApplicationsfrm_Load(object sender, EventArgs e)
        {
            _FiilDGVWithLocalDLApplications();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewLocalDrivingLicenseApplicationfrm localDrivingLicenseApplicationfrm = new NewLocalDrivingLicenseApplicationfrm();
            localDrivingLicenseApplicationfrm.ShowDialog();
            _FiilDGVWithLocalDLApplications();
        }

        private void tbFiltering_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilter.SelectedIndex == -1)
            {
                tbFiltering.Visible = false;
                return;
            }
            else
            {
                tbFiltering.Visible = true;
            }
            switch (cbFilter.SelectedItem.ToString())
            {
                case "Full Name":
                case "Status":
                    if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case "National No":
                    if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case "L D L AppID":
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                    //case "None":
                    //    if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsSeparator(e.KeyChar))
                    //    {
                    //        e.Handled = true;
                    //    }
                    //    break;
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "None")
            {
                tbFiltering.Visible = false;
            }
            else
            {
                tbFiltering.Visible = true;
            }
        }

        private void tbFiltering_TextChanged(object sender, EventArgs e)
        {
            _FilteringLocalDrivingAccessApplications();
        }
        private void _CheckTests()
        {
            int LDLAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplicationBusinessLayer localDrivingLicenseApplicationBusinessLayer = clsLocalDrivingLicenseApplicationBusinessLayer.FindByLDLAID(LDLAppID);
            clsApplicationsBusinesssLayer.enApplicationStatus appstaat= localDrivingLicenseApplicationBusinessLayer.ApplicationStatus;
            int PasedTestsCount = localDrivingLicenseApplicationBusinessLayer.PassedTests;
            bool IsLicenseCanceled = clsLocalDrivingLicenseApplicationBusinessLayer.IsApplicationCanceled(LDLAppID);
            bool IsLicenseExict=clsLicensesBusinessLayer.IsLicenseExist(localDrivingLicenseApplicationBusinessLayer.ApplicationID);
            issueDrivingLicenseToolStripMenuItem.Enabled = (PasedTestsCount == 3) && !IsLicenseExict;
            showLicenseToolStripMenuItem.Enabled = IsLicenseExict;
            editApplicationToolStripMenuItem.Enabled =/*!IsLicenseCanceled&&*/(localDrivingLicenseApplicationBusinessLayer.ApplicationStatus==clsApplicationsBusinesssLayer.enApplicationStatus.New);
            deleteApplicationToolStripMenuItem.Enabled =PasedTestsCount==0&&(localDrivingLicenseApplicationBusinessLayer.ApplicationStatus == clsApplicationsBusinesssLayer.enApplicationStatus.Canceled ||localDrivingLicenseApplicationBusinessLayer.ApplicationStatus == clsApplicationsBusinesssLayer.enApplicationStatus.New);
            cancelApplicationToolStripMenuItem.Enabled = (localDrivingLicenseApplicationBusinessLayer.ApplicationStatus == clsApplicationsBusinesssLayer.enApplicationStatus.New);

            bool PassedVisionTest =clsTestsBusinessLayer.IsPassedTheTest((int)dataGridView1.CurrentRow.Cells[0].Value,1);
            bool PassedWritingTest=clsTestsBusinessLayer.IsPassedTheTest((int)dataGridView1.CurrentRow.Cells[0].Value,2);
            bool passedStreetTest =clsTestsBusinessLayer.IsPassedTheTest((int)dataGridView1.CurrentRow.Cells[0].Value, 3);

            sechduleTestsToolStripMenuItem.Enabled =(!passedStreetTest||!PassedVisionTest||!PassedWritingTest)&& !IsLicenseCanceled && localDrivingLicenseApplicationBusinessLayer.ApplicationStatus == clsApplicationsBusinesssLayer.enApplicationStatus.New;
            if (sechduleTestsToolStripMenuItem.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled=!PassedVisionTest;
                scheduleWritingTestToolStripMenuItem.Enabled=PassedVisionTest&&!PassedWritingTest&&!passedStreetTest;
                scheduleStreetTestToolStripMenuItem.Enabled= PassedVisionTest && PassedWritingTest && !passedStreetTest;
            }
            //if (clsLocalDrivingLicenseApplicationBusinessLayer.IsApplicationCanceled((int)dataGridView1.CurrentRow.Cells[0].Value))
            //{
            //    cancelApplicationToolStripMenuItem.Enabled = false;
            //    editApplicationToolStripMenuItem.Enabled = false;
            //    showLicenseToolStripMenuItem.Enabled = false;   
            //    deleteApplicationToolStripMenuItem.Enabled = false;
            //    sechduleTestsToolStripMenuItem.Enabled = false;
            //    issueDrivingLicenseToolStripMenuItem.Enabled = false;
            //    showApplicationDetailsToolStripMenuItem.Enabled=true;
            //    showPersonLicenseHistoryToolStripMenuItem.Enabled=true ;
            //    return;
            //}
            //if (clsLicensesBusinessLayer.IsHasAnActiveLicenseForAGivenClassName((string)dataGridView1.CurrentRow.Cells[1].Value,clsLocalDrivingLicenseApplicationBusinessLayer.FindByLDLAID((int)dataGridView1.CurrentRow.Cells[0].Value).personInfo.personID))
            //{
            //    cancelApplicationToolStripMenuItem.Enabled = false;
            //    editApplicationToolStripMenuItem.Enabled = false;
            //    showLicenseToolStripMenuItem.Enabled = true;
            //    deleteApplicationToolStripMenuItem.Enabled = false;
            //    sechduleTestsToolStripMenuItem.Enabled = false;
            //    issueDrivingLicenseToolStripMenuItem.Enabled = false;
            //    showApplicationDetailsToolStripMenuItem.Enabled = true;
            //    showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
            //    return;
            //}
            //cancelApplicationToolStripMenuItem.Enabled = true;
            //editApplicationToolStripMenuItem.Enabled = true;
            //issueDrivingLicenseToolStripMenuItem.Enabled=false;
            //showLicenseToolStripMenuItem.Enabled=false;
            //deleteApplicationToolStripMenuItem.Enabled = true;
            //sechduleTestsToolStripMenuItem.Enabled = true;
            //showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
            //showApplicationDetailsToolStripMenuItem.Enabled = true;

            //if (!clsTestsBusinessLayer.IsPassedTheTest((int)dataGridView1.CurrentRow.Cells[0].Value, 1))
            //    {
            //        scheduleVisionTestToolStripMenuItem.Enabled = true;
            //        scheduleWritingTestToolStripMenuItem.Enabled = false;
            //        scheduleStreetTestToolStripMenuItem.Enabled = false;
            //    }
            //  else  if (!clsTestsBusinessLayer.IsPassedTheTest((int)dataGridView1.CurrentRow.Cells[0].Value, 2))
            //    {
            //        scheduleWritingTestToolStripMenuItem.Enabled = true;
            //        scheduleVisionTestToolStripMenuItem.Enabled = false;
            //        scheduleStreetTestToolStripMenuItem.Enabled = false;
            //    }
            // else   if (!clsTestsBusinessLayer.IsPassedTheTest((int)dataGridView1.CurrentRow.Cells[0].Value, 3))
            //    {
            //        scheduleStreetTestToolStripMenuItem.Enabled = true;
            //        scheduleVisionTestToolStripMenuItem.Enabled = false;
            //        scheduleWritingTestToolStripMenuItem.Enabled = false;
            //    }
            //    else
            //    {
            //        scheduleVisionTestToolStripMenuItem.Enabled = false;
            //        scheduleWritingTestToolStripMenuItem.Enabled = false;
            //        scheduleStreetTestToolStripMenuItem.Enabled = false;
            //        issueDrivingLicenseToolStripMenuItem.Enabled = true;
            //    sechduleTestsToolStripMenuItem.Enabled = false;
            //}
        }
        private void sechduleTestsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestAppointmentsfrm visionTest = new TestAppointmentsfrm((int)dataGridView1.CurrentRow.Cells[0].Value, TestAppointmentsfrm.enTestMode.VisionMode);
            visionTest.OnTestCompleted += _FiilDGVWithLocalDLApplications;
            visionTest.Show();
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowApplicationDetails showApplicationDetails = new frmShowApplicationDetails((int)dataGridView1.CurrentRow.Cells[0].Value);
            showApplicationDetails.Show();
        }

        private void sechduleTestsToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {

        }

        private void sechduleTestsToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            _CheckTests();
        }

        private void scheduleWritingTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestAppointmentsfrm writingTest = new TestAppointmentsfrm((int)dataGridView1.CurrentRow.Cells[0].Value, TestAppointmentsfrm.enTestMode.WritingMode);
            writingTest.OnTestCompleted += _FiilDGVWithLocalDLApplications;
            writingTest.Show();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestAppointmentsfrm streetTest = new TestAppointmentsfrm((int)dataGridView1.CurrentRow.Cells[0].Value, TestAppointmentsfrm.enTestMode.StreetMode);
            streetTest.OnTestCompleted += _FiilDGVWithLocalDLApplications;
            streetTest.Show();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLocalDrivingLicenseApplicationfrm localDrivingLicenseApplicationfrm = new NewLocalDrivingLicenseApplicationfrm((int)dataGridView1.CurrentRow.Cells[0].Value);
            localDrivingLicenseApplicationfrm.ShowDialog();
            _FiilDGVWithLocalDLApplications();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplicationBusinessLayer localDrivingLicenseApplicationBusinessLayer = clsLocalDrivingLicenseApplicationBusinessLayer.FindByLDLAID((int)dataGridView1.CurrentRow.Cells[0].Value);
            if (MessageBox.Show("Are You Sure You Want To Delete This Local Driving License Application", "Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (localDrivingLicenseApplicationBusinessLayer.DeleteLocalDrivingLicenseApplication())
                {
                    MessageBox.Show("Application Deleted Successfully", "Inform!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _FiilDGVWithLocalDLApplications();
                }
            }
        }

        private void issueDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IssueDrivingLicense issueDrivingLicense = new IssueDrivingLicense((int)dataGridView1.CurrentRow.Cells[0].Value);
            issueDrivingLicense.Show();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplicationBusinessLayer localDrivingLicenseApplicationBusinessLayer = clsLocalDrivingLicenseApplicationBusinessLayer.FindByLDLAID((int)dataGridView1.CurrentRow.Cells[0].Value);
            LicenseHistory licenseHistory = new LicenseHistory(localDrivingLicenseApplicationBusinessLayer.ApplicantID);
            licenseHistory.Show();
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplicationBusinessLayer localDrivingLicenseApplicationBusinessLayer = clsLocalDrivingLicenseApplicationBusinessLayer.FindByLDLAID((int)dataGridView1.CurrentRow.Cells[0].Value);
            if (MessageBox.Show("Are You Sure You Want To Cancel This Local Driving License Application", "Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                localDrivingLicenseApplicationBusinessLayer.ApplicationStatus =clsApplicationsBusinesssLayer.enApplicationStatus.Canceled;
                if (localDrivingLicenseApplicationBusinessLayer.Save())
                {
                    MessageBox.Show("Application Canceled Successfullly", "Inform!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _FiilDGVWithLocalDLApplications();
                }
                else
                {
                    MessageBox.Show("Application Wasn't Canceled Successfullly", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplicationBusinessLayer localDrivingLicenseApplicationBusinessLayer = clsLocalDrivingLicenseApplicationBusinessLayer.FindByLDLAID((int)dataGridView1.CurrentRow.Cells[0].Value);
            frmDriverLicenseInfo driverLicenseInfo =new frmDriverLicenseInfo(localDrivingLicenseApplicationBusinessLayer.ApplicationID);
            driverLicenseInfo.Show();
        }
    }
}
