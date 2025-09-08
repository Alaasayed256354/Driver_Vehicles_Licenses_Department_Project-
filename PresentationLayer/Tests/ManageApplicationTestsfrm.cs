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
    public partial class ManageApplicationTestsfrm : Form
    {
        public ManageApplicationTestsfrm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private DataTable dataTable=new DataTable();
        private void _FillApplicationTestsDGV()
        {
            dataTable = clsApplicationTestsBusinessLayer.GetAllApplicationTests();
            dataGridView1.DataSource=dataTable;
            dataGridView1.Refresh();
        }
        private void ManageApplicationTestsfrm_Load(object sender, EventArgs e)
        {
            _FillApplicationTestsDGV();
            lblRecords.Text = dataGridView1.RowCount.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateApplicationTestfrm applicationTestfrm = new UpdateApplicationTestfrm((int)dataGridView1.CurrentRow.Cells[0].Value);
            applicationTestfrm.ShowDialog();
            _FillApplicationTestsDGV();
        }
    }
}
