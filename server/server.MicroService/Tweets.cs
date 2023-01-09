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
using System.Text;

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
                    request1.Headers.Add("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAKthlAEAAAAAS%2BZoFBRCRqbdy05wokFoXm24lgY%3DknwW3XtvbroPk48HJhUdI46AbUx7cB3CIxqICOxGx8WzsfuT56");
                    //string tweetText = $"Congratulations! @{type} just bought a product from @{user}, in PromoIt you can promote the society and also earn points and buy cool products.";
                    request1.Content = new StringContent("{\"status\": \"Hello, world!\"}");
                    request1.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");                    
                    HttpResponseMessage response1 = await client1.SendAsync(request1);
                    response1.EnsureSuccessStatusCode();
                    responseBody = await response1.Content.ReadAsStringAsync();

                    /*HttpClient client1 = new HttpClient();
                    HttpRequestMessage request1 = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token");
                    request1.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("yauvoru5WnQtkLtC3qkkbcANj:wO5Dic1kLWJM0itg0FjKqEl9sR4CaOxkNUKDJgF7oMTCt0AI1p")));
                    request1.Content = new StringContent("grant_type=client_credentials");
                    request1.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                    HttpResponseMessage response1 = await client1.SendAsync(request1);
                    response1.EnsureSuccessStatusCode();
                    responseBody = await response1.Content.ReadAsStringAsync();*/
                    break;

                case "Get":
                    if (type == "retweet")
                    {
                        HttpClient client = new HttpClient();
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://api.twitter.com/2/tweets/search/recent?query=retweets_of%3A{user}%20%22{hashtag}%22%20has%3Ahashtags%20has%3Alinks%20is%3Aretweet");
                        request.Headers.Add("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAKthlAEAAAAAS%2BZoFBRCRqbdy05wokFoXm24lgY%3DknwW3XtvbroPk48HJhUdI46AbUx7cB3CIxqICOxGx8WzsfuT56");
                        HttpResponseMessage response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();
                    }else if(type == "tweet")
                    {
                        HttpClient client = new HttpClient();
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://api.twitter.com/2/tweets/search/recent?query=from%3A{user}%20{hashtag}%20has%3Ahashtags%20has%3Alinks%20-is%3Aretweet");
                        request.Headers.Add("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAKthlAEAAAAAS%2BZoFBRCRqbdy05wokFoXm24lgY%3DknwW3XtvbroPk48HJhUdI46AbUx7cB3CIxqICOxGx8WzsfuT56");
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
//yaNRbQ3H8Zq87AcQTRSFl9gXY
//XbcjQkoE5RaCkF6uZnHp8zavPrpMAlvqOYXzO1t1zk1xFFTu7z
//AAAAAAAAAAAAAAAAAAAAAKthlAEAAAAAS%2BZoFBRCRqbdy05wokFoXm24lgY%3DknwW3XtvbroPk48HJhUdI46AbUx7cB3CIxqICOxGx8WzsfuT56