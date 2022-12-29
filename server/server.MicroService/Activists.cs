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
    public static class Activists
    {
        [FunctionName("Activists")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Activists/{action}/{UserID?}")] HttpRequest req,
             string action, string UserID, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            /*string name = req.Query["name"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            */
            string responseMessage = "";
            Entities.Activists helper = new Entities.Activists();

            switch (action)
            {
                case "Add":
                    Activist a = System.Text.Json.JsonSerializer.Deserialize<Activist>(req.Body); //convert from json to activists object after post(react-axios)
                    helper.AddNewActivist(a.UserID, a.Name, a.Address, a.Phone, a.Money); //add to DB- run sql command and to list
                    responseMessage = System.Text.Json.JsonSerializer.Serialize(a); //to see if the new Activist object updated
                    return new OkObjectResult(responseMessage);
                    break;

                case "Remove":
                    if (UserID != null) //remove only by ActivistID
                    {
                        helper.DeleteActivistById(UserID);
                    }
                    break;

                case "Update":
                    if (UserID != null) //update only by ActivistID
                    {
                        Activist a2 = System.Text.Json.JsonSerializer.Deserialize<Activist>(req.Body);
                        helper.UpdateActivistById(UserID, a2.Name, a2.Address, a2.Phone, a2.Money);
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(a2);
                        return new OkObjectResult(responseMessage);
                    }
                    break;

                case "Get":
                    if (UserID == null) //get all
                    {
                        MainManager.Instance.Init4();
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.activistsList);
                        return new OkObjectResult(responseMessage);
                    }
                    else //get by ActivistID
                    {
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(helper.GetActivistById(UserID));
                        return new OkObjectResult(responseMessage);
                    }
                    break;
            }

            return new OkObjectResult(responseMessage);
        }
    }
}
