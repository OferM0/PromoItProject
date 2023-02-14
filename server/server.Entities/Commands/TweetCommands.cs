using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Tweetinvi;
using Tweetinvi.Models;
using server.Entities;
using Utilities;

namespace server.Entities.Commands
{/*
    public class TweetsGetCmd : BaseCommand, ICommand
    {
        
        public object Execute(params object[] param)
        {
            if (param.Length > 0) //get all
            {
                string responseBody = "";
                string type = (string)param[0];
                string user = (string)param[1];
                string hashtag = (string)param[2];
                try
                {
                    if (type == "retweet")
                    {
                        HttpClient client = new HttpClient();
                        HttpRequestMessage request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, $"https://api.twitter.com/2/tweets/search/recent?query=retweets_of%3A{user}%20%22{hashtag}%22%20has%3Ahashtags%20has%3Alinks%20is%3Aretweet");
                        request.Headers.Add("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAKthlAEAAAAAS%2BZoFBRCRqbdy05wokFoXm24lgY%3DknwW3XtvbroPk48HJhUdI46AbUx7cB3CIxqICOxGx8WzsfuT56");
                        HttpResponseMessage response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();
                        TweetData t = JsonConvert.DeserializeObject<TweetData>(responseBody);
                        return responseBody;
                    }
                    else if (type == "tweet")
                    {
                        HttpClient client = new HttpClient();
                        HttpRequestMessage request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, $"https://api.twitter.com/2/tweets/search/recent?query=from%3A{user}%20{hashtag}%20has%3Ahashtags%20has%3Alinks%20-is%3Aretweet");
                        request.Headers.Add("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAKthlAEAAAAAS%2BZoFBRCRqbdy05wokFoXm24lgY%3DknwW3XtvbroPk48HJhUdI46AbUx7cB3CIxqICOxGx8WzsfuT56");
                        HttpResponseMessage response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();
                        TweetData t = JsonConvert.DeserializeObject<TweetData>(responseBody);
                        return responseBody;
                    }
                }
                catch (Exception ex)
                {
                    return "Error: " + ex.Message;
                }
            }
            else
            {
                return "Error: Missing parameters.";
            }
        }
    }

    /*
    public class TweetsPostCmd : BaseCommand, ICommand
    {
        private static string APIKey = "38GtVBOTriwJDED3x7tbPlQXv";
        private static string APIKeySecret = "I6cMBtct7YqHtvAx10BMHTtdhYdwexopE3HUTq8wxWIh90B8Ni";
        private static string AccessToken = "1607255581571649536-SHd7bffqugCkgrHPmj6Qc6PClVWIuG";
        private static string AccessTokenSecret = "JOyLBJSudXASgjlV8q8I1NhA2QiqHhha7nYLAggJPzeKW";

        public object Execute(params object[] param)
        {
            if (param.Length > 0)
            {
                string activistTwitterHandle = (string)param[0];
                string companyTwitterHandle = (string)param[1];
                string productId = (string)param[2];

                try
                {
                    TwitterClient userClient = new TwitterClient(APIKey, APIKeySecret, AccessToken, AccessTokenSecret);
                    // request the user's information from Twitter API
                    //var user1 = await userClient.Users.GetAuthenticatedUserAsync();
                    // publish a tweet
                    string tweetText = $"Congratulations! @{activistTwitterHandle} just bought a product(id:{productId}) from @{companyTwitterHandle}, in PromoIt you can promote the society and also earn points and buy cool products.";
                    var tweet = await userClient.Tweets.PublishTweetAsync(tweetText);
                    return "Tweet Posted.";
                }
                catch (Exception ex)
                {
                    return "Error: " + ex.Message;
                }
            }
            else
            {
                return "Error: Missing parameters.";
            }
        }
    }*/
}
