using server.Data.Sql;
using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace server.Entities
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
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute  ClearList function in Tweets Entity." });
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
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetTweetsFromDB function in Tweets Entity." });
                ClearList();
                MainManager.Instance.tweetsList = ((Dictionary<string, Tweet>)tweetsQueries.ResetList());
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
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetTweetById(id:{id}) function in Tweets Entity." });
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
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetTweetsCountByTwitterHandle({user}) function in Tweets Entity." });
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
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute AddNewTweet function in Tweets Entity." });
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
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetTweetFromList(id:{id}) function in Tweets Entity." });
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
