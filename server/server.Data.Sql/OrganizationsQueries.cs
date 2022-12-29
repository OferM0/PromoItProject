using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using server.Model;

namespace server.Data.Sql
{
    public class OrganizationsQueries
    {
        public Dictionary<string, Organization> BuildOrganizationsList(SqlDataReader reader)
        {
            Dictionary<string, Organization> organizationsList = new Dictionary<string, Organization>();

            while (reader.Read())
            {
                Organization organization = new Organization();
                organization.UserID = reader.GetString(reader.GetOrdinal("UserID"));
                organization.Name = reader.GetString(reader.GetOrdinal("Name"));
                organization.Address = reader.GetString(reader.GetOrdinal("Address"));
                organization.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                organization.Url = reader.GetString(reader.GetOrdinal("Url"));
                organizationsList.Add(organization.UserID, organization);
            }
            return organizationsList;
        }
        public Organization BuildOrganization(SqlDataReader reader)
        {
            Organization organization = new Organization();

            while (reader.Read())
            {
                organization.UserID = reader.GetString(reader.GetOrdinal("UserID"));
                organization.Name = reader.GetString(reader.GetOrdinal("Name"));
                organization.Address = reader.GetString(reader.GetOrdinal("Address"));
                organization.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                organization.Url = reader.GetString(reader.GetOrdinal("Url"));
            }
            return organization;
        }
        public object ResetList()
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult("select * from Organizations", BuildOrganizationsList);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void InsertOrganizationToDB(string UserID, string Name, string Address, string Phone, string Url)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Insert Into Organizations(UserID, Name, Address, Phone, Url) Values('{UserID}','{Name}','{Address}','{Phone}','{Url}')");
            }
            catch (Exception ex)
            {
            }
        }
        public object GetOrganizationFromDB(string UserID)
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult($"Select * from Organizations where UserID= '{UserID}'", BuildOrganization);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeleteOrganizationFromDB(string UserID)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Delete from Organizations where UserID= '{UserID}'");
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateOrganizationInDB(string UserID, string Name, string Address, string Phone, string Url)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Update Organizations set Name='{Name}' , Address='{Address}' , Phone='{Phone}' , Url='{Url}' where UserID= '{UserID}'");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
