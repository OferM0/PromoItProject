using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model;

namespace server.Data.Sql
{
    public class ProductsQueries
    {
        public List<Product> BuildProductsList(SqlDataReader reader)
        {
            List<Product> productsList = new List<Product>();

            while (reader.Read())
            {
                Product product = new Product();
                product.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                product.Name = reader.GetString(reader.GetOrdinal("Name"));
                product.Description = reader.GetString(reader.GetOrdinal("Description"));
                product.Price = reader.GetDecimal(reader.GetOrdinal("Price"));
                product.ActivistID = reader.GetString(reader.GetOrdinal("ActivistID"));
                product.CompanyID = reader.GetString(reader.GetOrdinal("CompanyID"));
                product.OrganizationID = reader.GetString(reader.GetOrdinal("OrganizationID"));
                product.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                product.DonatedByActivist = reader.GetBoolean(reader.GetOrdinal("DonatedByActivist"));
                productsList.Add(product);
            }
            return productsList;
        }
        public Product BuildProduct(SqlDataReader reader)
        {
            Product product = new Product();

            while (reader.Read())
            {
                product.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                product.Name = reader.GetString(reader.GetOrdinal("Name"));
                product.Description = reader.GetString(reader.GetOrdinal("Description"));
                product.Price = reader.GetDecimal(reader.GetOrdinal("Price"));
                product.ActivistID = reader.GetString(reader.GetOrdinal("ActivistID"));
                product.CompanyID = reader.GetString(reader.GetOrdinal("CompanyID"));
                product.OrganizationID = reader.GetString(reader.GetOrdinal("OrganizationID"));
                product.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                product.DonatedByActivist = reader.GetBoolean(reader.GetOrdinal("DonatedByActivist"));
            }
            return product;
        }

        public object ResetList()
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult("select * from Products", BuildProductsList);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void InsertProductToDB(string Name, string Description, decimal Price, string ActivistID, string CompanyID, string OrganizationID, int CampaignID, bool DonatedByActivist)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Insert Into Products(Name, Description, Price,ActivistID,CompanyID,OrganizationID,CampaignID,DonatedByActivist) Values('{Name}','{Description}','{Price}','{ActivistID}','{CompanyID}','{OrganizationID}'),'{CampaignID}','{DonatedByActivist}')");
            }
            catch (Exception ex)
            {
            }
        }

        public object GetProductFromDB(string id)
        {
            try
            {
                return DAL.SqlQuery.RunCommandResult($"select * from Products where Id= '{id}'", BuildProduct);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeleteProductFromDB(string id)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Delete from Products where Id= '{id}'");
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateProductInDB(string Id, string Name, string Description, decimal Price, string ActivistID, string CompanyID, string OrganizationID, int CampaignID, bool DonatedByActivist)
        {
            try
            {
                DAL.SqlQuery.RunNonQueryCommand($"Update Products set Name='{Name}' , Description='{Description}' , Price='{Price}', ActivistID='{ActivistID}' , CompanyID='{CompanyID}' , OrganizationID='{OrganizationID}' , CampaignID='{CampaignID}' , DonatedByActivist='{DonatedByActivist}' where Id= '{Id}'");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
