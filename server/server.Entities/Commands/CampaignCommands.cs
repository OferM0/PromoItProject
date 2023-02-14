using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utilities;

namespace server.Entities.Commands
{
    public class CampaignsGetCmd : BaseCommand, ICommand
    {
        public object Execute(params object[] param)
        {
            if (param[0] == null) //get all
            {
                try
                {
                    MainManager.Instance.log.LogEvent(new LogItem { LogTime=DateTime.Now, Type="Event", Message= "Initialize campaigns list, and execute sql command to get all campaigns from DB and return to user." });
                    MainManager.Instance.Init7();
                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.campaignsList);
                }
                catch (Exception ex)
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to get all campaigns from DB and return to user." });
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
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to get one campaign(id:{id}) from DB and return it to user." });
                        return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.campaigns.GetCampaignById(id));
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to get one campaign(id:{id}) from DB and return it to user." });
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to get one campaign from DB and return it to user" });
                    return "Error: Missing parameters.";
                }
            }

        }
    }

    public class CampaignsUpdateCmd : BaseCommand, ICommand
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
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to update campaign(id:{id})." });
                        Campaign c2 = System.Text.Json.JsonSerializer.Deserialize<Campaign>(body);
                        MainManager.Instance.campaigns.UpdateCampaignById(id, c2.Name, c2.Description, c2.Url, c2.Hashtag, c2.Active);
                        return System.Text.Json.JsonSerializer.Serialize(c2);
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to update campaign(id:{id})."});
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Error in parameters, Failed to execute sql command to update campaign." });
                    return "Error: error in parameters.";
                }
            }
            else
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to update campaign." });
                return "Error: Missing parameters.";
            }
        }
    }

    public class CampaignsAddCmd : BaseCommand, ICommand
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
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to add campaign." });
                        Campaign c = System.Text.Json.JsonSerializer.Deserialize<Campaign>(body);
                        MainManager.Instance.campaigns.AddNewCampaign(c.OrganizationID, c.Name, c.Description, c.Url, c.Hashtag, c.Active, c.CreateDate);
                        return System.Text.Json.JsonSerializer.Serialize(c);
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to add campaign." });
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Error in parameters, Failed to execute sql command to add campaign." });
                    return "Error: error in parameters.";
                }
            }
            else
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to add campaign." });
                return "Error: Missing parameters.";
            }
        }
    }

    public class CampaignsRemoveCmd : BaseCommand, ICommand
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
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to remove campaign(id:{id})." });
                        MainManager.Instance.campaigns.DeleteCampaignById(id);
                        return "Campaign Removed successfully";
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to remove campaign(id:{id})." });
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Error in parameters, Failed to execute sql command to remove campaign." });
                    return "Error: error in parameters";
                }
            }
            else
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to remove campaign." });
                return "Error: Missing parameters";
            }
        }
    }
}
