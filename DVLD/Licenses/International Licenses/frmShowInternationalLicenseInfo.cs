using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.International_Licenses
{
    public partial class frmShowInternationalLicenseInfo : Form            // #
    {

        private int _InternationlLocalLicenseID;

        public frmShowInternationalLicenseInfo(int InternationlLocalLicenseID)
        {
            InitializeComponent();
            _InternationlLocalLicenseID = InternationlLocalLicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverInternationalLicensesInfo1.LoadInfo(_InternationlLocalLicenseID);
        }


    }
}
