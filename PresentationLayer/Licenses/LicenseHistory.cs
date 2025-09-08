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
    public partial class LicenseHistory : Form
    {
        private int _ApplicantID;
        private DataTable _dtLocalDrivingLicenses;
        private DataTable _dtInternationalDrivingLicenses;
        public LicenseHistory(int ApplicantID)
        {
            InitializeComponent();
            _ApplicantID = ApplicantID;
        }
        private void _LoadPersonDetails()
        {
            ctrlPersonDetails1._LoadPersonInfo(_ApplicantID);
        }
        private void _LoadPersonLocalDrivingLicenses()
        {
            _dtLocalDrivingLicenses = clsLicensesBusinessLayer.GetAllLocalDrvingLicenseOfPerson(_ApplicantID);
            dataGridView2.DataSource = _dtLocalDrivingLicenses;
            lblLocalRecords.Text = dataGridView2.RowCount.ToString();
            dataGridView2.Refresh();
        }
        private void _LoadPersonInternationalDrivingLicenses()
        {
            _dtInternationalDrivingLicenses = clsInternationalDrivingLicenseBusinessLayer.GetAllInternationalDrvingLicenseOfPerson(_ApplicantID);
            dataGridView1.DataSource = _dtInternationalDrivingLicenses;
            lblInternationalRecords.Text = dataGridView1.RowCount.ToString();
            dataGridView1.Refresh();
        }
        private void LicenseHistory_Load(object sender, EventArgs e)

        {
            _LoadPersonDetails();
            _LoadPersonLocalDrivingLicenses();
            _LoadPersonInternationalDrivingLicenses();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDriverLicenseInfo driverLicenseInfo = new frmDriverLicenseInfo((int)dataGridView2.CurrentRow.Cells[1].Value);
            driverLicenseInfo.ShowDialog();
        }

        private void showLicenseInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmShowInternationalLicenseDetails internationalLicenseDetails = new frmShowInternationalLicenseDetails((int)dataGridView1.CurrentRow.Cells[0].Value);
            internationalLicenseDetails.ShowDialog();
        }
    }
}
