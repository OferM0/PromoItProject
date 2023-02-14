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
using Tweetinvi;
using Tweetinvi.Models;
using server.Entities;
using Utilities;
using server.Model;
using System.Collections.Generic;
using Tweetinvi.Core.Models;

namespace server.MicroService
{
    public static class Tweets
    {
       //private static string APIKey = "38GtVBOTriwJDED3x7tbPlQXv";
       //private static string APIKeySecret = "I6cMBtct7YqHtvAx10BMHTtdhYdwexopE3HUTq8wxWIh90B8Ni";
       //private static string AccessToken = "1607255581571649536-SHd7bffqugCkgrHPmj6Qc6PClVWIuG";
       //private static string AccessTokenSecret = "JOyLBJSudXASgjlV8q8I1NhA2QiqHhha7nYLAggJPzeKW";

        private static string APIKey = Environment.GetEnvironmentVariable("APIKey");
        private static string APIKeySecret = Environment.GetEnvironmentVariable("APIKeySecret");
        private static string AccessToken = Environment.GetEnvironmentVariable("AccessToken");
        private static string AccessTokenSecret = Environment.GetEnvironmentVariable("AccessTokenSecret");

        [FunctionName("Tweets")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Tweets/{action}/{type}/{user}/{hashtag}")] HttpRequest req,
            string action, string type, string user, string hashtag, Microsoft.Extensions.Logging.ILogger log1)
        {
            //in case of post--> type = activistTwitterHandle, user = companyTwitterHandle, hashtag = productId

            log1.LogInformation("C# HTTP trigger function processed a request.");

            MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Activate Twitter Azure function- api." });
            string responseBody = "";

            switch (action)
            {
                case "Post":
                    MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Run Tweets.Post command" });
                    try
                    {
                        TwitterClient userClient = new TwitterClient(APIKey, APIKeySecret, AccessToken, AccessTokenSecret);
                        // publish a tweet
                        string tweetText = $"Congratulations! @{type} just bought a product(id:{hashtag}) from @{user}, in PromoIt you can promote the society and also earn points and buy cool products.";
                        var tweet = await userClient.Tweets.PublishTweetAsync(tweetText);
                        responseBody = tweet.Text;
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute Tweets.Post command, {ex.Message}" });
                        responseBody = "Failed to post tweet.";
                    }

                    break;
                /*
                case "Get":
                    if (type == "retweet")
                    {
                        HttpClient client = new HttpClient();
                        HttpRequestMessage request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, $"https://api.twitter.com/2/tweets/search/recent?query=retweets_of%3A{user}%20%22{hashtag}%22%20has%3Ahashtags%20has%3Alinks%20is%3Aretweet");
                        request.Headers.Add("Authorization", Environment.GetEnvironmentVariable("TweeterBarear"));
                        HttpResponseMessage response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();
                    }
                    else if(type == "tweet")
                    {
                        HttpClient client = new HttpClient();
                        HttpRequestMessage request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, $"https://api.twitter.com/2/tweets/search/recent?query=from%3A{user}%20{hashtag}%20has%3Ahashtags%20has%3Alinks%20-is%3Aretweet");
                        request.Headers.Add("Authorization", Environment.GetEnvironmentVariable("TweeterBarear"));
                        HttpResponseMessage response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();                      
                    }
                    break;
                */
            }

            return new OkObjectResult(responseBody);
        }
    }
}