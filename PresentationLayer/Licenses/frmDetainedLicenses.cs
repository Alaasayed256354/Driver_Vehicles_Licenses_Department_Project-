using LicenseBusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Driver_VehiclesLicensesDepartmentProject
{
    public partial class frmDetainedLicenses : Form
    {
        public frmDetainedLicenses()
        {
            InitializeComponent();
        }
        private DataTable dt;
        private DataView dv;
        private void _FillDGVWithDetainedLicenses()
        {
            dt=clsDetainedLicenseBusineesLayer.GetAllDetainedLicenses();
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            lbldriversRecords.Text= dt.Rows.Count.ToString();
        }
        private void _FilterDetainedLicenses()
        {
//            None
//Detain ID
//Is Released
//National No
//Full Name
//Release Application ID
            if (string.IsNullOrEmpty(tbFilterDetainedLicenses.Text.Trim())|| cbFilteration.SelectedIndex == 0)
            {
                dataGridView1.DataSource = dt;
                lbldriversRecords.Text = dataGridView1.RowCount.ToString();
                return;
            }
            string FilterText=tbFilterDetainedLicenses.Text;
            string FilterBy = "";
            switch (cbFilteration.SelectedIndex) {
                case 1:
                    FilterBy = $"DetainID={FilterText}";
                    break;
                case 3:
                    FilterBy = $"NationalNo LIKE'{FilterText}%'";
                    break;
                case 4:
                    FilterBy = $"FullName LIKE '{FilterText}%'";
                    break;
                case 5:
                    FilterBy = $"ReleaseApplicationID={FilterText}";
                    break;
            }
            dv.RowFilter = FilterBy;
            dataGridView1.DataSource= dv;
            lbldriversRecords.Text =dataGridView1.RowCount.ToString();
        }
        private void frmDetainedLicense_Load(object sender, EventArgs e)
        {
            _FillDGVWithDetainedLicenses();
            dv = new DataView(dt);
            tbFilterDetainedLicenses.Visible = false;
            cbFilterIsRelease.Visible = false;
            cbFilteration.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilteration_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilteration.SelectedIndex == 0)
            {
                dataGridView1.DataSource = dt;
                tbFilterDetainedLicenses.Text = string.Empty;
                cbFilterIsRelease.Visible=false;
                tbFilterDetainedLicenses.Visible = false;
            }
            else if(cbFilteration.SelectedIndex == 2)
            {
                cbFilterIsRelease.Visible=true;
                tbFilterDetainedLicenses.Visible=false;
            }
            else
            {
                cbFilterIsRelease.Visible = false;
                tbFilterDetainedLicenses.Visible = true;
            }
        }

        private void tbFilterDetainedLicenses_TextChanged(object sender, EventArgs e)
        {
            _FilterDetainedLicenses();
        }

        private void cbFilterIsRelease_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterIsRelease.SelectedIndex == 0) {
                dv.RowFilter = "IsReleased=1";
            }
            else
            {
                dv.RowFilter = "IsReleased=0";
            }
            dataGridView1.DataSource = dv;
        }

        private void tbFilterDetainedLicenses_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (cbFilteration.SelectedIndex)
            {
                case 1:
                case 5:
                    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case 3:
                    if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case 4:
                    if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           int PersonID= clsApplicationsBusinesssLayer.Find(clsLicensesBusinessLayer.FindByLicenseID((int)dataGridView1.CurrentRow.Cells[1].Value).ApplicationID).ApplicantID;
            ShowDetailsForm showDetailsForm=new ShowDetailsForm(PersonID);
            showDetailsForm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDriverLicenseInfo driverLicenseInfo=new frmDriverLicenseInfo(clsLicensesBusinessLayer.FindByLicenseID((int)dataGridView1.CurrentRow.Cells[1].Value).ApplicationID);
            driverLicenseInfo.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            LicenseHistory licenseHistory=new LicenseHistory(clsApplicationsBusinesssLayer.Find(clsLicensesBusinessLayer.FindByLicenseID((int)dataGridView1.CurrentRow.Cells[1].Value).ApplicationID).ApplicantID);
            licenseHistory.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseLicense releaseLicense = new frmReleaseLicense((int)dataGridView1.CurrentRow.Cells[1].Value);
            releaseLicense.ShowDialog();
            _FillDGVWithDetainedLicenses();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (!clsDetainedLicenseBusineesLayer.IsLicensedetained((int)dataGridView1.CurrentRow.Cells[0].Value))
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = false;
            }
            else
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmReleaseLicense releaseLicense = new frmReleaseLicense(-1);
            releaseLicense.ShowDialog();
            _FillDGVWithDetainedLicenses();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense detainedLicenses = new frmDetainLicense(-1);
            detainedLicenses.ShowDialog();
            _FillDGVWithDetainedLicenses();
        }
    }
}
