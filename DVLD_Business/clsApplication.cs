using DVLD_DataAccess;
using System;
using System.Data;


namespace DVLD_Business
{
    public class clsApplication        // #
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enum enApplicationType { NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3, // he
            ReplaceDamagedDrivingLicense = 4, RelaseDetainedDrivingLicense = 5, NewInternationalLicense = 6, RetakeTest = 7};



        public enMode Mode = enMode.AddNew;
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3};      // he

        public int ApplicationID {  get; set; }
        public int ApplicationPersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public string ApplicationFullName
        {
            get
            {
                return clsPerson.Find(ApplicationPersonID).FullName;
            }
        }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationTypeInfo;
        public enApplicationStatus ApplicationStatus { set; get; }                                      //  he
        //public byte ApplicationStatus { get; set; }                         II
        public string StatusText
        {
            get {

                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";

                }

            }
        }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;                   //  he


        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicationPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsApplication(int ApplicationID, int ApplicationPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
            float PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicationPersonID = ApplicationPersonID;
            this.PersonInfo = clsPerson.Find(ApplicationPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);

            Mode = enMode.Update;

        }

        private bool _AddNewApplication()
        {
            //call DataAccess Layer

            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicationPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte) this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            //call DataAccess Layer

            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicationPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte) this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public static clsApplication FindBaseApplication(int ApplicationID)                        //  هيك method فسميت ال Base class هاد  he
        { 
            int ApplicationPersonID = -1, ApplicationTypeID = -1, CreateByUserID = -1;      
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            byte ApplicationStatus = 1;             //   byteهي DBلانو بال II
            float PaidFees = 0;

            if (clsApplicationData.GetApplicationInfoByID(ApplicationID, ref ApplicationPersonID, ref ApplicationDate,
                ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreateByUserID))

                //we return new object of that person with the right data
                return new clsApplication(ApplicationID, ApplicationPersonID, ApplicationDate,
                                ApplicationTypeID, (enApplicationStatus) ApplicationStatus, LastStatusDate, PaidFees, CreateByUserID);
                                                                                //   PK  اتوقع لانو هي بالاصل عبارة عن  int ماحولها لانم لانو بالاصل معرفها   ApplicationTypeID II
                                                                                // وكمان مو دائما الافتراضي الرقم واحد
            else
                return null;

        }

        public bool Cancel()                  //  static انا كنت عاملها 
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 2);
        }                         

        public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 3);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateApplication();
            }

            return false;
        }

        public bool Delete()             // بتفهم ليس ماعملها ستاتكclsLocalDrivingLicensesApplicationsشوف الحذف بال  static انا كنت عاملها 
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }                            

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }

        public static bool DoespersonHaveActiveApplication(int PersonID, int ApplicationTypeID)                     //   1 references  عنده II
        { 
            return clsApplicationData.DoesPerosnHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public bool DoespersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoespersonHaveActiveApplication(this.ApplicationTypeID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)                //   1 references  عنده II
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID); 
        }

        public static int GetApplicationGetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType enApplicationType, int LicenseClass)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (int)enApplicationType, LicenseClass);
        }

        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)                
        {
            return GetActiveApplicationID(this.ApplicationPersonID, ApplicationTypeID);
        }

        //public static DataTable GetAllApplication()                         هو مو عاملها 
        //{
        //    return clsApplicationData.GetAllApplications();
        //}




    }
}
