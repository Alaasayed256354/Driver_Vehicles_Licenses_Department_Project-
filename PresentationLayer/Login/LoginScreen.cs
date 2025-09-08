using LicenseBusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_VehiclesLicensesDepartmentProject
{
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
            rememberHandler = new RememberHandler();
        }
        private RememberHandler rememberHandler;
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {
            //var (UserName, password) = rememberHandler.CheckRemeberedUser();
            //if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(password))
            //{
            //    tbPassword.Text = password;
            //    tbUserName.Text = UserName;
            //}
            //else
            //{
            //    tbPassword.Text = "";
            //    tbUserName.Text = "";
            //}
        }

        private void cbRememberMe_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbPassword.Text)|| string.IsNullOrEmpty(tbUserName.Text))
            {
                MessageBox.Show("Password/UserName Cannot Be Empty", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string userName = tbUserName.Text;
            //a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3
            string Password = clsHashingPassword.HashingPassword(tbPassword.Text.Trim());
            bool rememberMe = cbRememberMe.Checked;
          clsUsersBusinessLayer user = clsUsersBusinessLayer.Find(userName,Password);
            if (user != null) {
                if (user.IsActive)
                {
                    Global.CurrentUser = user;
                        rememberHandler._SaveUserCredentials(userName, Password, rememberMe);
                    this.Hide();
                    Mainscreen mainscreen = new Mainscreen(this);
                    mainscreen.ShowDialog();
                }
                else
                {
                    MessageBox.Show("User Is Not Active");
                }
            }
            else
            {
                MessageBox.Show("User Name Or Password Is Wrong","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void tbUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbUserName.Text))
            {
                //e.Cancel = true;
                tbUserName.Focus();
                errorProvider1.SetError(tbUserName, "User Name Is Required");
            }
            else
            {
               // e.Cancel = false;
                errorProvider1.SetError(tbUserName, "");
            }
        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbPassword.Text))
            {
                //e.Cancel = true;
                tbPassword.Focus();
                errorProvider1.SetError(tbPassword, "Password Is Required");
            }
            else
            {
               // e.Cancel = false;
                errorProvider1.SetError(tbPassword, "");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void LoginScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
