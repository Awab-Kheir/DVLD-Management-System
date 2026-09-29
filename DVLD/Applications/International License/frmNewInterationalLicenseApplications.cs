using DVLD.Classes;
using DVLD.Global_Classes;
using DVLD.Licenses;
using DVLD.Licenses.International_Licenses;
using DVLD.Licenses.Local_Licenses.Controls;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_Business.clsApplication;

namespace DVLD.Applications.International_License
{
    public partial class frmNewInterationalLicenseApplications : Form       //  #            //   SelectedLicenseInfo  برأيي الفكرة الاساسية والحلوة انه استفاد من  II
    {

        private int _InternationalLicenseID = -1;


        public frmNewInterationalLicenseApplications()
        {
            InitializeComponent();
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            if(SelectedLicenseID == -1)                             // return هو حاطتها تحت اللينك فصح عم يطلع بس عم ينكتب عالفورم  -1 قبل مايعمل  II            
                return;
            
            lblLocalLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);                      // فوق مو مثله تحت  if لانو انا حطيت ال  true برأيي فورا يحطها  II


            //check the license class, person could not issue international license without having
            //normal license of class 3.
            if(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassID !=3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //check if person already have an active international license
            int ActiveInternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);

            if(ActiveInternationalLicenseID != -1)
            {
                MessageBox.Show("Person already has an active international license with ID = " + ActiveInternationalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = true;
                _InternationalLicenseID = ActiveInternationalLicenseID;
                btnIssueLicense.Enabled = false;
                return;
            }



            btnIssueLicense.Enabled = true;
        }

        private void frmNewInterationalLicenseApplications_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));          //add one year.
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)      //        عامل وراثة ولما يحفظ تلقائيا لح ينحفظ الطلب  clsInternationalLicense احلا فكرة انو ماعمل اوبجيت لطلب خلص ال II
        {


            if(MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsInternationalLicense InternationlLicense = new clsInternationalLicense();                       //   اخذوا القيم يلي بدي ياها default constractor ماحطتهم لانو هن خلص بال IsActive وApplicationTypeID تحت ال II
            //those are the information for the base application, because it inhirts from application, they are part of the sub class.

            InternationlLicense.ApplicationPersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonInfo.PersonID;
            InternationlLicense.ApplicationDate = DateTime.Now;
            InternationlLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;     //   لانو هاد الطلب مجرد مابطلبه بصدرله الرخصة II + he
            InternationlLicense.LastStatusDate = DateTime.Now;
            InternationlLicense.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees;
            InternationlLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            InternationlLicense.DriverID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID;
            InternationlLicense.IssuedUsingLocalLicenseID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID;   //ctrlDriverLicenseInfoWithFilter1.LicenseID;  II
            InternationlLicense.IssueDate = DateTime.Now;
            InternationlLicense.ExpirationDate = DateTime.Now.AddYears(1);    //  settings سميه DB بالنظام عميل جدول بال flexable اذا حابب تعملها  hard coded انا عملتها he
                                                                                // وجيبها من هناك وحطها بدل الواحد هون وهيك لازم تكون انا عملت هيك مشان السرعة بس internationalLicenseValidity سميه field وحط فيه
            InternationlLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            if (!InternationlLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }




            lblApplicationID.Text = InternationlLicense.ApplicationID.ToString();
            _InternationalLicenseID = InternationlLicense.InternationalLicensesID;
            lblInternationalLicenseID.Text = InternationlLicense.InternationalLicensesID.ToString();
            MessageBox.Show("Internationl License Issued Successfully with ID = " + InternationlLicense.InternationalLicensesID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueLicense.Enabled = false;  
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;


        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = 
                new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);    //      برأيي في طرق تانية ممكن بس هاد اسرع وكمان هو عم يشكل حلول II
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }

        private void frmNewInterationalLicenseApplications_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

    
    }
}
