using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace LicenseDataAccessLayer
{
    public class CountryDataAccess
    {
        public static DataTable GetAllCountries()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM Countries";
            SqlCommand command=new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
            }
            catch (Exception ex)
            {
                clsEventLog.HandlingException(ex);

                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dataTable;
        }
        public static bool GetCountryInfoByID(int ID, ref string CountryName)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM Countries WHERE CountryID=@ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    CountryName = reader["CountryName"].ToString();
                }
                IsFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                clsEventLog.HandlingException(ex);
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return IsFound;
        }
    }
}
