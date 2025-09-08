using LicenseDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseBusinessLogicLayer
{
    public class clsTestsBusinessLayer
    {
        private bool _AddNewTest()
        {
            this.TestID = clsTestsDataAccess.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
            return (this.TestID != -1);
        }
        public int TestAppointmentID {  get; set; }
        public clsTestAppointmentsBusinessLayer TestAppointmentsBusinessLayer { get; set; }
        public  bool TestResult {  get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
        public int TestID {  get; set; }
        public clsTestsBusinessLayer() { 
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = -1;
            this.TestAppointmentID = -1;
        }
      
        public bool Save()
        {
            return _AddNewTest();
        }
        public static bool IsPassedTheTest(int LDLAppID, int TestTypeID)
        {
            return clsTestsDataAccess.IsPassedTheTest(LDLAppID, TestTypeID);
        }
        public static bool IsHeRetakeTest(int LDLAppID, int TestTypeID)
        {
            return clsTestsDataAccess.IsHeRetakeTest(LDLAppID, TestTypeID);
        }
        public static bool IsThisTestWasARetakeTest(int TestAppointmentID, int TestTypeID)
        {
            return clsTestsDataAccess.IsThisTestWasARetakeTest(TestAppointmentID,TestTypeID);
        }
    }
}
