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
    public static class Contacts
    {
        [FunctionName("Contacts")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Contacts/{action}/{id?}")] HttpRequest req,
            string action, string id, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            /*string name = req.Query["name"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            */
            string responseMessage = "";
            Entities.Contacts helper = new Entities.Contacts();

            switch (action)
            {
                case "Add":
                    Contact c = System.Text.Json.JsonSerializer.Deserialize<Contact>(req.Body); //convert from json to contacts object after post(react-axios)
                    helper.AddNewContact(c.Name, c.Email, c.Phone, c.Message); //add to DB- run sql command and to list
                    responseMessage = System.Text.Json.JsonSerializer.Serialize(c); //to see if the new contact object updated
                    return new OkObjectResult(responseMessage);
                    break;

                case "Remove":
                    if (id != null) //remove only by id
                    {
                        helper.DeleteContactById(id);
                    }
                    break;

                case "Update":
                    if (id != null) //update only by id
                    {
                        Contact c2 = System.Text.Json.JsonSerializer.Deserialize<Contact>(req.Body);
                        helper.UpdateContactById(id, c2.Name, c2.Email, c2.Phone, c2.Message);
                        responseMessage=System.Text.Json.JsonSerializer.Serialize(c2);
                        return new OkObjectResult(responseMessage);
                    }
                    break;

                case "Get":
                    if (id == null) //get all
                    {
                        MainManager.Instance.Init2();
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.contactsList);
                        return new OkObjectResult(responseMessage);
                    }
                    else //get by id
                    {
                        responseMessage = System.Text.Json.JsonSerializer.Serialize(helper.GetContactById(id));
                        return new OkObjectResult(responseMessage);
                    }
                    break;
            }

            return new OkObjectResult(responseMessage);           
        }
    }
}
