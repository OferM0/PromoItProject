using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace server.Entities.Commands
{
    public class ProductsGetCmd : BaseCommand, ICommand
    {
        public object Execute(params object[] param)
        {
            if (param[0] == null) //get all
            {
                try
                {
                    MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Initialize products list, and execute sql command to get all products from DB and return to user." });
                    MainManager.Instance.Init();
                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList);
                }
                catch (Exception ex)
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to get all products from DB and return to user." });
                    return "Error: " + ex.Message;
                }
            }
            else //get by id
            {
                string id = (string)param[0];
                if (id != null)
                {
                    try
                    {
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to get one product(id:{id}) from DB and return it to user." });
                        return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.products.GetProductById(id));
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to get one product(id:{id}) from DB and return it to user." });
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to get one product from DB and return it to user" });
                    return "Error: Missing parameters";
                }
            }

        }
    }

    public class ProductsUpdateCmd : BaseCommand, ICommand
    {
        public object Execute(params object[] param)
        {
            if (param.Length > 0)
            {
                string id = (string)param[0];
                string body = (string)param[1];
                if (id != null && body != null) //update only by id
                {
                    try
                    {
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to update product(id:{id})." });
                        Product p2 = System.Text.Json.JsonSerializer.Deserialize<Product>(body);
                        MainManager.Instance.products.UpdateProductById(id, p2.Name, p2.Description, p2.Price, p2.ActivistID, p2.CompanyID, p2.OrganizationID, p2.CampaignID, p2.DonatedByActivist, p2.Shipped);
                        return System.Text.Json.JsonSerializer.Serialize(p2);
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to update product(id:{id})." });
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Error in parameters, Failed to execute sql command to update product." });
                    return "Error: error in parameters";
                }
            }
            else
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to update product." });
                return "Error: Missing parameters";
            }
        }
    }

    public class ProductsAddCmd : BaseCommand, ICommand
    {
        public object Execute(params object[] param)
        {
            if (param.Length > 0)
            {
                string body = (string)param[1];
                if (body != null) //update only by id
                {
                    try
                    {
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to add product." });
                        Product p = System.Text.Json.JsonSerializer.Deserialize<Product>(body);
                        MainManager.Instance.products.AddNewProduct(p.Name, p.Description, p.Price, p.ActivistID, p.CompanyID, p.OrganizationID, p.CampaignID, p.DonatedByActivist, p.Shipped, p.Image);
                        return System.Text.Json.JsonSerializer.Serialize(p);
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to add product." });
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Error in parameters, Failed to execute sql command to add product." });
                    return "Error: error in parameters";
                }
            }
            else
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to add product." });
                return "Error: Missing parameters";
            }
        }
    }

    public class ProductsRemoveCmd : BaseCommand, ICommand
    {
        public object Execute(params object[] param)
        {
            if (param.Length > 0) //get all
            {
                string id = (string)param[0];
                if (id != null)
                {
                    try
                    {
                        MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute sql command to remove product(id:{id})." });
                        MainManager.Instance.products.DeleteProductById(id);
                        return "Product Removed successfully";
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to execute sql command to remove product(id:{id})." });
                        return "Error: " + ex.Message;
                    }
                }
                else
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Error in parameters, Failed to execute sql command to remove product." });
                    return "Error: error in parameters";
                }
            }
            else
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Missing parameters, Failed to execute sql command to remove product." });
                return "Error: Missing parameters";
            }
        }
    }
}
