using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace server.DAL
{
    public class SqlQuery
    {
        public delegate void SetDataReader_delegate(SqlDataReader reader);
        public delegate object SetResulrDataReader_delegate(SqlDataReader reader);

        public static void RunNonQueryCommand(string sqlQuery)
        {
            string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Northwind;Data Source=localhost\\sqlexpress"/*ConfigurationManager.AppSettings["connectionString"]*/;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = sqlQuery;
                // Adapter
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    //Reader
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void RunCommand(string sqlQuery, SetDataReader_delegate func)
        {
            string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Northwind;Data Source=localhost\\sqlexpress"/*ConfigurationManager.AppSettings["connectionString"]*/;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = sqlQuery;
                // Adapter
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    //Reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        func(reader);
                    }
                }
            }
        }

        public static object RunCommandResult(string sqlQuery, SetResulrDataReader_delegate func)
        {
            object ret = null;
            string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Northwind;Data Source=localhost\\sqlexpress"/*ConfigurationManager.AppSettings["connectionString"]*/;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = sqlQuery;
                // Adapter
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    //Reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ret = func(reader);
                    }
                }
            }
            return ret;
        }
    }
}
