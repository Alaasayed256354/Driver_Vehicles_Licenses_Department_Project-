using LicenseDataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseBusinessLogicLayer
{
    public class clsDetainedLicenseBusineesLayer
    {
        private enum enMode { AddNew, Update };
        private enMode _Mode;
        public clsDetainedLicenseBusineesLayer() {
            this.DetainID = -1;
            this.DetainDate = DateTime.Now;
            this.ReleaseDate = null;
            this.ReleaseApplicationID = -1;
            this.LicenseID = -1;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleasedByUserID = -1;
            this.FineFees = -1;
            _Mode = enMode.AddNew;
        }
        private clsDetainedLicenseBusineesLayer(int DetainID, int LicenseiD, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime? ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.DetainDate = DetainDate;
            this.ReleaseDate = ReleaseDate;
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.LicenseID = LicenseiD;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserId=clsUsersBusinessLayer.Find(CreatedByUserID);
            this.IsReleased = IsReleased;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleasedByUserId=clsUsersBusinessLayer.Find(ReleasedByUserID);//Composition
            this.FineFees = FineFees;
            _Mode = enMode.Update;
        }
        // int DetainID;
        //public int Detainid{
        //      get{ return DetainID; }
        //     set { DetainID = value; }
        //}
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUsersBusinessLayer CreatedByUserId {  get; set; }
        public bool IsReleased { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public clsUsersBusinessLayer ReleasedByUserId { get; set; }
        public int ReleaseApplicationID { get; set; }

        private bool _AddnewDetainedLicense()
        {
            this.DetainID = clsDetainedLicensesDataAccess.AddNewDetainedLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
            return (this.DetainID != -1);
        }
        private bool _UpdateReleasedLicense()
        {
            return clsDetainedLicensesDataAccess.UpdateDetainedLicense(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
        }
        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicensesDataAccess.GetAllDetainedLicenses();
        }
        public static bool IsLicensedetained(int DetainID)
        {
            return clsDetainedLicensesDataAccess.IsLicenseDetained(DetainID);
        }
        public bool Save()
        {
            try
            {
                switch (_Mode)
                {
                    case enMode.AddNew:
                        if (_AddnewDetainedLicense())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                   break;
                    case enMode.Update:
                        if(_UpdateReleasedLicense())
                        {
                            return true;
                        }
                        break;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            return false;
        }
        public static clsDetainedLicenseBusineesLayer FindByLicenseID(int LicenseID) {
            int DetainID = -1; DateTime DetainDate=DateTime.Now; decimal FineFees = -1; int CreatedByUserID = -1; bool IsReleased =false; DateTime? ReleaseDate=DateTime.Now; int ReleasedByUserID=-1; int ReleaseApplicationID=-1;
            if (clsDetainedLicensesDataAccess.GetLicensedetainedInfo(LicenseID,ref DetainID,ref DetainDate,ref FineFees,ref CreatedByUserID,ref IsReleased,ref ReleaseDate,ref ReleasedByUserID,ref ReleaseApplicationID))
            {
                return new clsDetainedLicenseBusineesLayer(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            return null;    
         }
    }
}
