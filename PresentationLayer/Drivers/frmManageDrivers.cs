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
    public partial class frmManageDrivers : Form
    {
        private DataView dv;
        private DataTable dt;
        public frmManageDrivers()
        {
            InitializeComponent();
        }
        private void _FillDataGridVieWithDrivers()
        {
            dt = clsDriversBusinesslayer.GetAllDrivers();
            //dt.DefaultView.ToTable
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
        }
        private void _FilterDrivers()
        {
            if (string.IsNullOrEmpty(tbFilterDrivers.Text) || cbFilteration.SelectedIndex == -1 || cbFilteration.SelectedIndex == 0)
            {
                dataGridView1.DataSource = dt;
                return;
            }
            dv = new DataView(dt);
            string FilterBy = cbFilteration.SelectedItem.ToString();
            string FilterText = tbFilterDrivers.Text;
            string Filter = "";
            switch (FilterBy)
            {
                case "Driver ID":
                    Filter = $"DriverID={FilterText}";
                    break;
                case "Person ID":
                    Filter = $"PersonID={FilterText}";
                    break;
                case "National No":
                    Filter = $"NationalNo LIKE '{FilterText}%'";
                    break;
                case "Full Name":
                    Filter = $"FullName LIKE '{FilterText}%'";
                    break;
            }
            dv.RowFilter = Filter;
            dataGridView1.DataSource = dv;
        }
        private void frmDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            _FillDataGridVieWithDrivers();
            lbldriversRecords.Text = dataGridView1.RowCount.ToString();
            cbFilteration.SelectedItem = "None";
            tbFilterDrivers.Enabled = false;
        }

        private void cbFilteration_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilteration.SelectedIndex == 0)
            {
                dataGridView1.DataSource=dt;
                tbFilterDrivers.Text=string.Empty;
                tbFilterDrivers.Enabled = false;
            }
            else
            {
                tbFilterDrivers.Enabled = true;
            }
        }

        private void tbFilterDrivers_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (cbFilteration.SelectedItem.ToString())
            {
                case "Driver ID":
                    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case "Person ID":
                    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case "National No":
                    if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case "Full Name":
                    if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }

        private void tbFilterDrivers_TextChanged(object sender, EventArgs e)
        {
            _FilterDrivers();
            lbldriversRecords.Text = dataGridView1.RowCount.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDetailsForm PersonDetailsForm = new ShowDetailsForm((int)dataGridView1.CurrentRow.Cells[1].Value);
            PersonDetailsForm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LicenseHistory licenseHistory = new LicenseHistory((int)dataGridView1.CurrentRow.Cells[1].Value);
            licenseHistory.ShowDialog();
        }
    }
}
