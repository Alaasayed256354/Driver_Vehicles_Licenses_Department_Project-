using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
namespace LicenseDataAccessLayer
{
   public class clsUserDataAccess
    {
        public static bool GetUserInfoByUserID(int UserID,ref  string UserName,ref int PersonID,ref string Password,ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM Users WHERE UserID=@UserID";
            SqlCommand command=new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    IsActive = (bool)reader["IsActive"];
                    UserName = reader["UserName"].ToString();
                    Password = reader["Password"].ToString();
                    PersonID = (int)reader["PersonID"];
                }
                reader.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
        public static bool GetUserPasswordByUserID(int UserID,ref string Password)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Password FROM Users WHERE UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    Password = reader["Password"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
        public static bool GetUserInfoByUserNameAndPassword(ref int UserID,string UserName,ref int PersonID,  string Password, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM Users WHERE UserName=@UserName AND Password=@Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password",Password);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    UserID = (int)reader["UserID"];
                    IsActive = (bool)reader["IsActive"];
                    PersonID = (int)reader["PersonID"];
                }
                reader.Close();
            }
            catch (Exception ex) {
               
                throw new Exception(ex.Message);  
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
        public static bool UpdatePassword(int UserID,string Password)
        {
            int rowsaffacted = 0;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "UPDATE Users SET Password=@Password WHERE UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                rowsaffacted = command.ExecuteNonQuery();
            }
            catch (Exception ex) {
              clsEventLog.HandlingException(ex);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return (rowsaffacted>0);
        }
        public static DataTable GetAllUsers()
        {
            DataTable dataTable= new DataTable();
            SqlConnection Connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Users.UserID, Users.PersonID, People.FirstName+' '+ People.SecondName+' '+People.ThirdName+' '+ People.LastName as FullName, Users.UserName, Users.IsActive FROM Users INNER JOIN People ON Users.PersonID = People.PersonID";
            SqlCommand command=new SqlCommand(query,Connection);
            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) {
                clsEventLog.HandlingException(ex);

                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
            return dataTable;
        }
        public static bool IsExist(int UserID)
        {
            bool IsExist=false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Found=1 FROM Users WHERE UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsExist = reader.HasRows;
                reader.Close();
            }
            catch(Exception ex)
            {
                clsEventLog.HandlingException(ex);

                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return IsExist;
        }
        public static bool IsExist(string UserName)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Found=1 FROM Users WHERE UserName=@UserName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName",UserName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsExist = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                clsEventLog.HandlingException(ex);

                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return IsExist;
        }
        public static bool IsUserExist(int PersonID)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Found=1 FROM Users WHERE PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID",PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsExist = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                clsEventLog.HandlingException(ex);

                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return IsExist;
        }
        public static bool DeleteUser(int UserID)
        {
            int rowsaffacted = 0;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "DELETE FROM Users WHERE UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                rowsaffacted = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsEventLog.HandlingException(ex);

                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return (rowsaffacted > 0);
        }
        public static int AddNewUser(int PersonID,string UserName,string Password,bool isActive)
        {
            int UserId = -1;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "INSERT INTO Users VALUES(@PersonID,@UserName,@Password,@isActive) SELECT Scope_Identity();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@isActive", isActive);
            try
            {
                connection.Open();
               object obj=command.ExecuteScalar();
                if(obj!=null&&int.TryParse(obj.ToString(),out int Rslt))
                {
                    UserId = Rslt;
                }
            }
            catch (Exception ex)
            {
                clsEventLog.HandlingException(ex);

                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return UserId;
        }
        public static bool UpdateUser(int USerID,int PersonID,string UserName,string Password,bool IsActive)
        {
            int rowsAffacted = 0;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "UPDATE Users SET PersonID=@PersonID,UserName=@UserName,Password=@Password,IsActive=@IsActive WHERE UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", USerID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            try
            {
                connection.Open();
                rowsAffacted = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsEventLog.HandlingException(ex);

                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return (rowsAffacted>0);
        }
    }
}
