using LicenseDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LicenseBusinessLogicLayer
{
    public class clsApplicationTestsBusinessLayer
    {
        public enum enTestType { VisionTest=1,WrittenTest=2,StreetTest=3};
            public int AppTestID {  get; set; }
            public string TestTypeTitle { get; set; }
            public string TestTypeDescription {  get; set; }
            public decimal TestTypeFees {  get; set; }
        public clsApplicationTestsBusinessLayer() { }
    private  clsApplicationTestsBusinessLayer(int AppTestID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees) {
            this.AppTestID = AppTestID;
            this.TestTypeTitle=TestTypeTitle;
            this.TestTypeDescription=TestTypeDescription;
            this.TestTypeFees=TestTypeFees;
        }
        public static DataTable GetAllApplicationTests()
        {
            return clsApplicationTestsDataAccesscs.GetAllApplicationTests();
        }
        public bool UpdateApplicationTest()
        {
            return clsApplicationTestsDataAccesscs.UpdateApplicationTest(this.AppTestID,this.TestTypeTitle,this.TestTypeDescription,this.TestTypeFees);
        }
        public static clsApplicationTestsBusinessLayer Find(int AppTestID)
        {
            string TestTypeTitle = "";string TestTypeDescription = ""; decimal TestTypeFees = -1;
            if(clsApplicationTestsDataAccesscs.GetApplicationTestsByTestID(AppTestID,ref TestTypeTitle,ref TestTypeDescription,ref TestTypeFees))
            {
                return new clsApplicationTestsBusinessLayer(AppTestID,TestTypeTitle,TestTypeDescription,TestTypeFees);
            }
            else
            {
                return null;
            }
        }
    }
}
