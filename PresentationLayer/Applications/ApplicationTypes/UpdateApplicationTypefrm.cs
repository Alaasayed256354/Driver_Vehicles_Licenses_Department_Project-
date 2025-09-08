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
    public partial class UpdateApplicationTypefrm : Form
    {
        private clsApplicationTypesBusinessLayer applicationBusinessLayer=new clsApplicationTypesBusinessLayer();
        private int _ApplicationTypeID;
        public UpdateApplicationTypefrm(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)//e contains details about key user pressed
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))//iscontrol() it checks if keychar is tab or enter or backspace or ruler 
            {
                e.Handled = true;//it makes if key pressed not Letter or control(tab,Enter,BackSpace)will prevent this key to be Proccessed or appear in the textbox
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(tbTitle.Text) || string.IsNullOrWhiteSpace(tbTitle.Text))
            {
                e.Cancel = true;
                tbTitle.Focus();
                errorProvider1.SetError(tbTitle, "Invalid Input");
            }
            else
            {
                errorProvider1.SetError(tbTitle, "");
                e.Cancel = false;
            }
        }
        private void _LoadApplicationTypeInfo()
        {
            applicationBusinessLayer = clsApplicationTypesBusinessLayer.Find(_ApplicationTypeID);
            //if (applicationBusinessLayer != null)
            //{
                tbTitle.Text = applicationBusinessLayer.ApplicationTypeTitle;
                tbFees.Text = applicationBusinessLayer.ApplicationFees.ToString();
                lblID.Text = applicationBusinessLayer.ApplicationTypeID.ToString();
            //}
            //else
            //{
            //    MessageBox.Show("donkey");
            //}
        }
        private void UpdateApplicationTypefrm_Load(object sender, EventArgs e)
        {
            _LoadApplicationTypeInfo();
        }

        private void tbFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbFees.Text) || string.IsNullOrWhiteSpace(tbFees.Text))
            {
                e.Cancel = true;
                tbFees.Focus();
                errorProvider1.SetError(tbFees, "Invalid Input");
            }
            else
            {
                errorProvider1.SetError(tbFees, "");
                e.Cancel = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            applicationBusinessLayer.ApplicationFees =decimal.Parse(tbFees.Text);
            applicationBusinessLayer.ApplicationTypeTitle = tbTitle.Text.Trim();
            if (applicationBusinessLayer.UpdateApplicationType())
            {
                MessageBox.Show("Application Type Updated Successfully", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Application Type Wasn't Updated Successfully", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
