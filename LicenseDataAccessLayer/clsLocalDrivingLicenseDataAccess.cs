using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseDataAccessLayer
{
    public class clsLocalDrivingLicenseDataAccess
    {
        public static int AddNewLocalDrivingLicense(int ApplicationID, int LicenseClassID)
        {

            int LocalDrivingLicenseID = -1;
            // int ApplicationID = AddNewDrivingLicenseApplication(ApplicationDate,ApplicantID,ApplicationStatus,CreatedByUserID,PaidFees,LastStratusDate,ApplicationTypeID);
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "INSERT INTO LocalDrivingLicenseApplications VALUES(@ApplicationID,@LicenseClassID) SELECT Scope_Identity();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Rslt))
                {
                    LocalDrivingLicenseID = Rslt;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); };
            return LocalDrivingLicenseID;
        }
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM LocalDrivingLicenseApplications_View";
            SqlCommand command = new SqlCommand(query, connection);
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
        public static int IsHasAnNewActiveApplication(int PersonID, string ClassName)
        {
            int AppID = -1;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Applications.ApplicationID FROM  Applications INNER JOIN LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN  LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID where People.PersonID=@PersonID  and LicenseClasses.ClassName=@ClassName and Applications.ApplicationStatus=1;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Rslt))
                {
                    AppID = Rslt;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return AppID;
        }
        public static int IsHasACompletedApplication(int PersonID, string ClassName)
        {
            int AppID = -1;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Applications.ApplicationID FROM  Applications INNER JOIN LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN  LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID where People.PersonID=@PersonID  and LicenseClasses.ClassName=@ClassName and Applications.ApplicationStatus=3;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Rslt))
                {
                    AppID = Rslt;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return AppID;
        }
        public static bool IsApplicationCanceled(int LDLAID)
        {
            bool IsCanceled = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Found=1 FROM Applications INNER JOIN LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@LDLAID and Applications.ApplicationStatus=2;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAID", LDLAID);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int rslt))
                {
                    IsCanceled = (rslt == 1) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return IsCanceled;
        }
        public static bool GetLocalDrivingLicenseApplicationByLDLAID(int LDLAppID, ref int ApplicationID, ref int LicenseClass, ref int PassedTests)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT LocalDrivingLicenseApplications.LicenseClassID, Applications.ApplicationID, PassedTestCount = (SELECT DISTINCT COUNT(TestAppointments.TestTypeID)FROM TestAppointments INNER JOIN Tests ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID WHERE TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID  AND Tests.TestResult = 1) FROM LocalDrivingLicenseApplications INNER JOIN Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID INNER JOIN LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID WHERE LocalDrivingLicenseApplicationID=@LDLAppID";
            ;
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = reader.HasRows;
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClass = (int)reader["LicenseClassID"];
                    PassedTests = (int)reader["PassedTestCount"];
                }
                reader.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return IsFound;
        }
        public static bool GetLocalDrivingLicenseApplicationByAppID(int ApplicationID, ref int LDLAppID, ref int LicenseClass, ref int PassedTests)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT LocalDrivingLicenseApplications.LicenseClassID,LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, PassedTestCount = (SELECT DISTINCT COUNT(TestAppointments.TestTypeID)FROM TestAppointments INNER JOIN Tests ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID WHERE TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID  AND Tests.TestResult = 1) FROM LocalDrivingLicenseApplications INNER JOIN Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID INNER JOIN LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID WHERE LocalDrivingLicenseApplications.ApplicationID=@AppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound =true;
                    LDLAppID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClass = (int)reader["LicenseClassID"];
                    PassedTests = (int)reader["PassedTestCount"];
                }
                reader.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return IsFound;
        }
        public static bool UpadteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);

            string query = @"Update  LocalDrivingLicenseApplications  
                            set ApplicationID = @ApplicationID,
                                LicenseClassID = @LicenseClassID
                            where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("LicenseClassID", LicenseClassID);


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
        public static bool DeleteLocalDrivingLicenseApplication(int LDLAID)
        {
            int rowsAffacted = 0;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "delete from LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID=@LDLAID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAID", LDLAID);
            try
            {
                connection.Open();
                rowsAffacted = command.ExecuteNonQuery();
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
        public static byte TotalTrialPerTest(int LDLAID, int TestTypeID) { 
        byte totalTrialPerTest = 0;
            SqlConnection connectionn=new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT COUNT(TestAppointments.TestAppointmentID) FROM LocalDrivingLicenseApplications INNER JOIN TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID  INNER JOIN  Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID INNER JOIN TestTypes ON TestAppointments.TestTypeID = TestTypes.TestTypeID WHERE TestTypes.TestTypeID=@TestTypeID and LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@LDLAppID;";
            SqlCommand command=new SqlCommand(query, connectionn);
            command.Parameters.AddWithValue("@TestTypeID",TestTypeID);
            command.Parameters.AddWithValue("@LDLAppID",LDLAID);
            try
            {
                connectionn.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Rslt))
                {
                    totalTrialPerTest = Convert.ToByte(Rslt);
                }
            }
            catch (Exception messeage) {throw new Exception(messeage.Message); }
            finally { connectionn.Close(); }
            return totalTrialPerTest;
        }

    }
    }
