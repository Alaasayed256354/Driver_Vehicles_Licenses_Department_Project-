using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseDataAccessLayer
{
    public class clsDriversDataAccess
    {
        public static int AddNewDriver(int  PersonID,int CreatedByUserID,DateTime CreatedDate)
        {
            int DriverID = -1;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "INSERT INTO Drivers VALUES(@PersonID,@CreatedByUserID,@CreatedDate) SELECT SCOPE_IDENTITY();";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Rslt))
                {
                    DriverID = Rslt;
                }
            }
            catch (Exception ex) { 
            throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return DriverID;
        }
        public static int IsHeDriver(int PersonID) { 
            int DriverID=-1;
            SqlConnection connection=new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT DriverID FROM Drivers WHERE PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID",PersonID);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if(obj!=null && int.TryParse(obj.ToString(),out int Rslt))
                {
                    DriverID = Rslt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return DriverID;    
        }
        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Drivers.DriverID, Drivers.PersonID, People.NationalNo, People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as FullName, Drivers.CreatedDate\r\n,(SELECT COUNT(Licenses.LicenseID)as ActiveLicences\r\nFROM Licenses  where  Licenses.IsActive=1 and Licenses.DriverID=Drivers.DriverID)as ActiveLicenses FROM Drivers INNER JOIN  People ON Drivers.PersonID = People.PersonID";
            SqlCommand cmd = new SqlCommand(query, connection); try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return dt;
        }
    }
}
