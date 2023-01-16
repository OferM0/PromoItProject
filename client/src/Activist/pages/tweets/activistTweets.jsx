import React, { useState, useEffect, useContext } from "react";
import "./activistTweets.css";
import { getUsers } from "../../../services/user.service";
import {
  TwitterTimelineEmbed,
  TwitterShareButton,
  TwitterHashtagButton,
  TwitterMentionButton,
  TwitterTweetEmbed,
} from "react-twitter-embed";
import { UserDetailsContext } from "../../../context/userDetails.context";

export const ActivistTweetsPage = (props) => {
  const { userDetails } = useContext(UserDetailsContext);

  return (
    <div className="tweetsPage2">
      <div className="tweetsList2">
        <TwitterTimelineEmbed
          sourceType="profile"
          screenName={userDetails.TwitterHandle}
          options={{ height: 600, width: 900 }}
        />
      </div>
    </div>
  );
};

/*
import {
  getTweetsByUser,
  getRetweetsOfUser,
} from "../../../services/twitter.service";

  const [tweets, setTweets] = useState([]);


  const func = async () => {
    for (let j = 0; j < users.length; j++) {
      if (users[j].Role === "Social Activist") {
        for (let i = 0; i < campaignsArr.length; i++) {
          //console.log(users[j].TwitterHandle);
          //console.log(campaignsArr[i].Hashtag);

          let response = await getTweetsByUser(
            users[j].TwitterHandle,
            campaignsArr[i].Hashtag
          );
          if (Array.isArray(Object.values(response.data)[0])) {
            //console.log(Object.values(response.data)[0]);
            for (var k = 0; k < Object.values(response.data)[0].length; k++) {
              let obj = {
                userName: users[j].TwitterHandle,
                tweetID: Object.values(response.data)[0][k].id,
              };
              //console.log(obj);
              setTweets((prev) => [...prev, obj]);
            }
          }
        }
      }
    }
    //console.log(Object.values(tweets));
    let groupedArrays = tweets.reduce((acc, curr) => {
      acc[curr.userName] = acc[curr.userName] || {
        userName: curr.userName,
        tweetID: [],
      };
      acc[curr.userName].tweetID.push(curr.tweetID);
      return acc;
    }, {});
    let final = Object.values(groupedArrays);
    setTweets(final);
  };


  return (
    <div className="tweetsPage">
        {tweets.map((tweet) => {
            return (
              <div className="tweetsList">
                {tweet.tweetID.map((tweerIdArr) => {
                  <TwitterTweetEmbed tweetId={tweerIdArr} />;
                })}
              </div>
            );
          })}
    </div>
  );
};

*/
