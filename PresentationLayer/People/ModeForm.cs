using Driver_VehiclesLicensesDepartmentProject.Util;
using LicenseBusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_VehiclesLicensesDepartmentProject
{
    public partial class ModeForm : Form
    {
        private clsPerson _Person;
        private int _PersonID;
        public event Action<int> OnAddedSuccessfully;
        private enum enMode { AddNew, Update };
        private enMode _Mode = enMode.AddNew;
        DataTable dataTableCountry;
        public ModeForm()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public ModeForm(int PersonId)
        {
            InitializeComponent();
            _PersonID =PersonId;
            _Mode = enMode.Update;
        }



      
        private void _FillComboBoxWithCountries()
        {
          dataTableCountry= clsCountryBusinessLayer.GetAllCountries();
            foreach (DataRow row in dataTableCountry.Rows)
            {
                cbCountries.Items.Add(row["CountryName"]);
            }
        }
       private void _ResetDdefaultValues()
        {
            _FillComboBoxWithCountries();
            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblMode.Text = "Update Person";
            }
            llRemoveImage.Visible = false;
            tbfirstName.Select();
            cbCountries.SelectedItem = "Egypt";
            dtbDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtbDateOfBirth.Value = dtbDateOfBirth.MaxDate;
            dtbDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            rbMale.Checked=true;
            pbImage.Image = Properties.Resources.patient_boy;
        }
        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("Person With ID=" + _PersonID + " Wasn't Found");
                this.Close();
                return;
            }

                lblPersonID.Text = _Person.personID.ToString();
                tbfirstName.Text = _Person.FirstName;
                tbLastName.Text = _Person.LastName;
                tbsecondName.Text = _Person.Secondname;
                tbThirdName.Text = _Person.ThirdName;
                tbPhone.Text = _Person.Phone;
                tbEmail.Text = _Person.Email;
                tbAddress.Text = _Person.Address;
                tbNationalNo.Text = _Person.NationalNo;
                dtbDateOfBirth.Value = _Person.DateOfBirth;
                cbCountries.SelectedItem = _Person.NationalityCountryID;
                if (_Person.Gendor == 0)
                {
                    rbMale.Focus();
                    rbMale.Checked = true;
                }
                else
                {
                    rbFemale.Checked = true;
                }
            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                using (FileStream fs = new FileStream(_Person.ImagePath, FileMode.Open, FileAccess.Read))
                {
                    pbImage.Image = Image.FromStream(fs); // Load the image without locking the file
                }
                //pbImage.Image = Image.FromFile(_Person.ImagePath);
                llRemoveImage.Visible = true;
            }
            else
            {
                pbImage.Image = null;
                llRemoveImage.Visible = false;
            }

        }
       private void SetValidation(string Pattern,TextBox textBox,string errormessage,CancelEventArgs e)
        {
            if (!Regex.IsMatch(textBox.Text, Pattern) || string.IsNullOrWhiteSpace(textBox.Text))
            {
                e.Cancel = true;//Prevent user from leaving the current control
                textBox.Focus();
                errorProvider1.SetError(textBox, "Please Enter A Valid" + errormessage);
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox, "");//error provider will hide because the message is empty
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void SetBictureBoxImage(string ImageName)
        {
            pbImage.Image = Properties.Resources.patient_boy;/* Image.FromFile(ImagePath);*/
            // pbImage.Image = Properties.Resources.patient_boy;
            pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void ModeForm_Load(object sender, EventArgs e)
        {
            _ResetDdefaultValues();
            if(_Mode==enMode.Update)
            _LoadData();
        }

     

       private void lblMode_Click(object sender, EventArgs e)
        {

        }

        private void rbMale_CheckedChanged_1(object sender, EventArgs e)
        {
            //if (pbImage.ImageLocation == null)
            //{
                SetBictureBoxImage(@"patient_boy");
            //}
        }

        private void rbFemale_CheckedChanged_1(object sender, EventArgs e)
        {
            //if (pbImage.ImageLocation == null)
            //{
                SetBictureBoxImage(@"patient_girl");
            //}
        }
     
        private int CountEmptyFields()
        {
            int count = 0;
            if (string.IsNullOrEmpty(tbAddress.Text))
            {
                count++;
            }
            if (string.IsNullOrEmpty(tbfirstName.Text))
            {
                count++;
            }
             if (string.IsNullOrEmpty(tbsecondName.Text))
            {
                count++;
            }
             if (string.IsNullOrEmpty(tbThirdName.Text))
            {
                count++;
            }
             if (string.IsNullOrEmpty(tbLastName.Text))
            {
                count++;
            }
             if (string.IsNullOrEmpty(tbPhone.Text))
            {
                count++;
            }
             if (string.IsNullOrEmpty(tbNationalNo.Text))
            {
                count++;
            }
           return count;
        }
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbImage_Click(object sender, EventArgs e)
        {

        }

        private void tbFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImage.Visible = true;
            openFileDialog1.Title = "Select An Image";
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK) { 
             string ImageLocation=openFileDialog1.FileName;
                pbImage.ImageLocation = ImageLocation;
                //pictureBox1.Image = Properties.Resources.phone__1_;
                //pictureBox1.Image = Image.FromFile("FilePath");This TYoo will Prevent You From Deleting The Image
                //pictureBox1.Load("ImagePath");//it is not prefer to use load becaues it will prevent you to delete the image
               using (FileStream fs = new FileStream(ImageLocation, FileMode.Open, FileAccess.Read))
{
                    pbImage.Image = Image.FromStream(fs); // Load the image without locking the file
                }
                //pbImage.Image=Image.FromFile(ImageLocation);
                llRemoveImage.Visible = true;
            }
            else
            {
                pbImage.ImageLocation = null;
                llRemoveImage.Visible = false;
            }

        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImage.ImageLocation = null;
            pbImage.Image=(rbFemale.Checked)?Properties.Resources.person_girl__3_:Properties.Resources.patient_boy;
            llRemoveImage.Visible=false;
        }

        private void tbfirstName_Validating(object sender, CancelEventArgs e)
        {
            SetValidation(@"^[a-zA-Z\s]+$", tbfirstName, " Name", e);
            //^ RePresent the start of the string and $ RePresent The End Of string
            //string Pattern = @"^[a-zA-Z\s]+$";
            //if (!Regex.IsMatch(tbfirstName.Text, Pattern)||string.IsNullOrWhiteSpace(tbfirstName.Text)) {
            //       //regex used to ensure correct format
            //    e.Cancel = true;//prevent user from leaving current control
            //    errorProvider1.SetError(tbfirstName, "Please Enter A Valid Name(letters and spaces only)");
            //}
            //else
            //{
            //    e.Cancel = false;//can leave the current control
            //    errorProvider1.SetError(tbfirstName, "");//the error provider will hide because it's message is empty
            //}
        }

        private void tbsecondName_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbsecondName_Validating(object sender, CancelEventArgs e)
        {
            SetValidation(@"^[a-zA-Z\s]+$",tbsecondName, " Name", e);
        }

        private void tbThirdName_Validating(object sender, CancelEventArgs e)
        {
            SetValidation(@"^[a-zA-Z\s]+$", tbThirdName, " Name", e);
        }

        private void tbLastName_Validating(object sender, CancelEventArgs e)
        {
            SetValidation(@"^[a-zA-Z\s]+$", tbLastName, " Name", e);
        }

        private void tbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            SetValidation(@"^[a-zA-Z0-9\s]+$", tbNationalNo, " National No", e);
            if (clsPerson.IsExist(tbNationalNo.Text))
            {
                e.Cancel = true;
                tbNationalNo.Focus();
                errorProvider1.SetError(tbNationalNo, "National Number Is Used For another Person");
            }
        }

        private void tbPhone_Validating(object sender, CancelEventArgs e)
        {
            SetValidation(@"^[0-9\s]+$", tbPhone, " Phone Number", e);
        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbEmail.Text))
            {
                SetValidation(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", tbEmail, " Email", e);
            }
        }

        private void tbAddress_Validating(object sender, CancelEventArgs e)
        {
            
                SetValidation(@"^[a-zA-Z0-9\s,.-]+$", tbAddress, " Address", e);
        }
        private bool _HandlePersonImage()
        {
            if (_Person.ImagePath != pbImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException ex) { throw new Exception(ex.Message); }
                }
                if (pbImage.ImageLocation != null)
                {
                    string SourcePath = pbImage.ImageLocation.ToString();
                    if (clsUtil.CopyImageToProjectImageFolder(ref SourcePath))
                    {
                        pbImage.ImageLocation = SourcePath;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }
        //private bool _HandlePersonImage()
        //{
        //    try
        //    {
        //        // Check if the image path has changed
        //        if (_Person.ImagePath != pbImage.ImageLocation)
        //        {
        //            // Delete the old image file if it exists
        //            if (!string.IsNullOrEmpty(_Person.ImagePath) && File.Exists(_Person.ImagePath))
        //            {
        //                File.Delete(_Person.ImagePath);
        //            }

        //            // Copy the new image if one is loaded in pbImage
        //            if (!string.IsNullOrEmpty(pbImage.ImageLocation))
        //            {
        //                string sourcePath = pbImage.ImageLocation;
        //                if (clsUtil.CopyImageToProjectImageFolder(ref sourcePath))
        //                {
        //                    // Update PictureBox with the new path
        //                    pbImage.ImageLocation = sourcePath;
        //                    return true;
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    return false;
        //                }
        //            }
        //        }
        //        return true; // No changes needed
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error Handling Image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!_HandlePersonImage()) 
                return;
            int EmptyFields = CountEmptyFields();
            if (EmptyFields == 0 && (rbFemale.Checked == true || rbMale.Checked == true))
            {
                _Person.FirstName = tbfirstName.Text.ToString();
                _Person.LastName = tbLastName.Text;
                _Person.Email = tbEmail.Text;
                _Person.Phone = tbPhone.Text;
                _Person.Secondname = tbsecondName.Text;
                _Person.ThirdName = tbThirdName.Text;
                _Person.Address = tbAddress.Text;
                _Person.DateOfBirth = dtbDateOfBirth.Value;
                _Person.NationalNo = tbNationalNo.Text;
                _Person.NationalityCountryID = cbCountries.SelectedIndex + 1;
                if (rbMale.Checked)
                {
                    _Person.Gendor = 0;
                }
                else
                {
                    _Person.Gendor = 1;
                }
                _Person.ImagePath = pbImage.ImageLocation ?? "";
                //if (pbImage.ImageLocation != null)
                //{
                //    _Person.ImagePath = pbImage.ImageLocation;
                //}
                //else
                //{
                //    _Person.ImagePath = "";
                //}
                if (_Person.Save())
                {
                    OnAddedSuccessfully?.Invoke(_Person.personID);
                    lblPersonID.Text=_Person.personID.ToString();
                    MessageBox.Show("Person Saved Successfully");
                }
                else
                {
                    MessageBox.Show("Person Wasn't Save");
                }
            }
            else
            {
                MessageBox.Show("There Is/Are "+EmptyFields+"Empty Fields", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void rbMale_Click(object sender, EventArgs e)
        {
        }

        private void rbFemale_Click(object sender, EventArgs e)
        {
        }
    }
}
