using Dapper;
using FileExplorerMultiTask.DataLayer;
using FileExplorerMultiTask.Models;
using System;
using System.Text;
using System.Collections.Generic;

namespace FileExplorerMultiTask.Services
{
    public class Logging
    {
        public delegate void WriteMessage(string objectname, int hresult, string message);
        public delegate void WriteMessageToDbLog(string objectname, int hresult, string message);
        private IActionDBExplorer<ExplorerTasksLog> ActionTaskLog;

        public int Id { get; set; }
        public Guid Keyid { get; set; }
        public void ToPrint(string objectname, int hresult, string message)
        {
            message = hresult > 0 ? message + ": error = " + hresult.ToString() : message;
            message = string.Format("{0}: ", objectname) + message;
            Console.WriteLine(message);
        }
        public void ToDbLog(string objectname, int hresult, string message)
        {
            message = hresult > 0 ? message + ": error = " + hresult.ToString() : message;
            message = string.Format("{0}: ", objectname) + message;
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Keyid", this.Keyid, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);
            dp.Add("@HResult", hresult, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            dp.Add("@Message", message, System.Data.DbType.AnsiString, System.Data.ParameterDirection.Input);
            ActionTaskLog = new ActionDBExplorer<ExplorerTasksLog>(this);
            ActionTaskLog.Insert(dp);
        }
        public override string ToString()
        {
            return this.GetType().Name; 
        }

    }
}
