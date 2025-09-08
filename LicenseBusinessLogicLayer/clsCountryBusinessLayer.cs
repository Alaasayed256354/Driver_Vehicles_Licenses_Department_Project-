using LicenseDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LicenseBusinessLogicLayer
{
    public class clsCountryBusinessLayer
    {
        public string CountryName {  get; set; }
        public int CountryID {  get; set; }
     private   clsCountryBusinessLayer(int CountryId,string CountryName) {
           this.CountryID = CountryId;
            this.CountryName = CountryName;
        }
        public static DataTable GetAllCountries()
        {
            return CountryDataAccess.GetAllCountries();
        }
        public static clsCountryBusinessLayer Find(int CountryID)
        {
            string CountryName = "";
            if (CountryDataAccess.GetCountryInfoByID(CountryID,ref CountryName))
            {
                return new clsCountryBusinessLayer(CountryID,CountryName);
            }
            else
            {
                return null;
            }
        }
    }
}
