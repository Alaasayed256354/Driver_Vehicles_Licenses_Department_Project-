using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LicenseDataAccessLayer
{
    public class clsInternationalDrivingLicenseDataAccess
    {
        public static DataTable GetAllInternationalDrivingLicense()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive FROM  InternationalLicenses";
            SqlCommand command=new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)//because reader.read doesn't read the first record
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return dt;
        }
        public static DataTable GetAllInternationalDrvingLicenseOfPerson(int PersonID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT InternationalLicenses.* FROM InternationalLicenses INNER JOIN Drivers ON InternationalLicenses.DriverID = Drivers.DriverID INNER JOIN People ON Drivers.PersonID = People.PersonID WHERE People.PersonID=@PersonID and IsActive=1 ;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            finally { connection.Close(); }
            return dt;
        }
        public static int AddNewInternationalLicense( int ApplicationID,int DriverID,int IssuedUsingLocalLicenseID,DateTime IssueDate, DateTime ExpirationDate,bool IsActive,int CreatedByUserID)
        {
            int ILID=-1;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "Update InternationalLicenses set IsActive=0  INSERT INTO InternationalLicenses VALUES(@ApplicationID,@DriverID,@IssuedUsingLocalLicenseID,@IssueDate,@ExpirationDate,@IsActive,@CreatedByUserID) SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate); 
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                object OBJ=command.ExecuteScalar();
                if (OBJ!=null&&int.TryParse(OBJ.ToString(),out int rslt))
                {
                   ILID= rslt;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return ILID;
        }
        public static bool IsHasInternationalLicense(int DriverID)
        {
            bool IsHas = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            //string query = "SELECT 1 AS FOUND FROM InternationalLicenses INNER JOIN Applications ON InternationalLicenses.ApplicationID = Applications.ApplicationID INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID WHERE People.NationalNo=@NationalNo;";
            string query = "SELECT Found = 1 FROM InternationalLicenses  where DriverID = @DriverID and InternationalLicenses.IsActive = 1";
            SqlCommand command= new SqlCommand(query, connection);  
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                object OBJ = command.ExecuteScalar();
                if (OBJ != null && int.TryParse(OBJ.ToString(), out int rslt))
                {
                   IsHas=(rslt==1)?true:false;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return IsHas;
        }
        public static bool GetInternationalLicenseByIntLicenseID(int IntLicenseID,ref int ApplicationID,ref int DriverID,ref int IssuedUsingLocalLicenseID,ref DateTime IssueDate,ref DateTime Expirationdate,ref bool IsActive,ref int CreatedByUserID)
        {
            bool IsFound=false;
            SqlConnection connection= new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM InternationalLicenses WHERE InternationalLicenseID=@InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID",IntLicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound=true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    Expirationdate = (DateTime)reader["Expirationdate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return IsFound;
        }
        public static bool GetInternationalLicenseByIntAppID(int ApplicationID, ref int IntLicenseID, ref int DriverID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime Expirationdate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM InternationalLicenses WHERE ApplicationID=@ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    IntLicenseID = (int)reader["InterNationalLicenseID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    Expirationdate = (DateTime)reader["Expirationdate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return IsFound;
        }
    }
}
