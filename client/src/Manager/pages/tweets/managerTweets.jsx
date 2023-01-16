import React, { useState, useEffect } from "react";
import "./managerTweets.css";
//import ScriptTag from "react-script-tag";
//import { Helmet } from "react-helmet";
import { getUsers } from "../../../services/user.service";
import { TwitterTimelineEmbed, TwitterTweetEmbed } from "react-twitter-embed";

export const ManagerTweetsPage = (props) => {
  const [users, setUsers] = useState([]);
  const [from, setFrom] = useState("Manager");
  const [FilterUsers, setFilterUsers] = useState([]);

  const handleFromChange = (event) => {
    setFrom(event.target.value);
  };

  const fetchData = async () => {
    let response = await getUsers();
    if (response.status === 200) {
      setUsers(Object.values(response.data));
      setFilterUsers(Object.values(response.data));
      //console.log(usersDetails);
    }
  };

  const handleFilter = (onChangeEvent) => {
    let freeText = onChangeEvent.target.value;
    if (freeText) {
      let filteredArr = users.filter((user) =>
        user.TwitterHandle.toLowerCase().includes(freeText.toLowerCase())
      );
      setFilterUsers(filteredArr);
    } else {
      setFilterUsers(users);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div className="tweetsPage">
      <div onChange={(event) => handleFromChange(event)} className="wrapper3">
        <input
          type="radio"
          id="Manager"
          name="select"
          value="Manager"
          checked={from === "Manager"}
          onChange={handleFromChange}
        />
        <label htmlFor="Manager" className="option Manager">
          <div class="dot"></div>
          <span>Manager</span>
        </label>
        <input
          type="radio"
          id="Activists"
          name="select"
          value="Activists"
          checked={from === "Activists"}
          onChange={handleFromChange}
        />
        <label htmlFor="Activists" className="option Activists">
          <div class="dot"></div>
          <span>Activists</span>
        </label>
      </div>
      {from === "Activists" ? (
        <div classNameName="input-group input-group-sm mb-3 filterContainer">
          <input
            placeholder="Search tweets by Twitter Handle"
            type="text"
            className="form-control filter2"
            aria-label="Small"
            aria-describedby="inputGroup-sizing-sm"
            onChange={handleFilter}
          />
        </div>
      ) : (
        <></>
      )}
      {from === "Manager"
        ? users.map((user) => {
            if (user.Role === "Manager") {
              let { TwitterHandle } = user;
              return (
                <div className="tweetsList">
                  <TwitterTimelineEmbed
                    sourceType="profile"
                    screenName={TwitterHandle}
                    options={{ height: 500, width: 900 }}
                  />
                </div>
              );
            }
          })
        : FilterUsers.map((user) => {
            if (user.Role === "Social Activist") {
              let { TwitterHandle } = user;
              return (
                <div className="tweetsList">
                  <TwitterTimelineEmbed
                    sourceType="profile"
                    screenName={TwitterHandle}
                    options={{ height: 500, width: 900 }}
                  />
                </div>
              );
            }
          })}
      {/* {FilterUsers.map((user) => {
        if (user.Role === "Manager" && from === "Manager") {
          let { TwitterHandle } = user;
          return (
            <div className="tweetsList">
              <TwitterTimelineEmbed
                sourceType="profile"
                screenName={TwitterHandle}
                options={{ height: 500, width: 900 }}
              />
            </div>
          );
        } else if (user.Role === "Social Activist" && from === "Activists") {
          let { TwitterHandle } = user;
          return (
            <div className="tweetsList">
              <TwitterTimelineEmbed
                sourceType="profile"
                screenName={TwitterHandle}
                options={{ height: 500, width: 900 }}
              />
            </div>
          );
        }
      })} */}
    </div>
  );
};

/*
import React, { useState, useEffect } from "react";
import "./managerTweets.css";
//import ScriptTag from "react-script-tag";
import { Helmet } from "react-helmet";
import { getUsers } from "../../../services/user.service";
import {
  TwitterTimelineEmbed,
  TwitterShareButton,
  TwitterHashtagButton,
  TwitterMentionButton,
  TwitterTweetEmbed,
} from "react-twitter-embed";
import { getCampaigns } from "../../../services/campaign.service";
import {
  getTweetsByUser,
  getRetweetsOfUser,
} from "../../../services/twitter.service";

export const ManagerTweetsPage = (props) => {
  const [users, setUsers] = useState([]);
  const [from, setFrom] = useState("Manager");
  const [campaignsArr, setCampaignsArr] = useState([]);
  const [tweets, setTweets] = useState([]);

  const handleFromChange = (event) => {
    setFrom(event.target.value);
  };

  const fetchData = async () => {
    let response = await getUsers();
    if (response.status === 200) {
      setUsers(Object.values(response.data));
      //console.log(usersDetails);
    }
  };

  const fetchData2 = async () => {
    let response = await getCampaigns();
    if (response.status === 200) {
      setCampaignsArr(Object.values(response.data));
    }
  };

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

  useEffect(() => {
    fetchData();
    fetchData2();
  }, []);

  return (
    <div className="tweetsPage">
      <div onChange={(event) => handleFromChange(event)} className="wrapper3">
        <input
          type="radio"
          id="Manager"
          name="select"
          value="Manager"
          checked={from === "Manager"}
          onChange={handleFromChange}
        />
        <label htmlFor="Manager" className="option Manager">
          <div class="dot"></div>
          <span>Manager</span>
        </label>
        <input
          type="radio"
          id="Activists"
          name="select"
          value="Activists"
          checked={from === "Activists"}
          onClick={func}
          onChange={handleFromChange}
        />
        <label htmlFor="Activists" className="option Activists">
          <div class="dot"></div>
          <span>Activists</span>
        </label>
      </div>
      <>
        {from === "Manager" ? (
          <div className="tweetsList">
            <TwitterTimelineEmbed
              sourceType="profile"
              screenName="Ofermord"
              options={{ height: 500, width: 900 }}
            />
          </div>
        ) : from === "Activists" ? (
          tweets.map((tweet) => {
            return (
              <div className="tweetsList">
                {tweet.tweetID.map((tweerIdArr) => {
                  <TwitterTweetEmbed tweetId={tweerIdArr} />;
                })}
              </div>
            );
          })
        ) : (
          <></>
        )}
      </>
    </div>
  );
};

*/
