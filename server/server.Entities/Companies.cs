using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Data.Sql;
using server.Model;

namespace server.Entities
{
    public class Companies
    {
        CompaniesQueries companiesQueries = new CompaniesQueries();

        public void ClearList()
        {
            MainManager.Instance.companiesList.Clear();
        }

        public void GetCompaniesFromDB()
        {
            ClearList();
            MainManager.Instance.companiesList = ((Dictionary<string, Company>)companiesQueries.ResetList());
            //return MainManager.Instance.companiesList;
        }

        public Company GetCompanyById(string UserID)
        {
            Company company = ((Company)companiesQueries.GetCompanyFromDB(UserID));
            return company;
        }

        public void AddNewCompany(string UserID, string Name, string Address, string Phone)
        {
            Company company = new Company
            {
                UserID = UserID,
                Name = Name,
                Address = Address,
                Phone = Phone,
            };
            MainManager.Instance.companiesList.Add(UserID, company);

            companiesQueries.InsertCompanyToDB(UserID, Name, Address, Phone);
        }

        public void UpdateCompanyById(string UserID, string Name, string Address, string Phone)  
        {
            companiesQueries.UpdateCompanyInDB(UserID, Name, Address, Phone);
            //MainManager.Instance.companiesList[UserID]=new Company { UserID=UserID, Name = Name, Address = Address, Phone = Phone};
        }

        public Company GetCompanyFromList(string UserID)
        {
            return MainManager.Instance.companiesList[UserID];
        }

        public void DeleteCompanyById(string UserID)
        {
            /*if (MainManager.Instance.companiesList.Count == 0)
            {

            }
            else
            {
                MainManager.Instance.companiesList.RemoveAt(UserID);*/
            companiesQueries.DeleteCompanyFromDB(UserID);
            //}
        }
    }
}
