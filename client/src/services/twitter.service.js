import axios from "axios";
import { startEndpoint } from "./api";
import e from "cors";
//const axios = require("axios");

// const tweet =
//   "https://api.twitter.com/2/tweets/search/recent?query=from%3AOfermord%20BringYourBrave%20has%3Ahashtags%20has%3Alinks%20-is%3Aretweet";

// export const getUserTweet = async () => {
//   return axios
//     .get(`${tweet}`, {
//       headers: {
//         Authorization:
//           "AAAAAAAAAAAAAAAAAAAAACUClAEAAAAAVXTzImWLJSpfWFCSyDwNpD1Zpzs%3DpZNno2etkfBfQVs94Q767phGl8etdSPOKogPU3KxrA2iVlPKiV",
//       },
//     })
//     .then(function (response) {
//       console.log(response.data);
//       console.log(response.status);
//     });
// };

// export const getTweets = async () => {
//   try {
//     let response = await axios.get(
//       "https://api.twitter.com/2/tweets/search/recent?query=from%3AOfermord%20BringYourBrave%20has%3Ahashtags%20has%3Alinks%20-is%3Aretweet",
//       {
//         // params: {
//         //   query:
//         //     "from:Ofermord BringYourBrave has:hashtags has:links -is:retweet",
//         // },
//         headers: {
//           Authorization:
//             "Bearer AAAAAAAAAAAAAAAAAAAAACUClAEAAAAAVXTzImWLJSpfWFCSyDwNpD1Zpzs%3DpZNno2etkfBfQVs94Q767phGl8etdSPOKogPU3KxrA2iVlPKiV",
//         },
//       }
//     );
//     return response;
//   } catch (e) {
//     console.log(e);
//   }
// };

export const getTweetsByUser = async (User, Hashtag) => {
  try {
    let endpoint = startEndpoint + `/Tweets/Get/tweet/${User}/${Hashtag}`;
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

export const getRetweetsOfUser = async (User, Hashtag) => {
  try {
    let endpoint = startEndpoint + `/Tweets/Get/retweet/${User}/${Hashtag}`;
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

export const postTweetsByManager = async (
  activistTwitterHandle,
  companyTwitterHandle,
  productId
) => {
  try {
    let endpoint =
      startEndpoint +
      `/Tweets/Post/${activistTwitterHandle}/${companyTwitterHandle}/${productId}`;
    let response = await axios.post(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};
