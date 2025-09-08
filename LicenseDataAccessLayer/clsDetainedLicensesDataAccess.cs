using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseDataAccessLayer
{
    public class clsDetainedLicensesDataAccess
    {
        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT DetainedLicenses.DetainID, DetainedLicenses.LicenseID, DetainedLicenses.DetainDate, DetainedLicenses.IsReleased, DetainedLicenses.FineFees, DetainedLicenses.ReleaseDate, People.NationalNo, People.FirstName+' '+People.SecondName+' ' +People.ThirdName+' '+People.LastName as FullName, DetainedLicenses.ReleaseApplicationID FROM DetainedLicenses INNER JOIN Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID INNER JOIN Applications ON Licenses.ApplicationID = Applications.ApplicationID AND Licenses.ApplicationID = Applications.ApplicationID INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    //reader.Read();
                    dataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return dataTable;
        }
        public static bool IsLicenseDetained(int LicenseID) { 
         bool IsDetained=false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            //@ tells compiler to write it as it even it a reserved keyword
            string query = "SELECT Found=1 FROM DetainedLicenses WHERE LicenseID=@LicenseID and IsReleased=0";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj!=null&&int.TryParse(obj.ToString(),out int Rslt))
                {
                   IsDetained=(Rslt==1)?true:false;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return IsDetained;
        }
        public static int AddNewDetainedLicense(int LicenseiD,DateTime DetainDate,decimal FineFees,int CreatedByUserID,bool IsReleased,DateTime? ReleaseDate,int ReleasedByUserID,int ReleaseApplicationID)
        {
            int DetainID = -1;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "INSERT INTO DetainedLicenses VALUES(@LicenseiD,@DetainDate,@FineFees,@CreatedByUserID,@IsReleased,@ReleaseDate,@ReleasedByUserID,@ReleaseApplicationID)SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseiD", LicenseiD);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            command.Parameters.AddWithValue("@ReleaseDate", (ReleaseDate==null)? (object)DBNull.Value : ReleaseDate);
            command.Parameters.AddWithValue("@ReleasedByUserID", (ReleasedByUserID == -1) ? (object)DBNull.Value : ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID",(ReleaseApplicationID==-1) ? (object)DBNull.Value : ReleaseApplicationID);
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Rslt))
                {
                    DetainID =Rslt;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return DetainID;
        }
        public static bool UpdateDetainedLicense(int DetainID, int LicenseiD, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime? ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int rowsAffacted =0;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "UPDATE DetainedLicenses SET LicenseID=@LicenseiD,DetainDate=@DetainDate,FineFees=@FineFees,CreatedByUserID=@CreatedByUserID,IsReleased=@IsReleased,ReleaseDate=@ReleaseDate,ReleasedByUserID=@ReleasedByUserID,ReleaseApplicationID=@ReleaseApplicationID WHERE DetainID=@DetainiD";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainiD", DetainID);
            command.Parameters.AddWithValue("@LicenseiD", LicenseiD);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            command.Parameters.AddWithValue("@ReleaseDate", (ReleaseDate == null) ? (object)DBNull.Value : ReleaseDate);
            command.Parameters.AddWithValue("@ReleasedByUserID", (ReleasedByUserID == -1) ? (object)DBNull.Value : ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", (ReleaseApplicationID == -1) ? (object)DBNull.Value : ReleaseApplicationID);
            try
            {
                connection.Open();
                rowsAffacted= command.ExecuteNonQuery();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return (rowsAffacted>0);
        }
        //it is better to make a fuction to release a detained licens insted of updating all value only update some of these values
        public static bool GetLicensedetainedInfo(int LicenseID,ref int DetainID,ref DateTime DetainDate,ref decimal FineFees,ref int CreatedByUserID,ref bool IsReleased,ref DateTime? ReleaseDate,ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound =false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM DetainedLicenses WHERE LicenseID=@LicenseID and IsReleased=0";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader= command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    DetainID = (int)reader["DetainID"];
                    DetainDate =(DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];
                    if (reader["ReleaseDate"] != DBNull.Value)
                    {
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                    }
                    else
                    {
                        ReleaseDate = null;
                    }
                    if (reader["ReleasedByUserID"] != DBNull.Value)
                    {
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    }
                    else
                    {
                        ReleasedByUserID =-1;
                    }
                    if (reader["ReleaseApplicationID"] != DBNull.Value)
                    {
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    }
                    else
                    {
                        ReleaseApplicationID = -1;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return IsFound;
        }
    }
}
