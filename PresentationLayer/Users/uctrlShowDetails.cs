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
    public partial class uctrlShowDetails : UserControl
    {
        private int _UserID;
        public clsUsersBusinessLayer _usersBusinessLayer;
        public uctrlShowDetails()
        {
            InitializeComponent();
        }
        public void _LoadUserInfo(int UserId)
        {
            _UserID = UserId;
                _usersBusinessLayer = clsUsersBusinessLayer.Find(_UserID);
                lblUserID.Text = _UserID.ToString();
                lblUserName.Text = _usersBusinessLayer.UserName;
            lblIsActive.Text = (_usersBusinessLayer.IsActive == true) ? "Yes" : "No";
            ctrlPersonDetails1._LoadPersonInfo(_usersBusinessLayer.PersonID);

        }
        private void uctrlShowDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
