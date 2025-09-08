using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LicenseDataAccessLayer
{
    public class clsLicensesDataAccess
    {
        public static int AddNewLicense(int ApplicationID,int DriverID,int LicenseClass,DateTime IssueDate,DateTime ExpirationDate,string Notes,decimal PaidFees,bool IsActive,int IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "INSERT INTO Licenses VALUES(@ApplicationID,@DriverID,@LicenseClass,@IssueDate,@ExpirationDate,@Notes,@PaidFees,@IsActive,@IssueReason,@CreatedByUserID) SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@Notes", (Notes == "") ? (object)DBNull.Value : Notes);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason ", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Rslt))
                {
                    LicenseID = Rslt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return LicenseID;
        }
        public static bool IsHasAnActiveLicenseForThisLicenseClass(string ClassName,int PersonID)
        {
            bool Has = false;
            SqlConnection connectionn = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT 1 AS Found FROM Licenses INNER JOIN Applications ON Licenses.ApplicationID = Applications.ApplicationID INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID INNER JOIN LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID WHERE People.PersonID =@PersonID  AND LicenseClasses.ClassName =@ClassName AND Licenses.IsActive = 1;";
            SqlCommand command=new SqlCommand(query, connectionn);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            try
            {
                connectionn.Open();
                object obj = command.ExecuteScalar();
                if(obj!=null && int.TryParse(obj.ToString(),out int Rslt))
                {
                    Has = (Rslt == 1) ? true : false;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            finally { connectionn.Close(); }    
            return Has;
        }
        public static bool IsLicenseExpired(int LicenseId)
        {
            bool IsExpired = false;
            SqlConnection connectionn = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT 1 AS FOUND FROM Licenses WHERE Licenses.LicenseID=@LicenseID AND Licenses.ExpirationDate<GETDATE();";
            SqlCommand command = new SqlCommand(query, connectionn);
            command.Parameters.AddWithValue("@LicenseID", LicenseId);
            try
            {
                connectionn.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Rslt))
                {
                    IsExpired = (Rslt == 1) ? true : false;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            finally { connectionn.Close(); }
            return IsExpired;
        }
        
        public static bool GetLicenseInfoByAppID(int ApplicationID, ref int LicenseID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedBuUserID)
        {
            bool IsFind = false;
            SqlConnection connectionn = new SqlConnection(ConnectionString.GetConnectionString);
            string qyery = "SELECT LicenseID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes,PaidFees, IsActive, IssueReason,CreatedByUserID FROM Licenses WHERE ApplicationID=@ApplicationID";
            SqlCommand command = new SqlCommand(qyery, connectionn);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connectionn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    IsFind = reader.HasRows;
                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = (reader["Notes"] == (object)DBNull.Value) ? "" : (string)reader["Notes"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedBuUserID = (int)reader["CreatedByUserID"];
                    PaidFees = (decimal)reader["PaidFees"];
                }
                reader.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            finally { connectionn.Close(); }
            return IsFind; 
        }
        public static bool GetLicenseInfoByLicenseID( int LicenseID,ref int ApplicationID , ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes,ref decimal PaidFees ,ref bool IsActive, ref byte IssueReason,ref int CreatedBuUserID)
        {
            bool IsFind = false;
            SqlConnection connectionn = new SqlConnection(ConnectionString.GetConnectionString);
            string qyery = "SELECT ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees,IsActive, IssueReason,CreatedByUserID FROM Licenses WHERE LicenseID=@LicenseID";
            SqlCommand command = new SqlCommand(qyery, connectionn);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connectionn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFind = reader.HasRows;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    Notes = (reader["Notes"] == (object)DBNull.Value) ? "" : (string)reader["Notes"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedBuUserID = (int)reader["CreatedByUserID"];
                    PaidFees = (decimal)reader["PaidFees"];
                }
                reader.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
            finally { connectionn.Close(); }
            return IsFind;
        }
        public static DataTable GetAllLocalDrvingLicenseOfPerson(int PersonID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Licenses.LicenseID, Applications.ApplicationID, LicenseClasses.ClassName, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive FROM     Licenses INNER JOIN  Applications ON Licenses.ApplicationID = Applications.ApplicationID INNER JOIN  People ON Applications.ApplicantPersonID = People.PersonID INNER JOIN  LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID where People.PersonID=@PersonID and (Applications.ApplicationTypeID in(1,2,3,4)) ORDER BY Licenses.LicenseID desc";
            SqlCommand command=new SqlCommand(query, connection);
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
        public static bool Updatelicense(int licenseID,int ApplicationID, int DriverID,int LicenseClass,DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = @"Update Licenses set ApplicationID = @ApplicationID,DriverID = @DriverID,LicenseClass=@LicenseClass,IssueDate=@IssueDate,ExpirationDate=@ExpirationDate,Notes=@Notes,PaidFees=@PaidFees,IsActive=@IsActive,IssueReason=@IssueReason,CreatedByUserID=@CreatedByUserID where LicenseiD=@LicenseiD";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseiD", licenseID);
            command.Parameters.AddWithValue("ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("DriverID", DriverID);
            command.Parameters.AddWithValue("LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("IssueDate", IssueDate);
            command.Parameters.AddWithValue("ExpirationDate",ExpirationDate);
            command.Parameters.AddWithValue("Notes",(Notes=="")?(object)DBNull.Value:Notes);
            command.Parameters.AddWithValue("PaidFees",PaidFees);
            command.Parameters.AddWithValue("IsActive",IsActive);
            command.Parameters.AddWithValue("IssueReason",IssueReason);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }
        public static bool IsLicenseActive(int LicenseID)
        {
            bool IsActive = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Found=1 FROM Licenses WHERE licenseID=@LicenseID AND IsActive=1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int rslt))
                {
                    IsActive = (rslt == 1) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return IsActive;
        }
        public static bool IsLicenseExist(int AppID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Found=1 FROM Licenses WHERE ApplicationID=@AppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int rslt))
                {
                    IsExist = (rslt == 1) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return IsExist;
        }
        //public static DataTable GetAllInternationalDrvingLicenseOfPerson(int PersonID)
        //{
        //    DataTable dt = new DataTable();
        //    SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
        //    string query = "SELECT Licenses.LicenseID, Applications.ApplicationID, LicenseClasses.ClassName, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive FROM     Licenses INNER JOIN  Applications ON Licenses.ApplicationID = Applications.ApplicationID INNER JOIN  People ON Applications.ApplicantPersonID = People.PersonID INNER JOIN  LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID where People.PersonID=@PersonID and Applications.ApplicationTypeID=6";
        //    SqlCommand command = new SqlCommand(query, connection);
        //    command.Parameters.AddWithValue("@PersonID", PersonID);
        //    try
        //    {
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        dt.Load(reader);
        //        reader.Close();
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message, ex); }
        //    finally { connection.Close(); }
        //    return dt;
        //}
    }
}
