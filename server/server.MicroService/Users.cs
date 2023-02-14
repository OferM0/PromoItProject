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
using Utilities;

namespace server.MicroService
{
    public static class Users
    {
        [FunctionName("Users")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Users/{action}/{UserID?}")] HttpRequest req,
            string action, string UserID, Microsoft.Extensions.Logging.ILogger log1)
        {
            log1.LogInformation("C# HTTP trigger function processed a request.");
            /*string name = req.Query["name"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            */
            //string responseMessage = "";
            //Entities.Users helper = new Entities.Users(MainManager.Instance.log);

            //Users.Add
            //Users.Remove    
            //Users.Update
            //Users.Get
            MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Activate Users Azure function- api." });
            string cmdName = "Users." + action;
            ICommand cmd = MainManager.Instance.commandsManager.CommandList[cmdName];
            if (cmd != null)
            {
                try
                {
                    MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Run {cmdName} command" });
                    string body = await req.ReadAsStringAsync();
                    return new OkObjectResult(cmd.Execute(UserID, body));
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
                    User u = System.Text.Json.JsonSerializer.Deserialize<User>(req.Body); //convert from json to users object after post(react-axios)
                    MainManager.Instance.users.AddNewUser(u.UserID, u.Role, u.Name, u.Address, u.Phone, u.Url, u.Status, u.TwitterHandle, u.CreateDate); //add to DB- run sql command and to list
                    responseMessage = System.Text.Json.JsonSerializer.Serialize(u); //to see if the new User object updated
                    return new OkObjectResult(responseMessage);
                    break;

                case "Remove":
                    if (UserID != null) //remove only by UserID
                    {
                        MainManager.Instance.users.DeleteUserById(UserID);
                    }
                    break;

                case "Update":
                    if (UserID != null) //update only by UserID
                    {
                        User u2 = System.Text.Json.JsonSerializer.Deserialize<User>(req.Body);
                        MainManager.Instance.users.UpdateUserById(UserID, u2.Name, u2.Address, u2.Phone, u2.Url, u2.Status);
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
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.users.GetUserById(UserID));
                        return new OkObjectResult(responseMessage);
                    }
                    break;
            }

            return new OkObjectResult(responseMessage);
            */
        }
    }
}
