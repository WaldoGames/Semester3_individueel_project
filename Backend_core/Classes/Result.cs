using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.Classes
{
        public static class TestVar
        {
        public static bool test = false;
        }


        public class SimpleResult
        {
            private string errorMessage = string.Empty;

            public string ErrorMessage
            {
                get { return errorMessage; }
                set { errorMessage = Environment.NewLine+ "Error " + value; if(!TestVar.test)log.Log(value); }
            }

            private string warningMessage = string.Empty;

            public string WarningMessage
            {
                get { return warningMessage; }
                set { warningMessage = Environment.NewLine+ "Warning " + value; if (!TestVar.test) log.Log(value); }
        }

            public bool IsFailedWarning => !string.IsNullOrEmpty(WarningMessage);
            public bool IsFailedError => !string.IsNullOrEmpty(ErrorMessage);

            public bool IsFailed => (IsFailedWarning||IsFailedError);

            ErrorLog log = new ErrorLog();
        }
        public class Result<T> : SimpleResult
        {
            public T? Data { get; set; }

        }

        public class NullableResult<T> : Result<T>
        {
            public NullableResult()
            {
            }
            public NullableResult(Result<T> result)
            {
                Data = result.Data;
            }
            public bool IsEmpty => Data == null;
        }
        public class ErrorLog
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string filename = "/MusicApp_ErrorLog.txt";

            public void Log(string message)
            {
                File.AppendAllText(path + filename, "\n" + DateTime.Now.ToShortTimeString() + ": " + message);
            }
        }
}
