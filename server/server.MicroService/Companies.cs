using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using server.Model;
using server.Entities;

namespace server.MicroService
{
    /*
    public static class Companies
    {
        [FunctionName("Companies")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Companies/{action}/{UserID?}")] HttpRequest req,
            string action, string UserID, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string name = req.Query["name"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            
            string responseMessage = "";
            //Entities.Companies helper = new Entities.Companies(MainManager.Instance.log);

            switch (action)
            {
                case "Add":
                    Company c = System.Text.Json.JsonSerializer.Deserialize<Company>(req.Body); //convert from json to company object after post(react-axios)
                    MainManager.Instance.companies.AddNewCompany(c.UserID, c.Name, c.Address, c.Phone); //add to DB- run sql command and to list
                    responseMessage = System.Text.Json.JsonSerializer.Serialize(c); //to see if the new Company object updated
                    return new OkObjectResult(responseMessage);
                    break;

                case "Remove":
                    if (UserID != null) //remove only by UserID
                    {
                        MainManager.Instance.companies.DeleteCompanyById(UserID);
                    }
                    break;

                case "Update":
                    if (UserID != null) //update only by UserID
                    {
                        Company c2 = System.Text.Json.JsonSerializer.Deserialize<Company>(req.Body);
                        MainManager.Instance.companies.UpdateCompanyById(UserID, c2.Name, c2.Address, c2.Phone);
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(c2);
                        return new OkObjectResult(responseMessage);
                    }
                    break;

                case "Get":
                    if (UserID == null) //get all
                    {
                        MainManager.Instance.Init6();
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.companiesList);
                        return new OkObjectResult(responseMessage);
                    }
                    else //get by UserID
                    {
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.companies.GetCompanyById(UserID));
                        return new OkObjectResult(responseMessage);
                    }
                    break;
            }

            return new OkObjectResult(responseMessage);
        }
    }*/
}
