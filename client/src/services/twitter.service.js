import axios from "axios";
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
    const api =
      "http://localhost:7181/api/Tweets/Get/tweet/" + User + "/" + Hashtag;
    let response = await axios.get(api);
    return response;
  } catch (e) {
    console.log(e);
  }
};

export const getRetweetsOfUser = async (User, Hashtag) => {
  try {
    let response = await axios.get(
      `http://localhost:7181/api/Tweets/Get/retweet/${User}/${Hashtag}`
    );
    return response;
  } catch (e) {
    console.log(e);
  }
};
