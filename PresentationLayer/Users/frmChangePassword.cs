using LicenseBusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_VehiclesLicensesDepartmentProject
{
    public partial class frmChangePassword : Form
    {
        private int _UserID;
        private string UserPassword;
        //private clsUsersBusinessLayer _usersBusinessLayer;
        public frmChangePassword(int UsserId)
        {
            InitializeComponent();
            _UserID=UsserId;
        }
       
        private void ctrlPersonDetails1_Load(object sender, EventArgs e)
        {
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            uctrlShowDetails1._LoadUserInfo(_UserID);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valide!", "Validiation Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //if (string.IsNullOrEmpty(tbCurrentPassword.Text) || string.IsNullOrWhiteSpace(tbCurrentPassword.Text))
            //{
            //    MessageBox.Show("Currrent Password Cannot Be Empty", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if (string.IsNullOrEmpty(tbNewPassword.Text.Trim()))
            //{
            //    MessageBox.Show("New Password Cannot Be Empty", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if (string.IsNullOrEmpty(tbConfirmPassword.Text) || string.IsNullOrWhiteSpace(tbConfirmPassword.Text))
            //{
            //    MessageBox.Show("Confirm Password", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            uctrlShowDetails1._usersBusinessLayer.Password = tbNewPassword.Text;
            if (uctrlShowDetails1._usersBusinessLayer.UpdatePassword())
            {
                MessageBox.Show("Password Updated Successfully");
            }
            else
            {
                MessageBox.Show("Password Didn't Change");
            }
        }
        
        private void maskedTexmtbCurrentPasswordtBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                tbCurrentPassword.Focus();
                errorProvider1.SetError(tbCurrentPassword, "Current Password Cannot Be empty");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbCurrentPassword, null);
            }
            UserPassword = clsUsersBusinessLayer.GetUserPassword(_UserID);
            if(clsHashingPassword.HashingPassword(tbCurrentPassword.Text) != UserPassword)
            {
                e.Cancel = true;
                tbCurrentPassword.Focus();
                errorProvider1.SetError(tbCurrentPassword,"This Password Doesn't Match Your Current Password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbCurrentPassword, null);
            }
        }

        private void tbNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                tbNewPassword.Focus();
                errorProvider1.SetError(tbNewPassword, "Current Password Cannot Be empty");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbNewPassword, null);
            }
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (tbConfirmPassword.Text != tbNewPassword.Text)
            {
                e.Cancel = true;
                //button1.Enabled=false;
                tbCurrentPassword.Focus();
                errorProvider1.SetError(tbConfirmPassword, "New Password doesn't Match This Password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbConfirmPassword,"");
            }
        }

        private void tbConfirmPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void uctrlShowDetails1_Load(object sender, EventArgs e)
        {

        }

        private void tbConfirmPassword_MouseLeave(object sender, EventArgs e)
        {
            //if (tbConfirmPassword.Text != tbNewPassword.Text)
            //{
            //    tbCurrentPassword.Focus();
            //    errorProvider1.SetError(tbConfirmPassword, "New Password doesn't Match This Password");
            //}
            //else
            //{
            //    button1.Enabled=true;
            //    errorProvider1.SetError(tbConfirmPassword, "");
            //}
        }
    }
}
