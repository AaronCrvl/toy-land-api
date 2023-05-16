using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryToyLand.Log
{
    public class CustomException
    {
        public CustomException(Exception e)
        {
            var logReport = new StringBuilder();
            logReport.AppendLine($"Date: {DateTime.Now}");
            logReport.AppendLine($"TargetSite: {e.TargetSite}");
            logReport.AppendLine($"StackTrace: {e.StackTrace}");
            logReport.AppendLine($"Message: {e.Message}");
            logReport.AppendLine(Environment.NewLine);

            VerifyLogDirectoryAndFileCreated();
            var st = new StreamWriter(@"C:\temp\ToyLandLog\Log.txt");
            st.WriteLine(logReport);
        }
        #region Private Properties

        #endregion   

        #region Methods
        void VerifyLogDirectoryAndFileCreated()
        {
            var directory = new System.IO.DirectoryInfo(@"C:\temp\ToyLandLog");
            if (directory.Exists)
            {
                if (directory.EnumerateFiles().Where(x => x.Name == "Log").Count() > 0)
                    return;
                else
                    return;
            }
            else
            {
                directory.Create();
                return;
            }
        }
        #endregion
    }
}
