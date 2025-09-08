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
    public partial class frmShowDetails : Form
    {

        private int _UserID;
        public frmShowDetails(int UsserId)
        {
            InitializeComponent();
            _UserID = UsserId;
        }

        private void frmShowDetails_Load(object sender, EventArgs e)
        {
            uctrlShowDetails1._LoadUserInfo(_UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
