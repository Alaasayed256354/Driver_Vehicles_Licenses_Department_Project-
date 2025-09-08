using LicenseDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseBusinessLogicLayer
{
    public class clsLicenseClassesBusinessLayer
    {
        public int ClsassID { get; }
        public string ClassName {  get; set; }
        public string ClassDescription {  get; set; }

        public byte MinimumAllowedAge {  get; set; }
        public byte DefaultValidityLength { get; set; }
        public decimal ClassFees {  get; set; }
        public clsLicenseClassesBusinessLayer(int ClsassID, string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, decimal ClassFees) { 
           this.ClsassID = ClsassID;
           this.ClassName = ClassName;
           this.ClassDescription = ClassDescription;
        this.MinimumAllowedAge=MinimumAllowedAge;
            this.DefaultValidityLength=DefaultValidityLength;
            this.ClassFees=ClassFees;
        }
        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassesDataAccess.GetAllLicenseClasses();
        }
        public static clsLicenseClassesBusinessLayer Find(int LicenseClassID)
        {
            string ClassName = ""; string ClassDescription = ""; byte MinimumAllowedAge = 0; byte DefaultValidityLength =0; decimal ClassFees = -1;
            if(clsLicenseClassesDataAccess.GetLicenseClassByID(LicenseClassID,ref ClassName,ref ClassDescription,ref MinimumAllowedAge,ref DefaultValidityLength,ref ClassFees))
            {
                return new clsLicenseClassesBusinessLayer(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }
    }
}
