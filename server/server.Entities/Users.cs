using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using server.Data.Sql;
using Utilities;

namespace server.Entities
{
    public class Users : BaseEntity
    {
        public Users(Logger log) : base(log) { usersQueries = new UsersQueries(base._log); }

        UsersQueries usersQueries;

        public void ClearList()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute  ClearList function in Users Entity." });
                MainManager.Instance.usersList.Clear();
            }
            catch(Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public void GetUsersFromDB()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetUsersFromDB function in Users Entity." });
                ClearList();
                MainManager.Instance.usersList = ((Dictionary<string, User>)usersQueries.ResetList());
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public User GetUserById(string UserID)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetUserById(id:{UserID}) function in Users Entity." });
                User user = ((User)usersQueries.GetUserFromDB(UserID));
                return user;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public void AddNewUser(string UserID, string Role, string Name, string Address, string Phone, string Url, decimal Status, string TwitterHandle, string CreateDate)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute AddNewUser function in Users Entity." });
                User user = new User
                {
                    UserID = UserID,
                    Role = Role,
                    Name = Name,
                    Address = Address,
                    Phone = Phone,
                    Url = Url,
                    Status = Status,
                    TwitterHandle = TwitterHandle,
                    CreateDate = CreateDate
                };
                MainManager.Instance.usersList.Add(UserID, user);
                usersQueries.InsertUserToDB(UserID, Role, Name, Address, Phone, Url, Status, TwitterHandle, CreateDate);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }

        }

        public void UpdateUserById(string UserID, string Name, string Address, string Phone, string Url, decimal Status)  //not update to Role
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateUserById(id:{UserID}) function in Users Entity." });
                usersQueries.UpdateUserInDB(UserID, Name, Address, Phone, Url, Status);
            }
            catch (Exception ex) 
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public void UpdateUserStatusById(string UserID, decimal Status) 
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateUserStatusById(id:{UserID}), new status{Status} function in Users Entity." });
                usersQueries.UpdateUserStatusInDB(UserID, Status);
                MainManager.Instance.usersList[UserID].Status = Status;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public User GetUserFromList(string UserID)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetUserFromList(id:{UserID}) function in Users Entity." });
                return MainManager.Instance.usersList[UserID];
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public void DeleteUserById(string UserID)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteUserById(id:{UserID}) function in Users Entity." });
                usersQueries.DeleteUserFromDB(UserID);
            }
            catch(Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }

        }
    }
}
