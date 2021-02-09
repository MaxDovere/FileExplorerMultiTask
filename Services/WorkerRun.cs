using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using Dapper;
using FileExplorerMultiTask.DataLayer;
using FileExplorerMultiTask.Models;

namespace FileExplorerMultiTask.Services
{
    public class WorkerRun
    {
        private Logging _log { get; set; }
        public string Name { get; set; } = "";
        private Logging.WriteMessage _logMessage { get; }
        private Logging.WriteMessageToDbLog _logMessageDbLog { get; }
        private TaskExplorer.RunTask _runtask { get; set; }
        private Guid Keyid { get; set; }
        public WorkerRun(Logging log, string name)
        {
            Name = name;
            _log = log;
            _logMessage = new Logging.WriteMessage(_log.ToPrint);
            _logMessageDbLog = new Logging.WriteMessageToDbLog(_log.ToDbLog);
        }
        public long Go(string action, string parms)
        {
            long result = 0;
            TaskExplorer data = new TaskExplorer(_log) { Name = Name, CreationTime = DateTime.Now.Ticks };
            data.ThreadNum = Thread.CurrentThread.ManagedThreadId;

            _runtask = new TaskExplorer.RunTask(data.Start);
            result = _runtask(action, parms);
            result = data.NIterations;

            return result;
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }

}
