using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using LicenseDataAccessLayer;

namespace LicenseBusinessLogicLayer
{
    public class clsPerson
    {
     private enum enMode { AddNewMode=0,UpdteMode=1};
     private enMode _Mode=enMode.AddNewMode;
    private bool _AddNewPerson()
        {
            this.personID= clsPeopleDataAccess.AddNewPerson(this.NationalNo,this.FirstName,this.Secondname,this.ThirdName,this.LastName,
                                                    this.DateOfBirth,this.Gendor,this.Address,this.Phone,this.Email,this.NationalityCountryID,this.ImagePath);
            return (this.personID!=-1);
        }
     private bool _UpdatePerson()
        {
            return clsPeopleDataAccess.UpdatePersonInfo(this.personID,this.NationalNo,this.FirstName,this.Secondname,this.ThirdName,this.LastName
                                                       ,this.DateOfBirth,this.Gendor,this.Address,this.Phone,this.Email,this.NationalityCountryID,this.ImagePath); 
        }
    public int personID { get; set; }
    public string FirstName { get; set; }
    public string Secondname {  get; set; }
    public string ThirdName {  get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth{ get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string NationalNo { get; set; }
    public int NationalityCountryID { get; set; }
    public byte Gendor {  get; set; }
    public string ImagePath {  get; set; }
     public clsPerson()
        {
            this.personID =-1;
            this.FirstName = "";
            this.Secondname = "";
            this.ThirdName ="";
            this.LastName = "";
            this.DateOfBirth =DateTime.Now;
            this.Email = "";
            this.Phone = "";
            this.Address ="";
            this.NationalNo ="";
            this.NationalityCountryID =-1;
            this.ImagePath = "";
            this.Gendor =0;
            _Mode = enMode.AddNewMode;
        }
      private  clsPerson(int PersonID ,string FirstName, string Secondname, string ThirdName ,
        string LastName, DateTime DateOfBirth , string Email , string Phone,
        string Address,string NationalNo,int NationalityCountryId, byte Gendor, string ImagePath)
        {
            this.personID = PersonID;
            this.FirstName = FirstName;
            this.Secondname = Secondname;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.NationalNo = NationalNo;
            this.NationalityCountryID = NationalityCountryId;
            this.ImagePath = ImagePath;
            this.Gendor = Gendor;
            _Mode = enMode.UpdteMode;//Because We Make Find Before Update
        }
    public static DataTable GetAllPersons()
        {
            return clsPeopleDataAccess.GetAllPersons();
        }
     public static bool DeletePerson(int PersonId)
        {
            return clsPeopleDataAccess.DeletePerson(PersonId);
        }
    public static bool IsExist(int PersonId)
        {
            return clsPeopleDataAccess.IsPersonExist(PersonId);
        }
        public static bool IsExist(string NationalNo)
        {
            return clsPeopleDataAccess.IsPersonExist(NationalNo);
        }
        public static clsPerson Find(int PersonId)
        {
            string FirstName=""; string Secondname="";string ThirdName = "";
            string LastName = "";DateTime DateOfBirth=DateTime.Now;string Email = "";string Phone = "";
            string Address = "";string NationalNo="";int NationalityCountryId=-1;byte Gendor=0;string ImagePath = "";
            if(clsPeopleDataAccess.GetPersonInfoByID(PersonId,ref NationalNo,ref FirstName,ref Secondname,ref ThirdName
                ,ref LastName,ref DateOfBirth,ref Gendor,ref Address,ref Phone,ref Email,ref NationalityCountryId,ref ImagePath)) {
                return new clsPerson(PersonId,FirstName,Secondname,ThirdName,
         LastName,DateOfBirth,Email,Phone,Address,NationalNo,NationalityCountryId,Gendor,ImagePath);
            }
            else
            {
                return null;
            }
        }
        public static clsPerson Find(string NationalNo)
        {
            int PersonId = -1;  string FirstName = ""; string Secondname = ""; string ThirdName = "";
            string LastName = ""; DateTime DateOfBirth = DateTime.Now; string Email = ""; string Phone = "";
            string Address = ""; int NationalityCountryId = -1; byte Gendor = 0; string ImagePath = "";
            if (clsPeopleDataAccess.GetPersonInfoByNationalNo(ref PersonId,NationalNo, ref FirstName, ref Secondname, ref ThirdName
                , ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryId, ref ImagePath))
            {
                return new clsPerson(PersonId, FirstName, Secondname, ThirdName,
         LastName, DateOfBirth, Email, Phone, Address, NationalNo, NationalityCountryId, Gendor, ImagePath);
            }
            else
            {
                return null;
            }
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNewMode:
                    if (_AddNewPerson())
                    {
                        _Mode = enMode.UpdteMode;
                        return true;
                    }
                        return false;
                case enMode.UpdteMode:
                    return (_UpdatePerson());
            }
            return false;
        }
    }
}
