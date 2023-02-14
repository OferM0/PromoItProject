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
    public class UsersQueries : BaseDataSql
    {
        public UsersQueries(Logger log):base(log) { }
        public Dictionary<string, User> BuildUsersList(SqlDataReader reader)
        {
            Dictionary<string, User> usersList = new Dictionary<string, User>();

            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute BuildUsersList function in UsersQueries." });
                while (reader.Read())
                {
                    User user = new User();
                    user.UserID = reader.GetString(reader.GetOrdinal("UserID"));
                    user.Role = reader.GetString(reader.GetOrdinal("Role"));
                    user.Name = reader.GetString(reader.GetOrdinal("Name"));
                    user.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                    user.Address = reader.GetString(reader.GetOrdinal("Address"));
                    user.Url = reader.GetString(reader.GetOrdinal("Url"));
                    user.Status = reader.GetDecimal(reader.GetOrdinal("Status"));
                    user.TwitterHandle = reader.GetString(reader.GetOrdinal("TwitterHandle"));
                    user.CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate")).ToString("yyyy-MM-dd");
                    usersList.Add(user.UserID, user);
                }
                return usersList;
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run BuildUsersList function in UsersQueries." });
                throw ex;
            }

        }
        public User BuildUser(SqlDataReader reader)
        {
            User user = new User();

            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute BuildUser function in UsersQueries." });
                while (reader.Read())
                {
                    user.UserID = reader.GetString(reader.GetOrdinal("UserID"));
                    user.Role = reader.GetString(reader.GetOrdinal("Role"));
                    user.Name = reader.GetString(reader.GetOrdinal("Name"));
                    user.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                    user.Address = reader.GetString(reader.GetOrdinal("Address"));
                    user.Url = reader.GetString(reader.GetOrdinal("Url"));
                    user.Status = reader.GetDecimal(reader.GetOrdinal("Status"));
                    user.TwitterHandle = reader.GetString(reader.GetOrdinal("TwitterHandle"));
                    user.CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate")).ToString("yyyy-MM-dd");
                }
                return user;
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run BuildUser function in UsersQueries." });
                throw ex;
            }
        }
        public object ResetList()
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute ResetList function in UsersQueries." });
                return DAL.SqlQuery.RunCommandResult("select * from Users", BuildUsersList);
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run ResetList function in UsersQueries." });
                throw ex;
            }
        }
        public void InsertUserToDB(string UserID, string Role, string Name, string Address, string Phone, string Url, decimal Status, string TwitterHandle, string CreateDate)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute InsertUserToDB function in UsersQueries." });
                DAL.SqlQuery.RunNonQueryCommand($"Insert Into Users(UserID, Role, Name, Address, Phone, Url, Status, TwitterHandle, CreateDate) Values('{UserID}','{Role}','{Name}','{Address}','{Phone}','{Url}','{Status}','{TwitterHandle}','{CreateDate}')");
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run InsertUserToDB function in UsersQueries." });
                throw ex;
            }
}
        public object GetUserFromDB(string UserID)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetUserFromDB(id:{UserID}) function in UsersQueries." });
                return DAL.SqlQuery.RunCommandResult($"Select * from Users where UserID= '{UserID}'", BuildUser);
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run GetUserFromDB function in UsersQueries." });
                throw ex;
            }
}

        public void DeleteUserFromDB(string UserID)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteUserFromDB(id:{UserID}) function in UsersQueries." });
                DAL.SqlQuery.RunNonQueryCommand($"Delete from Users where UserID= '{UserID}'");
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run DeleteUserFromDB function in UsersQueries." });
                throw ex;
            }
        }

        public void UpdateUserInDB(string UserID, string Name, string Address, string Phone, string Url, decimal Status)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateUserInDB(id:{UserID}) function in UsersQueries." });
                DAL.SqlQuery.RunNonQueryCommand($"Update Users set Name='{Name}' , Address='{Address}' , Phone='{Phone}' , Url='{Url}' , Status='{Status}' where UserID= '{UserID}'");
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run UpdateUserInDB function in UsersQueries." });
                throw ex;
            }
        }

        public void UpdateUserStatusInDB(string UserID, decimal Status)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateUserStatusInDB(id:{UserID}) function in UsersQueries." });
                DAL.SqlQuery.RunNonQueryCommand($"Update Users set Status='{Status}' where UserID= '{UserID}'");
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run UpdateUserStatusInDB function in UsersQueries." });
                throw ex;
            }
        }
    }
}
