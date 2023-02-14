using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace server.Data.Sql
{
    public class BaseDataSql
    {
        public Logger _log;        
        public BaseDataSql(Logger log) { log = _log; }
    }
}
