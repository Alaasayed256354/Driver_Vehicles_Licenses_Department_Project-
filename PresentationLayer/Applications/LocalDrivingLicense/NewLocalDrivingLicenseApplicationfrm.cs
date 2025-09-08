using LicenseBusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Driver_VehiclesLicensesDepartmentProject
{
    public partial class NewLocalDrivingLicenseApplicationfrm : Form
    {
        enum enMode {AddNew,Update };
        private enMode _Mode;
        private int _LDLAppID;
        private clsLocalDrivingLicenseApplicationBusinessLayer localDrivingLicenseApplicationBusinessLayer = new clsLocalDrivingLicenseApplicationBusinessLayer();
        public NewLocalDrivingLicenseApplicationfrm()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public NewLocalDrivingLicenseApplicationfrm(int LDLAPPID)
        {
            InitializeComponent();
            _LDLAppID = LDLAPPID;
            _Mode=enMode.Update;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void tbPersonalInfo_Click(object sender, EventArgs e)
        {

        }
        private void _LoadComboBoxWithLicenseClasses()
        {
            DataTable dataTable = clsLicenseClassesBusinessLayer.GetAllLicenseClasses();
            foreach (DataRow row in dataTable.Rows) {
                cbLicenseClasses.Items.Add(row["ClassName"]);
            }
        }
        private void NewLocalDrivingLicenseApplicationfrm_Load(object sender, EventArgs e)
        {
            _LoadComboBoxWithLicenseClasses();
            _LoadDataOfPerson();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //tcLocaldrivingLicense.SelectedTab = tbApplicationInfo;
            if (ctrlPersonDetails1.IsEmpty)
            {
                btnSave.Enabled = false;
                tbApplicationInfo.Enabled = false;
               MessageBox.Show("Select Person First", "Not Allowed!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                btnSave.Enabled =true;
                tbApplicationInfo.Enabled = true;
                tbApplicationInfo.Enabled =true;
                tcLocaldrivingLicense.SelectedIndex = 1;
            }
        }

        private void tbFilterPeople_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (cbFilteration.SelectedItem)
            {
                case "PersonID":
                    //if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                        e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
                    break;
                case "NationalNo":
                    if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                        e.Handled = true;
                    break;
            }
        }
        private void _LoadDataOfPerson()
        {
            if (_Mode == enMode.AddNew)
            {
                gbFilter.Enabled = true;
                this.Text = "Add New Local Driving License Apllication";
                lblLDLAppID.Text = "Add New Local Driving License Apllication";
                lblFees.Text =clsApplicationTypesBusinessLayer.Find((int)clsApplicationsBusinesssLayer.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
                cbLicenseClasses.SelectedIndex =2;
                tbApplicationInfo.Enabled =false;
                btnSave.Enabled =false; 
                lblCreatedBy.Text=Global.CurrentUser.UserName;
                lblapplicationDate.Text=DateTime.Now.ToShortDateString();
                localDrivingLicenseApplicationBusinessLayer=new clsLocalDrivingLicenseApplicationBusinessLayer();
            }
            else
            {
                localDrivingLicenseApplicationBusinessLayer=clsLocalDrivingLicenseApplicationBusinessLayer.FindByLDLAID(_LDLAppID);
                lblDLAplicatIonID.Text=localDrivingLicenseApplicationBusinessLayer.LDLAppID.ToString();
                lblapplicationDate.Text=localDrivingLicenseApplicationBusinessLayer.ApplicationDate.ToShortDateString();
                lblFees.Text=localDrivingLicenseApplicationBusinessLayer.PaidFees.ToString();
                lblCreatedBy.Text=localDrivingLicenseApplicationBusinessLayer.UserInfo.UserName;
                cbLicenseClasses.SelectedItem=localDrivingLicenseApplicationBusinessLayer.LicenseClassesBusinessLayer.ClassName;
                gbFilter.Enabled=false;
                btnSave.Enabled=true;
                tbApplicationInfo.Enabled=true;
                this.Text = "Update Local Driving License Apllication";
                lblLDLAppID.Text = "Update Local Driving License Apllication";
                ctrlPersonDetails1._LoadPersonInfo(localDrivingLicenseApplicationBusinessLayer.ApplicantID);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFilterPeople.Text) || string.IsNullOrWhiteSpace(tbFilterPeople.Text)||cbFilteration.SelectedIndex==-1)
            {
                MessageBox.Show("There Is Nothing To Search For!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbFilteration.SelectedIndex==1)
            {
                ctrlPersonDetails1._LoadPersonInfo(int.Parse(tbFilterPeople.Text));
            }
              else
            {
                ctrlPersonDetails1._LoadPersonInfo(tbFilterPeople.Text);
            }
        }

        private void OnAddedSuccessfully(int PersonId)
        {
            cbFilteration.SelectedItem = "Person ID";
            tbFilterPeople.Text = PersonId.ToString();
            ctrlPersonDetails1._LoadPersonInfo(PersonId);
        }

        private void AddPerson_Click(object sender, EventArgs e)
        {
            ModeForm modeForm = new ModeForm();
            modeForm.OnAddedSuccessfully += OnAddedSuccessfully;
            modeForm.Show();
        }
        private int _GetLicenseClassID()
        {
            switch (cbLicenseClasses.SelectedIndex)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 3;
                case 3: 
                    return 4;
                case 4: 
                    return 5;
                case 5: 
                    return 6;
                case 6:
                    return 7;
                default:
                    return -1;
            }
        }
        //private bool ManageRepeatedApplications()
        //{
        //    DataTable dataTable=clsLocalDrivingLicenseApplicationBusinessLayer.GetLicensesClassesOfUserByID(int.Parse(ctrlPersonDetails1.lblPersonId.Text));
        //   foreach(DataRow row in dataTable.Rows)
        //    {
        //        if (row["ClassName"].ToString() == cbLicenseClasses.SelectedItem.ToString() &&int.Parse(row["Applicationstatus"].ToString())==1)
        //        {
        //            ApplicationIDForApplicationUserWannaRepeat = int.Parse(row["ApplicationID"].ToString());
        //            return false;
        //        }
        //    }
        //   return true;
        //}
        private void btnSave_Click(object sender, EventArgs e)
        {
            int IsHasActiveApp = clsLocalDrivingLicenseApplicationBusinessLayer.IsHasAnNewActiveApplication(int.Parse(ctrlPersonDetails1.lblPersonId.Text), cbLicenseClasses.SelectedItem.ToString());
            int IsHasCompleteApp = clsLocalDrivingLicenseApplicationBusinessLayer.IsHasACompletedApplication(int.Parse(ctrlPersonDetails1.lblPersonId.Text), cbLicenseClasses.SelectedItem.ToString());
            if (clsLicensesBusinessLayer.IsHasAnActiveLicenseForAGivenClassName(cbLicenseClasses.SelectedItem.ToString(),ctrlPersonDetails1.SelectedPerson.personID))
            {
                MessageBox.Show("The Selected Person Already Has An License For The Selected License With ", "Not Allowed!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (IsHasActiveApp!=-1)
            {
                MessageBox.Show("Choose Another Licence Claaa The Selected Person Has An Active Application For The Selected Class With Id="+IsHasActiveApp,"Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (IsHasCompleteApp != -1) {
                MessageBox.Show("Choose Another Licence Claaa The Selected Person Has A Completed Application For The Selected Class With App Id=" + IsHasCompleteApp, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            localDrivingLicenseApplicationBusinessLayer.PaidFees =decimal.Parse(lblFees.Text);
            localDrivingLicenseApplicationBusinessLayer.ApplicationDate=DateTime.Now;
            localDrivingLicenseApplicationBusinessLayer.LastStatusDate=DateTime.Now;
            localDrivingLicenseApplicationBusinessLayer.ApplicationStatus = clsApplicationsBusinesssLayer.enApplicationStatus.New;
            localDrivingLicenseApplicationBusinessLayer.ApplicantID =int.Parse(ctrlPersonDetails1.lblPersonId.Text);
            localDrivingLicenseApplicationBusinessLayer.CreatedByUserID = Global.CurrentUser.UserID;
            localDrivingLicenseApplicationBusinessLayer.ApplicationTypeID = 1;
            localDrivingLicenseApplicationBusinessLayer.LicenseClassID = _GetLicenseClassID();
            if (localDrivingLicenseApplicationBusinessLayer.Save())
            {
                lblDLAplicatIonID.Text = localDrivingLicenseApplicationBusinessLayer.LDLAppID.ToString();
                MessageBox.Show("Local Driving License Saved Successfully", "Inform!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Text = "Update Local Driving License Apllication";
                lblLDLAppID.Text ="Update Local Driving License Apllication";
            }
            else
            {
                MessageBox.Show("Local Driving License Wasn't Added Successfully", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbApplicationInfo_Click(object sender, EventArgs e)
        {
            if (ctrlPersonDetails1.IsEmpty)
            {
                MessageBox.Show("Select Person First", "Not Allowed!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                tcLocaldrivingLicense.SelectedIndex = 1;
            }
        }

        private void tbFilterPeople_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFilterPeople.Text) || string.IsNullOrWhiteSpace(tbFilterPeople.Text))
            {
                ctrlPersonDetails1.Clear();
            }
        }

        private void mtbApplicationDate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void NewLocalDrivingLicenseApplicationfrm_Activated(object sender, EventArgs e)
        {
            tbFilterPeople.Focus();
        }
    }
    }
