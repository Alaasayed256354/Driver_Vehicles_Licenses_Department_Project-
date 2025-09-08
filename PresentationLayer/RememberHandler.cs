using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
namespace Driver_VehiclesLicensesDepartmentProject
{
    public  class RememberHandler
    {
        private string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"Credentials.txt");
        private string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\UserCredentials";
        public  void _SaveUserCredentials(string UserName, string Password, bool Remeber)
        {
            try
            {
                if (Remeber)
                {
                    Registry.SetValue(KeyPath, "UserName", UserName, RegistryValueKind.String);
                    Registry.SetValue(KeyPath, "Password", Password, RegistryValueKind.String);
                    //File.WriteAllText(FilePath, $"{UserName}:{Password}");
                }
                else
                {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(KeyPath, true)) { 
                        if (key != null)
                        {
                            key.DeleteValue("UserName", false);
                            key.DeleteValue("Password", false);

                        }
                    }
                }
            }
            catch(Exception ex) {throw new Exception(ex.Message); }
        }
        public (string userName, string Password) CheckRemeberedUser()//Tuple
        {
            try
            {

                //if (File.Exists(FilePath))
                //{
                //    string[] data = File.ReadAllText(FilePath).Split(':');
                //    if (data.Length == 2)
                //    {
                //        return (data[0], data[1]);
                //    }
                //}

                string Password=Registry.GetValue(KeyPath, "Password",null)as string;
                string UserName=Registry.GetValue(KeyPath,"UserName",null) as string;
                if (Password!=null&&UserName!=null)
                {
                    return (UserName, Password);
                }
            }
            catch(Exception ex) { throw new Exception(ex.Message); }    
            return (string.Empty, string.Empty);
        }
    }
}
