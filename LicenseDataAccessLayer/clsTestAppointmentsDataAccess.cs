using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Diagnostics;
namespace LicenseDataAccessLayer
{
    public class clsTestAppointmentsDataAccess
    {
        public static int AddNewAppointment(int TestTypeID,int LocalDrivingLicenseApplicationID,DateTime AppointmentDate,decimal PaidFees,int CreateaedByUserID,bool IsLocked,int RetakeTestApplicationID)
        {
            int AppointmentID = -1;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "INSERT INTO TestAppointments VALUES(@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees,@CreateaedByUserID,@IsLocked,@RetakeTestApplicationID) SELECT Scope_Identity();"; ;
            SqlCommand command =new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreateaedByUserID", CreateaedByUserID);
            command.Parameters.AddWithValue("@RetakeTestApplicationID", (RetakeTestApplicationID==-1)? (object)DBNull.Value: RetakeTestApplicationID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(),out int rslt))
                    {
                    AppointmentID = rslt;
                    }
            }
            catch(Exception ex) {
                clsEventLog.HandlingException(ex);

                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return AppointmentID;
        }
        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees,int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int rowsaffacted = 0;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "UPDATE TestAppointments SET TestTypeID=@TestTypeID,LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID,AppointmentDate=@AppointmentDate,PaidFees=@PaidFees,CreatedByUserID=@CreatedByUserID,IsLocked=@IsLocked,RetakeTestApplicationID=@RetakeTestApplicationID WHERE TestAppointmentID=@TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@RetakeTestApplicationID", ((RetakeTestApplicationID==-1)?(object)DBNull.Value:RetakeTestApplicationID));
            try
            {
                connection.Open();
                 rowsaffacted=command.ExecuteNonQuery();
            }
            catch (Exception ex) {
                clsEventLog.HandlingException(ex);

                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return (rowsaffacted>0);
        }
        public static bool GetTestAppointmentByID(int TestAppointmentId, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID,ref DateTime AppointmentDate,ref decimal PaidFees,ref int CreatedByUserID,ref bool IsLocked,ref int RetakeTestApplicationID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentId=@TestAppointmentId;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentId", TestAppointmentId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsExist = true;
                    TestTypeID =(int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID =(int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate =(DateTime)reader["AppointmentDate"];
                    PaidFees =(decimal)reader["PaidFees"];
                    CreatedByUserID =(int)reader["CreatedByUserID"];
                    IsLocked =(bool)reader["IsLocked"];
                    if (reader["RetakeTestApplicationID"] ==DBNull.Value)
                    {
                        RetakeTestApplicationID = -1;
                    }
                    else
                    {
                        RetakeTestApplicationID=(int)reader["RetakeTestApplicationID"];
                    }
                    reader.Close();
                }
            }
            catch (Exception ex) {
                clsEventLog.HandlingException(ex);

                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return IsExist;
        }
        public static bool IsHasAnPreviousOpenAppointment(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            bool IsHas = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Found=1 FROM TestAppointments INNER JOIN LocalDrivingLicenseApplications ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID AND TestAppointments.IsLocked=0 AND TestAppointments.TestTypeID=@TestTypeID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if(obj!=null&&int.TryParse(obj.ToString(),out int Rslt))
                {
                    IsHas = true;
                }
            }
            catch (Exception ex) {
                clsEventLog.HandlingException(ex);

                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return IsHas;
        }
        public static DataTable GetAppointmentsOfAGivenPersonOfAGivenTestType(int LDLAppID,int TestTypeID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM TestAppointments WHERE LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID AND TestTypeID=@TestTypeID ORDER BY TestAppointmentID desc;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
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
            catch (Exception ex) {
                clsEventLog.HandlingException(ex);

                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return dataTable;
        }
        public static DateTime? GetLastAppointmentOfPersonOfAGivenTestType(int LDLAppID, int TestTypeID)
        {
            DateTime? Appointmentdt=null;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = " select top 1 AppointmentDate from TestAppointments where LocalDrivingLicenseApplicationID in(@LocalDrivingLicenseApplicationID) and TestTypeID=@TestTypeID order by TestAppointmentID desc;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                object obj= command.ExecuteScalar();
                if (obj!=null&&DateTime.TryParse(obj.ToString(),out DateTime dt))
                {
                   Appointmentdt = dt;
                }
                else
                {
                    Appointmentdt = null;
                }
            }
            catch (Exception ex) {
                clsEventLog.HandlingException(ex);

            }
            finally { connection.Close(); }
            return Appointmentdt;
        }
        public static bool IsTestClosed(int TestAppointmentID)
        {
            bool IsRetakeTest = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Found=1 FROM TestAppointments WHERE TestAppointments.IsLocked=1 AND TestAppointments.TestAppointmentID=@TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Rslt))
                {
                    IsRetakeTest = (Rslt == 1) ? true : false;
                }
            }
            catch (Exception ex) {
                clsEventLog.HandlingException(ex);

                throw new Exception(ex.Message); 
            }
            finally { connection.Close(); }
            return IsRetakeTest;
        }
    }
}
