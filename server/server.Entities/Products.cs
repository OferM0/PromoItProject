using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using server.Model;
using System.Data;
using server.Data.Sql;
using Utilities;

namespace server.Entities
{
    public class Products : BaseEntity
    {
        public Products(Logger log) : base(log) { productsQueries = new ProductsQueries(base._log); }

        ProductsQueries productsQueries;

        public void ClearList()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute  ClearList function in Products Entity." });
                MainManager.Instance.productsList.Clear();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }      

        public void GetProductsFromDB()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetProductsFromDB function in Products Entity." });
                ClearList();
                MainManager.Instance.productsList = ((List<Product>)productsQueries.ResetList());
                //return MainManager.Instance.productsList;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public Product GetProductById(string id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetProductById(id:{id}) function in Products Entity." });
                Product product = ((Product)productsQueries.GetProductFromDB(id));
                return product;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public void AddNewProduct(string Name, string Description, decimal Price, string ActivistID, string CompanyID, string OrganizationID, int CampaignID, bool DonatedByActivist, bool Shipped, string Image)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute AddNewProduct function in Products Entity." });
                Product product = new Product
                {
                    Name = Name,
                    Description = Description,
                    Price = Price,
                    ActivistID = ActivistID,
                    CompanyID = CompanyID,
                    OrganizationID = OrganizationID,
                    CampaignID = CampaignID,
                    DonatedByActivist = DonatedByActivist,
                    Shipped = Shipped,
                    Image = Image
                };
                MainManager.Instance.productsList.Add(product);
                productsQueries.InsertProductToDB(Name, Description, Price, ActivistID, CompanyID, OrganizationID, CampaignID, DonatedByActivist, Shipped, Image);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public void UpdateProductById(string Id, string Name, string Description, decimal Price, string ActivistID, string CompanyID, string OrganizationID, int CampaignID, bool DonatedByActivist, bool Shipped)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateProductById(id:{Id}) function in Products Entity." });
                productsQueries.UpdateProductInDB(Id, Name, Description, Price, ActivistID, CompanyID, OrganizationID, CampaignID, DonatedByActivist, Shipped);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public Product GetProductFromList(string id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetProductFromList(id:{id}) function in Products Entity." });
                return MainManager.Instance.productsList[int.Parse(id)];
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public void DeleteProductById(string id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteProductById(id:{id}) function in Products Entity." });
                productsQueries.DeleteProductFromDB(id);
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }
    }
}
