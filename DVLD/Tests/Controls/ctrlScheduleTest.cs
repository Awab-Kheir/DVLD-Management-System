using DVLD.Classes;
using DVLD.Controls;
using DVLD.Global_Classes;
using DVLD.Properties;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_Business.clsTestType;

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl        // #
    {

        public enum enMode { AddNew =  0, Update = 1 };                                           //he
        private enMode _Mode = enMode.AddNew;
        public enum enCreationMode {  FirstTimeSchedule=0, RetakeTestSchedule=1 };                //he
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;


        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;

        public clsTestType.enTestType TestTypeID 
        { 
            get
            {
                return _TestTypeID;
            }
            
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case clsTestType.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case clsTestType.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }

                    case clsTestType.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;
                        }
                }
            }
        }




        /*private bool _gbRetakeTestInfoEnabled = true;  
        public bool gbRetakeTestInfoEnabled
        {
            get
            {
                return _gbRetakeTestInfoEnabled;
            }
            set
            {
                _gbRetakeTestInfoEnabled = value;
                gbRetakeTestInfo.Enabled = _gbRetakeTestInfoEnabled;
            }
        }*/

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        public void LoadInfo(int LocalDrivingLicenseApplicationID, int AppointmentID=-1)    // Reset..() ولا  _Fill...() ماعمل  II 
        {                                                                                   // Reset()  _Fill() وجواته في ctrlDrivingLicenseApplicationInfo1 في جواها هاد  frmListTestAppointments مع انه بال  II
            //if no appointment id this means AddNew mode otherwise it's update mode.
            if (AppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _TestAppointmentID = AppointmentID;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);



            if (_LocalDrivingLicenseApplication == null)
            {
                //ResetLocalDrivingLicenseInfo();           //  كنت عاملها II
                MessageBox.Show("No LocalDrivingLicense with LocalDrivingLicenseApplicationID = " + _LocalDrivingLicenseApplicationID.ToString(), 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }
            //_FillLocalDrivingLicenseInfo();    //              كنت عاملها II

            //decide if the creation mode is retake test or not based if the person attended this test before
            
            if (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestTypeID))
                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;

            if(_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRetakeAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = "0";
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }


            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;   //clsLicenseClass.Find(_LocalDrivingLicense.LicenseClassID).ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;

            //this will show the trials for this test before
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();
                                   


            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestType.Find(_TestTypeID).Fees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";

                _TestAppointment = new clsTestAppointment();
            }
            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }


            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();
            //lblTotalFees.Text = (_TestAppointment.PaidFees + clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees).ToString();         غلط لانو ممكن ماعم يعيد الفحص بالمفروض صفر للاعادة II

            if (!_HandleActiveTestAppointmentConstraint())
                return;

            if (!_HandleAppointmentLockedConstraint())
                return;

            if (!_HandlePrviousTestConstraint())
                return;


        }

        private bool _HandleActiveTestAppointmentConstraint()      // مالح يفتحله الفورم من الاصل btnAddNew برأيي مو ضروري لانو بالاصل اذا عنده موعد نشط وكبس  II  
        {
            if(_Mode == enMode.AddNew && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                lblUserMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }
            return true;
        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Applintment with ID = " + _TestAppointmentID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;

            }
            lblFees.Text = _TestAppointment.PaidFees.ToString(); 

            //we compare the current date with the appointment date to set the min date.
            if(DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0 ) 
                dtpTestDate.MinDate = DateTime.Now;
            else
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate;

            dtpTestDate.Value = _TestAppointment.AppointmentDate;

            if(_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeAppFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();  //(clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest)).Fees.ToString();    II
                gbRetakeTestInfo.Enabled = true;                                     //    LoadInfo(...) هي موجودة بال  II
                lblTitle.Text = "Scheule Retake Test";                            //    LoadInfo(...) هي موجودة بال  II
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
            }

            return true;

        }

        private bool _HandleAppointmentLockedConstraint()
        {
            //if appointment is locked that means the person already sat for this test
            //we cannot update locked appointment
            if(_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already sat for the test, appointment locked.";
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }
            else 
                lblUserMessage.Visible = false;

            return true;
        }

        private bool _HandlePrviousTestConstraint()               //  control  بس مشان اذا يلي اشتغل الشاشات خربط وفوته انا كمان بحمي على مستوى ال Menu هو يلي عمله زيادة تحقق لانو بالاصل انا عامل تحقق عالكبسات تبعات ال II
        {
            //we need to make sure that this person passed the prvious required test before apply to the new test.
            //person cannot apply for written test unless s/he passes the visiontest.
            //person cannot apply for street test unless s/he passes the written test.

            switch(TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    lblUserMessage.Visible = false;

                    return true;

                case clsTestType.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass vision test 1.
                    if(!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled=false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }

                    return true;

                case clsTestType.enTestType.StreetTest:
                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }

                    return true;

            }
            return true;
        }

        private bool _HandleRetakeApplication()                 //     أفكار هو عمله he
        {
            //this will decide to create a seperate application for retake test or not
            // and will create it if needed, then it will linkit to the appointment.
            if(_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                //incase the mode is add new and vreation mode is retake test we should create a seperate application for it.
                //then we link it with the appointment.

                //First Create Application
                clsApplication Application = new clsApplication();

                Application.ApplicationPersonID = _LocalDrivingLicenseApplication.ApplicationPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;                        //  .Completed he
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;


                if (!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;

            }

            return true;

        }

        /*                                                           II
        public void ResetLocalDrivingLicenseInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;
            lblLocalDrivingLicenseAppID.Text = "[????]";
            lblDrivingClass.Text = "[????]";
            lblFullName.Text = "[????]";
            lblTrial.Text = "0";
            dtpTestDate.Text = DateTime.Now.ToString();
            lblFees.Text = "[????]";
            lblRetakeAppFees.Text = "0";
            lblTotalFees.Text = "[????]";
            lblRetakeTestAppID.Text = "N/A";

            //  ResetLocalDrivingLicenseInfo مو عامل ctrlScheduleTest خطها هون بس هو بال  new ال   frmAddUpdateLocalDrivinLicense  بال II

        }
        */

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!_HandleRetakeApplication())
                return;

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;    //_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;    he
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
