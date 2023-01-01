using server.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Data.Sql
{
    public class CampaignsQueries
    {
        public List<Campaign> BuildCampaignsList(SqlDataReader reader)
        {
            List<Campaign> campaignsList = new List<Campaign>();

            while (reader.Read())
            {
                Campaign campaign = new Campaign();
                campaign.Id = reader.GetInt32(reader.GetOrdinal("Id"));                
                campaign.OrganizationID = reader.GetString(reader.GetOrdinal("OrganizationID"));
                campaign.Name = reader.GetString(reader.GetOrdinal("Name"));
                campaign.Description = reader.GetString(reader.GetOrdinal("Description"));
                campaign.Url = reader.GetString(reader.GetOrdinal("Url"));
                campaign.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                campaignsList.Add(campaign);
            }
            return campaignsList;
        }
        public Campaign BuildCampaign(SqlDataReader reader)
        {
            Campaign campaign = new Campaign();

            while (reader.Read())
            {
                campaign.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                campaign.OrganizationID = reader.GetString(reader.GetOrdinal("OrganizationID"));
                campaign.Name = reader.GetString(reader.GetOrdinal("Name"));
                campaign.Description = reader.GetString(reader.GetOrdinal("Description"));
                campaign.Url = reader.GetString(reader.GetOrdinal("Url"));
                campaign.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
            }
            return campaign;
        }

        public object ResetList()
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult("select * from Campaigns", BuildCampaignsList);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void InsertCampaignToDB(string OrganizationID, string Name, string Description, string Url, string Hashtag)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Insert Into Campaigns(OrganizationID, Name, Description, Url, Hashtag) Values('{OrganizationID}','{Name}','{Description}','{Url}','{Hashtag}')");
            }
            catch (Exception ex)
            {
            }
        }

        public object GetCampaignFromDB(string id)
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult($"select * from Campaigns where Id= '{id}'", BuildCampaign);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeleteCampaignFromDB(string id)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Delete from Campaigns where Id= '{id}'");
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateCampaignInDB(string Id, string Name, string Description, string Url, string Hashtag)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Update Campaigns set Name='{Name}' , Description='{Description}' , Url='{Url}', Hashtag='{Hashtag}' where Id= '{Id}'");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
