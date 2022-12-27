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
                    helper.AddNewProduct(p.ProductID, p.ProductName, p.SupplierID, p.CategoryID, p.QuantityPerUnit, p.UnitPrice, p.UnitsInStock, p.UnitsOnOrder, p.ReorderLevel, p.Discontinued); //add to DB- run sql command and to list
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
                        helper.UpdateProductById(id, p2.ProductID, p2.ProductName, p2.SupplierID, p2.CategoryID, p2.QuantityPerUnit, p2.UnitPrice, p2.UnitsInStock, p2.UnitsOnOrder, p2.ReorderLevel, p2.Discontinued);
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
