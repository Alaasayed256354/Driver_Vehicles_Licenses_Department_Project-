using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml.Linq;
using LicenseBusinessLogicLayer;
namespace Driver_VehiclesLicensesDepartmentProject
{
    public partial class ManageUsers : Form
    {
      public ManageUsers()
        {
            InitializeComponent();
        }
        private DataView dataView;
        private DataTable dataTable;

        private void _FillDataGridWithUsers()
        {
            dataTable=clsUsersBusinessLayer.GetAllUsers();
            dataView=new DataView(dataTable);
            dgvUsers.DataSource= dataTable;
            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[3].HeaderText = "User Name";
                dgvUsers.Columns[4].HeaderText = "Is Active";
            }
            dgvUsers.Refresh();
        }
    
        private void _FilterDataOfUsers()
        {
           
            if (cbFilter.SelectedIndex<=0)
            {
                dgvUsers.DataSource = dataTable;
                return;
            }
            string Filter = "";
            string filterText = tbFilteration.Text.Trim();
            string FilterBy = cbFilter.SelectedItem.ToString();
            dataView = new DataView(dataTable);
            switch (FilterBy) {
                case "User ID":
                    if (int.TryParse(filterText, out int uid))
                        Filter = $"UserID={uid}";
                    break;
                case "Person ID":
                    if (int.TryParse(filterText, out int pid))
                        Filter = $"PersonID={pid}";
                    break;
                case "Full Name":
                    Filter = $"FullName LIKE '{filterText}%'";
                    break;
                case "User Name":
                    Filter = $"UserName LIKE '{filterText}%'";
                    break;
                case "Is Active":
                    return;
            }
            dataView.RowFilter = Filter;
            dgvUsers.DataSource= dataView;
        }
        private void _FilterIsActive()
        {
             //dataView = new DataView(dataTable);
            string Filter = "";
            if (cbIsActiveFilter.SelectedItem.ToString() == "All")
            {
                //dgvUsers.DataSource = dataTable;
                dataView.RowFilter = "";
                //return;
            }
            else if (cbIsActiveFilter.SelectedItem.ToString() == "Yes")
            {
                Filter = $"IsActive=1";
            }
            else
            {
                Filter = $"IsActive=0";
            }
            dataView.RowFilter = Filter;
            dgvUsers.DataSource = dataView;
        }
        private void ManageUsers_Load(object sender, EventArgs e)
        {
            cbIsActiveFilter.Visible = false;
            tbFilteration.Visible = false;  
            cbFilter.SelectedIndex = 0;
            _FillDataGridWithUsers();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilter.SelectedIndex == 0)
            {
                dgvUsers.DataSource=dataTable;
                tbFilteration.Text =string.Empty;
                tbFilteration.Visible = false;
                cbIsActiveFilter.Visible= false;
            }
            else if (cbFilter.SelectedItem.ToString() == "Is Active")
            {
                tbFilteration.Visible= false;
                cbIsActiveFilter.Visible=true;
                cbIsActiveFilter.SelectedIndex = 0;
            }
            else
            {
                tbFilteration.Visible=true;
                cbIsActiveFilter.Visible=false;
            }
        }

        private void mtbFilteration_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
        }

        private void cbIsActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterIsActive();  
        }

        private void mtbFilteration_TextChanged(object sender, EventArgs e)
        {
            _FilterDataOfUsers();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowDetails showDetails = new frmShowDetails((int)dgvUsers.CurrentRow.Cells[0].Value);
            showDetails.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frmchangePassword = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frmchangePassword.ShowDialog();
            _FillDataGridWithUsers();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure You Want To Delete This User?", "Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)==DialogResult.OK)
            {
               if( clsUsersBusinessLayer.DeleteUser((int)dgvUsers.CurrentRow.Cells[0].Value)){
                    MessageBox.Show("User Deleted Successfully");
                    _FillDataGridWithUsers();
                }
                else
                {
                    MessageBox.Show("User Is Related To Other Information");
                }
            }
            else
            {
                MessageBox.Show("Person Wasn't Deleted");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserModefrm userModefrm = new UserModefrm();
            userModefrm.ShowDialog();
            _FillDataGridWithUsers();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserModefrm userModefrm=new UserModefrm();
            userModefrm.ShowDialog();
            _FillDataGridWithUsers();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implmented Yet");
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implmented Yet");
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserModefrm userModefrm = new UserModefrm((int)dgvUsers.CurrentRow.Cells[0].Value);
            userModefrm.ShowDialog();
            _FillDataGridWithUsers();
        }

        private void tbFilteration_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (cbFilter.SelectedItem)
            {
                case "User ID":
                case "Person ID":
                    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case "Full Name":
                    if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case "User Name":
                    if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }

        private void tbFilteration_TextChanged(object sender, EventArgs e)
        {
            _FilterDataOfUsers();
        }
    }
}
