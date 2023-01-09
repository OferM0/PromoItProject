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

namespace server.MicroService
{
    public static class Products
    {
        [FunctionName("Products")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get","post", Route = "Products/{action}/{id?}")] HttpRequest req,
            string action, string id, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            /*string name = req.Query["name"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;*/
            string responseMessage="";
            Entities.Products helper = new Entities.Products();

            switch (action)
            {
                case "Add":
                    Product p = System.Text.Json.JsonSerializer.Deserialize<Product>(req.Body); //convert from json to product object after post(react-axios)
                    helper.AddNewProduct(p.Name, p.Description, p.Price, p.ActivistID, p.CompanyID, p.OrganizationID, p.CampaignID, p.DonatedByActivist, p.Shipped, p.Image); //add to DB- run sql command and to list
                    responseMessage = System.Text.Json.JsonSerializer.Serialize(p); //to see if the new product object updated
                    return new OkObjectResult(responseMessage);
                    break;

                case "Remove":
                    if (id != null)
                    {
                        helper.DeleteProductById(id);
                    }
                    break;

                case "Update":
                    if (id != null)
                    {
                        Product p2 = System.Text.Json.JsonSerializer.Deserialize<Product>(req.Body);
                        helper.UpdateProductById(id, p2.Name, p2.Description, p2.Price, p2.ActivistID, p2.CompanyID, p2.OrganizationID, p2.CampaignID, p2.DonatedByActivist, p2.Shipped);
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
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(helper.GetProductById(id));
                        return new OkObjectResult(responseMessage);
                    }
                    break;
            }       

            return new OkObjectResult(responseMessage);
        }
    }
}
