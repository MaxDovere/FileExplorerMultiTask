using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using FileExplorerMultiTask.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;
using FileExplorerMultiTask.Services;
using Dapper;
using Z.Dapper.Plus;

namespace FileExplorerMultiTask.DataLayer
{
    public class ActionDBExplorer<T>: IActionDBExplorer<T>
    {
        private string ConnectionString { get; } = "";
        private Logging _log { get; set; }
        private Logging.WriteMessage _logMessage { get; }
        private Logging.WriteMessageToDbLog _logMessageDbLog { get; }
        public ActionDBExplorer(Logging log)
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["explorer"].ConnectionString;
            _log = log;
            _logMessage = new Logging.WriteMessage(_log.ToPrint);
            _logMessageDbLog = new Logging.WriteMessageToDbLog(_log.ToDbLog);
        }

        private string CommandToAction(string action)
        {
            switch (typeof(T).Name.ToLower())
            {
                case "explorerobjects":
                    switch(action.ToLower())
                    {
                        case "insert":
                            return "[dbo].[spexp_InsertExplorerObjects]";
                        case "update":
                            return "[dbo].[spexp_UpdateExplorerObjects]";
                        case "delete-all":
                        case "delete":
                            return "[dbo].[spexp_DeleteExplorerObjects]";
                        default:
                            break;
                    }
                    break;
                case "explorerobjectsparental":
                    switch (action.ToLower())
                    {
                        case "insert":
                            return "[dbo].[spexp_InsertExplorerObjectsParental]";
                        case "update":
                            return "[dbo].[spexp_UpdateExplorerObjectsParental]";
                        case "delete-all":
                        case "delete":
                            return "[dbo].[spexp_DeleteExplorerObjectsParental]";
                        default:
                            break;
                    }
                    break;
                case "explorertasks":
                    switch (action.ToLower())
                    {
                        case "insert":
                            return "[dbo].[spexp_InsertExplorerTasks]";
                        case "update":
                            return "[dbo].[spexp_UpdateExplorerTasks]";
                        case "delete-all":
                        case "delete":
                            return "[dbo].[spexp_DeleteExplorerTasks]";
                        default:
                            break;
                    }
                    break;
                case "explorertaskslog":
                    switch (action.ToLower())
                    {
                        case "insert":
                            return "[dbo].[spexp_InsertExplorerTasksLog]";
                        case "delete-all":
                        case "delete":
                            return "[dbo].[spexp_DeleteExplorerTasksLog]";
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return "";
        }
        public List<T> GetAll(DynamicParameters parms)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    string query = CommandToAction("GetAll");

                    con.ConnectionString = ConnectionString;
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                        return (List<T>)con.Query<T>(query, parms);
                    }
                }
            }
            catch (Exception ex)
            {
                _logMessage(this.ToString(), ex.HResult, ex.Message);
                _logMessageDbLog(this.ToString(), ex.HResult, ex.Message);
            }
            return new List<T>();
        }
        public async Task<List<T>> GetAllAsync(DynamicParameters parms)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    string query = CommandToAction("GetAllAsync");

                    con.ConnectionString = ConnectionString;
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                        return await Task.FromResult(con.QueryAsync<T>(query, parms).Result.AsList());
                    }
                }
            }
            catch (Exception ex)
            {
                _logMessage(this.ToString(), ex.HResult, ex.Message);
                _logMessageDbLog(this.ToString(), ex.HResult, ex.Message);
            }
            return new List<T>();
        }

        public T GetSingle(DynamicParameters parms)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    string query = CommandToAction("GetSingle");

                    con.ConnectionString = ConnectionString;
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                        return con.QuerySingle<T>(query, parms);
                    }
                }
            }
            catch (Exception ex)
            {
                _logMessage(this.ToString(), ex.HResult, ex.Message);
                _logMessageDbLog(this.ToString(), ex.HResult, ex.Message);
            }
            return default;
        }
        public T GetSingleOrDefault(DynamicParameters parms)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    string query = CommandToAction("GetSingleOrDefault");
                    con.ConnectionString = ConnectionString;
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                        return con.QuerySingleOrDefault<T>(query, parms);
                    }
                }
            }
            catch(Exception ex)
            {
                _logMessage(this.ToString(), ex.HResult, ex.Message);
                _logMessageDbLog(this.ToString(), ex.HResult, ex.Message);
                //Console.WriteLine(ex.Message);
            }
            return default;
        }
        public int Insert(DynamicParameters parms)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    string query = CommandToAction("Insert");
                    con.ConnectionString = ConnectionString;
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                        var returnValue = con.ExecuteScalar<int>(query, parms, commandType: CommandType.StoredProcedure);
                        return Convert.ToInt32(returnValue);
                    }
                }
            }
            catch (Exception ex)
            {
                _logMessage(this.ToString(), ex.HResult, ex.Message);
                _logMessageDbLog(this.ToString(), ex.HResult, ex.Message);
            }
            return default;
        }
        public int Update(DynamicParameters parms)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    string query = CommandToAction("Update");
                    con.ConnectionString = ConnectionString;
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                        var returnValue = con.ExecuteScalar<int>(query, parms, commandType: CommandType.StoredProcedure);
                        return Convert.ToInt32(returnValue);
                    }
                }
            }
            catch (Exception ex)
            {
                _logMessage(this.ToString(), ex.HResult, ex.Message);
                _logMessageDbLog(this.ToString(), ex.HResult, ex.Message);
            }
            return default;
        }
        public int Delete(DynamicParameters parms)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    string query = CommandToAction("Delete");

                    con.ConnectionString = ConnectionString;
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                        return con.ExecuteScalar<int>(query, parms, commandType: CommandType.StoredProcedure);
                    }
                    return default;
                }
            }
            catch (Exception ex)
            {
                _logMessage(this.ToString(), ex.HResult, ex.Message);
                _logMessageDbLog(this.ToString(), ex.HResult, ex.Message);
                return default;
            }
        }
        public T GetSingleCustom(string query, DynamicParameters parms)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = ConnectionString;
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                        return con.QuerySingle<T>(query, parms);
                    }
                    return default;
                }
            }
            catch (Exception ex)
            {
                _logMessage(this.ToString(), ex.HResult, ex.Message);
                _logMessageDbLog(this.ToString(), ex.HResult, ex.Message);
                //Console.WriteLine(ex.Message);
                return default;
            }
        }
        public List<T> GetAllCustom(string query, DynamicParameters parms)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = ConnectionString;
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                        return (List<T>)con.Query<T>(query, parms);
                    }
                }
            }
            catch (Exception ex)
            {
                _logMessage(this.ToString(), ex.HResult, ex.Message);
                _logMessageDbLog(this.ToString(), ex.HResult, ex.Message);
            }
            return new List<T>();
        }
        public void MassiveInsert(EntityList<T> list)
        {
            // Execute
            var sb = new StringBuilder();

            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    string query = CommandToAction("Insert");
                    con.ConnectionString = ConnectionString;
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                        con.UseBulkOptions(options =>
                        {
                            options.Log = s => sb.AppendLine(s);
                        })
                        .BulkInsert(list);
                        
                        _logMessage(this.ToString(), 0, sb.ToString());
                        _logMessageDbLog(this.ToString(), 0, sb.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                _logMessage(this.ToString(), ex.HResult, ex.Message);
                _logMessageDbLog(this.ToString(), ex.HResult, ex.Message);
            }
        }
        public void MassiveUpdate(EntityList<T> list)
        {
            // Execute
            var sb = new StringBuilder();

            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    string query = CommandToAction("Insert");
                    con.ConnectionString = ConnectionString;
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                        con.UseBulkOptions(options =>
                        {
                            options.Log = s => sb.AppendLine(s);
                        })
                        .BulkMerge(list);

                        _logMessage(this.ToString(), 0, sb.ToString());
                        _logMessageDbLog(this.ToString(), 0, sb.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                _logMessage(this.ToString(), ex.HResult, ex.Message);
                _logMessageDbLog(this.ToString(), ex.HResult, ex.Message);
            }
        }

        public override string ToString()
        {
            return this.GetType().Name + "<" + typeof(T).Name + ">";
        }

    }
}
