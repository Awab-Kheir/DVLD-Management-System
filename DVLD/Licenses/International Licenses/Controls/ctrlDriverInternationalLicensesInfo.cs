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


namespace DVLD.Licenses.International_Licenses.Controls
{
    public partial class ctrlDriverInternationalLicensesInfo : UserControl        //  #
    {
        private int _InternationalLicenseID;   // = -1;   II
        private clsInternationalLicense _InternationalLicense;


        public ctrlDriverInternationalLicensesInfo()
        {
            InitializeComponent();
        }

        public int InterationalLicenseID
        {
            get { return _InternationalLicenseID; }
        }

        //public clsInternationalLicense InternationalLicense                     ممكن بس مابدنا ياه  II
        //{
        //    get { return _InternationalLicense; }
        //}



        private void _LoadPersonImage()
        {
            if (_InternationalLicense.PersonInfo.Gendor == 0)                    //   ctrlDriverLicenseInfo لانو ناقلها نقل من  _InternationalLicense.DriverInfo.PersonID.Gendor هو حاطت II
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _InternationalLicense.PersonInfo.ImagePath;

            if (ImagePath != "")                                 
                if (File.Exists(ImagePath))                     
                    pbPersonImage.ImageLocation = ImagePath;
                else                                                 
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //else
            //pbPersonImage.ImageLocation = null;           II
        }

        public void LoadInfo(int InternaionalLicenseID)
        {

            _InternationalLicenseID = InternaionalLicenseID;
            _InternationalLicense = clsInternationalLicense.Find(_InternationalLicenseID);
            if(_InternationalLicense == null)
            {
                MessageBox.Show("Could not find International License ID = " + _InternationalLicenseID.ToString(),
                    "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicensesID.ToString();
            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
            lblLocalLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblFullName.Text = _InternationalLicense.PersonInfo.FullName;                                      //    ...DrivreInfo.PersonInfo...  he
            lblNationalNo.Text = _InternationalLicense.PersonInfo.NationalNo;
            lblGendor.Text = _InternationalLicense.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = clsFormat.DateToShort(_InternationalLicense.PersonInfo.DateOfBirth);


            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_InternationalLicense.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_InternationalLicense.ExpirationDate);

            _LoadPersonImage();

        }





    }
}
