using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmShowLicenseInfo : Form      //  #
    {
        private int _LicenseID;       //  = -1;   II

        public frmShowLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowLicenseInfo_Load(object sender, EventArgs e)
        {
            //      شيك لانو بالاصل مالح يعرضله شي اذا مثلا هاد الشخص عنده من هي الرخصة من قبل User Control قبل مايعبي ل frmIssueDriverLicenseFirstTime بال  II
            //     (يعني الغايو من الفورم هي اني طلع رخصة فأنا بالاصل مالح اعرض شي اذا كان مالازم يأصدر رخصة)
            //        اما هون مجرد عرض عادي
            ctrlDriverLicenseInfo1.LoadInfo(_LicenseID);
        }
    }
}
