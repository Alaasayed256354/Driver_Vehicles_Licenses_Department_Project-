using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_VehiclesLicensesDepartmentProject
{
    public partial class Mainscreen : Form
    {
        LoginScreen Loginscreen;
        public Mainscreen(LoginScreen loginScreen)
        {
            InitializeComponent();
            Loginscreen = loginScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagePeopleForm peopleForm=new ManagePeopleForm();
            peopleForm.Show();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageUsers manageUsers=new ManageUsers();
            manageUsers.Show();
        }
        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.CurrentUser = null;
            string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Credentials.txt");
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
            Loginscreen.Show();
            this.Close();
            //Loginscreen.tbUserName.Text = string.Empty;
            //Loginscreen.tbPassword.Text = string.Empty;
        }
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword changePassword = new frmChangePassword(Global.CurrentUser.UserID);
            changePassword.ShowDialog();

        }

        private void showCurrentUserDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowDetails showDetails = new frmShowDetails(Global.CurrentUser.UserID);
            showDetails.Show();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageApplicationTypesfrmcs applicationTypesfrmcs = new ManageApplicationTypesfrmcs();
            applicationTypesfrmcs.Show();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageApplicationTestsfrm applicationTestsfrm= new ManageApplicationTestsfrm();
            applicationTestsfrm.Show();
        }

        private void localDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLocalDrivingLicenseApplicationfrm localDrivingLicenseApplicationfrm=new NewLocalDrivingLicenseApplicationfrm();
            localDrivingLicenseApplicationfrm.Show();
        }
        LocalDrivingLicenseApplicationsfrm localDrivingLicense = new LocalDrivingLicenseApplicationsfrm();
        private void localDrivingLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (localDrivingLicense.IsDisposed){
                localDrivingLicense = new LocalDrivingLicenseApplicationsfrm();
            }
            //localDrivingLicense.MdiParent = this;
            localDrivingLicense.Show();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDrivers frmManageDrivers = new frmManageDrivers();
            frmManageDrivers.Show();
        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageInternationalLicense manageInternationalLicense = new frmManageInternationalLicense();
            manageInternationalLicense.Show();
        }

        private void Mainscreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Loginscreen.Show();
            //Loginscreen.Close();    
        }

        private void internationalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formNewInternationalDrivingLicense internationalDrivingLicense = new formNewInternationalDrivingLicense();
            internationalDrivingLicense.Show();
        }

        private void reNewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLicense renewLicense = new frmRenewLicense();
            renewLicense.Show();
        }

        private void replacmentForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDamgedAndLostLicenses issueDamgedAndLostLicenses=new frmIssueDamgedAndLostLicenses();
            issueDamgedAndLostLicenses.Show();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplicationsfrm localDrivingLicenseApplicationsfrm = new LocalDrivingLicenseApplicationsfrm();
            localDrivingLicenseApplicationsfrm.Show();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainedLicenses detainedLicenses = new frmDetainedLicenses();
            detainedLicenses.Show();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense detainLicense = new frmDetainLicense(-1);
            detainLicense.Show();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseLicense releaseLicense=new frmReleaseLicense(-1);
            releaseLicense.Show();
        }

        private void releaseDetinedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseLicense releaseLicense = new frmReleaseLicense(-1);
            releaseLicense.Show();
        }
    }
}
