using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace server.Entities.Commands
{
     public class ContactsGetCmd : BaseCommand, ICommand
    {
        public object Execute(params object[] param)
        {
            if (param[0] == null) //get all
            {
                try
                {
                    MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Initialize contacts list, and execute sql command to get all contacts from DB and return to user." });
                    MainManager.Instance.Init2();
                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.contactsList);
                }
                catch (Exception ex)
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to get all contacts from DB and return to user." });
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
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to get one contact(id:{id}) from DB and return it to user." });
                        return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.contacts.GetContactById(id));
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to get one contact(id:{id}) from DB and return it to user." });
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to get one contact from DB and return it to user" });
                    return "Error: Missing parameters";
                }
            }

        }
    }

    public class ContactsUpdateCmd : BaseCommand, ICommand
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
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to update contact(id:{id})." });
                        Contact c2 = System.Text.Json.JsonSerializer.Deserialize<Contact>(body);
                        MainManager.Instance.contacts.UpdateContactById(id, c2.Name, c2.Email, c2.Phone, c2.Message);
                        return System.Text.Json.JsonSerializer.Serialize(c2);
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to update contact(id:{id})." });
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Error in parameters, Failed to execute sql command to update contact." });
                    return "Error: error in parameters";
                }
            }
            else
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to update contact." });
                return "Error: Missing parameters";
            }
        }
    }

    public class ContactsAddCmd : BaseCommand, ICommand
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
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to add contact." });
                        Contact c = System.Text.Json.JsonSerializer.Deserialize<Contact>(body);
                        MainManager.Instance.contacts.AddNewContact(c.Name, c.Email, c.Phone, c.Message);
                        return System.Text.Json.JsonSerializer.Serialize(c);
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to add contact." });
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Error in parameters, Failed to execute sql command to add contact." });
                    return "Error: error in parameters";
                }
            }
            else
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to add contact." });
                return "Error: Missing parameters";
            }
        }
    }

    public class ContactsRemoveCmd : BaseCommand, ICommand
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
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to remove contact(id:{id})." });
                        MainManager.Instance.contacts.DeleteContactById(id);
                        return "Contact Removed successfully";
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to remove contact(id:{id})." });
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Error in parameters, Failed to execute sql command to remove contact." });
                    return "Error: error in parameters";
                }
            }
            else
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to remove contact." });
                return "Error: Missing parameters";
            }
        }
    }
}
