using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace server.MicroService
{
    public static class Tweets
    {
        [FunctionName("Tweets")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Tweets/{action}/{type?}/{user?}/{hashtag?}")] HttpRequest req,
            string action, string type, string user, string hashtag,  ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string responseBody = "";

            switch (action)
            {
                case "Post":
                    HttpClient client1 = new HttpClient();
                    HttpRequestMessage request1 = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/2/tweets");
                    request1.Headers.Add("Authorization", "Bearer a3VXSG1CZktKdFZLSHNRb1pHWVRDaWNYX1RpSTdmVjdYRlNTTktWdFViMVkyOjE2NzI4NjM5MjA4NjE6MToxOmF0OjE");
                    //string tweetText = $"Congratulations! @{type} just bought a product from @{user}, in PromoIt you can promote the society and also earn points and buy cool products.";
                    request1.Content = new StringContent("{\"text\":\"Congratulations @Ofermord\"}");
                    request1.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");                    
                    HttpResponseMessage response1 = await client1.SendAsync(request1);
                    response1.EnsureSuccessStatusCode();
                    responseBody = await response1.Content.ReadAsStringAsync();
                    break;

                case "Get":
                    if (type == "retweet")
                    {
                        HttpClient client = new HttpClient();
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://api.twitter.com/2/tweets/search/recent?query=retweets_of%3A{user}%20%22{hashtag}%22%20has%3Ahashtags%20has%3Alinks%20is%3Aretweet");
                        request.Headers.Add("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAACUClAEAAAAAVXTzImWLJSpfWFCSyDwNpD1Zpzs%3DpZNno2etkfBfQVs94Q767phGl8etdSPOKogPU3KxrA2iVlPKiV");
                        HttpResponseMessage response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();
                    }else if(type == "tweet")
                    {
                        HttpClient client = new HttpClient();
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://api.twitter.com/2/tweets/search/recent?query=from%3A{user}%20{hashtag}%20has%3Ahashtags%20has%3Alinks%20-is%3Aretweet");
                        request.Headers.Add("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAACUClAEAAAAAVXTzImWLJSpfWFCSyDwNpD1Zpzs%3DpZNno2etkfBfQVs94Q767phGl8etdSPOKogPU3KxrA2iVlPKiV");
                        HttpResponseMessage response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();
                    }
                    break;
            }

            return new OkObjectResult(responseBody);
        }
    }
}
