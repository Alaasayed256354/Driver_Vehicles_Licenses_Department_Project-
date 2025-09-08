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
    public partial class ShowDetailsForm : Form
    {
       // private int _PersonID;
       // ctrlPersonDetails ctrlPersonDetail = new ctrlPersonDetails();
        public ShowDetailsForm(int personId)
        {
            InitializeComponent();
          // _PersonID = personId;
            ctrlPersonDetails1._LoadPersonInfo(personId);
        }

        private void ShowDetailsForm_Load(object sender, EventArgs e)
        {
            //ctrlPersonDetail._LoadPersonInfo(_PersonID);
        }

        private void ctrlPersonDetails1_Load(object sender, EventArgs e)
        {

        }
    }
}
