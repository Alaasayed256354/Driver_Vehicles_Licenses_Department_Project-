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
    public partial class ManageApplicationTypesfrmcs : Form
    {
        public ManageApplicationTypesfrmcs()
        {
            InitializeComponent();
        }
       private DataTable dataTable=new DataTable();
        private void _FillApplicationTypeGV()
        {
            dataTable = clsApplicationTypesBusinessLayer.GetAllApplicationTypes();
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
        }
        private void ManageApplicationTypesfrmcs_Load(object sender, EventArgs e)
        {
            _FillApplicationTypeGV();
            lblRecords.Text = dataGridView1.RowCount.ToString();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateApplicationTypefrm updateApplication = new UpdateApplicationTypefrm((int)dataGridView1.CurrentRow.Cells[0].Value);
            updateApplication.ShowDialog();
            _FillApplicationTypeGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
