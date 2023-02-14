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
using Utilities;

namespace server.MicroService
{
    public static class Campaigns
    {
        [FunctionName("Campaigns")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Campaigns/{action}/{id?}")] HttpRequest req,
            string action, string id, Microsoft.Extensions.Logging.ILogger log1)
        {

            log1.LogInformation("C# HTTP trigger function processed a request.");
            /*string name = req.Query["name"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            */
            //string responseMessage = "";
            //Entities.Campaigns helper = new Entities.Campaigns(MainManager.Instance.log);

            //Campaigns.Add
            //Campaigns.Remove    
            //Campaigns.Update
            //Campaigns.Get
            MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Activate Campaign Azure function- api." });
            string cmdName = "Campaigns." + action;
            ICommand cmd = MainManager.Instance.commandsManager.CommandList[cmdName];
            if (cmd != null)
            {
                try
                {
                    MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Run {cmdName} command" });
                    string body = await req.ReadAsStringAsync();
                    return new OkObjectResult(cmd.Execute(id, body));
                }
                catch(Exception ex)
                {
                    MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute command {ex.Message}" });
                    return new BadRequestObjectResult("Error "+ex.Message);
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
                    Campaign c = System.Text.Json.JsonSerializer.Deserialize<Campaign>(req.Body); //convert from json to campaigns object after post(react-axios)
                    MainManager.Instance.campaigns.AddNewCampaign(c.OrganizationID, c.Name, c.Description, c.Url, c.Hashtag, c.Active, c.CreateDate); //add to DB- run sql command and to list
                    responseMessage = System.Text.Json.JsonSerializer.Serialize(c); //to see if the new campaign object updated
                    return new OkObjectResult(responseMessage);
                    break;

                case "Remove":
                    if (id != null) //remove only by id
                    {
                        MainManager.Instance.campaigns.DeleteCampaignById(id);
                    }
                    break;

                case "Update":
                    if (id != null) //update only by id
                    {
                        Campaign c2 = System.Text.Json.JsonSerializer.Deserialize<Campaign>(req.Body);
                        MainManager.Instance.campaigns.UpdateCampaignById(id, c2.Name, c2.Description, c2.Url, c2.Hashtag, c2.Active);
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
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.campaigns.GetCampaignById(id));
                        return new OkObjectResult(responseMessage);
                    }
                    return new OkObjectResult(responseMessage);
                    break;
            }
            */

            //return new OkObjectResult(responseMessage);
        }
    }
}
