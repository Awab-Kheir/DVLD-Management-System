using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmLocalDrivingLicenseApplicationInfo : Form      //  #
    {
        private int _LocalDrivingLicenseApplicationID = -1;              //          بس برأي هيك الصح ApplicationID هو مسميه II

        public frmLocalDrivingLicenseApplicationInfo(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            //      شيك لانو بالاصل مالح يعرضله شي اذا مثلا هاد الشخص عنده من هي الرخصة من قبل User Control قبل مايعبي ل frmIssueDriverLicenseFirstTime بال  II
            //     (يعني الغايو من الفورم هي اني طلع رخصة فأنا بالاصل مالح اعرض شي اذا كان مالازم يأصدر رخصة)
            //        اما هون مجرد عرض عادي
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);
        }
    }
}
