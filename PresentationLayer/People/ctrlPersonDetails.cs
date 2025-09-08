using LicenseBusinessLogicLayer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Driver_VehiclesLicensesDepartmentProject
{
    public partial class ctrlPersonDetails : UserControl
    {
        public ctrlPersonDetails()
        {
            InitializeComponent();
        }

        private int _PeronId;
        private string _NationalNo;
        private clsPerson _Person;
        private clsCountryBusinessLayer _CountryBusinessLayer;
        private bool isEmpty = true;//If You Need To Access SomeThing From Any Class It Is Better to Access It Using Getter
        public clsPerson SelectedPerson
        {
            get { return _Person; }
        }
        public bool IsEmpty
        {
            get { return isEmpty; }
        }
        public void _LoadPersonInfo(int PersonId)
        {
            _PeronId = PersonId;
            _Person = clsPerson.Find(PersonId);
            if (_Person != null)
            {
                isEmpty = false;
                _CountryBusinessLayer = clsCountryBusinessLayer.Find(_Person.NationalityCountryID);
                lblPersonId.Text = PersonId.ToString();
                string PersonName = (_Person.FirstName ?? "") + " " + (_Person.Secondname ?? "") + " " + (_Person.ThirdName ?? "") + " " + (_Person.LastName ?? "");
                lblPersonName.Text = PersonName;
                lblNationalNo.Text = _Person.NationalNo;
                if (_Person.Gendor == 0)
                {
                    lblGendor.Text = "Male";
                }
                else
                {
                    lblGendor.Text = "Female";
                }
                lblEmail.Text = _Person.Email;
                lblAddress.Text = _Person.Address;
                lblDatteOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
                lblPhone.Text = _Person.Phone;
                if (_CountryBusinessLayer != null)
                {
                    lblCountry.Text = _CountryBusinessLayer.CountryName;
                }
                if (_Person.ImagePath != "")
                {
                    pbimage.ImageLocation=(_Person.ImagePath);
                    pbimage.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    pbimage.Visible = false;
                }
            }
            else
            {
                Clear();
                MessageBox.Show("Person Not Found");
            }
        }
        public void Clear()
        {
            isEmpty = true;
            _PeronId = 0;
            lblPersonName.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblPersonId.Text = "[???]";
            lblGendor.Text = "[???]";
            lblEmail.Text = "[???]";
            lblAddress.Text = "[???]";
            lblDatteOfBirth.Text = "[???]";
            lblDatteOfBirth.Text = "[???]";
            lblPhone.Text = "[???]";
            lblCountry.Text = "[???]";
            pbimage.ImageLocation = null;
        }
        public void _LoadPersonInfo(string NationalNo)
        {
            _NationalNo = NationalNo;
            _Person = clsPerson.Find(NationalNo);
            if (_Person != null)
            {
                isEmpty = false;
                _CountryBusinessLayer = clsCountryBusinessLayer.Find(_Person.NationalityCountryID);
                lblPersonId.Text = _Person.personID.ToString();
                string PersonName = (_Person.FirstName ?? "") + " " + (_Person.Secondname ?? "") + " " + (_Person.ThirdName ?? "") + " " + (_Person.LastName ?? "");
                lblPersonName.Text = PersonName;
                lblNationalNo.Text = _NationalNo;
                if (_Person.Gendor == 0)
                {
                    lblGendor.Text = "Male";
                }
                else
                {
                    lblGendor.Text = "Female";
                }
                lblEmail.Text = _Person.Email;
                lblAddress.Text = _Person.Address;
                lblDatteOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
                lblPhone.Text = _Person.Phone;
                if (_CountryBusinessLayer != null)
                {
                    lblCountry.Text = _CountryBusinessLayer.CountryName;
                }
                if (_Person.ImagePath != "")
                {
                    pbimage.ImageLocation=(_Person.ImagePath);
                    pbimage.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    pbimage.ImageLocation = (_Person.Gendor == 0)?Properties.Resources.patient_boy.ToString():Properties.Resources.person_girl__3_.ToString();
                    //pbimage.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Person Not Found");
            }
        }
        private void ctrlPersonDetails_Load(object sender, EventArgs e)
        {

        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ModeForm modeForm = new ModeForm(_PeronId);
            modeForm.ShowDialog();
            _LoadPersonInfo(_PeronId);
        }

        private void lblPersonId_Click(object sender, EventArgs e)
        {

        }

        private void pbimage_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
