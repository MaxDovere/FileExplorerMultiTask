using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Configuration;
using FileExplorerMultiTask.Services;

namespace FileExplorerMultiTask
{
    public class Program
    {
        public static int DEBUG = Int32.Parse(ConfigurationManager.AppSettings["DEBUG"]);
        public static int MASSIVECOUNT = Int32.Parse(ConfigurationManager.AppSettings["MASSIVECOUNT"]);
        public static void Main()
        {
            
            string _sAction = ConfigurationManager.AppSettings["action"];
            string _sSeparetion = ConfigurationManager.AppSettings["charsplit"];
            string[] paths = _sAction.Split(_sSeparetion);
            if (paths.Length < 3)
            {
                _sAction = _sAction + _sSeparetion + _sSeparetion + _sSeparetion + _sSeparetion;
                paths = _sAction.Split(_sSeparetion);
            }

            long[] result = new long[4];

            #region ParallelTasks
            // Perform three tasks in parallel on the source array
            Parallel.Invoke(() =>
                                {
                                    WorkerRun work = new WorkerRun(new Logging(), "#1");
                                    result[0] = work.Go("Start Cleaning Task", "");
                                },
                                () =>
                                {
                                    WorkerRun work = new WorkerRun(new Logging(), "#2");
                                    result[1] = work.Go("Start Explorer Task", paths[0]);
                                },  // close first Action

                                () =>
                                {
                                    WorkerRun work = new WorkerRun(new Logging(), "#3");
                                    result[2] = work.Go("Start Explorer Task", paths[1]);
                                }, //close second Action

                                () =>
                                {
                                    WorkerRun work = new WorkerRun(new Logging(), "#4");
                                    result[3] = work.Go("Start Explorer Task", paths[2]);
                                } //close third Action
                                ); //close parallel.invoke


            Console.WriteLine(string.Format("Returned from Parallel.Invoke; [return Task 1 = {0} - return Task 2 = {1} - return Task 3 = {2} - return Task 4 = {3}]", result[0], result[1], result[2], result[3]));
            #endregion

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        /// <summary>
        /// Will return null if the CLR datetime will not fit in an SQL datetime
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime? EnsureSQLSafe(DateTime? datetime)
        {
            if (datetime.HasValue && (datetime.Value < (DateTime)SqlDateTime.MinValue || datetime.Value > (DateTime)SqlDateTime.MaxValue))
                return null;

            return datetime;
        }

    }
}