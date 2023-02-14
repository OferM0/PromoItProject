using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using server.Model;
using Utilities;

namespace server.Data.Sql
{/*
    public class CompaniesQueries : BaseDataSql
    {
        public CompaniesQueries(Logger log) : base(log) { }

        public Dictionary<string, Company> BuildCompaniesList(SqlDataReader reader)
        {
            Dictionary<string, Company> companiesList = new Dictionary<string, Company>();

            while (reader.Read())
            {
                Company company = new Company();
                company.UserID = reader.GetString(reader.GetOrdinal("UserID"));
                company.Name = reader.GetString(reader.GetOrdinal("Name"));
                company.Address = reader.GetString(reader.GetOrdinal("Address"));
                company.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                companiesList.Add(company.UserID, company);
            }
            return companiesList;
        }
        public Company BuildCompany(SqlDataReader reader)
        {
            Company company = new Company();

            while (reader.Read())
            {
                company.UserID = reader.GetString(reader.GetOrdinal("UserID"));
                company.Name = reader.GetString(reader.GetOrdinal("Name"));
                company.Address = reader.GetString(reader.GetOrdinal("Address"));
                company.Phone = reader.GetString(reader.GetOrdinal("Phone"));
            }
            return company;
        }
        public object ResetList()
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult("select * from Companies", BuildCompaniesList);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void InsertCompanyToDB(string UserID, string Name, string Address, string Phone)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Insert Into Companies(UserID, Name, Address, Phone, Money) Values('{UserID}','{Name}','{Address}','{Phone}')");
            }
            catch (Exception ex)
            {
            }
        }
        public object GetCompanyFromDB(string UserID)
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult($"Select * from Companies where UserID= '{UserID}'", BuildCompany);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeleteCompanyFromDB(string UserID)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Delete from Companies where UserID= '{UserID}'");
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateCompanyInDB(string UserID, string Name, string Address, string Phone)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Update Companies set Name='{Name}' , Address='{Address}' , Phone='{Phone}' where UserID= '{UserID}'");
            }
            catch (Exception ex)
            {

            }
        }
    }*/
}
