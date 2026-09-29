using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestType                // #
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enTestType { VisionTest =1, WrittenTest = 2, StreetTest = 3 };

        public clsTestType.enTestType ID { get; set; }           //enum هو نفسه ال TestTypeبتع ال  ID لانو ال 
        public string Tiltle { get; set; }
        public string Description { get; set; }
        public float Fees { get; set; }

        public clsTestType()
        {
            this.ID = clsTestType.enTestType.VisionTest;
            this.Tiltle = "";
            this.Description = "";
            this.Fees = 0;
            Mode = enMode.AddNew;

        }

        private clsTestType(clsTestType.enTestType ID, string TestTypeTiltle, string Description, float TestTypeFees)
        {
            this.ID = ID;
            this.Tiltle = TestTypeTiltle;
            this.Description = Description;

            this.Fees = TestTypeFees;
            Mode = enMode.Update;

        }

        private bool _AddNewTestType()          // مالها داعي II
        {
            //call DataAccess Layer
            this.ID = (clsTestType.enTestType) clsTestTypeData.AddNewTestType(this.Tiltle, this.Description, this.Fees);

            return (this.Tiltle != "");
        }

        private bool _UpdateTestType()
        {
            //call DataAccess Layer

            return clsTestTypeData.UpdateTestType((int) this.ID, this.Tiltle, this.Description, this.Fees);
        }

        public static clsTestType Find(clsTestType.enTestType TestTypeID)
        {
            string Title = "", Description = "";
            float Fees = 0;

            if (clsTestTypeData.GetTestTypeInfoByID((int) TestTypeID, ref Title, ref Description, ref Fees))  

                return new clsTestType(TestTypeID, Title, Description, Fees);
            else
                return null;

        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateTestType();
            }

            return false;
        }

    }
}
