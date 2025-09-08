using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicenseDataAccessLayer;
namespace LicenseBusinessLogicLayer
{
   public class clsUsersBusinessLayer
    { 

        private enum enMode { AddNew,UpdateUser};
       private enMode _Mode;
        public int UserID {  get; set; }
        public string UserName { get; set; }    
        public string Password { get; set; }
        public int PersonID {  get; set; }
        public bool IsActive {  get; set; }
     public   clsUsersBusinessLayer() {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.PersonID = -1;
            this.IsActive = false;
            _Mode = enMode.AddNew;
        }
        private clsUsersBusinessLayer(int UserID, string UserName, string Password,int PersonID , bool IsActive)
        {
            this.UserID = UserID;
            this.UserName = UserName;
            this.Password = Password;
            this.PersonID = PersonID;
            this.IsActive = IsActive;
            _Mode = enMode.UpdateUser;
        }
        private bool _AddUser()
        {
            this.Password=clsHashingPassword.HashingPassword(this.Password);
            this.UserID =clsUserDataAccess.AddNewUser(this.PersonID,this.UserName,this.Password,this.IsActive);
            return (this.UserID != -1);
        }
        private bool _UpdateUser()
        {
            this.Password = clsHashingPassword.HashingPassword(this.Password);
            return clsUserDataAccess.UpdateUser(this.UserID,this.PersonID,this.UserName,this.Password,this.IsActive);
        }
       public static DataTable GetAllUsers()
        {
            return clsUserDataAccess.GetAllUsers();
        }
        public static clsUsersBusinessLayer Find(int UserID) {
            string UserName = ""; string Password = ""; int PersonID = -1; bool IsActive =default;
            if (clsUserDataAccess.GetUserInfoByUserID(UserID, ref UserName, ref PersonID, ref Password, ref IsActive))
            {
                return new clsUsersBusinessLayer(UserID, UserName, Password, PersonID, IsActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUsersBusinessLayer Find(string UserName,string Password)
        {
            int UserID=-1; int PersonID = -1; bool IsActive=default;
           if(clsUserDataAccess.GetUserInfoByUserNameAndPassword(ref UserID,UserName, ref PersonID,Password,ref IsActive))
            {
                return new clsUsersBusinessLayer(UserID, UserName, Password, PersonID, IsActive);
            }
            else
            {
                return null;
            }
        }
        public static string GetUserPassword(int UserID)
        {
            String Password = "";
            if(clsUserDataAccess.GetUserPasswordByUserID(UserID,ref Password))
            {
                return Password;
            }
            else
            {
                return null;
            }
        }
        public bool UpdatePassword()
        {
            this.Password = clsHashingPassword.HashingPassword(this.Password);
            return clsUserDataAccess.UpdatePassword(this.UserID, this.Password);
        }
        public static bool DeleteUser(int UserID) {
            return clsUserDataAccess.DeleteUser(UserID);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddUser())
                    {
                        _Mode = enMode.UpdateUser;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.UpdateUser:
                    return _UpdateUser();
            }
            return false;
        }
        public static bool IsExist(int UserID)
        {
            return clsUserDataAccess.IsExist(UserID);
        }
        public static bool IsExist(string UserName)
        {
            return clsUserDataAccess.IsExist(UserName);
        }
        public static bool IsUserExist(int PersonID)
        {
            return clsUserDataAccess.IsUserExist(PersonID);
        }
    }
}
