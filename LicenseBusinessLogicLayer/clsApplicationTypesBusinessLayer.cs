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
    public class clsApplicationTypesBusinessLayer
    {
        public clsApplicationTypesBusinessLayer() { }
       private clsApplicationTypesBusinessLayer(int ApplicationTypeID , string ApplicationTypeTitle , decimal ApplicationFees ) {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
        }

        public string ApplicationTypeTitle{get; set;}
        public decimal ApplicationFees{get; set;}
        public int ApplicationTypeID { get; set;}

        public static DataTable GetAllApplicationTypes()
        {
           return clsApplicationTypesDataAccess.GetAllApplicartionTypes();
        }
        public bool UpdateApplicationType()
        {
            return clsApplicationTypesDataAccess.UpdateAppTypes(this.ApplicationTypeID,this.ApplicationTypeTitle,this.ApplicationFees);
        }
        public static clsApplicationTypesBusinessLayer Find(int ApplicationTypeID)
        {
             string ApplicationTypeTitle = ""; decimal ApplicationFees =-1;
            if (clsApplicationTypesDataAccess.GetApplicatinTypeInfoByID(ApplicationTypeID,ref ApplicationTypeTitle,ref ApplicationFees))
            {
                return new clsApplicationTypesBusinessLayer(ApplicationTypeID,ApplicationTypeTitle,ApplicationFees);
            }
            else
            {
                return null;
            }
        }
    }
}
