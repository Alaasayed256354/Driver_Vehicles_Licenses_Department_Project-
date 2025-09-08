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
    public partial class frmManageInternationalLicense : Form
    {
        public frmManageInternationalLicense()
        {
            InitializeComponent();
        }
        DataTable dt;
        private void _FillDGWWithInternationalLicenses()
        {
            dt = clsInternationalDrivingLicenseBusinessLayer.GetAllInternationalLicenses();
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            lblRecords.Text = dataGridView1.RowCount.ToString();
        }
        private void _FilteringInternationalDrivingAccessApplications()
        {
            DataView dataView = new DataView(dt);
            string filter = "";
            string FilterText = tbFiltering.Text.ToString();
            if (string.IsNullOrEmpty(FilterText) || string.IsNullOrEmpty(cbFilter.SelectedItem.ToString()) || cbFilter.SelectedItem.ToString() == "None" || string.IsNullOrWhiteSpace(FilterText))
            {
                dataGridView1.DataSource = dt;
                return;
            }
            switch (cbFilter.SelectedItem.ToString())
            {
                case "Driver ID":
                    filter = $"DriverID = '{FilterText}'";
                    break;
                case "I.L.ID":
                    filter = $"InternationalLicenseID='{FilterText}'";
                    break;
            }
            dataView.RowFilter = filter;
            dataGridView1.DataSource = dataView;
        }
        private void btnAddIL_Click(object sender, EventArgs e)
        {
            formNewInternationalDrivingLicense newInternationalDrivingLicense = new formNewInternationalDrivingLicense();
            newInternationalDrivingLicense.ShowDialog();
            _FillDGWWithInternationalLicenses();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbFiltering_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (cbFilter.SelectedIndex == -1)
            //{
            //    tbFiltering.Visible = false;
            //    return;
            //}
            //else
            //{
            //    tbFiltering.Visible = true;
            //}
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "None")
            {
                tbFiltering.Text=string.Empty;
                dataGridView1.DataSource = dt;
                tbFiltering.Visible = false;
            }
            else
            {
                tbFiltering.Visible = true;
            }
        }

        private void frmManageInternationalLicense_Load(object sender, EventArgs e)
        {
            _FillDGWWithInternationalLicenses();
        }

        private void tbFiltering_TextChanged(object sender, EventArgs e)
        {
            _FilteringInternationalDrivingAccessApplications();
            lblRecords.Text = dataGridView1.RowCount.ToString();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsInternationalDrivingLicenseBusinessLayer internationalDrivingLicenseBusinessLayer = clsInternationalDrivingLicenseBusinessLayer.FindByAppID((int)dataGridView1.CurrentRow.Cells[1].Value);
            ShowDetailsForm showDetails = new ShowDetailsForm(internationalDrivingLicenseBusinessLayer.ApplicantID);
            showDetails.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowInternationalLicenseDetails showInternationalLicenseDetails=new frmShowInternationalLicenseDetails((int)dataGridView1.CurrentRow.Cells[0].Value);
            showInternationalLicenseDetails.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsApplicationsBusinesssLayer applicationsBusinesssLayer = clsApplicationsBusinesssLayer.Find((int)dataGridView1.CurrentRow.Cells[1].Value);
            LicenseHistory licenseHistory =new LicenseHistory(applicationsBusinesssLayer.ApplicantID);
            licenseHistory.ShowDialog();
        }
    }
}
