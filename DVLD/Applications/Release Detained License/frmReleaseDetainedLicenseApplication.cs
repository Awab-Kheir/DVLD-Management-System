using DVLD.Classes;
using DVLD.Global_Classes;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
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

namespace DVLD.Applications.Release_Detained_License
{
    public partial class frmReleaseDetainedLicenseApplication : Form              // #
    {
        private int _SelectedLicenseID = -1;         // _ReleaseLicenseID

        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicenseApplication(int LicenseID)                     //  he
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID;
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_SelectedLicenseID);
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReleaseDetainedLicenseApplication_Load(object sender, EventArgs e)         
        {
            //lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RelaseDetainedDrivingLicense).Fees.ToString();      // لح يضرب اذا ماحطينا هي بالحدث LicenseID يلي بياخذ  constructor اذا استدعينا الفورم من ال II
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            if (_SelectedLicenseID == -1)                                //     llShowLicenseHistory.Enabled  هو حاطتها بعد II
                return;

            lblLicenseID.Text = _SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);


            //ToDo: make sure the license is not detained already
            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License is not detained, choose another one."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;                                           //      هو موحاطتها II
                return;
            }

            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RelaseDetainedDrivingLicense).Fees.ToString();  // لانو  lblTotalFees لح يضرب عند  LicenseID يلي بياخذ  constructor بس لح يضرب لما تستدعي الفورم عن طريق ال frmRelease.._Load كنت حاطتها فوق بال II
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;                                                                                 //  frmRelease.._Load لح ينبنى ويتم اطلاق الحدث قبل usercontol لانو وقتها ال

            lblDetainID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();           //   ليعبي clsDetain من object كان بده يجيب  composition فكرة حلوة لو مااستعمل II
            //lblLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.LicenseID.ToString();  //  هو حاطه بس برأي ماله داعي خلص فوق عبيناها

            //lblCreatedByUser.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.CreatedByUserInfo.UserName;  //  هو حاطتها بس برأي غلط الا اذا قصده بده اسم المستحدم يلي حجز مو يلي فك الحجز  II
            lblDetainDate.Text = clsFormat.DateToShort(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainDate);
            lblFineFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();


            btnRelease.Enabled = true;
        }

        private void frmReleaseDetainedLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            //      وقت اصدار رخصة دولية عباهم فورا بالكبسة لانو كان بدو شغالات من الفورم يعبي فيها وتنويع افكار اما هون   II
            //    business  عملنا مثل اصدرا الرخصة لاول مرة الشغل بال 

            int ApplicationID = -1;

            bool IsReleased = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);         
                                                                                                                                                                    
     
            //lblApplicationID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.ReleaseApplicationID.ToString();         // بعد ماتم فك حجزها DB هون مالح يظبط لانو الرخصة يلي بالكونترول مارجعت نجابت من ال composition ال II
            //   lblApplicationID.Text  ترجع اوبجيكت لعبي فيه ل .ReleaseLicense انا كنت محلي ال
            lblApplicationID.Text = ApplicationID.ToString();        // he                                                                      ليحل القصة افكار حلوة ref هو استخدم فكرة ال


            if (!IsReleased)
            {
                MessageBox.Show("Faild to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Detained License released Successfully ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);


            btnRelease.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLIcenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm =
            new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);    //      برأيي في طرق تانية ممكن بس هاد اسرع وكمان هو عم يشكل حلول II
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }




    }
}
