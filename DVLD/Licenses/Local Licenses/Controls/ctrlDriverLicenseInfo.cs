using DVLD.Global_Classes;
using DVLD.Properties;
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
using System.IO;


namespace DVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl      //  #           
    {
        private int _LicenseID;    // = -1;  II
        private clsLicense _License;

        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        public int LicenseID              //  he
        {
            get { return _LicenseID; }
        }

        public clsLicense SelectedLicenseInfo               // عم شكل الحلول control جوا object ل composition  عملت  he
        { get { return _License; } }


        private void _LoadPersonimage()
        {
            if (_License.DriverInfo.PersonInfo.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;

            if(ImagePath != "")                                   //  DB بال as a Guid  معناها هاد الشخص قله صورة ومخزنة he
                if(File.Exists(ImagePath))                      //   ولالأ  Local Drive بتأكد هل مازالت الصورة موجودة عندي بال 
                    pbPersonImage.ImageLocation = ImagePath;
                else                                                  //    delete وعاملها Drive هون بكزن زاحد رايح عال
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //else
            //pbPersonImage.ImageLocation = null;           II
        }

        public void LoadInfo(int LicenseID)                         //        الطريقة يلي حاب فيها الشعلات لتعباية حلوة II
        {
            _LicenseID = LicenseID;
            _License = clsLicense.Find(_LicenseID);
            if( _License == null )
            {
                MessageBox.Show("Could not find License ID = " +  _LicenseID.ToString(),"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }


            lblLicenseID.Text = _License.LicenseID.ToString();
            lblIsActive.Text = (_License.IsActive) ? "Yes" : "No";
            lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";
            lblClass.Text = _License.LicenseClassInfo.ClassName;
            lblFullName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = (_License.DriverInfo.PersonInfo.Gendor == 0) ? "Male" : "FeMale";
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.DateOfBirth);

            lblDriverID.Text = _License.DriverID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            _LoadPersonimage();


        }

    }
}
