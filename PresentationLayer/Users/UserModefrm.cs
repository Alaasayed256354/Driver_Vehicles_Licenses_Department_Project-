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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Driver_VehiclesLicensesDepartmentProject
{
    public partial class UserModefrm : Form
    {
        private int UserId;
        enum enMode { AddNew,Update};
        enMode _Mode;
        clsUsersBusinessLayer usersBusinessLayer;
        public UserModefrm()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public UserModefrm(int userId)
        {
            InitializeComponent();
            UserId = userId;
                _Mode = enMode.Update;
        }
        private void _SetButtonNext()
        {
            if (_Mode == enMode.Update)
            {
                tabControl1.SelectedIndex = 1;
                return;
            }
            if (string.IsNullOrEmpty(mtbFilterText.Text.Trim())) { 
                MessageBox.Show("Select Person First ", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            switch (cbFilteration.SelectedItem)
            {
                case "National No":
                    if (ctrlPersonDetails1.IsEmpty == true)
                    {
                        MessageBox.Show("No Person With This " + cbFilteration.SelectedItem + $" ({mtbFilterText.Text})", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (clsUsersBusinessLayer.IsUserExist(int.Parse(ctrlPersonDetails1.lblPersonId.Text)))
                    {
                        MessageBox.Show("Selected Person Already Has A User,Choose Another One.", "Select Another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        tabControl1.SelectedTab = tbLoginInfo;//will go login info tab
                        //tabControl1.SelectedIndex = 1;//will go too the login info tab
                        //tabControl1.SelectedTab = tabControl1.TabPages[0];
                        btnSave.Enabled = true;
                    }
                    break;
                case "Person ID":
                    if (ctrlPersonDetails1.IsEmpty == true)
                    {
                        MessageBox.Show("No Person With This " + cbFilteration.SelectedItem + $" ({mtbFilterText.Text})", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (clsUsersBusinessLayer.IsUserExist(int.Parse(ctrlPersonDetails1.lblPersonId.Text)))
                    {
                        MessageBox.Show("Selected Person Already Has A User,Choose Another One.", "Select Another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        tabControl1.SelectedTab = tbLoginInfo;//will go login info tab
                        //tabControl1.SelectedIndex = 1;//will go too the login info tab
                        btnSave.Enabled = true;
                    }
                    break;
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            _SetButtonNext();
        }
        private void _SetMaskedTextBox()
        {
            if(cbFilteration.SelectedItem.ToString()=="National No")
            {
                mtbFilterText.Mask = null;
            }
            else
            {
                mtbFilterText.Mask = "000000000";
            }
        }
        private void cbFilteration_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SetMaskedTextBox();
        }
        private void _LoadUserData()
        {
            if (_Mode==enMode.AddNew)
            {
                btnSave.Enabled = false;
                lblMode.Text = "Add New User";
                this.Text = "Add New User";
                groupBox1.Enabled = true;
                usersBusinessLayer=new clsUsersBusinessLayer();
                return;
            }
            usersBusinessLayer = clsUsersBusinessLayer.Find(UserId);
            if (usersBusinessLayer == null)
            {
                MessageBox.Show("User With ID ("+UserId+") Not Found");
                this.Close();
                return;
            }
            btnSave.Enabled = true;
            lblMode.Text ="Update User";
            this.Text= "Update User";
            groupBox1.Enabled = false;
            lblUserID.Text =usersBusinessLayer.UserID.ToString();
            tbUserName.Text= usersBusinessLayer.UserName;
            tbPassword.Text = string.Empty;/* usersBusinessLayer.Password;*/
            tbConfirmPassword.Text = string.Empty;/*usersBusinessLayer.Password;*/
            cbIsActive.Checked = usersBusinessLayer.IsActive;
            ctrlPersonDetails1._LoadPersonInfo(usersBusinessLayer.PersonID);
        }
        private void _SetSearchButton()
        {
            switch (cbFilteration.SelectedItem)
            {
                case "National No":
                    if (clsPerson.IsExist(mtbFilterText.Text))
                    {
                        ctrlPersonDetails1._LoadPersonInfo(mtbFilterText.Text);
                    }
                    else
                    {
                        ctrlPersonDetails1.Clear();
                        MessageBox.Show("No Person With This " + cbFilteration.SelectedItem + $" ({mtbFilterText.Text})");
                    }
                    break;
                case "Person ID":
                    if (clsPerson.IsExist(int.Parse(mtbFilterText.Text)))
                    {
                        ctrlPersonDetails1._LoadPersonInfo(int.Parse(mtbFilterText.Text));
                    }
                    else
                    {
                        ctrlPersonDetails1.Clear();
                        MessageBox.Show("No Person With This " + cbFilteration.SelectedItem + $" ({mtbFilterText.Text})");
                    }
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            _SetSearchButton();
        }

        private void UserModefrm_Load(object sender, EventArgs e)
        {
           // btnSave.Enabled = false;
            ctrlPersonDetails1.Clear();
            _LoadUserData();
        }

      
        private void OnAddedSuccessfully(int PersonId)
        {
            cbFilteration.SelectedItem = "Person ID";
            mtbFilterText.Text = PersonId.ToString();
            ctrlPersonDetails1._LoadPersonInfo(PersonId);
        }
        private void AddPerson_Click_1(object sender, EventArgs e)
        {
            ModeForm modeForm = new ModeForm();
            modeForm.OnAddedSuccessfully +=OnAddedSuccessfully;
            modeForm.ShowDialog();
        }

        private void mtbFilterText_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mtbFilterText_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mtbFilterText.Text) || string.IsNullOrWhiteSpace(mtbFilterText.Text))
            {
                ctrlPersonDetails1.Clear();
            }
        }

        private void tbUserName_TextChanged(object sender, EventArgs e)
        {
           
        }
        private void _SetValidation(string Message,System.Windows.Forms.TextBox textBox,CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox?.Text))
            {
                e.Cancel = true;
                textBox.Focus();
                errorProvider1.SetError(textBox,Message+ " Is Manditory");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox, "");
            }
        }
        private void tbUserName_Validating(object sender, CancelEventArgs e)
        {
            _SetValidation("User Name", tbUserName, e);   
        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            _SetValidation("Password", tbPassword, e);
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            _SetValidation("Confirm", tbConfirmPassword, e);
            if (tbConfirmPassword.Text != tbPassword.Text)
            {
                e.Cancel = true;
                tbConfirmPassword.Focus();
                errorProvider1.SetError(tbConfirmPassword,"No Match");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbConfirmPassword, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbPassword.Text)||string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                MessageBox.Show("Password Cannot Be Empty","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(tbConfirmPassword.Text) || string.IsNullOrWhiteSpace(tbConfirmPassword.Text))
            {
                MessageBox.Show("Confirm Password", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tbConfirmPassword.Text != tbPassword.Text) {
                MessageBox.Show("Confirmed Password Isn't Match Password", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            usersBusinessLayer.UserName = tbUserName.Text;
            usersBusinessLayer.Password = tbConfirmPassword.Text;
            usersBusinessLayer.PersonID=int.Parse(ctrlPersonDetails1.lblPersonId.Text);
            usersBusinessLayer.IsActive=(cbIsActive.Checked);
            if (usersBusinessLayer.Save())
            {
                MessageBox.Show("User Saved Successfully");
                lblMode.Text = "Update User";
                this.Text = "Update User";
                lblUserID.Text = usersBusinessLayer.UserID.ToString();
            }
            else
            {
                MessageBox.Show("User Wasn't Save");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
