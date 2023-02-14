using server.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace server.Data.Sql
{
    public class TweetsQueries : BaseDataSql
    {
        public TweetsQueries(Logger log) : base(log) { }

        public Dictionary<string, Tweet> BuildTweetsList(SqlDataReader reader)
        {
            Dictionary<string, Tweet> tweetsList = new Dictionary<string, Tweet>();

            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute BuildTweetsList function in TweetsQueries." });
                while (reader.Read())
                {
                    Tweet tweet = new Tweet();
                    tweet.Id = reader.GetString(reader.GetOrdinal("Id"));
                    tweet.Text = reader.GetString(reader.GetOrdinal("Text"));
                    tweet.TwitterHandle = reader.GetString(reader.GetOrdinal("TwitterHandle"));
                    tweet.Type = reader.GetString(reader.GetOrdinal("Type"));
                    tweetsList.Add(tweet.Id, tweet);
                }
                return tweetsList;
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run BuildTweetsList function in TweetsQueries." });
                throw ex;
            }
        }
        public Tweet BuildTweet(SqlDataReader reader)
        {
            Tweet tweet = new Tweet();

            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute BuildTweet function in TweetsQueries." });
                while (reader.Read())
                {
                    tweet.Id = reader.GetString(reader.GetOrdinal("Id"));
                    tweet.Text = reader.GetString(reader.GetOrdinal("Text"));
                    tweet.TwitterHandle = reader.GetString(reader.GetOrdinal("TwitterHandle"));
                    tweet.Type = reader.GetString(reader.GetOrdinal("Type"));
                }
                return tweet;
            } 
            catch(Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run BuildTweet function in TweetsQueries." });
                throw ex;
            }
        }

        public object ResetList()
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute ResetList function in TweetsQueries." });
                return DAL.SqlQuery.RunCommandResult("select * from Tweets", BuildTweetsList);
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run ResetList function in TweetsQueries." });
                throw ex;
            }
        }

        public void InsertTweetToDB(string Id, string Text, string TwitterHandle, string Type)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute InsertTweetToDB function in TweetsQueries." });
                DAL.SqlQuery.RunNonQueryCommand($"Insert Into Tweets(Id, Text, TwitterHandle, Type) Values('{Id}','{Text}','{TwitterHandle}','{Type}')");
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run InsertTweetToDB function in TweetsQueries." });
                throw ex;
            }
        }


        public object GetTweetFromDB(string id)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetTweetFromDB(id:{id}) function in TweetsQueries." });
                return DAL.SqlQuery.RunCommandResult($"select * from Tweets where Id= '{id}'", BuildTweet);
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run GetTweetFromDB function in TweetsQueries." });
                throw ex;
            }
        }

        public object GetTweetsCountFromDB(string user)
        {
            try
            {
                //this._log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetTweetsCountFromDB(user:{user}) function in TweetsQueries." });
                return DAL.SqlQuery.RunCommandResult($"select * from Tweets where TwitterHandle= '{user}'", BuildTweet);
            }
            catch (Exception ex)
            {
                //this._log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"{ex.Message}. Failed to run GetTweetsCountFromDB function in TweetsQueries." });
                throw ex;
            }
        }
    }
}
