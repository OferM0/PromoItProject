using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utilities
{
    public class LogDB : ILogger
    {
        public Queue<LogItem> eventsQueue { get; set; }
        public Queue<LogItem> errorsQueue { get; set; }
        Task task;
        Task task2;
        public bool stop = false;

        public void Init()
        {
            eventsQueue = new Queue<LogItem>();
            errorsQueue = new Queue<LogItem>();

            LogMessage(new LogItem { Type="Event", Message="Logs Started", LogTime=DateTime.Now });

            task = Task.Run(() =>
            {
                while (!stop)
                {
                    if (eventsQueue.Count > 0)
                    {
                        LogMessage(eventsQueue.Dequeue());
                    }
                    if (errorsQueue.Count > 0)
                    {
                        LogException(errorsQueue.Dequeue());
                    }
                    Thread.Sleep(100);
                }
            });

            task2= Task.Run(() =>
            {
                while (!stop)
                {
                    LogCheckHouseKeeping();
                    Thread.Sleep(3600000);
                }
            });
        }

        public void LogMessage(LogItem log)
        {
            /*
            using (SqlConnection connection = new SqlConnection("Data Source=(local);Initial Catalog=Logs;Integrated Security=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Logs (Message, Type, LogTime) VALUES (@Message, 'Event', @LogTime)", connection))
                {
                    command.Parameters.AddWithValue("@Message", log.Message);
                    command.Parameters.AddWithValue("@LogTime", log.LogTime);
                    command.ExecuteNonQuery();
                }
            }*/
            try
            {
                server.DAL.SqlQuery.RunNonQueryCommand($"Insert Into Logs(Message, Type, LogTime) Values ('{log.Message}','{log.Type}','{log.LogTime}')");
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                // Log the exception
                Console.WriteLine("Error inserting message into logs: " + ex.Message);
                Console.ResetColor();
            }
        }

        public void LogException(LogItem log)
        {
            /*
            using (SqlConnection connection = new SqlConnection("Data Source=(local);Initial Catalog=Logs;Integrated Security=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Logs (Message, Type, LogTime) VALUES (@Message, 'Exception', @LogTime)", connection))
                {
                    command.Parameters.AddWithValue("@Message", log.Message);
                    command.Parameters.AddWithValue("@LogTime", log.LogTime);
                    command.ExecuteNonQuery();
                }
            }*/
            try
            {
                server.DAL.SqlQuery.RunNonQueryCommand($"Insert Into Logs(Message, Type, LogTime) Values ('{log.Message}','{log.Type}','{log.LogTime}')");
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                // Log the exception
                Console.WriteLine("Error inserting exception into logs: " + ex.Message);
                Console.ResetColor();
            }
        }

        public void LogCheckHouseKeeping()
        {
            /*
            using (SqlConnection connection = new SqlConnection("Data Source=(local);Initial Catalog=Logs;Integrated Security=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Logs WHERE LogTime < @LogTime", connection))
                {
                    command.Parameters.AddWithValue("@LogTime", DateTime.Now.AddMonths(-3));
                    command.ExecuteNonQuery();
                }
            }*/
            try
            {
                server.DAL.SqlQuery.RunNonQueryCommand($"Delete from Logs where LogTime < '{DateTime.Now.AddMonths(-3)}'");
                //LogMessage(new LogItem { Type = "Event", Message = "Logs Started", LogTime = DateTime.Now });
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                // Log the exception
                Console.WriteLine("Error cleaning up logs: " + ex.Message);
                Console.ResetColor();
            }
        }
    }
}
