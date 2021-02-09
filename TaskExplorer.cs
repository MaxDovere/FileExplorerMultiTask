using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using FileExplorerMultiTask.Models;
using FileExplorerMultiTask.DataLayer;
using FileExplorerMultiTask.Services;

namespace FileExplorerMultiTask
{
    public class TaskExplorer
    {
        public long CreationTime;
        public string Name;
        public int ThreadNum;
        public long NIterations;
        
        private EntityList<ExplorerObjects> cacheListFiles { get; set; }
        private EntityList<ExplorerObjects> cacheListDirs { get; set; }

        public delegate long RunTask(string action, string parms);
        private bool _bStopped { get; set; }
        private Logging _log { get; }
        private Logging.WriteMessage _logMessage { get; }
        private Logging.WriteMessageToDbLog _logMessageDbLog { get; }
        private IActionDBExplorer<ExplorerTasks> ActionExTask;
        private IActionDBExplorer<ExplorerObjects> ActionExObj;
        public TaskExplorer(Logging log)
        {
            _log = log;
            _logMessage = new Logging.WriteMessage(_log.ToPrint);
            _logMessageDbLog = new Logging.WriteMessageToDbLog(_log.ToDbLog);
            ActionExTask = new ActionDBExplorer<ExplorerTasks>(_log);
            ActionExObj = new ActionDBExplorer<ExplorerObjects>(_log);

        }
        private bool InsertTask(string Message)
        {
            try
            {
                _bStopped = false;
                var dp = new DynamicParameters();
                Guid Keyid = Guid.NewGuid();
                dp.Add("@Keyid", Keyid, DbType.Guid, ParameterDirection.Input);
                dp.Add("@Name", Name, DbType.AnsiString, ParameterDirection.Input);
                dp.Add("@Message", Message, DbType.AnsiString, ParameterDirection.Input);
                dp.Add("@ThreadNum", ThreadNum, DbType.Int32, ParameterDirection.Input);
                dp.Add("@NIterations", NIterations, DbType.Int32, ParameterDirection.Input);
                dp.Add("@Actived", 1, DbType.Int32, ParameterDirection.Input);
                int Id = ActionExTask.Insert(dp);
                
                if (Keyid.ToString().Length > 0)
                    _log.Keyid = Keyid;

                _logMessage(this.ToString(), 0, " - Insert Task: " + Message);
                _logMessageDbLog(this.ToString(), 0, "Insert Task in Db: " + Message);

                return true;
            }
            catch (Exception ex)
            {
                _logMessage(this.ToString(), ex.HResult, ex.Message);
                _logMessageDbLog(this.ToString(), ex.HResult, ex.Message);
            }
            return false;
        }
        private bool FinishTask(string Message)
        {
            bool result = false;
            try
            {
                _logMessage(this.ToString(), 0, string.Format("Closed Task {0} in Db: [{1}]", Name, Message));
                _logMessageDbLog(this.ToString(), 0, string.Format("Closed Task {0} in Db: [{1}]", Name,  Message));

                var dp = new DynamicParameters();
                dp.Add("@Keyid", _log.Keyid, DbType.Guid, ParameterDirection.Input);
                ActionExTask.Update(dp);

                result = true;
            }
            catch (Exception ex)
            {
                _logMessage(this.ToString(), ex.HResult, ex.Message);
                _logMessageDbLog(this.ToString(), ex.HResult, ex.Message);
            }
            finally
            {
                _bStopped = true;
            }
            return result;
        }
        private long Cleaning()
        {
            long result = 0;
            try
            {
                _logMessage(this.ToString(), 0, "Inizio pulizia archivi!");
                _logMessageDbLog(this.ToString(), 0, "Inizio pulizia archivi!");

                var dp = new DynamicParameters();
                dp.Add("@id", 0, DbType.Int32, ParameterDirection.Input);
                ActionExObj.Delete(dp);

                _logMessage(this.ToString(), 0, "Conclusa attività pulizia con esito positivo!");
                _logMessageDbLog(this.ToString(), 0, "Conclusa attività pulizia con esito positivo!");

                //dp = new DynamicParameters();
                //dp.Add("@keyid", _log.Keyid, DbType.Guid, ParameterDirection.Input);
                //ActionExTask.Delete(dp);

            }
            catch (Exception ex)
            {
                result = ex.HResult;
                _logMessage(this.ToString(), ex.HResult, ex.Message);
                _logMessageDbLog(this.ToString(), ex.HResult, ex.Message);
            }
            return result;

        }
        public long Start(string action, string parms)
        {
            long result = 0;
            this.InsertTask("Task Start!");

            _logMessage(this.ToString(), 0, string.Format("Start task {0} with path [{0}]", Name, parms));
            _logMessageDbLog(this.ToString(), 0, string.Format("Start task {0} with path [{0}]", Name, parms));

            switch (action.ToLower())
            {
                case "start cleaning task":
                    Cleaning();
                    break;
                case "start explorer task":
                    cacheListFiles = new EntityList<ExplorerObjects>();
                    cacheListDirs = new EntityList<ExplorerObjects>();
                    result = DirectorySearch(parms);
                    break;
                default:
                    break;
            }

            Stop(parms);

            return result;
        }
        public void Stop(string parms)
        {
            _logMessage(this.ToString(), 0, string.Format("Finish task {0} with path [{1}]", Name, parms));
            _logMessageDbLog(this.ToString(), 0, string.Format("Finish task {0} with path [{1}]", Name, parms));

            this.FinishTask("Task Stop!");
            _bStopped = true;
        }
        private long DirectorySearch(string path)
        {
            try
            {                
                if (_bStopped)
                    return NIterations;

                if (!Directory.Exists(path))
                {
                    _logMessage(this.ToString(), -1, "That path to directory not exists.");
                    _logMessageDbLog(this.ToString(), -1, "That path to directory not exists.");
                    return -1;
                }
                string[] root = path.Split(':');
                DriveInfo dr = new DriveInfo(root[0]);

                foreach (string fname in Directory.GetFiles(path))
                {
                    try
                    {
                        FileInfo fi = new FileInfo(fname);

                        cacheListFiles.Add(new ExplorerObjects()
                        {
                            Keyid = Guid.NewGuid(),                            
                            Fullpath = fi.FullName,
                            Drive = dr.RootDirectory.Name,
                            Name = fi.Name,
                            IsDirectory = 0,
                            Length = fi.Length,
                            Attributes = fi.Attributes.ToString(),
                            DirectoryName = fi.DirectoryName, 
                            Extension = fi.Extension,
                            FileCreationTime = Program.EnsureSQLSafe(fi.CreationTime),
                            FileLastAccessTime = Program.EnsureSQLSafe(fi.LastAccessTime),
                            FileLastWriteTime = Program.EnsureSQLSafe(fi.LastWriteTime)
                        });

                        if(cacheListFiles.Count >= Program.MASSIVECOUNT)
                        {
                            ActionExObj.MassiveInsert(cacheListFiles);
                            cacheListFiles = new EntityList<ExplorerObjects>();
                        }

                        NIterations += 1;
                    }
                    catch (Exception ex)
                    {
                        _logMessage(this.ToString(), ex.HResult, ex.Message + ": " + fname);
                        _logMessageDbLog(this.ToString(), ex.HResult, ex.Message + ": " + fname);
                    }
                }

                if (cacheListFiles.Count > 0)
                {
                    ActionExObj.MassiveInsert(cacheListFiles);
                    cacheListFiles = new EntityList<ExplorerObjects>();
                }

                foreach (string dname in Directory.GetDirectories(path))
                {
                    try
                    {

                        DirectoryInfo di = new DirectoryInfo(dname);

                        cacheListDirs.Add(new ExplorerObjects()
                        {
                            Keyid = Guid.NewGuid(),
                            Fullpath = di.FullName,
                            Drive = dr.RootDirectory.Name,
                            Name = di.Name,
                            IsDirectory = 1,
                            Length = 0,
                            Attributes = di.Attributes.ToString(),
                            DirectoryName = "",
                            Extension = di.Extension,
                            FileCreationTime = Program.EnsureSQLSafe(di.CreationTime),
                            FileLastAccessTime = Program.EnsureSQLSafe(di.LastAccessTime),
                            FileLastWriteTime = Program.EnsureSQLSafe(di.LastWriteTime)
                        });

                        if (cacheListDirs.Count >= Program.MASSIVECOUNT)
                        {
                            ActionExObj.MassiveInsert(cacheListDirs);
                            cacheListDirs = new EntityList<ExplorerObjects>();
                        }


                        NIterations += 1;
                    }
                    catch(Exception ex)
                    {
                        _logMessage(this.ToString(), ex.HResult, ex.Message + ": " + dname);
                        _logMessageDbLog(this.ToString(), ex.HResult, ex.Message + ": " + dname);
                    }

                    DirectorySearch(dname);
                }
                if (cacheListDirs.Count > 0)
                {
                    ActionExObj.MassiveInsert(cacheListDirs);
                    cacheListDirs = new EntityList<ExplorerObjects>();
                }
            }
            catch (Exception ex)
            {
                _logMessage(this.ToString(), ex.HResult, ex.Message);
                _logMessageDbLog(this.ToString(), ex.HResult, ex.Message);
            }
            return NIterations;
        }
        public override string ToString()
        {
            return this.GetType().Name;
        }

    }
}
