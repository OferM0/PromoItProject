using server.Data.Sql;
using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Entities
{
    public class Campaigns
    {
        CampaignsQueries campaignsQueries = new CampaignsQueries();

        public void ClearList()
        {
            MainManager.Instance.campaignsList.Clear();
        }

        public void GetCampaignsFromDB()
        {
            ClearList();
            MainManager.Instance.campaignsList = ((List<Campaign>)campaignsQueries.ResetList());
            //return MainManager.Instance.campaignsList;
        }

        public Campaign GetCampaignById(string id)
        {
            Campaign campaign = ((Campaign)campaignsQueries.GetCampaignFromDB(id));
            return campaign;
        }

        public void AddNewCampaign(string OrganizationID, string Name, string Description, string Url, string Hashtag, bool Active)
        {
            Campaign campaign = new Campaign
            {
                OrganizationID = OrganizationID,
                Name = Name,
                Description = Description,
                Url = Url,
                Hashtag = Hashtag,
                Active = Active
            };
            MainManager.Instance.campaignsList.Add(campaign);

            campaignsQueries.InsertCampaignToDB(OrganizationID, Name, Description, Url, Hashtag, Active);
        }

        public void UpdateCampaignById(string Id, string Name, string Description, string Url, string Hashtag, bool Active)
        {
            campaignsQueries.UpdateCampaignInDB(Id, Name, Description, Url, Hashtag, Active);
            //MainManager.Instance.campaignsList[id]=new Campaign { Id=Id, OrganizationID=OrganizationID, Name=Name, Description=Description, Url=Url, Hashtag=Hashtag, Active=Active};
        }

        public Campaign GetCampaignFromList(string id)
        {
            return MainManager.Instance.campaignsList[int.Parse(id)];
        }

        public void DeleteCampaignById(string id)
        {
            /*if (MainManager.Instance.campaignsList.Count == 0)
            {

            }
            else
            {
                MainManager.Instance.campaignsList.RemoveAt(id);*/
            campaignsQueries.DeleteCampaignFromDB(id);
            //}
        }
    }
}
