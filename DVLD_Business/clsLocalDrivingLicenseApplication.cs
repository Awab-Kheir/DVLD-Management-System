using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Business.clsTestType;

namespace DVLD_Business
{
    public class clsLocalDrivingLicenseApplication : clsApplication          // #
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int LocalDrivingLicenseApplicationID {  get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo;
        public string PersonFullName                        // لح هيك رجع حطها مع انه وارث مثلها person اتوقع مشان يكون الاسم في  he
        {
            get 
            {
                return base.PersonInfo.FullName;
                //return clsPerson.Find(ApplicationPersonID).FullName;
            }                     
        }

        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;

            Mode = enMode.AddNew;
        }
                                                              // private constructor ماظبط لانو هداك : base جربت II
        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID,
            int ApplicationID, int ApplicationPersonID, DateTime ApplicationDate, int ApplicationTypeID,
            enApplicationStatus ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreateByUserID, int LicenseClassID)
            //: base(ApplicationID, ApplicationPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreateByUserID)       II
        {
            this.LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.ApplicationPersonID = ApplicationPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = (int)ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreateByUserID;
            this.LicenseClassID= LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            this.PersonInfo = clsPerson.Find(ApplicationPersonID);                    //     بس مالح يروح لهنيك عم يعمل كلشي هون clsApplication هيك لانو صح هاد السطر موجود بال  PersonFullName لازم تحطه اذا جبت ال II

            Mode = enMode.Update;
        }

        private bool _AddNewLocalDrivingLicensesApplications()
        {
            //call DataAccess Layer

            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsData.AddNewLocalDrivingLicenseApplication(this.ApplicationID,
                this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicensesApplications()
        {
            //call DataAccess Layer

            return clsLocalDrivingLicenseApplicationsData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID,
                this.ApplicationID, this.LicenseClassID);
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingAppLicenseID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplicationID,
                ref ApplicationID, ref LicenseClassID))
            {
                //now we find the base application 
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                //we return new object of that person with the right data
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID,
                    Application.ApplicationID, Application.ApplicationPersonID, Application.ApplicationDate,
                    Application.ApplicationTypeID, (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                    Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }              

            else
                return null;

        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplicationInfoByApplicationID(ApplicationID,
                ref LocalDrivingLicenseApplicationID, ref LicenseClassID))
            {
                //now we find the base application 
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                //we return new object of that person with the right data
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID,
                    Application.ApplicationID, Application.ApplicationPersonID, Application.ApplicationDate,
                    Application.ApplicationTypeID, (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                    Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }

            else
                return null;

        }

        public bool Save()
        {
            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)Mode;
            if(!base.Save()) 
                return false;

            //After we save the main application now we save the sub application.
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicensesApplications())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateLocalDrivingLicensesApplications();
            }

            return false;
        }

        public static DataTable GetAllLocalDrivingLicensesApplications()
        {
            return clsLocalDrivingLicenseApplicationsData.GetAllLocalDrivingLicenseApplications();
        }
                                                                               // فخلص مبين Static يمكن لانو بالاصل ماله Delete سماه بس  II
        public bool Delete()
        {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
            //First we delete the Local Driving License Application
            IsLocalDrivingApplicationDeleted = clsLocalDrivingLicenseApplicationsData.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);

            if(!IsLocalDrivingApplicationDeleted)
                return false;
            //Then we delete the base Application
            IsBaseApplicationDeleted = base.Delete();            // base كان ما قدر يستعمل Method static لو ال


            return IsBaseApplicationDeleted;
        }

        public bool DoesPassTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesPassPreviousTest(clsTestType.enTestType CurrentTestType)                 //  he
        {
            switch(CurrentTestType)
            {
                case clsTestType.enTestType.VisionTest:
                    //in This case no required prvious test to pass.
                    return true;
                case clsTestType.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass vision test 1.

                    return this.DoesPassTestType(clsTestType.enTestType.VisionTest);

                case clsTestType.enTestType.StreetTest:
                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    return this.DoesPassTestType(clsTestType.enTestType.WrittenTest);

                default:
                    return false;
            }
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)                         // DataAccess برأيي اذا بده يعمل وحده ستاتك هيك فيخلي يلي فوق يلي مالها ستاتك تنادي لستاتك بدل ماتوصل لل  II // he
        {
            return clsLocalDrivingLicenseApplicationsData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public byte TotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)                         // DataAccess برأيي اذا بده يعمل وحده ستاتك هيك فيخلي يلي فوق يلي مالها ستاتك تنادي لستاتك بدل ماتوصل لل  II // he
        {
            return clsLocalDrivingLicenseApplicationsData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)                            // DataAccess برأيي اذا بده يعمل وحده ستاتك هيك فيخلي يلي تحتها يلي مالها ستاتك تنادي لستاتك بدل ماتوصل لل  II // he
        {
            return clsLocalDrivingLicenseApplicationsData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public bool AttendedTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public clsTest GetLastTestPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicationPersonID, this.LicenseClassID, TestTypeID);
        }

        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)            // DataAccess برأيي اذا بده يعمل وحده ستاتك هيك فيخلي يلي فوق يلي مالها ستاتك تنادي لستاتك بدل ماتوصل لل  II // he
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        public bool PassedAllTest()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTest(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }

        public int IssueLicenseForTheFirstTime(string Notes, int CreatedByUserID)
        {
            int _DriverID = -1;

            clsDriver _Driver = clsDriver.FindByPersonID(this.ApplicationPersonID);

            if (_Driver == null)
            {
                //we check if the driver already there for this person.
                _Driver = new clsDriver();

                _Driver.PersonID = this.ApplicationPersonID;
                _Driver.CreatedByUserID = CreatedByUserID;

                if (_Driver.Save())
                {
                    _DriverID = _Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                _DriverID = _Driver.DriverID;
            }

            //now we driver is there, so we add new license

            clsLicense _License = new clsLicense();

            _License.ApplicationID = this.ApplicationID;
            _License.DriverID = _DriverID;
            _License.LicenseClassID = this.LicenseClassID;
            _License.IssueDate = DateTime.Now;
            _License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            _License.Notes = Notes;
            _License.PaidFees = this.LicenseClassInfo.ClassFees;
            _License.IsActive = true;
            _License.IssueReason = clsLicense.enIssueReason.FirstTime;
            _License.CreatedByUserID = CreatedByUserID;


            if (_License.Save())
            {
                //now we should set the application status to complete
                this.SetComplete();                                         //         فكرة من عنده كنت نسيانها  he

                return _License.LicenseID;

            }
            else
                return -1;


        }

        public bool IsLicenseIssued()                     //  he
        {
            return (GetActiveLicenseID() != -1);
        }

        public int GetActiveLicenseID()
        {//this will get the license id that belongs to this application 
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicationPersonID, this.LicenseClassID);
        }
    }
}
