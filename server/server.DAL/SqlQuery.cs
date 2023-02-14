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
            try
            {
                //string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=PromoIt;Data Source=localhost\\sqlexpress"/*ConfigurationManager.AppSettings["connectionString"]*/;
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("MyConnectionString")))
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
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        public static void RunCommand(string sqlQuery, SetDataReader_delegate func)
        {
            try
            {
                //string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=PromoIt;Data Source=localhost\\sqlexpress"/*ConfigurationManager.AppSettings["connectionString"]*/;
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("MyConnectionString")))
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
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public static object RunCommandResult(string sqlQuery, SetResulrDataReader_delegate func)
        {
            object ret = null;
            try
            {
                //string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=PromoIt;Data Source=localhost\\sqlexpress"/*ConfigurationManager.AppSettings["connectionString"]*/;
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("MyConnectionString")))
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return ret;
        }
    }
}
