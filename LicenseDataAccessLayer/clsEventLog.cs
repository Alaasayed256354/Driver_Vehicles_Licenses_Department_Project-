using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace LicenseDataAccessLayer
{
    public class clsEventLog
    {
        private static string ProjectName = "DVLD";
        //static clsEventLog()//static const cannot be public or protected because it always private because it controlled by compiler in run time and cannot take parameters because it cannot called by object and it callled when 1-access any staticmeber in class 2-create an object of class
        //{
        //    if (EventLog.SourceExists(ProjectName))
        //    {
        //        EventLog.CreateEventSource(ProjectName, "Application");
        //    }
        //}
        /// <summary>
        /// this func stores the exception errors in eventlog\viewer
        /// </summary>
        /// <param name="ex">
        /// the exception to log
        /// </param>
        public static void HandlingException(Exception ex)
        {
            EventLog.WriteEntry(ProjectName, ex.Message, EventLogEntryType.Information);
        }
    }
}
