using System;
using System.Data;
using System.Data.SqlClient;

namespace LicenseDataAccessLayer
{
    public class clsPeopleDataAccess
    {
        public static bool GetPersonInfoByID(int PersonId,ref string NationalNo,ref string FirstName,ref string SecondName,ref string ThirdName,ref string LastName,ref DateTime DateOfBirth
                      ,ref byte Gendor,ref string Address,ref string Phone,ref string Email,ref int NationalityCountryID,ref string ImagePath)
        {
            bool IsExist = false;
            SqlConnection connection=new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM People WHERE PersonID=@PersonId;";
            SqlCommand command=new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@PersonId",PersonId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    IsExist = true;
                    NationalNo =(string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    if (reader["Email"]!= DBNull.Value){
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                    reader.Close();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { connection.Close(); }
            return IsExist;
        }
        public static bool GetPersonInfoByNationalNo(ref int PersonId, string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth
                      , ref byte Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsExist = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT * FROM People WHERE NationalNo=@NationalNo;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo",NationalNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    IsExist = true;
                    PersonId= (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                    reader.Close(); 
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { connection.Close(); }
            return IsExist;
        }
        public static int AddNewPerson(string NationalNo,string FirstName,string SecondName,string ThirdName,string LastName,DateTime DateOfBirth
                      ,byte Gendor,string Address,string Phone,string Email,int NationalityCountryID,string ImagePath)
        {
            int PersonID = -1;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "INSERT INTO People VALUES (@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,@Gendor,@Address,@Phone,@Email,@NationalityCountryID,@ImagePath)" +
                "SELECT Scope_Identity();";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName",FirstName);
            command.Parameters.AddWithValue("@SecondName",SecondName);
            command.Parameters.AddWithValue("@ThirdName",ThirdName);
            command.Parameters.AddWithValue("@LastName",LastName);
            command.Parameters.AddWithValue("@DateOfBirth",DateOfBirth);
            command.Parameters.AddWithValue("@Gendor",Gendor);
            command.Parameters.AddWithValue("@Address",Address);
            command.Parameters.AddWithValue("@Phone",Phone);
            if (Email == "")
            {
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", Email);
            }
            command.Parameters.AddWithValue("@NationalityCountryID",NationalityCountryID);
            if (ImagePath == "")
            {
                command.Parameters.AddWithValue("@ImagePath",DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int Rslt))
                {
                    PersonID = Rslt;
                }
            }
            catch (Exception ex) { 
              Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return PersonID;
        }
        public static DataTable GetAllPersons()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            String QUERY = "SELECT * FROM People;";
            SqlCommand command = new SqlCommand(QUERY, connection);
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
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally {connection.Close(); }
            return dataTable;
        }
        public static bool UpdatePersonInfo(int PersonID,string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth
                      , byte Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int rowsaffacted = 0;
            SqlConnection connection=new SqlConnection(ConnectionString.GetConnectionString);
            string query="UPDATE People SET NationalNo=@NationalNo,FirstName=@FirstName,SecondName=@SecondName,ThirdName=@ThirdName,LastName=@LastName,DateOfBirth=@DateOfBirth,Gendor=@Gendor,Address=@Address,Phone=@Phone,Email=@Email,NationalityCountryID=@NationalityCountryID,ImagePath=@ImagePath WHERE PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@PersonID",PersonID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName",SecondName);
            command.Parameters.AddWithValue("@ThirdName",ThirdName);
            command.Parameters.AddWithValue("@LastName",LastName);
            command.Parameters.AddWithValue("@DateOfBirth",DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone",Phone);
            command.Parameters.AddWithValue("@Email",Email);
            command.Parameters.AddWithValue("@NationalityCountryID",NationalityCountryID);
            if (ImagePath !="")
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath",System.DBNull.Value);
            }
            try
            {
                connection.Open();
                rowsaffacted= command.ExecuteNonQuery();
            }
            catch (Exception ex) {Console.WriteLine(ex.Message); }
            finally { connection.Close(); }
            return (rowsaffacted > 0);
        }
        public static bool DeletePerson(int PersonId)
        {

            int rowsaffacted = 0;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = @"DELETE FROM People WHERE PersonID=@PersonId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonId", PersonId);
            try
            {
                connection.Open();
                rowsaffacted = command.ExecuteNonQuery();
            }
            catch (Exception ex) {Console.WriteLine(ex.Message); }
            finally { connection.Close(); }
            return (rowsaffacted > 0);
        }
        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Found=1 FROM People WHERE PersonID=@PersonID";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { connection.Close(); }
            return isFound;
        }
        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString.GetConnectionString);
            string query = "SELECT Found=1 FROM People WHERE NationalNo=@NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { connection.Close(); }
            return isFound;
        }
    }
}
