using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model;
using Utilities;

namespace server.Data.Sql
{
    public class ProductsQueries : BaseDataSql
    {
        public ProductsQueries(Logger log) : base(log) { }

        public List<Product> BuildProductsList(SqlDataReader reader)
        {
            List<Product> productsList = new List<Product>();

            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute BuildProductsList function in ProductsQueries." });
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
                    product.Shipped = reader.GetBoolean(reader.GetOrdinal("Shipped"));
                    product.Image = reader.GetString(reader.GetOrdinal("Image"));
                    productsList.Add(product);
                }
                return productsList;
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run BuildProductsList function in ProductQueries." });
                throw ex;
            }
        }
        public Product BuildProduct(SqlDataReader reader)
        {
            Product product = new Product();

            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute BuildProduct function in ProductsQueries." });
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
                    product.Shipped = reader.GetBoolean(reader.GetOrdinal("Shipped"));
                    product.Image = reader.GetString(reader.GetOrdinal("Image"));
                }
                return product;
            }
            catch(Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run BuildProduct function in ProductQueries." });
                throw ex;
            }
        }

        public object ResetList()
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute ResetList function in ProductsQueries." });
                return DAL.SqlQuery.RunCommandResult("select * from Products", BuildProductsList);
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run ResetList function in ProductQueries." });
                throw ex;
            }
        }

        public void InsertProductToDB(string Name, string Description, decimal Price, string ActivistID, string CompanyID, string OrganizationID, int CampaignID, bool DonatedByActivist, bool Shipped, string Image)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute InsertProductToDB function in ProductsQueries." });
                DAL.SqlQuery.RunNonQueryCommand($"Insert Into Products(Name, Description, Price,ActivistID,CompanyID,OrganizationID,CampaignID,DonatedByActivist, Shipped, Image) Values('{Name}','{Description}','{Price}','{ActivistID}','{CompanyID}','{OrganizationID}','{CampaignID}','{DonatedByActivist}','{Shipped}','{Image}')");
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run InsertProductToDB function in ProductQueries." });
                throw ex;
            }
        }

        public object GetProductFromDB(string id)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetProductFromDB(id:{id}) function in ProductsQueries." });
                return DAL.SqlQuery.RunCommandResult($"select * from Products where Id= '{id}'", BuildProduct);
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run GetProductFromDB function in ProductQueries." });
                throw ex;
            }
        }

        public void DeleteProductFromDB(string id)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteProductFromDB(id:{id}) function in ProductsQueries." });
                DAL.SqlQuery.RunNonQueryCommand($"Delete from Products where Id= '{id}'");
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run DeleteProductFromDB function in ProductQueries." });
                throw ex;
            }
        }

        public void UpdateProductInDB(string Id, string Name, string Description, decimal Price, string ActivistID, string CompanyID, string OrganizationID, int CampaignID, bool DonatedByActivist, bool Shipped)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateProductInDB(id:{Id}) function in ProductsQueries." });
                DAL.SqlQuery.RunNonQueryCommand($"Update Products set Name='{Name}' , Description='{Description}' , Price='{Price}', ActivistID='{ActivistID}' , CompanyID='{CompanyID}' , OrganizationID='{OrganizationID}' , CampaignID='{CampaignID}' , DonatedByActivist='{DonatedByActivist}' , Shipped='{Shipped}' where Id= '{Id}'");
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run UpdateProductInDB function in ProductQueries." });
                throw ex;
            }
        }
    }
}
