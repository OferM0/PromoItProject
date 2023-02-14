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
    public class CampaignsQueries : BaseDataSql
    {
        public CampaignsQueries(Logger log) : base(log) { }

        public List<Campaign> BuildCampaignsList(SqlDataReader reader)
        {
            List<Campaign> campaignsList = new List<Campaign>();

            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute BuildCampaignsList function in CampaignQueries." });
                while (reader.Read())
                {
                    Campaign campaign = new Campaign();
                    campaign.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    campaign.OrganizationID = reader.GetString(reader.GetOrdinal("OrganizationID"));
                    campaign.Name = reader.GetString(reader.GetOrdinal("Name"));
                    campaign.Description = reader.GetString(reader.GetOrdinal("Description"));
                    campaign.Url = reader.GetString(reader.GetOrdinal("Url"));
                    campaign.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                    campaign.Active = reader.GetBoolean(reader.GetOrdinal("Active"));
                    campaign.CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate")).ToString("yyyy-MM-dd");
                    campaignsList.Add(campaign);
                }
                return campaignsList;
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run BuildCampaignsList function in CampaignQueries." });
                throw ex;
            }
        }
        public Campaign BuildCampaign(SqlDataReader reader)
        {
            Campaign campaign = new Campaign();

            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute BuildCampaign function in CampaignQueries." });
                while (reader.Read())
                {
                    campaign.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    campaign.OrganizationID = reader.GetString(reader.GetOrdinal("OrganizationID"));
                    campaign.Name = reader.GetString(reader.GetOrdinal("Name"));
                    campaign.Description = reader.GetString(reader.GetOrdinal("Description"));
                    campaign.Url = reader.GetString(reader.GetOrdinal("Url"));
                    campaign.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                    campaign.Active = reader.GetBoolean(reader.GetOrdinal("Active"));
                    campaign.CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate")).ToString("yyyy-MM-dd");
                }
                return campaign;
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run BuildCampaign function in CampaignQueries." });
                throw ex;
            }
        }

        public object ResetList()
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute ResetList function in CampaignQueries." });
                return DAL.SqlQuery.RunCommandResult("select * from Campaigns", BuildCampaignsList);
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run ResetList function in CampaignQueries." });
                throw ex;
            }
        }

        public void InsertCampaignToDB(string OrganizationID, string Name, string Description, string Url, string Hashtag, bool Active, string CreateDate)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute InsertCampaignToDB function in CampaignQueries." });
                DAL.SqlQuery.RunNonQueryCommand($"Insert Into Campaigns(OrganizationID, Name, Description, Url, Hashtag, Active, CreateDate) Values('{OrganizationID}','{Name}','{Description}','{Url}','{Hashtag}','{Active}','{CreateDate}')");
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run InsertCampaignToDB function in CampaignQueries." });
                throw ex;
            }
        }

        public object GetCampaignFromDB(string id)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetCampaignFromDB(id:{id}) function in CampaignQueries." });
                return DAL.SqlQuery.RunCommandResult($"select * from Campaigns where Id= '{id}'", BuildCampaign);
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run GetCampaignFromDB function in CampaignQueries." });
                throw ex;
            }
        }

        public void DeleteCampaignFromDB(string id)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteCampaignFromDB(id:{id}) function in CampaignQueries." });
                DAL.SqlQuery.RunNonQueryCommand($"Delete from Campaigns where Id= '{id}'");
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run DeleteCampaignFromDB function in CampaignQueries." });
                throw ex;
            }
        }

        public void UpdateCampaignInDB(string Id, string Name, string Description, string Url, string Hashtag, bool Active/*, DateTime CreateDate*/)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateCampaignInDB(id:{Id}) function in CampaignQueries." });
                DAL.SqlQuery.RunNonQueryCommand($"Update Campaigns set Name='{Name}' , Description='{Description}' , Url='{Url}', Hashtag='{Hashtag}', Active='{Active}' where Id= '{Id}'");
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run UpdateCampaignInDB function in CampaignQueries." });
                throw ex;
            }
        }
    }
}
