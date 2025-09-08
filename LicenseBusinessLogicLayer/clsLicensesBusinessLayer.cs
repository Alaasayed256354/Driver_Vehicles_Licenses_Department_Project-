using LicenseDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseBusinessLogicLayer
{
    public class clsLicensesBusinessLayer
    {
        private enum enMode { AddNew,Update};
        private enMode _Mode;
        public clsLicensesBusinessLayer()
        {
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = -1;
           this.IsActive = false;
          this.IssueReason =0;
          this.CreatedByUserID = 0;
            _Mode=enMode.AddNew;
        }
        private clsLicensesBusinessLayer(int licenseID, int ApplicationID,int driverID, int licenseClass, DateTime issueDate, DateTime expirationDate, string notes,decimal PaidFees, bool isActive, byte issueReason,int CreatedByUserID)
        {
            this.LicenseID = licenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = driverID;
            this.LicenseClass = licenseClass;
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.Notes = notes;
            this.IssueReason= issueReason;
            this.PaidFees= PaidFees;
            this.IsActive = isActive;
            this.CreatedByUserID = CreatedByUserID;
            _Mode = enMode.Update;
        }


        public  int LicenseID = -1;
      public  int ApplicationID {  get; set; }
        public clsApplicationsBusinesssLayer AppplicationInfo { get; set; }
        public int DriverID { get; set; }
        public clsDriversBusinesslayer DriverInfo { get; set; }
        public int LicenseClass { get; set; }
        public clsLicenseClassesBusinessLayer LicenseClassInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes {  get; set; }
        public decimal PaidFees {  get; set; }
        public bool IsActive {  get; set; }
        public byte IssueReason { get; set; }
        public int CreatedByUserID {  get; set; }
        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicensesDataAccess.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
            return (LicenseID != -1);
        }
        private bool _UpdateLicense()
        {
            return clsLicensesDataAccess.Updatelicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    break;
                    case enMode.Update:
                    return _UpdateLicense();
            }
            return false;
        }
        public static bool IsHasAnActiveLicenseForAGivenClassName(string className,int PersonID)
        {
            return clsLicensesDataAccess.IsHasAnActiveLicenseForThisLicenseClass(className, PersonID);  
        }
        public static bool IsLicenseExpired(int LicenseID)
        {
            return clsLicensesDataAccess.IsLicenseExpired(LicenseID);
        }
        public static clsLicensesBusinessLayer FindByApplicationID(int ApplicationID)
        {
            int LicenseID = -1;int DriverID = -1;int LicenseClass = -1;  DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now; string Notes = ""; decimal PaidFees = -1; bool IsActive = false; byte IssueReason = default; int CreatedByuserID=-1;
            if (clsLicensesDataAccess.GetLicenseInfoByAppID(ApplicationID,ref LicenseID,ref DriverID,ref LicenseClass,ref IssueDate,ref ExpirationDate,ref Notes,ref PaidFees,ref IsActive,ref IssueReason,ref CreatedByuserID))
            {
                return new clsLicensesBusinessLayer(LicenseID,ApplicationID,DriverID,LicenseClass,IssueDate,ExpirationDate,Notes, PaidFees, IsActive,IssueReason,CreatedByuserID);
            }
            return null;
        }
        public static clsLicensesBusinessLayer FindByLicenseID(int LicenseID)
        {
            int ApplicationID = -1; int DriverID = -1; int LicenseClass = -1; DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now; string Notes = "";decimal PaidFees =-1; bool IsActive = false; byte IssueReason = default; int CreatedByuserID = -1;
            if (clsLicensesDataAccess.GetLicenseInfoByLicenseID( LicenseID,ref ApplicationID,ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees,ref IsActive, ref IssueReason,ref CreatedByuserID))
            {
                return new clsLicensesBusinessLayer(LicenseID,ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees,IsActive, IssueReason,CreatedByuserID);
            }
            return null;
        }
        public static DataTable GetAllLocalDrvingLicenseOfPerson (int PersonID) {
            return clsLicensesDataAccess.GetAllLocalDrvingLicenseOfPerson(PersonID);
        }
        public static bool IsLicenseActive(int LicenseID)
        {
            return clsLicensesDataAccess.IsLicenseActive(LicenseID);
        }
        public static bool IsLicenseExist(int AppID)
        {
            return clsLicensesDataAccess.IsLicenseExist(AppID);
        }
        //public static DataTable GetAllInternationalDrvingLicenseOfPerson(int PersonID)
        //{
        //    return clsLicensesDataAccess.GetAllInternationalDrvingLicenseOfPerson(PersonID);
        //}
    }
}
