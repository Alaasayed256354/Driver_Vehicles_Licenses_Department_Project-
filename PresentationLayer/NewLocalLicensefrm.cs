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
    public partial class NewLocalLicensefrm : Form
    {
        public NewLocalLicensefrm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tbApplicationInfo;
        }

        private void tbFilterPeople_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (cbFilteration.SelectedItem)
            {
                case "PersonID":
                    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                        e.Handled = true;
                    break;
                case "NationalNo":
                    if(!char.IsLetter(e.KeyChar)&&!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar))
                        e.Handled = true;
                    break;
            }
        }
        private void _LoadDataOfPerson()
        {
            if (cbFilteration.SelectedItem.ToString()== "PersonID")
            {
                ctrlPersonDetails1._LoadPersonInfo(int.Parse(tbFilterPeople.Text));
            }
            else
            {
                ctrlPersonDetails1._LoadPersonInfo(tbFilterPeople.Text);
            }
        }
        private void btnSerach_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFilterPeople.Text) || string.IsNullOrWhiteSpace(tbFilterPeople.Text))
            {
                MessageBox.Show("There Is Nothing To Search For!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadDataOfPerson();
        }
        private void OnAddedSuccessfully(int PersonId)
        {
            cbFilteration.SelectedItem = "PersonID";
            tbFilterPeople.Text = PersonId.ToString();
            ctrlPersonDetails1._LoadPersonInfo(PersonId);
        }
        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            ModeForm modeForm = new ModeForm(-1);
            modeForm.OnAddedSuccessfully += OnAddedSuccessfully;
            modeForm.Show();
        }

        private void tbApplicationInfo_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
        private void _LoadApplicationInfo()
        {
            lblCreatedBy.Text = Global.CurrentUser.UserName;
            mtbDate.Text=DateTime.Now.ToShortDateString();
            lblFees.Text = "15";
        }
        private void NewLocalLicensefrm_Load(object sender, EventArgs e)
        {
            _LoadApplicationInfo();

        }
    }
}
