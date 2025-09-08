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
using LicenseBusinessLogicLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
namespace Driver_VehiclesLicensesDepartmentProject
{
    public partial class ManagePeopleForm : Form
    {
        private static DataTable dt= clsPerson.GetAllPersons();
        private  DataTable _SubSetOfAllColumns=dt.DefaultView.ToTable(false,"PersonID");//this table will only contain one column of personid
        DataView dataView = new DataView(dt);
        private void _FillGridViewWithData()
        {
            dt= clsPerson.GetAllPersons();
            dataGridView1.DataSource=dt;
            cbFilteration.SelectedIndex = 0;
            if(dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Person ID";
                dataGridView1.Columns[1].HeaderText = "National No";
                dataGridView1.Columns[2].HeaderText = "First Name";
                dataGridView1.Columns[3].HeaderText = "Second Name";
                dataGridView1.Columns[4].HeaderText = "Third Nmae";
                dataGridView1.Columns[5].HeaderText = "Last Name";
                dataGridView1.Columns[6].HeaderText = "Date Of Birth";
                dataGridView1.Columns[7].HeaderText = "Gendor";
                dataGridView1.Columns[8].HeaderText = "Address";
                dataGridView1.Columns[9].HeaderText = "Phone";
                dataGridView1.Columns[10].HeaderText = "Email";
                dataGridView1.Columns[11].HeaderText = "Nationality ID";
                dataGridView1.Columns[12].HeaderText = "Image Path";
            }
         
            dataGridView1.Refresh();
        }
        public ManagePeopleForm()
        {
            InitializeComponent();
        }

        private void ManagePeopleForm_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("DateOnly", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                row["DateOnly"] = Convert.ToDateTime(row["DateOfBirth"]).ToString("yyyy-MM-dd");
            }
            textBox1.Visible = false;
            maskedTextBox1.Visible=false;
             _FillGridViewWithData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbFilteration_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbFilteration_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilteration.SelectedIndex == 0)
            {
                maskedTextBox1.Visible = false;
                textBox1.Visible = false;
                dataGridView1.DataSource = dt; 
            }
            else if(cbFilteration.SelectedIndex ==11)
            {
                textBox1.Visible=false;
                maskedTextBox1.Visible=true;
            }
            else
            {
                maskedTextBox1.Visible = false;
                textBox1.Visible = true;
            }
            //  _FilterationDataGridView(cbFilteration, mtbFilteration);
        }

        private void mtbFilteration_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
        }

        private void mtbFilteration_TextChanged(object sender, EventArgs e)
        {
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implmented Yet");
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDetailsForm showDetailsForm = new ShowDetailsForm((int)dataGridView1.CurrentRow.Cells[0].Value);
            showDetailsForm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure You Want To Delete This Person", "Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                clsPerson person = clsPerson.Find((int)dataGridView1.CurrentRow.Cells[0].Value);
                if (person != null)
                {
                    if(person.ImagePath != "")
                    {
                        if (File.Exists(person.ImagePath))
                        {
                            File.Delete(person.ImagePath);
                        }
                    }
                    if (clsPerson.DeletePerson((int)dataGridView1.CurrentRow.Cells[0].Value))
                    {

                        MessageBox.Show("Person Deleted Successfully");
                        _FillGridViewWithData();
                        dataGridView1.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Person Is Related To Other Information");
                    }
                }
            }
            else
            {
                MessageBox.Show("Person Wasn't Deleted");
            }
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModeForm modeForm = new ModeForm();
            modeForm.Show();
            _FillGridViewWithData();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModeForm modeForm = new ModeForm((int)dataGridView1.CurrentRow.Cells[0].Value);
            modeForm.ShowDialog();
            _FillGridViewWithData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ModeForm modeForm = new ModeForm();
            modeForm.ShowDialog();
            _FillGridViewWithData();
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implmented Yet");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string ComboBoxItem = cbFilteration.SelectedItem.ToString();
            string FilterColumn = "";
            switch (ComboBoxItem)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "First Name":
                    FilterColumn = "FirstName";
                    break;
                case "Second Name":
                    FilterColumn = "SecondName";
                    break;
                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;
                case "Last Name":
                    FilterColumn = "LastName";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "Nationality":
                    FilterColumn = "NationalityCountryID";
                    break;
                case "Phone":
                    FilterColumn = "Phone";
                    break;
                case "Email":
                    FilterColumn = "Email";
                    break;
                case "Address":
                    FilterColumn = "Address";
                    break;
                case "Date Of Birth":
                    FilterColumn = "DateOfBirth";
                    break;
                case "Gendor":
                    FilterColumn = "Gendor";
                    break;
            }
            string filterText = textBox1.Text.ToString();
            string filter = "";

            if (string.IsNullOrEmpty(filterText))
            {
                dataGridView1.DataSource = dt;
                return;
            }

            if (FilterColumn == "PersonID" || FilterColumn == "Gendor" || FilterColumn == "NationalityCountryID")
            {
                filter = $"{FilterColumn}='{filterText}'";
            }
            else
            {
                filter = $"{FilterColumn} LIKE '{filterText}%'";
            }
           dataView.RowFilter = filter;
            dataGridView1.DataSource = dataView;//set DGV to DataView
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

           
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maskedTextBox1.Text))
            {
                dataGridView1.DataSource =dt;
                return;
            }
            DateTime inputDate;
            if (DateTime.TryParse(maskedTextBox1.Text, out inputDate))
            {
                // Filter using the "DateOnly" column
                dataView.RowFilter = $"DateOnly = #{inputDate:yyyy-MM-dd}#";

                // Bind the filtered DataView to the DataGridView
                dataGridView1.DataSource = dataView;
            }
            else
            {
                dataGridView1.DataSource =dt;
                return;
            }
            dataGridView1.DataSource=dataView;//set DGV to DataView
        }
    }
}
