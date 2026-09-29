using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsInternationalLicense : clsApplication        // #         //  تاني مرة بيستخدم وراثة وانا كنت عاملها بدون وراثة  II
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public clsDriver DriverInfo;            
        public int InternationalLicensesID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        public clsInternationalLicense()
        {
            //here we set the application type to New International License.                       he
            this.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;

            this.InternationalLicensesID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;

            Mode = enMode.AddNew;
        }

        private clsInternationalLicense(int ApplicationID, int ApplicationPersonID, DateTime ApplicationDate,                 //public هو عامله  II
            enApplicationStatus enApplicationStatus, DateTime LastStatusDate, float PaidFees,
            int CreatedByUserID, int InternationalLicensesID, int DriverID,
            int IssedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive)
        {

            //this is for the base class
            base.ApplicationID = ApplicationID;
            base.ApplicationPersonID = ApplicationPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            base.ApplicationStatus = enApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;

            this.InternationalLicensesID = InternationalLicensesID;
            this.ApplicationID = ApplicationID;                                         //   حلوة he
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive; 
            this.CreatedByUserID = CreatedByUserID;                                     //   حلوة he


            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.PersonInfo = clsPerson.Find(ApplicationPersonID);                                //   DriverInfo بدون  PesonInfo. تحت وانا عم عبي فورا قول ctrlDriverInternationalLicensesInfo هي انا عملتها مشان بال  II
                                                                                                  //  وهو بجيب معلومات الشخص بس مشان الافكار DriverInfo طبعا بقدر اعمل مثله وخلص ضل عال
            Mode = enMode.Update;     

        }

        private bool _AddNewInternationalLicense()
        {
            //call DataAccess Layer

            this.InternationalLicensesID = clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
                this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

            return (this.InternationalLicensesID != -1);
        }

        private bool _UpdateInternationalLicense()
        {
            //call DataAccess Layer

            return clsInternationalLicenseData.UpdateInternationalLicense(this.InternationalLicensesID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
                this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

        }

        public static clsInternationalLicense Find(int InternationalLicensesID)
        {
            int ApplicationID = -1, DriverID = -1, IssedUsingLocalLicenseID = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;

            if (clsInternationalLicenseData.GetInternationalLicenseInfoByID(InternationalLicensesID, ref ApplicationID, ref DriverID,
                        ref IssedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))

            {
                //now we find the base application
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);


                return new clsInternationalLicense(Application.ApplicationID, Application.ApplicationPersonID, Application.ApplicationDate, 
                                        (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                        Application.PaidFees, CreatedByUserID, InternationalLicensesID, DriverID,
                                        IssedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive);
            }          

            else
                return null;

        }

        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();
        }

        public bool Save()
        {
            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            //After we save the main application now we save the sub application.

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateInternationalLicense();
            }

            return false;
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }

        public static DataTable GetAllDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetAllDriverInternationalLicenses(DriverID);                
        }



     

    }
}
