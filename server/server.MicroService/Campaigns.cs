using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using server.Entities;
using server.Model;

namespace server.MicroService
{
    public static class Campaigns
    {
        [FunctionName("Campaigns")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Campaigns/{action}/{id?}")] HttpRequest req,
            string action, string id, ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");
            /*string name = req.Query["name"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            */
            string responseMessage = "";
            Entities.Campaigns helper = new Entities.Campaigns();

            switch (action)
            {
                case "Add":
                    Campaign c = System.Text.Json.JsonSerializer.Deserialize<Campaign>(req.Body); //convert from json to campaigns object after post(react-axios)
                    helper.AddNewCampaign(c.OrganizationID, c.Name, c.Description, c.Url, c.Hashtag, c.Active); //add to DB- run sql command and to list
                    responseMessage = System.Text.Json.JsonSerializer.Serialize(c); //to see if the new campaign object updated
                    return new OkObjectResult(responseMessage);
                    break;

                case "Remove":
                    if (id != null) //remove only by id
                    {
                        helper.DeleteCampaignById(id);
                    }
                    break;

                case "Update":
                    if (id != null) //update only by id
                    {
                        Campaign c2 = System.Text.Json.JsonSerializer.Deserialize<Campaign>(req.Body);
                        helper.UpdateCampaignById(id, c2.Name, c2.Description, c2.Url, c2.Hashtag, c2.Active);
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(c2);
                        return new OkObjectResult(responseMessage);
                    }
                    break;

                case "Get":
                    if (id == null) //get all
                    {
                        MainManager.Instance.Init7();
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.campaignsList);
                        return new OkObjectResult(responseMessage);
                    }
                    else //get by id
                    {
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(helper.GetCampaignById(id));
                        return new OkObjectResult(responseMessage);
                    }
                    break;
            }

            return new OkObjectResult(responseMessage);
        }
    }
}
