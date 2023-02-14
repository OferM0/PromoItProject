using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using Utilities;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using server.Data.Sql;
using Tweetinvi.Models.V2;
using static System.Net.Mime.MediaTypeNames;

namespace server.Entities
{
    public class MainManager
    {
        private ConfigurationKeys config;
        public Logger log;
        public CommandsManager commandsManager;
        public Products products;
        public Contacts contacts;
        public Users users;
        public Campaigns campaigns;
        public Tweets tweets;

        public CampaignsQueries campaignsQueries; //only for first initialize of list for getting tweets
        public UsersQueries usersQueries;
        public TweetsQueries tweetsQueries;
        public ProductsQueries productsQueries;

        //public Activists activists;
        //public Organizations organizations;
        //public Companies companies;
        Task task;
        private MainManager()
        {
            InitManager();
        }
        private static readonly MainManager _instance = new MainManager();
        public static MainManager Instance { get { return _instance; } }

        public List<Contact> contactsList = new List<Contact>();
        public Dictionary<string, User> usersList = new Dictionary<string, User>();
        public List<Campaign> campaignsList = new List<Campaign>();
        public List<Product> productsList = new List<Product>();
        public Dictionary<string, Tweet> tweetsList = new Dictionary<string, Tweet>();
        //public Dictionary<string, Activist> activistsList = new Dictionary<string, Activist>();
        //public Dictionary<string, Organization> organizationsList = new Dictionary<string, Organization>();
        //public Dictionary<string, Company> companiesList = new Dictionary<string, Company>();


        public void InitManager()
        {
            log = new Logger(Environment.GetEnvironmentVariable("LoggerType"));
            commandsManager = new CommandsManager();
            products = new Products(/*Instance.*/log);
            contacts = new Contacts(/*Instance.*/log);
            users = new Users(/*Instance.*/log);
            campaigns = new Campaigns(/*Instance.*/log);
            tweets = new Tweets(/*Instance.*/log);
            //activists = new Activists(/*Instance.*/log);
            //organizations = new Organizations(/*Instance.*/log);
            //companies = new Companies(/*Instance.*/log);

            //Init7(); //Init7 function gets all campaigns from DB and put them in List<Campaign> => MainManager.Instance.campaignsList
            campaignsQueries = new CampaignsQueries(log);
            campaignsList = ((List<Campaign>)campaignsQueries.ResetList());

            //Init3(); //Init3 function gets all users from DB and put them in Dictionary<string,User> when the string key is UserID=> MainManager.Instance.usersDictionary
            usersQueries = new UsersQueries(log);        
            usersList = ((Dictionary<string, User>)usersQueries.ResetList());

            tweetsQueries= new TweetsQueries(log);
            tweetsList = ((Dictionary<string, Tweet>)tweetsQueries.ResetList());

            productsQueries = new ProductsQueries(log);
            productsList = ((List<Product>)productsQueries.ResetList());
           
            task = Task.Run(async () =>
            {
                while (true)
                {
                    //log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Start Tweet Task function." });
                    try
                    {
                        for (int i = 0; i < usersList.Count; i++)
                        {
                            var user = usersList.ElementAt(i);

                            if (user.Value.Role == "Social Activist")
                            {
                                for (int j = 0; j < campaignsList.Count; j++)
                                {
                                    var campaign = campaignsList[j];
                                    string responseBody = "";

                                    HttpClient client = new HttpClient();
                                    HttpRequestMessage request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, $"https://api.twitter.com/2/tweets/search/recent?query=retweets_of%3A{user.Value.TwitterHandle}%20%22{campaign.Hashtag}%22%20has%3Ahashtags%20has%3Alinks%20is%3Aretweet");
                                    request.Headers.Add("Authorization", Environment.GetEnvironmentVariable("TweeterBarear"));
                                    HttpResponseMessage response = await client.SendAsync(request);
                                    response.EnsureSuccessStatusCode();
                                    responseBody = await response.Content.ReadAsStringAsync();

                                    var tweetsJson = JObject.Parse(responseBody);

                                    if (tweetsJson.ContainsKey("data"))
                                    {
                                        var tweetsArray = tweetsJson["data"].ToArray();
                                        for (int k = 0; k < tweetsArray.Length; k++)
                                        {
                                            var tweetJson = tweetsArray[k];
                                            var id = tweetJson["id"].Value<string>();
                                            var text = tweetJson["text"].Value<string>();
                                            var tweet = new Tweet
                                            {
                                                Id = id,
                                                Text = text,
                                                Type = "retweet",
                                                TwitterHandle = user.Value.TwitterHandle
                                            };
                                            if (!tweetsList.ContainsKey(id))
                                            {
                                                tweetsQueries.InsertTweetToDB(tweet.Id, tweet.Text, tweet.TwitterHandle, tweet.Type);
                                                tweetsList.Add(id, tweet);
                                            }
                                        }
                                    }

                                    HttpClient client1 = new HttpClient();
                                    HttpRequestMessage request1 = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, $"https://api.twitter.com/2/tweets/search/recent?query=from%3A{user.Value.TwitterHandle}%20{campaign.Hashtag}%20has%3Ahashtags%20has%3Alinks%20-is%3Aretweet");
                                    request1.Headers.Add("Authorization", Environment.GetEnvironmentVariable("TweeterBarear"));
                                    HttpResponseMessage response1 = await client1.SendAsync(request1);
                                    response1.EnsureSuccessStatusCode();
                                    responseBody = await response1.Content.ReadAsStringAsync();

                                    var tweetsJson1 = JObject.Parse(responseBody);
                                    if (tweetsJson.ContainsKey("data"))
                                    {
                                        var tweetsArray1 = tweetsJson1["data"].ToArray();
                                        for (int k = 0; k < tweetsArray1.Length; k++)
                                        {
                                            var tweetJson1 = tweetsArray1[k];
                                            var id = tweetJson1["id"].Value<string>();
                                            var text = tweetJson1["text"].Value<string>();
                                            var tweet1 = new Tweet
                                            {
                                                Id = id,
                                                Text = text,
                                                Type = "tweet",
                                                TwitterHandle = user.Value.TwitterHandle
                                            };

                                            if (!tweetsList.ContainsKey(id))
                                            {
                                                tweetsQueries.InsertTweetToDB(tweet1.Id, tweet1.Text, tweet1.TwitterHandle, tweet1.Type);
                                                tweetsList.Add(id, tweet1);
                                            }
                                        }
                                    }

                                }

                                decimal priceOfPrductsBaughtByUser = productsList.Where(p => p.ActivistID == user.Value.UserID).Sum(p => p.Price); ;
                                int tweetCount = tweetsList.Values.Where(t => t.TwitterHandle == user.Value.TwitterHandle).Count();
                                decimal newStatus = tweetCount - priceOfPrductsBaughtByUser;
                                usersQueries.UpdateUserStatusInDB(user.Key, newStatus);
                                usersList[user.Key].Status = newStatus;
                            }
                        }

                        Thread.Sleep(3600 * 1000); // wait 1 hour
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run tweet Task function in MainManager." });
                        throw;
                    }
                }
            });
        }
      
        public void Init()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Initialize products List." });
                products.GetProductsFromDB();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run GetProductsFromDB function in MainManager." });
                throw;
            }
        }
        public void Init2()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Initialize contacts List." });
                contacts.GetContactsFromDB();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run GetContactsFromDB function in MainManager." });
                throw;
            }
        }
        public void Init3()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Initialize users List." });
                users.GetUsersFromDB();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run GetUsersFromDB function in MainManager." });
                throw;
            }
        }
        /*
        public void Init4()
        {
            try
            {
                activists.GetActivistsFromDB();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run GetActivistsFromDB function in MainManager." });
            }
        }
        public void Init5()
        {
            try
            {
                organizations.GetOrganizationsFromDB();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run GetOrganizationsFromDB function in MainManager." });
            }
        }
        
        public void Init6()
        {
            try
            {
                companies.GetCompaniesFromDB();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run GetCompaniesFromDB function in MainManager." });
            }
        }*/
        public void Init7()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Initialize campaigns List." });
                campaigns.GetCampaignsFromDB();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run GetCampaignsFromDB function in MainManager." });
                throw;
            }
        }

        public void Init8()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = "Initialize tweets List." });
                tweets.GetTweetsFromDB();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run GetTweetsFromDB function in MainManager." });
                throw;
            }
        }
    }
}
