using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_VehiclesLicensesDepartmentProject.Util
{
    internal class clsUtil
    {
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {

            // Check if the folder exists
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    // If it doesn't exist, create the folder
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }

            return true;

        }
        public static string ReplaceFileNameWithGuid(string FilePath)
        {
            //"C:\Users\RO\Pictures\WIN_20240616_10_46_43_Pro.jpg"
            FileInfo fi = new FileInfo(FilePath);
            string exct = fi.Extension;
            return GenerateGuid() + exct;
        }
        public static bool CopyImageToProjectImageFolder(ref string SourcePath)
        {
            string DestinationFolder = @"C:\DVLD_Images\";
            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }
            string DestinationFile = DestinationFolder + ReplaceFileNameWithGuid(SourcePath);
            try
            {
                File.Copy(SourcePath, DestinationFile, true);
            }
            catch (IOException iox)
            {
                throw new Exception(iox.Message);
                //return false;
            }
            SourcePath = DestinationFile;
            return true;
        }
    }
}
