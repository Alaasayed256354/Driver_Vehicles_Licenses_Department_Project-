using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace LicenseDataAccessLayer
{
    public class clsApplicationTestsDataAccesscs
    {
      public static DataTable GetAllApplicationTests()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM TestTypes";
            SqlCommand command=new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dataTable.Load(reader);
                reader.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return dataTable;
        }
        public static bool GetApplicationTestsByTestID(int AppTestID,ref string TestTypeTitle,ref string TestTypeDescription,ref decimal TestTypeFees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM TestTypes WHERE TestTypeID=@TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", AppTestID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = reader.HasRows;
                    TestTypeDescription = reader["TestTypeDescription"].ToString();
                    TestTypeFees = (decimal)reader["TestTypeFees"];
                    TestTypeTitle = reader["TestTypeTitle"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return IsFound;
        }
        public static bool UpdateApplicationTest(int AppTestID,string TestTypeTitle,string TestTypeDescription,decimal TestTypeFees)
        {
            int rowsaffacted = 0;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "UPDATE TestTypes SET TestTypeTitle=@TestTypeTitle,TestTypeDescription=@TestTypeDescription,TestTypeFees=@TestTypeFees WHERE TestTypeID=@TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription",TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("@TestTypeID", AppTestID);
            try
            {
                connection.Open();
                rowsaffacted = command.ExecuteNonQuery();   
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return (rowsaffacted > 0);
        }
    }
    }
