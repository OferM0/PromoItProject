using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace server.Entities.Commands
{
    public class UsersGetCmd : BaseCommand, ICommand
    {
        public object Execute(params object[] param)
        {
            if (param[0] == null) //get all
            {
                try
                {
                    MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Initialize users list, and execute sql command to get all users from DB and return to user." });
                    MainManager.Instance.Init3();
                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.usersList);
                }
                catch (Exception ex)
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to get all users from DB and return to user." });
                    return "Error: " + ex.Message;
                }
            }
            else //get by id
            {
                string id = (string)param[0];
                if (id != null)
                {
                    try
                    {
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to get one user(id:{id}) from DB and return it to user." });
                        return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.users.GetUserById(id));
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to get one user(id:{id}) from DB and return it to user." });
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to get one usern from DB and return it to user" });
                    return "Error: Missing parameters";
                }
            }

        }
    }

    public class UsersUpdateCmd : BaseCommand, ICommand
    {
        public object Execute(params object[] param)
        {
            if (param.Length > 0)
            {
                string id = (string)param[0];
                string body = (string)param[1];
                if (id != null && body != null) //update only by id
                {
                    try
                    {
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to update user(id:{id})." });
                        User u2 = System.Text.Json.JsonSerializer.Deserialize<User>(body);
                        MainManager.Instance.users.UpdateUserById(id, u2.Name, u2.Address, u2.Phone, u2.Url, u2.Status);
                        return System.Text.Json.JsonSerializer.Serialize(u2);
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to update user(id:{id})." });
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Error in parameters, Failed to execute sql command to update user." });
                    return "Error: error in parameters";
                }
            }
            else
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to update user." });
                return "Error: Missing parameters";
            }
        }
    }

    public class UsersAddCmd : BaseCommand, ICommand
    {
        public object Execute(params object[] param)
        {
            if (param.Length > 0)
            {
                string body = (string)param[1];
                if (body != null) //update only by id
                {
                    try
                    {
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to add user." });
                        User u = System.Text.Json.JsonSerializer.Deserialize<User>(body);
                        MainManager.Instance.users.AddNewUser(u.UserID, u.Role, u.Name, u.Address, u.Phone, u.Url, u.Status, u.TwitterHandle, u.CreateDate);
                        return System.Text.Json.JsonSerializer.Serialize(u);
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to add user." });
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Error in parameters, Failed to execute sql command to add user." });
                    return "Error: error in parameters";
                }
            }
            else
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to add user." });
                return "Error: Missing parameters";
            }
        }
    }

    public class UsersRemoveCmd : BaseCommand, ICommand
    {
        public object Execute(params object[] param)
        {
            if (param.Length > 0) //get all
            {
                string id = (string)param[0];
                if (id != null)
                {
                    try
                    {
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to remove user(id:{id})." });
                        MainManager.Instance.users.DeleteUserById(id);
                        return "User Removed successfully";
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to remove user(id:{id})." });
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Error in parameters, Failed to execute sql command to remove user." });
                    return "Error: error in parameters";
                }
            }
            else
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to remove user." });
                return "Error: Missing parameters";
            }
        }
    }
}