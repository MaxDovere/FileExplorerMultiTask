using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace FileExplorerMultiTask.Models
{
    interface IActionDBExplorer<T>
    {
        T GetSingleCustom(string query, DynamicParameters parms);
        List<T> GetAllCustom(string query, DynamicParameters parms);
        int Update(DynamicParameters parms);
        int Insert(DynamicParameters parms);
        int Delete(DynamicParameters parms);
        void MassiveInsert(EntityList<T> elist);
        void MassiveUpdate(EntityList<T> elist);
        T GetSingle(DynamicParameters parms);
        T GetSingleOrDefault(DynamicParameters parms);
        List<T> GetAll(DynamicParameters parms);
        Task<List<T>> GetAllAsync(DynamicParameters parms);
    }
}
