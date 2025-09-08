using LicenseBusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver_VehiclesLicensesDepartmentProject
{
   public static class Global
    {
       public static clsUsersBusinessLayer CurrentUser = clsUsersBusinessLayer.Find("","");
    }
}
