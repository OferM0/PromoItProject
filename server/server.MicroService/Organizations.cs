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
    public static class Organizations
    {
        [FunctionName("Organizations")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Organizations/{action}/{UserID?}")] HttpRequest req,
            string action, string UserID, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            /*string name = req.Query["name"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            */
            string responseMessage = "";
            Entities.Organizations helper = new Entities.Organizations();

            switch (action)
            {
                case "Add":
                    Organization o = System.Text.Json.JsonSerializer.Deserialize<Organization>(req.Body); //convert from json to organizations object after post(react-axios)
                    helper.AddNewOrganization(o.UserID, o.Name, o.Address, o.Phone, o.Url); //add to DB- run sql command and to list
                    responseMessage = System.Text.Json.JsonSerializer.Serialize(o); //to see if the new Organization object updated
                    return new OkObjectResult(responseMessage);
                    break;

                case "Remove":
                    if (UserID != null) //remove only by OrganizationID
                    {
                        helper.DeleteOrganizationById(UserID);
                    }
                    break;

                case "Update":
                    if (UserID != null) //update only by OrganizationID
                    {
                        Organization o2 = System.Text.Json.JsonSerializer.Deserialize<Organization>(req.Body);
                        helper.UpdateOrganizationById(UserID, o2.Name, o2.Address, o2.Phone, o2.Url);
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(o2);
                        return new OkObjectResult(responseMessage);
                    }
                    break;

                case "Get":
                    if (UserID == null) //get all
                    {
                        MainManager.Instance.Init5();
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.organizationsList);
                        return new OkObjectResult(responseMessage);
                    }
                    else //get by OrganizationID
                    {
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(helper.GetOrganizationById(UserID));
                        return new OkObjectResult(responseMessage);
                    }
                    break;
            }

            return new OkObjectResult(responseMessage);
        }
    }
}
