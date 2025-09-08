using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace LicenseDataAccessLayer
{
    public class clsApplicationsDataAccess
    {
      public static int AddNewApplication(DateTime ApplicationDate,int ApplicantID,int ApplicationStatus,int CreatedByUserID,decimal PaidFees,DateTime LastStratusDate,int ApplicationTypeID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "INSERT INTO Applications VALUES(@ApplicantID,@ApplicationDate,@ApplicationTypeID,@ApplicationStatus,@LastStatusDate,@PaidFees,@CreatedByUserID) SELECT Scope_Identity();";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantID", ApplicantID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus",ApplicationStatus );
            command.Parameters.AddWithValue("@LastStatusDate", LastStratusDate);
            command.Parameters.AddWithValue("@PaidFees",PaidFees );
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Rslt))
                {
                    ApplicationID = Rslt;
                }
            }
            catch (Exception ex) {throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return ApplicationID;
        }
        public static bool UpdateApplication(int ApplicationID,DateTime ApplicationDate, int ApplicantID, int ApplicationStatus, int CreatedByUserID, decimal PaidFees, DateTime LastStatusDate, int ApplicationTypeID)
        {
            int rowsAffacted = 0;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string Query = "Update Applications set ApplicantPersonID=@ApplicantID,ApplicationDate=@ApplicationDate,ApplicationTypeID=@ApplicationTypeID,ApplicationStatus=@ApplicationStatus,LastStatusDate=@LastStatusDate,PaidFees=@PaidFees,CreatedByUserID=@CreatedByUserID Where ApplicationID=@ApplicationID";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicantID", ApplicantID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                rowsAffacted = command.ExecuteNonQuery();
            }
            catch (Exception messeage) {
                throw new Exception(messeage.Message);
            }
            finally { 
            connection.Close();
            }
            return (rowsAffacted!=0);
        }
        public static bool DeleteApplication(int ApplicationID) { 
            int rowsAffacted = 0;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "delete from Applications where ApplicationID=@ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
               rowsAffacted= command.ExecuteNonQuery();
            }
            catch (Exception messeage)
            {
                throw new Exception(messeage.Message);
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffacted != 0);
        }
        public static DataTable GetAllApplications()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM Applications ORDER BY ApplicationDate";
             SqlCommand command=new SqlCommand(query,connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return dataTable;
        }
        public static bool GetApplicationInfoByID(int AppID,ref int ApplicantID,ref DateTime ApplicationDate,ref int ApplicationTypeID,ref byte ApplicationStatus,ref DateTime LastStatusTime,ref decimal PaidFees,ref int CreatedByUserID) { 
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = " SELECT * FROM Applications WHERE ApplicationID=@ApplicationID";
            SqlCommand command=new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@ApplicationID", AppID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound =true;
                    ApplicantID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusTime = (DateTime)reader["LastStatusDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByuserID"];
                }
                reader.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return IsFound;
        }
    }
}
//SELECT
//    LocalDrivingLicenseApplicationID,
//    LicenseClasses.ClassName AS ClassName,
//    People.NationalNo AS NationalNo,
//    People.FirstName + ' ' + People.SecondName + ' ' + ISNULL(People.ThirdName, '') + ' ' + People.LastName AS FullName,
//    Applications.ApplicationDate,
//    PassedTestCount = (
//        SELECT DISTINCT COUNT(TestAppointments.TestTypeID)
//        FROM TestAppointments 
//        INNER JOIN Tests ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
//        WHERE 
//            TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID 
//            AND Tests.TestResult = 1
//    ),
//    Status =
//    CASE
//        WHEN Applications.ApplicationStatus = 1 THEN 'New'
//        WHEN Applications.ApplicationStatus = 2 THEN 'Canceled'
//        WHEN Applications.ApplicationStatus = 3 THEN 'Completed'
//    END
//FROM 
//    LocalDrivingLicenseApplications 
//    INNER JOIN LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID 
//    INNER JOIN Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID 
//    INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID;
