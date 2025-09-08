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
    public partial class UpdateApplicationTestfrm : Form
    {
        private int _AppTestID;
        clsApplicationTestsBusinessLayer applicationTestsBusinessLayer = new clsApplicationTestsBusinessLayer();
        public UpdateApplicationTestfrm(int appTestID)
        {
            InitializeComponent();
            _AppTestID = appTestID;
        }
        private void _LoadApplicationTestInfo()
        {
            applicationTestsBusinessLayer=clsApplicationTestsBusinessLayer.Find(_AppTestID);
            tbDescription.Text=applicationTestsBusinessLayer.TestTypeDescription;
            tbTitle.Text = applicationTestsBusinessLayer.TestTypeTitle;
            tbFees.Text = applicationTestsBusinessLayer.TestTypeFees.ToString();
            lblID.Text = applicationTestsBusinessLayer.AppTestID.ToString();
        }
        private void UpdateApplicationTestfrm_Load(object sender, EventArgs e)
        {
            _LoadApplicationTestInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            applicationTestsBusinessLayer.TestTypeFees =decimal.Parse(tbFees.Text);
            applicationTestsBusinessLayer.TestTypeTitle=tbTitle.Text;
            applicationTestsBusinessLayer.TestTypeDescription=tbDescription.Text;
            if (applicationTestsBusinessLayer.UpdateApplicationTest())
            {
                MessageBox.Show("Application Type Updated Successfully", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Application Type Wasn't Updated Successfully", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
