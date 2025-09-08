using LicenseDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LicenseBusinessLogicLayer
{
    public class clsTestAppointmentsBusinessLayer
    {
        enum enMode { AddNew,Update};
        enMode _Mode;
        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentsDataAccess.AddNewAppointment(this.TestTypeID, this.LocalDrivingLicenseAppID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked,this.RetakeTestApplicationID);
            return (this.TestAppointmentID!=-1);
        }
        private bool _UpdateTestAppointment()
        {
          return clsTestAppointmentsDataAccess.UpdateTestAppointment(this.TestAppointmentID,this.TestTypeID,this.LocalDrivingLicenseAppID,this.AppointmentDate, this.PaidFees,this.CreatedByUserID,this.IsLocked,this.RetakeTestApplicationID);
        }
        public int TestAppointmentID { get; set; }
        public int TestTypeID {  get; set; }
        public int LocalDrivingLicenseAppID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees {  get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked {  get; set; }
       public int RetakeTestApplicationID {  get; set; }
        public clsApplicationsBusinesssLayer RetakeTestAppInfo { get; set; }
        public clsTestAppointmentsBusinessLayer() {
            this.TestTypeID = -1;
            this.LocalDrivingLicenseAppID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = -1;
            this.CreatedByUserID = -1;
            this.IsLocked =true;
            this.RetakeTestApplicationID = -1;
            _Mode = enMode.AddNew;
        }
        private clsTestAppointmentsBusinessLayer(int TestAppointmentId,int TestTypeID, int LocalDrivingLicenseApplicationID ,DateTime AppointmentDate ,decimal PaidFees,int createdByuserID,bool IsLocked,int RetakeTestApplicationID)
        {
            this.TestAppointmentID=TestAppointmentId;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseAppID =LocalDrivingLicenseApplicationID;
            this.AppointmentDate =AppointmentDate;
            this.PaidFees =PaidFees;
            this.CreatedByUserID =createdByuserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID=RetakeTestApplicationID;
            this.RetakeTestAppInfo=clsApplicationsBusinesssLayer.Find(RetakeTestApplicationID);
            _Mode = enMode.Update;
        }
        public static clsTestAppointmentsBusinessLayer Find(int TestAppointmentId)
        {
          int TestTypeID = -1;  int LocalDrivingLicenseApplicationID = -1;  DateTime AppointmentDate = DateTime.Now; int CreatedByUserID = -1; decimal PaidFees = -1;  bool IsLocked = false;int restaketestApplicationID = -1;
            if (clsTestAppointmentsDataAccess.GetTestAppointmentByID(TestAppointmentId,ref TestTypeID,ref LocalDrivingLicenseApplicationID,ref AppointmentDate,ref PaidFees,ref CreatedByUserID,ref IsLocked,ref restaketestApplicationID))
            {
                return new clsTestAppointmentsBusinessLayer( TestAppointmentId,  TestTypeID,  LocalDrivingLicenseApplicationID, AppointmentDate,  PaidFees,CreatedByUserID,  IsLocked,restaketestApplicationID);
            }
            else
            {
                return null;
            }
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if(_AddNewTestAppointment())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                       return false;
                    }
                case enMode.Update:
                    return _UpdateTestAppointment();
            }
            return false;
        }
        public static bool IsHasPreviousOpenAppointment(int LDLAppID,int TestTypeID)
        {
            return clsTestAppointmentsDataAccess.IsHasAnPreviousOpenAppointment(LDLAppID,TestTypeID);
        }
        public static DataTable GetAppointmentsOFGivenPersonOfGivenTestType(int LDLAppID, int TestTypeID)
        {
            return clsTestAppointmentsDataAccess.GetAppointmentsOfAGivenPersonOfAGivenTestType(LDLAppID,TestTypeID);
        }
        public static bool IsTestClosed(int TestAppointmentID)
        {
            return clsTestAppointmentsDataAccess.IsTestClosed(TestAppointmentID);
        }
        public static  DateTime? GetLastAppointmentOfPersonOfAGivenTestType(int LDLAppID, int TestTypeID)
        {
            return clsTestAppointmentsDataAccess.GetLastAppointmentOfPersonOfAGivenTestType(LDLAppID, TestTypeID);
        }
    }
}
