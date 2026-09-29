using DVLD.Controls;
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

namespace DVLD.Licenses.Local_Licenses.Controls            
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl          // #
    { 
        // Define a custom event handler delegate with parameters
        public event Action<int> OnLicenseSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID); //Raise the event with the parameter
            }
        }


        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        private int _LicenseID = -1;                    // برأيي منقدر بلاه وانا كان شغال عندي ومن دون مااستخدمه  he

        public int LicenseID
        {
            get { return ctrlDriverLicenseInfo1.LicenseID; }
        }

        public clsLicense SelectedLicenseInfo
        {
            get { return ctrlDriverLicenseInfo1.SelectedLicenseInfo; }
        }


        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }


        public void LoadLicenseInfo(int LicenseID)                             // public ولهيك خلاها frmReleaseDetained.. ممكن ناديها من برا فورا وماخلي المستحدم يحط شي مثل بال  II+ he
        {
            txtLicenseID.Text = LicenseID.ToString();                               //    برأيي مالها داعي II
            ctrlDriverLicenseInfo1.LoadInfo(int.Parse(txtLicenseID.Text));
            _LicenseID = ctrlDriverLicenseInfo1.LicenseID;                             //    برأيي مالها داعي II

            if (OnLicenseSelected != null && FilterEnabled)                //   event امه في حدا بيستناني على هي ال   OnLicenseSelected != null  he 
                // Raise the event with a parameter
                OnLicenseSelected(_LicenseID);
        }                               

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)                                      //                Find تبع ال onClick الانتر رقمه 13بالنظام فبروح بنادي ال II+he
            {
                btnFind.PerformClick();
            }

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue because the form is not valid
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;

            }

            _LicenseID = int.Parse(txtLicenseID.Text);              //  مو ضروري ياخد قيمة من الاصل منقدر نختصر كتابة LoadLicenseInfo برأيي لل II
            LoadLicenseInfo(_LicenseID);

        }

        public void txtLicenseIDFocus()               //he
        {
            txtLicenseID.Focus();
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtLicenseID, null);
            }
        }


    }
}
