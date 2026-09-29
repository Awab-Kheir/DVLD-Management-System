using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using DVLD_Business;

namespace DVLD.People
{
    public partial class frmListPeople : Form
    {

        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        // only select the columns that you want to show in the grid
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                             "FirstName", "SecondName", "ThirdName", "LastName",
                                                             "GendorCaption", "DateOfBirth", "CountryName",
                                                             "Phone", "Email");

                                                                                                                 //II
        enum enFilter { None =0, PersonID=1, NationalNo =2, FirstName=3, SecondName=4, ThirdName=5, LastName=6,  //II
                         Nationality=7, Gendor=8, Phone = 9, Email = 10}

        private void _RefreshPeopleList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                               "FirstName", "SecondName", "ThirdName", "LastName",
                                                                "GendorCaption", "DateOfBirth", "CountryName",
                                                                "Phone", "Email");
            dgvPeople.DataSource = _dtPeople;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();

        }

        public frmListPeople()
        {
            InitializeComponent();
        }

        //private void _FullColumnInComboBox()        هو حطها من الديزاين يمكن لانو قلال اما بمشروع كورس 18 كتار   II  
        //{
        //    cbFilterBy.Items.Add("None");
        //    cbFilterBy.Items.Add("Person ID");
        //    cbFilterBy.Items.Add("National No");
        //    cbFilterBy.Items.Add("First Name");
        //    cbFilterBy.Items.Add("Second Name");
        //    cbFilterBy.Items.Add("Third Name");
        //    cbFilterBy.Items.Add("Last Name");
        //    cbFilterBy.Items.Add("Nationality");
        //    cbFilterBy.Items.Add("Gendor");
        //    cbFilterBy.Items.Add("Phone");
        //    cbFilterBy.Items.Add("Email");
        //    cbFilterBy.SelectedIndex = 0;

        //}

        //private void _RefreshPeopleList()                         II
        //{
        //    //dgvAllPeople.DataSource = clsPerson.GetAllPeople();
        //    DataTable dtPerson = clsPerson.GetAllPeople();

        //    dgvPeople.Columns.Clear();
        //    dgvPeople.Columns.Add("PersonID", "Person ID");
        //    dgvPeople.Columns.Add("NationalNo", "National No");
        //    dgvPeople.Columns.Add("FirstName", "First Name");
        //    dgvPeople.Columns.Add("SecondName", "Second Name");
        //    dgvPeople.Columns.Add("ThirdName", "Third Name");
        //    dgvPeople.Columns.Add("LastName", "Last Name");
        //    dgvPeople.Columns.Add("GendorCaption", "Gendor");
        //    dgvPeople.Columns.Add("DateOfBirth", "Date Of Birth");
        //    dgvPeople.Columns.Add("CountryName", "Nationality");
        //    dgvPeople.Columns.Add("Phone", "Phone");
        //    dgvPeople.Columns.Add("Email", "Email");

        //    foreach (DataRow row in dtPerson.Rows)
        //    {
        //        dgvPeople.Rows.Add(row["PersonID"], row["NationalNo"], row["FirstName"], row["SecondName"],
        //                row["ThirdName"], row["LastName"], row["GendorCaption"], row["DateOfBirth"], row["NationalityCountryID"],
        //                row["Phone"], row["Email"]);

        //    }
        //    lblRecordsCount.Text = dtPerson.Rows.Count.ToString();
        //    //foreach (DataRow row in dtPerson.Rows)
        //    //{
        //    //    int i = dgvAllPeople.Rows.Add();
        //    //    dgvAllPeople.Rows[i].Cells["PersonID"].Value = row["PersonID"];
        //    //    dgvAllPeople.Rows[i].Cells["NationalNo"].Value = row["NationalNo"];      // II
        //    //}

        //}



        private void frmListPeople_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = _dtPeople;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
            if(dgvPeople.Rows.Count > 0)
            {
                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 110;

                dgvPeople.Columns[1].HeaderText = "National No.";
                dgvPeople.Columns[1].Width = 120;

                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 140;

                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Gendor";
                dgvPeople.Columns[6].Width = 120;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 140;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 120;

                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;

                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 170;

            }            
        }
                                                                                 //  II
        private void MtxtFilter_KeyUp(object sender, KeyEventArgs e)        //  II
        {

            //DataTable dtPerson = clsPerson.GetAllPeople();

            //dgvPeople.Columns.Clear();                          //  حله اكيد احسن انا كل فلترة عم امسح كل الاعمدة II  
            //dgvPeople.Columns.Add("PersonID", "Person ID");
            //dgvPeople.Columns.Add("NationalNo", "National No");
            //dgvPeople.Columns.Add("FirstName", "First Name");
            //dgvPeople.Columns.Add("SecondName", "Second Name");
            //dgvPeople.Columns.Add("ThirdName", "Third Name");
            //dgvPeople.Columns.Add("LastName", "Last Name");
            //dgvPeople.Columns.Add("Gendor", "Gendor");
            //dgvPeople.Columns.Add("DateOfBirth", "Date Of Birth");
            //dgvPeople.Columns.Add("NationalityCountryID", "Nationality");
            //dgvPeople.Columns.Add("Phone", "Phone");
            //dgvPeople.Columns.Add("Email", "Email");

            //DataView PeopleDataView1 = dtPerson.DefaultView;



            //if ((enFilter)cbFilterBy.SelectedIndex == enFilter.PersonID)
            //{
            //    int id;
            //    if (int.TryParse(MtxtFilter.Text, out id))
            //    {
            //        PeopleDataView1.RowFilter = "PersonID = " + id;
            //    }
            //    else
            //    {
            //        PeopleDataView1.RowFilter = "";
            //    }
            //    //PeopleDataView1.RowFilter = "PersonID = " + MtxtFilter.Text;
            //}

            //else if ((enFilter)cbFilterBy.SelectedIndex == enFilter.NationalNo) //  cbFilterBy.Text == "Notional No."    II
            //{
            //    if (!string.IsNullOrWhiteSpace(MtxtFilter.Text))
            //    {
            //        PeopleDataView1.RowFilter = "NationalNo LIKE '" + MtxtFilter.Text + "%'";
            //    }
            //    else
            //    {
            //        PeopleDataView1.RowFilter = ""; // يعرض كل البيانات
            //    }
            //}

            //else if ((enFilter)cbFilterBy.SelectedIndex == enFilter.FirstName)
            //{
            //    if (!string.IsNullOrWhiteSpace(MtxtFilter.Text))
            //    {
            //        PeopleDataView1.RowFilter = "FirstName LIKE '" + MtxtFilter.Text + "%'";
            //    }
            //    else
            //    {
            //        PeopleDataView1.RowFilter = ""; // يعرض كل البيانات
            //    }
            //}

            //else if ((enFilter)cbFilterBy.SelectedIndex == enFilter.SecondName)
            //{
            //    if (!string.IsNullOrWhiteSpace(MtxtFilter.Text))
            //    {
            //        PeopleDataView1.RowFilter = "SecondName LIKE '" + MtxtFilter.Text + "%'";
            //    }
            //    else
            //    {
            //        PeopleDataView1.RowFilter = ""; // يعرض كل البيانات
            //    }
            //}





            //dgvPeople.AutoGenerateColumns = false;
            //dgvPeople.Columns.Clear();

            //dgvPeople.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    Name = "PersonID",
            //    HeaderText = "Person ID",
            //    DataPropertyName = "PersonID"   // اسم العمود في الـ DataView
            //});

            //dgvPeople.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    Name = "NationalNo",
            //    HeaderText = "National No.",
            //    DataPropertyName = "NationalNo"   // اسم العمود في الـ DataView
            //});

            //dgvPeople.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    Name = "FirstName",
            //    HeaderText = "First Name ",
            //    DataPropertyName = "FirstName"   // اسم العمود في الـ DataView
            //});
            //dgvPeople.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    Name = "SecondName",
            //    HeaderText = "Second Name",
            //    DataPropertyName = "SecondName"   // اسم العمود في الـ DataView
            //});
            //dgvPeople.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    Name = "ThirdName",
            //    HeaderText = "Third Name",
            //    DataPropertyName = "ThirdName"   // اسم العمود في الـ DataView
            //});
            //dgvPeople.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    Name = "LastName",
            //    HeaderText = "Last Name",
            //    DataPropertyName = "LastName"   // اسم العمود في الـ DataView
            //});
            //dgvPeople.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    Name = "Gendor",
            //    HeaderText = "Gendor",
            //    DataPropertyName = "Gendor"   // اسم العمود في الـ DataView
            //});
            //dgvPeople.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    Name = "DateOfBirth",
            //    HeaderText = "Date Of Birth",
            //    DataPropertyName = "DateOfBirth"   // اسم العمود في الـ DataView
            //});
            //dgvPeople.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    Name = "NationalityCountryID",
            //    HeaderText = "Nationality",
            //    DataPropertyName = "NationalityCountryID"   // اسم العمود في الـ DataView
            //});
            //dgvPeople.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    Name = "Phone",
            //    HeaderText = "Phone",
            //    DataPropertyName = "Phone"   // اسم العمود في الـ DataView
            //});
            //dgvPeople.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    Name = "Email",
            //    HeaderText = "Email",
            //    DataPropertyName = "Email"   // اسم العمود في الـ DataView
            //});

            //dgvPeople.DataSource = PeopleDataView1;

            //lblRecordsCount.Text = PeopleDataView1.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name
            switch(cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
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

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value contains nothing.
            if(txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PersonID")
                //in this case we deal with integer not string.

                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());


            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }


            /*                                                                 II
            if ((enFilter)cbFilterBy.SelectedIndex == enFilter.PersonID || (enFilter)cbFilterBy.SelectedIndex == enFilter.Phone)
            {
                MtxtFilter.Visible = true;
                MtxtFilter.Clear();
                MtxtFilter.Mask = "0000000000000";
                MtxtFilter.Focus();
            }
            else if ((enFilter)cbFilterBy.SelectedIndex == enFilter.None)
            {
                MtxtFilter.Visible = false;
            }
            else
            {
                MtxtFilter.Visible = true;
                MtxtFilter.Focus();
                MtxtFilter.Clear();
                MtxtFilter.Mask = "";
            }
            */
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            Form frm = new frmShowPersonInfo(PersonID);    //frmPersonDetails frm  II
            frm.ShowDialog();
           
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson((int)dgvPeople.CurrentRow.Cells[0].Value);    //frmAddUpdatePerson frm II
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Not implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is Not implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;    II

            if (MessageBox.Show("Are you Sure you want to delete Person [" + dgvPeople.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question/*, MessageBoxDefaultButton.Button2*/) == DialogResult.OK)
            {
                //Perform Delete and refresh
                if (clsPerson.DeletePerson((int)dgvPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeopleList();
                }
                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson();    //frmAddUpdatePerson frm II
            frm.ShowDialog();

            _RefreshPeopleList();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson();       // frmAddUpdatePerson frm II
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPeople_DoubleClick(object sender, EventArgs e)
        {
            Form frm = new frmShowPersonInfo((int)dgvPeople.CurrentRow.Cells[0].Value);    //frmPersonDetails frm  II
            frm.ShowDialog();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if(cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
