using DVLD.Classes;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmIssueDriverLicenseFirstTime : Form        //    #                    (أفكار)   IssueLicenseForTheFirstTime عن طريق   business وكيف ماعمل كلشي بكبس الاصدار هون قسم بحيث خلا الشغل بال  frmIssueDriver..._Load()  التقسيم بحيث شو حط بال   II
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private clsLicense _License; //
        private clsDriver _Driver;// 

        private int _DriverID = -1;//


        public frmIssueDriverLicenseFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)
        {
            txtNotes.Focus();
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Applicaion with ID = " + _LocalDrivingLicenseApplicationID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            
            if(!_LocalDrivingLicenseApplication.PassedAllTest())       //     مالح يخلي الكبسة تشتغل بالاصل اذا مو ناجح بكلشي  cmsLocalDrivingLicenseApplications_Opening  كأنه زيادة اختبار لانو بال  II
            {
                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            //                                                                   بدو يشوف اذا بالاصل عنده من هي الرخصة من قبل وشعالة  II
            int LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();
            if(LicenseID != -1)
            {
                MessageBox.Show("Person already has License before with License ID = " + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);


        }

        private void btnIssueLicense_Click(object sender, EventArgs e)                                       //      عم نشكل بالحالات Load Form كان منقدر نشيك عالحالات هون لمايكبس على هي ومانسكر الفورم مثل فوق, بس شيكتا عالحالات فوق بال II+he
        { 
            
            int LicenseID = _LocalDrivingLicenseApplication.IssueLicenseForTheFirstTime(txtNotes.Text.Trim(),clsGlobal.CurrentUser.UserID);

            if(LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(), "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                //ctrlDrivingLicenseApplicationInfo1.LinkLicenseEnable = true;   II
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
