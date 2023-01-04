import React, { useContext, useState, useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import "./activistProfile.css";
import EditIcon from "@mui/icons-material/Edit";
import { Link } from "react-router-dom";
import { UserDetailsContext } from "../../../context/userDetails.context";
import { getProducts } from "../../../services/product.service";
import { getCampaigns } from "../../../services/campaign.service";
import {
  getTweetsByUser,
  getRetweetsOfUser,
} from "../../../services/twitter.service";
//import { getTweets } from "../../../services/twitter.service";
//import { getUserTweet } from "../../../services/twitter.service";
//import { Client } from "twitter-api-sdk";

// async function calcPerCampaign(user, hashtag) {
//   const client = new Client(
//     "AAAAAAAAAAAAAAAAAAAAACUClAEAAAAAVXTzImWLJSpfWFCSyDwNpD1Zpzs%3DpZNno2etkfBfQVs94Q767phGl8etdSPOKogPU3KxrA2iVlPKiV"
//   );

//   let sum = 0;

//   const response = await client.tweets.tweetsRecentSearch({
//     //----------get details on tweet(id)-----------
//     query:
//       "from:" + user + " " + hashtag + " has:hashtags has:links -is:retweet",
//   });
//   if (response.data !== undefined) {
//     let a = [];
//     a = Object.values(response.data).map((t) => t.id);
//     //console.log(a);
//     sum = a.length;
//     for (let i = 0; i < a.length; i++) {
//       const response2 = await client.users.tweetsIdRetweetingUsers(
//         //--------list of users retweeted this tweet by tweet id
//         a[i]
//       );
//       if (response2.data !== undefined) {
//         //-----------length og retweets
//         //console.log(response2.data);
//         sum = sum + response2.data.length;
//       } else {
//         //console.log(0);
//       }
//     }
//   }
//   return parseInt(sum);
// }

export const ActivistProfilePage = (props) => {
  const [productsArr, setProductsArr] = useState([]);
  const [campaignsArr, setCampaignsArr] = useState([]);
  const [tweetsArr, setTweetsArr] = useState([]);
  const [retweetsArr, setRetweetsArr] = useState([]);
  const { user } = useAuth0();
  const { userDetails } = useContext(UserDetailsContext);
  //console.log(userDetails);
  const [status, setStatus] = useState(0);
  //let status = 0;
  const fetchData = async () => {
    let response = await getProducts();
    if (response.status === 200) {
      setProductsArr(response.data);
    }
  };

  const fetchData2 = async () => {
    let response = await getCampaigns();
    if (response.status === 200) {
      setCampaignsArr(Object.values(response.data));
    }
  };

  // const fetchData3 = async (username, hashtag) => {
  //   let response = await getTweetsByUser(username, hashtag);
  //   if (response.status === 200) {
  //     setTweetsArr(Object.values(response.data));
  //   }
  // };

  // const fetchData4 = async (username, hashtag) => {
  //   let response = await getRetweetsOfUser(username, hashtag);
  //   if (response.status === 200) {
  //     setRetweetsArr(Object.values(response.data));
  //   }
  // };
  //const fetchData3 = async () => {
  //let response = await getTweets();
  //console.log(response);
  // if (response.status === 200) {
  //   setTweetsArr(Object.values(response.data));
  //   console.log(tweetsArr);
  // }
  //};
  const calculateStatus = async () => {
    let sum = 0;
    for (let i = 0; i < campaignsArr.length; i++) {
      let response = await getTweetsByUser("Ofermord", campaignsArr[i].Hashtag);
      if (response.status === 200) {
        setTweetsArr(Object.values(response.data));
      }
      let response2 = await getRetweetsOfUser(
        "Ofermord",
        campaignsArr[i].Hashtag
      );
      if (response2.status === 200) {
        setRetweetsArr(Object.values(response2.data));
      }
      //console.log(campaignsArr[i].Hashtag);
      sum = sum + retweetsArr.length + tweetsArr.length;
    }
    setStatus(sum);
  };

  useEffect(() => {
    fetchData();
    fetchData2();
    //fetchData3();
    //fetchData4();
    calculateStatus();
  }, []);

  return (
    <div className="profilePage">
      <div className="row container d-flex justify-content-center profilePanel">
        <div className="col-xl-6 col-md-12">
          <div className="card user-card-full">
            <div className="row m-l-0 m-r-0">
              <div className="col-sm-4 bg-c-lite-green user-profile">
                <div className="card-block text-center text-white">
                  <div className="m-b-25">
                    <img
                      src={user.picture}
                      alt={user.name}
                      className="img-radius"
                    />
                  </div>
                  <h6 className="profileName">{userDetails.Name}</h6>
                  <h6>{userDetails.Role}</h6>
                  <div className="moneyStatus">Status: {status} $</div>
                  <Link to="/activistProfile/edit" className="editBtn">
                    <span>Edit Profile</span>
                    <EditIcon />
                  </Link>
                </div>
              </div>
              <div className="col-sm-8">
                <div className="card-block">
                  <h6 className="m-b-20 p-b-5 b-b-default f-w-600">
                    Information
                  </h6>
                  <div className="row">
                    <div className="col-sm-6">
                      <p className="m-b-10 f-w-600">User ID</p>
                      <h6 className="text-muted f-w-400">{user.sub}</h6>
                    </div>
                    <div className="col-sm-6">
                      <p className="m-b-10 f-w-600">Email</p>
                      <h6 className="text-muted f-w-400">{user.email}</h6>
                    </div>
                  </div>
                  <h6 className="m-b-20 m-t-40 p-b-5"></h6>
                  <div className="row">
                    <div className="col-sm-6">
                      <p className="m-b-10 f-w-600">Address</p>
                      <h6 className="text-muted f-w-400">
                        {userDetails.Address}
                      </h6>
                    </div>
                    <div className="col-sm-6">
                      <p className="m-b-10 f-w-600">Phone</p>
                      <h6 className="text-muted f-w-400">
                        {userDetails.Phone}
                      </h6>
                    </div>
                  </div>
                  <h6 className="m-b-20 m-t-40 p-b-5"></h6>
                  <div className="row">
                    <div className="col-sm-6">
                      <p className="m-b-10 f-w-600">Products I Own</p>
                      <h6 className="text-muted f-w-400">
                        {
                          productsArr.filter(
                            (obj) =>
                              obj.ActivistID === user.sub &&
                              obj.DonatedByActivist === false
                          ).length
                        }
                      </h6>
                    </div>
                    <div className="col-sm-6">
                      <p className="m-b-10 f-w-600">Products Donated</p>
                      <h6 className="text-muted f-w-400">
                        {
                          productsArr.filter(
                            (obj) =>
                              obj.ActivistID === user.sub &&
                              obj.DonatedByActivist === true
                          ).length
                        }
                      </h6>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
