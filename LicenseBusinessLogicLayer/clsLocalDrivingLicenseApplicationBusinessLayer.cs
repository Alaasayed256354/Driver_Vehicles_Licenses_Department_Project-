
using LicenseDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LicenseBusinessLogicLayer
{
    public  class clsLocalDrivingLicenseApplicationBusinessLayer:clsApplicationsBusinesssLayer
    {
        private enum enMode { AddNewMode = 0, UpdteMode = 1 };
        private enMode _Mode = enMode.AddNewMode;
        public clsLocalDrivingLicenseApplicationBusinessLayer()
        {
            LicenseClassID = -1;
            LDLAppID = -1;
            _Mode = enMode.AddNewMode;
        }
        private clsLocalDrivingLicenseApplicationBusinessLayer(int ApplicantID, DateTime ApplicationDate, int ApplicationTypeID,
        enApplicationStatus ApplicationStatus, DateTime StatusTime, decimal PaidFees,
        int CreatedByUserID, int LDLAppID, int LicenseClassID, int ApplicationID, int PassedTests) : base(ApplicationID, ApplicantID, ApplicationDate, ApplicationTypeID, ApplicationStatus, StatusTime, PaidFees, CreatedByUserID)
        {
            this.PassedTests = PassedTests;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassesBusinessLayer=clsLicenseClassesBusinessLayer.Find(this.LicenseClassID);
            this.LDLAppID = LDLAppID;
            _Mode = enMode.UpdteMode;
        }
        public int LicenseClassID { get; set; }
        public clsLicenseClassesBusinessLayer LicenseClassesBusinessLayer;
        public int PassedTests { get; set;}
        public int LDLAppID { get; set; }
        private bool AddNewLocalDrivingLicenseApplication()
        {
            if (_AddNewApplication() == true)
            {
                this.LDLAppID = clsLocalDrivingLicenseDataAccess.AddNewLocalDrivingLicense(this.ApplicationID, this.LicenseClassID);
            }
            return (this.LDLAppID != -1);
        }
        private bool _UpdateLocalDrivingLicenseApplication()
        {
            if (_UpdateApplication() == true)
            {
                if (clsLocalDrivingLicenseDataAccess.UpadteLocalDrivingLicenseApplication(this.LDLAppID, this.ApplicationID, this.LicenseClassID))
                {
                    return true;
                }
            }
            return false;
        }
        public bool DeleteLocalDrivingLicenseApplication(/*int AppID, int LDLAID*/)
        {
            if (clsLocalDrivingLicenseDataAccess.DeleteLocalDrivingLicenseApplication(this.LDLAppID) == true)
            {
                return _DeleteAppplication();
            }
            return false;
        }
        //Using KeyWord New It Hides The Fuction in Base Class 
        public new bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNewMode:
                    if (AddNewLocalDrivingLicenseApplication())
                    {
                        _Mode = enMode.UpdteMode;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.UpdteMode:
                    return _UpdateLocalDrivingLicenseApplication();
            }
            return false;
        }
        public static DataTable GetAlllocalDrivingLicense()
        {
            return clsLocalDrivingLicenseDataAccess.GetAllLocalDrivingLicenseApplications();
        }
        public static int IsHasAnNewActiveApplication(int PersonID, string ClassName)
        {
            return clsLocalDrivingLicenseDataAccess.IsHasAnNewActiveApplication(PersonID, ClassName);
        }
        public static int IsHasACompletedApplication(int PersonID, string ClassName)
        {
            return clsLocalDrivingLicenseDataAccess.IsHasACompletedApplication(PersonID, ClassName);
        }
        public static bool IsApplicationCanceled(int LDLAID)
        {
            return clsLocalDrivingLicenseDataAccess.IsApplicationCanceled(LDLAID);
        }
        public static byte TotalTrialPerTest(int LDLAID,int TestTypeID)
        {
            return clsLocalDrivingLicenseDataAccess.TotalTrialPerTest(LDLAID,TestTypeID);
        }
        public static clsLocalDrivingLicenseApplicationBusinessLayer FindByLDLAID(int LDLAppID)
        {
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = -1; int CreatedByUserID = -1; int ApplicationID = -1;
            byte ApplicationStatus = 0; DateTime StatusTime = DateTime.Now; decimal PaidFees = -1;
            int LicenseClassID = -1; int PassedTests = -1; int ApplicantID = -1;
            if (clsLocalDrivingLicenseDataAccess.GetLocalDrivingLicenseApplicationByLDLAID(LDLAppID, ref ApplicationID, ref LicenseClassID, ref PassedTests))
            {
                if (clsApplicationsDataAccess.GetApplicationInfoByID(ApplicationID, ref ApplicantID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref StatusTime, ref PaidFees, ref CreatedByUserID))
                {
                    return new clsLocalDrivingLicenseApplicationBusinessLayer(ApplicantID, ApplicationDate, ApplicationTypeID,
                                                                               (enApplicationStatus)ApplicationStatus, StatusTime, PaidFees,
                                                                                CreatedByUserID, LDLAppID, LicenseClassID, ApplicationID, PassedTests);
                }
            }
            return null;
        }
        public static clsLocalDrivingLicenseApplicationBusinessLayer FindByAppID(int ApplicationID)
        {
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = -1; int CreatedByUserID= -1; int LDLAID = -1;
            byte ApplicationStatus = 0; DateTime StatusTime = DateTime.Now; decimal PaidFees = -1;
            int LicenseClassID = -1; int PassedTests = -1; int ApplicantID = -1;
            if (clsLocalDrivingLicenseDataAccess.GetLocalDrivingLicenseApplicationByAppID(ApplicationID, ref LDLAID, ref LicenseClassID, ref PassedTests))
            {
                if (clsApplicationsDataAccess.GetApplicationInfoByID(ApplicationID, ref ApplicantID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref StatusTime, ref PaidFees, ref CreatedByUserID))
                {
                    return new clsLocalDrivingLicenseApplicationBusinessLayer(ApplicantID, ApplicationDate, ApplicationTypeID,
                                                                               (enApplicationStatus)ApplicationStatus, StatusTime, PaidFees,
                                                                                CreatedByUserID, LDLAID, LicenseClassID, ApplicationID, PassedTests);
                }
            }
            return null;
        }
    }
}
