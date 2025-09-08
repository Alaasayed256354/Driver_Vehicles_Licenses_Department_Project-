using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace LicenseDataAccessLayer
{
    public class clsApplicationTypesDataAccess
    {
        public static DataTable GetAllApplicartionTypes()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM ApplicationTypes";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                   // reader.Read(); beacause it starts from the second row
                    dataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return dataTable;
        }
        public static bool UpdateAppTypes(int AppTypeID, string ApplicationTypeTitle, decimal ApplicationFees)
        {
            
                int rowsaffacted = 0;
                SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
                string query = "UPDATE ApplicationTypes SET ApplicationTypeTitle=@ApplicationTypeTitle,ApplicationFees=@ApplicationFees WHERE ApplicationTypeID=@ApplicationTypeID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
                command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
                command.Parameters.AddWithValue("@ApplicationTypeID", AppTypeID);
                try
                {
                    connection.Open();
                    rowsaffacted = command.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                finally { connection.Close(); }
                return (rowsaffacted > 0);
            }
        public static bool GetApplicatinTypeInfoByID(int ApplicationTypeID,ref string ApplicationTypeTitle, ref decimal ApplicationFees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID=@ApplicationTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    ApplicationTypeTitle = reader["ApplicationTypeTitle"].ToString();
                    ApplicationFees = (decimal)reader["ApplicationFees"];
                }
                reader.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { connection.Close(); }
            return IsFound;
        }

        }
}