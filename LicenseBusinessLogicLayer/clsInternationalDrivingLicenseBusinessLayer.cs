using LicenseDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseBusinessLogicLayer
{
    public class clsInternationalDrivingLicenseBusinessLayer:clsApplicationsBusinesssLayer
    {
      public  clsInternationalDrivingLicenseBusinessLayer() {
            InternationalLicenseID = -1;
            //ApplicationiD = -1;
            DriverID = -1;
            LocalLicenseID = -1;
            IssueDate=DateTime.Now;
            ExpirationDate = DateTime.Now;
            IsActive = false;
            CreatedByUserID = -1;
        }
        private clsInternationalDrivingLicenseBusinessLayer(int internationalLicenseID,int ApplicationID,int driverID, int localLicenseID, DateTime issueDate, DateTime expirationDate, bool isActive,DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus,DateTime StatusTime,decimal PaidFees,int ApplicantID,int CreatedByUserID) :base(ApplicationID, ApplicantID, ApplicationDate, ApplicationTypeID, ApplicationStatus, StatusTime, PaidFees, CreatedByUserID)

        {
           this.InternationalLicenseID = internationalLicenseID;
            this.DriverID = driverID;
            this.LocalLicenseID = localLicenseID;
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.IsActive = isActive;
            this.ApplicationID = ApplicationID;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.CreatedByUserID=CreatedByUserID;
            this.ApplicantID=ApplicantID;
            this.LastStatusDate=LastStatusDate;
            this.ApplicationDate = ApplicationDate;
            this.PaidFees = PaidFees;
        }

        public int InternationalLicenseID;
        //public int ApplicationiD;
        public int DriverID;
        public int LocalLicenseID;
        public DateTime IssueDate;
        public DateTime ExpirationDate;
        public bool IsActive;
        //public new int CreatedByUserId;

        private bool _AddNewInternationaLicense(){
            if (_AddNewApplication())
            {
                this.InternationalLicenseID = clsInternationalDrivingLicenseDataAccess.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.LocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            }
            return (this.InternationalLicenseID != -1);
        }
        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalDrivingLicenseDataAccess.GetAllInternationalDrivingLicense();
        } 
        public static bool IsHasInternationalLicense(int DriverID)
        {
            return clsInternationalDrivingLicenseDataAccess.IsHasInternationalLicense(DriverID);
        }
        public new bool Save()
        {
            return _AddNewInternationaLicense();    
        }
        public static clsInternationalDrivingLicenseBusinessLayer FindByIntLicenseId(int IntLicenseID){
            int ApplicationID = -1;int DriverID = -1;int IssuedUsingLocalLicenseID = -1;DateTime IssueDate = DateTime.Now;DateTime Expirationdate = DateTime.Now;bool IsActive = false;int CreatedByUserID = -1;
            DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1; byte ApplicationStatus = 0; DateTime StatusTime = DateTime.Now; decimal PaidFees = -1;int ApplicantID = -1;
            if (clsInternationalDrivingLicenseDataAccess.GetInternationalLicenseByIntLicenseID(IntLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref Expirationdate, ref IsActive, ref CreatedByUserID))
            {
              if(clsApplicationsDataAccess.GetApplicationInfoByID(ApplicationID, ref ApplicantID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref StatusTime, ref PaidFees, ref CreatedByUserID))
                {
   return new clsInternationalDrivingLicenseBusinessLayer(IntLicenseID,ApplicationID,DriverID,IssuedUsingLocalLicenseID, IssueDate, Expirationdate, IsActive,ApplicationDate,  ApplicationTypeID,  (enApplicationStatus)ApplicationStatus, StatusTime, PaidFees, ApplicantID, CreatedByUserID);
                }
            }
            return  null;
        }
        public static clsInternationalDrivingLicenseBusinessLayer FindByAppID(int ApplicationID)
        {
            int IntLicenseID = -1; int DriverID = -1; int IssuedUsingLocalLicenseID = -1; DateTime IssueDate = DateTime.Now; DateTime Expirationdate = DateTime.Now; bool IsActive = false; int CreatedByUserID = -1;
            DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1; byte ApplicationStatus = 0; DateTime StatusTime = DateTime.Now; decimal PaidFees = -1; int ApplicantID = -1;
            if (clsInternationalDrivingLicenseDataAccess.GetInternationalLicenseByIntAppID(ApplicationID, ref IntLicenseID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref Expirationdate, ref IsActive, ref CreatedByUserID))
            {
                if (clsApplicationsDataAccess.GetApplicationInfoByID(ApplicationID, ref ApplicantID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref StatusTime, ref PaidFees, ref CreatedByUserID))
                {
                    return new clsInternationalDrivingLicenseBusinessLayer(IntLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, Expirationdate, IsActive, ApplicationDate, ApplicationTypeID, (enApplicationStatus)ApplicationStatus, StatusTime, PaidFees, ApplicantID, CreatedByUserID);
                }
            }
            return null;
        }
        public static DataTable GetAllInternationalDrvingLicenseOfPerson(int PersonID)
        {
            return clsInternationalDrivingLicenseDataAccess.GetAllInternationalDrvingLicenseOfPerson(PersonID);
        }
    }
}
