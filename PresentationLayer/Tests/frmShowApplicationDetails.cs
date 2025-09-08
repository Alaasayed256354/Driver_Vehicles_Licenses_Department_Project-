using Driver_VehiclesLicensesDepartmentProject;
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
    public partial class frmShowApplicationDetails : Form
    {
        private int _LDLAID;
        public frmShowApplicationDetails(int LDLAID)
        {
            InitializeComponent();
            _LDLAID = LDLAID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void frmShowApplicationDetails_Load(object sender, EventArgs e)
        {
            uctrlDrivingLicenseApplicationInfo1._LoadDrivingLicenseApplicationInfo(_LDLAID);
        }
    }
}
