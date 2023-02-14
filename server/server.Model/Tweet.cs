using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class Tweet
    {
        public string Id { get; set; }
        public string Text { get; set; }        
        public string TwitterHandle { get; set; }
        public string Type { get; set; }
        //public string Hashtag { get; set; }
        //public string Url { get; set; }
        //public List<string> EditHistoryTweetIds { get; set; }
    }

    /*
    public class TweetMetaData
    {
        public string NewestId { get; set; }
        public string OldestId { get; set; }
        public int ResultCount { get; set; }
    }*/

    public class TweetData
    {
        public List<Tweet> Data { get; set; }
        //public TweetMetaData Meta { get; set; }
    }
}
