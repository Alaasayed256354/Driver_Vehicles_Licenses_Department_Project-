using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LicenseDataAccessLayer
{
    public class clsTestsDataAccess
    {
        public static int AddNewTest(int TestAppointmentID,bool TestResult,string Notes,int CreatedByUserID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string QUERY = "INSERT INTO Tests VALUES(@TestAppointmentID,@TestResult,@Notes,@CreatedByUserID) SELECT SCOPE_IDENTITY();";
            SqlCommand command=new SqlCommand(QUERY, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if(obj!=null&&int.TryParse(obj.ToString(),out int Rslt))
                {
                    TestID = Rslt;
                }
            }
            catch(Exception ex) {
                clsEventLog.HandlingException(ex);

                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return TestID;
        }
        public static bool IsPassedTheTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool IsHas = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Found=1 FROM   Tests INNER JOIN TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID INNER JOIN  LocalDrivingLicenseApplications ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID AND TestAppointments.IsLocked=1 AND Tests.TestResult=1 And TestAppointments.TestTypeID=@TestTypeID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Rslt))
                {
                    IsHas = (Rslt == 1) ? true:false ;
                }
            }
            catch (Exception ex) {
                clsEventLog.HandlingException(ex);

            }
            finally { connection.Close(); }
            return IsHas;
        }
        public static bool IsHeRetakeTest(int LDLAppID,int TestTypeID)
        {
            bool IsRetakeTest= false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT  Found=1 FROM TestAppointments WHERE TestAppointments.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID AND TestAppointments.TestTypeID=@TestTypeID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
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
        public static bool IsThisTestWasARetakeTest(int TestAppointmentID, int TestTypeID)
        {
            bool IsRetakeTest = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Top 1 Found=1 FROM TestAppointments   WHERE TestAppointments.TestAppointmentID=@TestAppointmentID AND TestAppointments.TestTypeID=@TestTypeID AND TestAppointments.RetakeTestApplicationID is not null;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
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
