using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseDataAccessLayer
{
    public class clsDrivingLicenseServicesDataAccess
    {
        public static int AddNewLocalDrivingLicense()
        {
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "INSERT INTO LocalDrivingLicenseApplications VALUES ()";
        }
    }
}
