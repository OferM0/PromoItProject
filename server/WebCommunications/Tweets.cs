using server.Data.Sql;
using server.Model;
using server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace server.WebCommunications
{
    public class Tweets: BasePromotionSystem
    {
        public Tweets(Logger log) : base(log)
        {
            tweetsQueries = new TweetsQueries(base._log);
        }
        TweetsQueries tweetsQueries;
        public void ClearList()
        {
            try
            {
                MainManager.Instance.tweetsList.Clear();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public void GetTweetsFromDB()
        {
            try
            {
                ClearList();
                MainManager.Instance.tweetsList = ((Dictionary<string, Tweet>)tweetsQueries.ResetList());
                //return MainManager.Instance.tweetsList;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public Tweet GetTweetById(string id)
        {
            try
            {
                Tweet tweet = ((Tweet)tweetsQueries.GetTweetFromDB(id));
                return tweet;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }          
        }

        public Tweet GetTweetsCountByTwitterHandle(string user)
        {
            try
            {
                Tweet tweet = ((Tweet)tweetsQueries.GetTweetsCountFromDB(user));
                return tweet;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }            
        }

        public void AddNewTweet(string Id, string Text, string TwitterHandle, string Type)
        {                       
            try                 
            {                   
                Tweet tweet = new Tweet
                {
                    Id = Id,
                    Text = Text,
                    TwitterHandle = TwitterHandle,
                    Type = Type,
                };
                MainManager.Instance.tweetsList.Add(Id, tweet);
                tweetsQueries.InsertTweetToDB(Id, Text, TwitterHandle, Type);
            }                                 
            catch (Exception ex)              
            {                                 
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }

        public Tweet GetTweetFromList(string id)
        {
            try
            {
                return MainManager.Instance.tweetsList[id];
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}." });
                throw;
            }
        }
    }
}
