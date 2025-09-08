using LicenseDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LicenseBusinessLogicLayer
{
    public  class clsApplicationsBusinesssLayer
    {
        private enum enMode { AddNewMode = 0, UpdteMode = 1 };
        private enMode _Mode = enMode.AddNewMode;
        public enum enApplicationType {NewDrivingLicense=1,RenewDrivingLicense=2,ReplaceLostDrivingLicense=3,ReplaceDamagedDerivingLicense=4,RealeaseDetainedDrivingLicense=5,NewInternationalDrivingLicense=6,RetakeTest=7};
        public enum enApplicationStatus {New=1,Canceled=2,Completed=3}; 
        public clsApplicationsBusinesssLayer() {
            ApplicantID =-1;
            ApplicationDate = DateTime.Now;
            ApplicationStatus =enApplicationStatus.New;
            CreatedByUserID = -1;
            PaidFees = -1;
            LastStatusDate = DateTime.Now;
            ApplicationTypeID = -1;
            ApplicationID = -1;
            _Mode = enMode.AddNewMode;
        }
        protected clsApplicationsBusinesssLayer(int ApplicationID,int ApplicantID, DateTime ApplicationDate , int ApplicationTypeID,
        enApplicationStatus ApplicationStatus, DateTime StatusTime ,decimal PaidFees,
       int CreatedByUserID)
        {
            this.ApplicationID= ApplicationID;
            this.ApplicationStatus = ApplicationStatus;
            this.ApplicationDate = ApplicationDate;
            this.LastStatusDate = StatusTime;
            this.PaidFees= PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo=clsUsersBusinessLayer.Find(CreatedByUserID);
            this.ApplicantID = ApplicantID;
            this.personInfo=clsPerson.Find(ApplicantID);
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo=clsApplicationTypesBusinessLayer.Find(ApplicationTypeID);  
            _Mode= enMode.UpdteMode;
        }


        public int ApplicationID { get; set; }
        public clsPerson personInfo {
            get; set;//This Caleed Composition To Able to get All Person Info How Made This Application
        }
        public DateTime ApplicationDate { get;set;}
        public decimal PaidFees {get;set;}
        public DateTime LastStatusDate { get;set;}
        public int ApplicantID { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUsersBusinessLayer UserInfo;
        public int ApplicationTypeID { get; set; }
        public clsApplicationTypesBusinessLayer ApplicationTypeInfo;
       public enApplicationStatus ApplicationStatus{ get; set;}
        public string StatusText { 
            get {
                switch (ApplicationStatus) {
                    case enApplicationStatus.New:
                        return "New";
                        case enApplicationStatus.Canceled:
                        return "Canceled";
                        case enApplicationStatus.Completed:
                        return "completed";
                        default:
                        return "UnKnown";
                }
            } 
        }
        protected bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationsDataAccess.AddNewApplication(this.ApplicationDate,this.ApplicantID,(byte)this.ApplicationStatus,this.CreatedByUserID,this.PaidFees,this.LastStatusDate,this.ApplicationTypeID);
            return (this.ApplicationID!=-1);
        }
        public bool _DeleteAppplication()
        {
            return clsApplicationsDataAccess.DeleteApplication(this.ApplicationID);
        }
        protected bool _UpdateApplication()
        {
            return clsApplicationsDataAccess.UpdateApplication(this.ApplicationID, this.ApplicationDate, this.ApplicantID, (byte)this.ApplicationStatus, this.CreatedByUserID, this.PaidFees, this.LastStatusDate, this.ApplicationTypeID);
        }
        public static DataTable GetAllApplications()
        {
            return clsApplicationsDataAccess.GetAllApplications();
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNewMode:
                    if (_AddNewApplication())
                    {
                        _Mode = enMode.UpdteMode;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.UpdteMode:
                    return _UpdateApplication();
            }
            return false;
        }
        public static clsApplicationsBusinesssLayer Find(int AppID)
        {
            int PersonID = -1; DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1;
            byte ApplicationStatus =0; DateTime StatusTime = DateTime.Now; decimal PaidFees = -1;
            int CreatedByUserID = -1;
            if (clsApplicationsDataAccess.GetApplicationInfoByID(AppID, ref PersonID, ref ApplicationDate, ref ApplicationTypeID,ref ApplicationStatus,ref StatusTime, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplicationsBusinesssLayer(AppID, PersonID, ApplicationDate, ApplicationTypeID
        ,(enApplicationStatus) ApplicationStatus, StatusTime, PaidFees,
        CreatedByUserID);
            }
            else
            {
                return null;
            }
        }
    }
}
