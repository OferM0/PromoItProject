using server.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace server.Data.Sql
{
    /*
    public class LogsQueries : BaseDataSql
    {
        public LogsQueries(Logger log) : base(log) { }

        public void InsertLogEventsToDB(string Type, string Message, DateTime LogTime)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Insert Into  Logs(Message, Type, LogTime) Values ('{Message}','{Type}','{LogTime}')");
            }
            catch
            {
            }
        }

        public void InsertLogExpectionsToDB(string Type, string Message, DateTime LogTime)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Insert Into  Logs(Message, Type, LogTime) Values ('{Message}','{Type}','{LogTime}')");
            }
            catch
            {
            }
        }

        public void DeleteLogsFromDB(string id)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Delete from Campaigns where Id= '{id}'");
            }
            catch (Exception ex)
            {

            }
        }
    }
    */
}
