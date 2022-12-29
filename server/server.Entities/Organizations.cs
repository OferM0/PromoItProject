using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Data.Sql;
using server.Model;

namespace server.Entities
{
    public class Organizations
    {
        OrganizationsQueries organizationsQueries = new OrganizationsQueries();

        public void ClearList()
        {
            MainManager.Instance.organizationsList.Clear();
        }

        public void GetOrganizationsFromDB()
        {
            ClearList();
            MainManager.Instance.organizationsList = ((Dictionary<string, Organization>)organizationsQueries.ResetList());
            //return MainManager.Instance.organizationsList;
        }

        public Organization GetOrganizationById(string UserID)
        {
            Organization organization = ((Organization)organizationsQueries.GetOrganizationFromDB(UserID));
            return organization;
        }

        public void AddNewOrganization(string UserID, string Name, string Address, string Phone, string Url)
        {
            Organization organization = new Organization
            {
                UserID = UserID,
                Name = Name,
                Address = Address,
                Phone = Phone,
                Url = Url,
            };
            MainManager.Instance.organizationsList.Add(UserID, organization);

            organizationsQueries.InsertOrganizationToDB(UserID, Name, Address, Phone, Url);
        }

        public void UpdateOrganizationById(string UserID, string Name, string Address, string Phone, string Url)  //not update to Role
        {
            organizationsQueries.UpdateOrganizationInDB(UserID, Name, Address, Phone, Url);
            //MainManager.Instance.organizationsList[UserID]=new Organization { UserID=UserID, Name = Name, Address = Address, Phone = Phone, Url = Url};
        }

        public Organization GetOrganizationFromList(string UserID)
        {
            return MainManager.Instance.organizationsList[UserID];
        }

        public void DeleteOrganizationById(string UserID)
        {
            /*if (MainManager.Instance.organizationsList.Count == 0)
            {

            }
            else
            {
                MainManager.Instance.organizationsList.RemoveAt(UserID);*/
            organizationsQueries.DeleteOrganizationFromDB(UserID);
            //}
        }
    }
}
