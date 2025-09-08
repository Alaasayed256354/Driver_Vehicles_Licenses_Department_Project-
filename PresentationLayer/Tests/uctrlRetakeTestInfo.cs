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
    public partial class uctrlRetakeTestInfo : UserControl
    {
        public uctrlRetakeTestInfo()
        {
            InitializeComponent();
        }
        private clsApplicationTestsBusinessLayer testsBusinessLayer;
        public void _LoadRetakeInfo(int TestTypeID,int RetakeAppID)
        {
            testsBusinessLayer = clsApplicationTestsBusinessLayer.Find(TestTypeID);
            lblAppFees.Text = clsApplicationTypesBusinessLayer.Find(7).ApplicationFees.ToString();
            lblTotalFees.Text = (5 + testsBusinessLayer.TestTypeFees).ToString();
            if (RetakeAppID!= -1)
            {
                lblRetakeTestAppID.Text = RetakeAppID.ToString();
            }
            else
            {
                lblRetakeTestAppID.Text = "[???]";
            }

        }
        private void uctrlRetakeTestInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
