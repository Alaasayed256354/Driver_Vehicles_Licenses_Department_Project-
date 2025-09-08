using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using LicenseDataAccessLayer;
namespace LicenseBusinessLogicLayer
{
    public class clsDriversBusinesslayer
    {
        public clsDriversBusinesslayer() {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
        }
       public int PersonID { get; set; }
       public clsPerson PersonInfo { get; set; }
      public int CreatedByUserID {  get; set; }
      public  int DriverID { get; set; }
public DateTime CreatedDate {  get; set; }
        private bool _AddNewdriver()
        {
            this.DriverID=clsDriversDataAccess.AddNewDriver(this.PersonID,this.CreatedByUserID,this.CreatedDate);
            return (this.DriverID!=-1);
        }
        public bool Save()
        {
            return _AddNewdriver();
        }
        public static int IsHeDriver(int PersonID)
        {
            return  clsDriversDataAccess.IsHeDriver(PersonID);
        }
        public static DataTable GetAllDrivers()
        {
            return clsDriversDataAccess.GetAllDrivers();
        }
    }
}
