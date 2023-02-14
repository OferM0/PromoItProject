using server.Data.Sql;
using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace server.Entities
{
    public class Campaigns:BaseEntity
    {
        public Campaigns(Logger log) : base(log)
        {
           campaignsQueries = new CampaignsQueries(base._log);
        }
        CampaignsQueries campaignsQueries;
        public void ClearList()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute  ClearList function in Campaigns Entity." });
                MainManager.Instance.campaignsList.Clear();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public void GetCampaignsFromDB()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetCampaignsFromDB function in Campaigns Entity." });
                ClearList();
                MainManager.Instance.campaignsList = ((List<Campaign>)campaignsQueries.ResetList());
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public Campaign GetCampaignById(string id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetCampaignById(id:{id}) function in Campaigns Entity." });
                Campaign campaign = ((Campaign)campaignsQueries.GetCampaignFromDB(id));
                return campaign;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }            
        }

        public void AddNewCampaign(string OrganizationID, string Name, string Description, string Url, string Hashtag, bool Active, string CreateDate)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute AddNewCampaign function in Campaigns Entity." });
                Campaign campaign = new Campaign
                {
                    OrganizationID = OrganizationID,
                    Name = Name,
                    Description = Description,
                    Url = Url,
                    Hashtag = Hashtag,
                    Active = Active,
                    CreateDate = CreateDate
                };
                MainManager.Instance.campaignsList.Add(campaign);
                campaignsQueries.InsertCampaignToDB(OrganizationID, Name, Description, Url, Hashtag, Active, CreateDate);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public void UpdateCampaignById(string Id, string Name, string Description, string Url, string Hashtag, bool Active/*, DateTime CreateDate*/)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateCampaignById(id:{Id}) function in Campaigns Entity." });
                campaignsQueries.UpdateCampaignInDB(Id, Name, Description, Url, Hashtag, Active/*, CreateDate*/);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public Campaign GetCampaignFromList(string id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetCampaignFromList(id:{id}) function in Campaigns Entity." });
                return MainManager.Instance.campaignsList[int.Parse(id)];
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public void DeleteCampaignById(string id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteCampaignById(id:{id}) function in Campaigns Entity." });
                campaignsQueries.DeleteCampaignFromDB(id);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }
    }
}
