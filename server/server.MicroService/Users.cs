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
    public static class Users
    {
        [FunctionName("Users")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Users/{action}/{UserID?}")] HttpRequest req,
            string action, string UserID, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            /*string name = req.Query["name"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            */
            string responseMessage = "";
            Entities.Users helper = new Entities.Users();

            switch (action)
            {
                case "Add":
                    User u = System.Text.Json.JsonSerializer.Deserialize<User>(req.Body); //convert from json to users object after post(react-axios)
                    helper.AddNewUser(u.UserID, u.Role, u.Name, u.Address, u.Phone, u.Url); //add to DB- run sql command and to list
                    responseMessage = System.Text.Json.JsonSerializer.Serialize(u); //to see if the new User object updated
                    return new OkObjectResult(responseMessage);
                    break;

                case "Remove":
                    if (UserID != null) //remove only by UserID
                    {
                        helper.DeleteUserById(UserID);
                    }
                    break;

                case "Update":
                    if (UserID != null) //update only by UserID
                    {
                        User u2 = System.Text.Json.JsonSerializer.Deserialize<User>(req.Body);
                        helper.UpdateUserById(UserID, u2.Name, u2.Address, u2.Phone, u2.Url);
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(u2);
                        return new OkObjectResult(responseMessage);
                    }
                    break;

                case "Get":
                    if (UserID == null) //get all
                    {
                        MainManager.Instance.Init3();
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.usersList);
                        return new OkObjectResult(responseMessage);
                    }
                    else //get by UserID
                    {
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(helper.GetUserById(UserID));
                        return new OkObjectResult(responseMessage);
                    }
                    break;
            }

            return new OkObjectResult(responseMessage);
        }
    }
}
