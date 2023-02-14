using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json;
using server.Entities;
using server.Model;
using Utilities;

namespace server.MicroService
{
    public static class Products
    {
        [FunctionName("Products")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get","post", Route = "Products/{action}/{id?}")] HttpRequest req,
            string action, string id, Microsoft.Extensions.Logging.ILogger log1)
        {
            log1.LogInformation("C# HTTP trigger function processed a request.");
            /*string name = req.Query["name"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;*/
            //string responseMessage="";
            //Entities.Products helper = new Entities.Products(MainManager.Instance.log);

            //Products.Add
            //Products.Remove    
            //Products.Update
            //Products.Get
            MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Activate Products Azure function- api." });
            string cmdName = "Products." + action;
            ICommand cmd = MainManager.Instance.commandsManager.CommandList[cmdName];
            if (cmd != null)
            {
                try
                {
                    MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Run {cmdName} command" });
                    string body = await req.ReadAsStringAsync();
                    return new OkObjectResult(cmd.Execute(id, body));
                }
                catch (Exception ex)
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute command {ex.Message}" });
                    return new BadRequestObjectResult("Error " + ex.Message);
                }
            }
            else
            {
                //Error
                return new BadRequestObjectResult("Error");
            }
            /*
            switch (action)
            {
                case "Add":
                    Product p = System.Text.Json.JsonSerializer.Deserialize<Product>(req.Body); //convert from json to product object after post(react-axios)
                    MainManager.Instance.products.AddNewProduct(p.Name, p.Description, p.Price, p.ActivistID, p.CompanyID, p.OrganizationID, p.ProductID, p.DonatedByActivist, p.Shipped, p.Image); //add to DB- run sql command and to list
                    responseMessage = System.Text.Json.JsonSerializer.Serialize(p); //to see if the new product object updated
                    return new OkObjectResult(responseMessage);
                    break;

                case "Remove":
                    if (id != null)
                    {
                        MainManager.Instance.products.DeleteProductById(id);
                    }
                    break;

                case "Update":
                    if (id != null)
                    {
                        Product p2 = System.Text.Json.JsonSerializer.Deserialize<Product>(req.Body);
                        MainManager.Instance.products.UpdateProductById(id, p2.Name, p2.Description, p2.Price, p2.ActivistID, p2.CompanyID, p2.OrganizationID, p2.ProductID, p2.DonatedByActivist, p2.Shipped);
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(p2);
                        return new OkObjectResult(responseMessage);
                    }
                    break;

                case "Get":
                    if(id == null)
                    {
                        MainManager.Instance.Init();
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList);
                        return new OkObjectResult(responseMessage);
                    }
                    else
                    {
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.products.GetProductById(id));
                        return new OkObjectResult(responseMessage);
                    }
                    break;
            }       

            return new OkObjectResult(responseMessage);
            */
        }
    }
}
