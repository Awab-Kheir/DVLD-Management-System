using DVLD.Licenses;
using DVLD.Licenses.International_Licenses;
using DVLD.People;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.International_License
{
    public partial class frmListInternationalLicenseApplications : Form            // #         //    frmListLocalDr...  نفس افكار ل II
    {
        private static DataTable _dtAllInternationalLicenseApplications;                    //     All  هو مو حاطت كلمة he
                                                                                            //  DB يلي لح ترجع من عنا من ال data ال to hold مشان  DataTable منعرف he

        public frmListInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _dtAllInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            cbFilterBy.SelectedIndex = 0;   // he

            dgvInternationaLicenses.DataSource = _dtAllInternationalLicenseApplications;
            lblInternationalLicensesRecords.Text = dgvInternationaLicenses.Rows.Count.ToString();

            if (dgvInternationaLicenses.Rows.Count > 0)
            {
                dgvInternationaLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationaLicenses.Columns[0].Width = 160;

                dgvInternationaLicenses.Columns[1].HeaderText = "Applicatoin ID";
                dgvInternationaLicenses.Columns[1].Width = 150;

                dgvInternationaLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationaLicenses.Columns[2].Width = 130;

                dgvInternationaLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationaLicenses.Columns[3].Width = 130;

                dgvInternationaLicenses.Columns[4].HeaderText = "Isuue Date";
                dgvInternationaLicenses.Columns[4].Width = 180;

                dgvInternationaLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationaLicenses.Columns[5].Width = 180;

                dgvInternationaLicenses.Columns[6].HeaderText = "Is Active";    
                dgvInternationaLicenses.Columns[6].Width = 120;

            }
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmNewInterationalLicenseApplications frm = new frmNewInterationalLicenseApplications();
            frm.ShowDialog();
            //refresh
            frmListInternationalLicenseApplications_Load(null, null);
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo((int)dgvInternationaLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(clsDriver.FindByDriverID((int)dgvInternationaLicenses.CurrentRow.Cells[2].Value).PersonID);
            frm.ShowDialog();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(clsDriver.FindByDriverID((int)dgvInternationaLicenses.CurrentRow.Cells[2].Value).PersonID);
            frm.ShowDialog();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }

            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;

                if(cbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;                  
                }
                else
                    txtFilterValue.Enabled = true;


                txtFilterValue.Text = "";
                txtFilterValue.Focus();

            }


        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {

            string FilterColumn = "Is Active";
            string FilterValue = cbIsReleased.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if (FilterValue == "All")
                _dtAllInternationalLicenseApplications.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string. 
                _dtAllInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);


            lblInternationalLicensesRecords.Text = dgvInternationaLicenses.Rows.Count.ToString();    
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Cloumn name
            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;

                case "Applicatoin ID":
                    { 
                        FilterColumn = "ApplicationID";
                        break;               
                    }

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":                                     //          برأيي بلاه II
                    FilterColumn = "IsActive";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value contains nothing.
            if (cbFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllInternationalLicenseApplications.DefaultView.RowFilter = "";
                lblInternationalLicensesRecords.Text = dgvInternationaLicenses.Rows.Count.ToString();
                return;
            }



            _dtAllInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());

            lblInternationalLicensesRecords.Text = dgvInternationaLicenses.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number only because all filters are numbers.
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }





    }
}
