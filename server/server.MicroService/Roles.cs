using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using server.Entities;
using Utilities;

namespace server.MicroService
{
    public static class Roles
    {
        [FunctionName("Roles")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Roles/{action}/{userId}/{roleType?}")] HttpRequest req,
            Microsoft.Extensions.Logging.ILogger log1, string action, string userId, string roleType)
        {
            MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Activate Campaign Azure function- api." });

            switch (action)
            {
                case "Get":
                    MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Run Roles.Get command" });
                    try
                    {
                        var urlGetRoles = $"https://dev-f2zytd3c37rwi0vd.us.auth0.com/api/v2/users/{userId}/roles";
                        var client = new RestClient(urlGetRoles);
                        var request = new RestRequest("", Method.Get);
                        request.AddHeader("authorization", Environment.GetEnvironmentVariable("Auth0Barear"));
                        var response = client.Execute(request);
                        if (response.IsSuccessful)
                        {
                            var json = JArray.Parse(response.Content);
                            return new OkObjectResult(json);
                        }
                        else
                        {
                            return new NotFoundResult();
                        }
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute Roles.Get command, {ex.Message}" });
                        return new NotFoundResult();
                    }

                    break;

                case "Add":
                    MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Run Roles.Add command" });

                    try
                    {
                        var urlPostRole = $"https://dev-f2zytd3c37rwi0vd.us.auth0.com/api/v2/users/{userId}/roles";
                        var client1 = new RestClient(urlPostRole);
                        var request1 = new RestRequest("", Method.Post);
                        request1.AddHeader("content-type", "application/json");
                        request1.AddHeader("authorization", Environment.GetEnvironmentVariable("Auth0Barear"));
                        request1.AddHeader("cache-control", "no-cache");
                        if (roleType == "Social Activist")
                        {
                            request1.AddParameter("application/json", "{ \"roles\": [ \"rol_HefCI0F9cCYasKo1\" ] }", ParameterType.RequestBody);
                        }
                        else if (roleType == "Company")
                        {
                            request1.AddParameter("application/json", "{ \"roles\": [ \"rol_r83OfAwPL65cWoFR\" ] }", ParameterType.RequestBody);
                        }
                        else if (roleType == "Non-Profit Organization")
                        {
                            request1.AddParameter("application/json", "{ \"roles\": [ \"rol_m8xSnTcGS7npp5z4\" ] }", ParameterType.RequestBody);
                        }
                        var response1 = client1.Execute(request1);
                        if (response1.IsSuccessful)
                        {
                            return new OkObjectResult("Ok");
                        }
                        else
                        {
                            return new NotFoundResult();
                        }
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute Roles.Add command, {ex.Message}" });
                        return new NotFoundResult();
                    }

                    break;
            }
            return new OkObjectResult("Not Ok");
        }
    }
}
//activist rol_HefCI0F9cCYasKo1
//organization rol_m8xSnTcGS7npp5z4
//company rol_r83OfAwPL65cWoFR