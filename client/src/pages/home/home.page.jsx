import React, { useContext } from "react";
import "./style.css";
import { UserDetailsContext } from "../../context/userDetails.context";
import { Link } from "react-router-dom";
// import { Client } from "twitter-api-sdk";

// export const trythis = async () => {
//   //const client = new Client(process.env.BEARER_TOKEN);
//   const client = new Client(
//     "AAAAAAAAAAAAAAAAAAAAACn8kwEAAAAAWTEBOtbAN%2FkKfSMh%2FH9NHUUmlB0%3D7tB0oiI0RmrmdqobAk3k4h4lT0lNOgzFeVtOAOpGpAuUCy0XQl"
//   );

//   const response = await client.tweets.tweetCountsFullArchiveSearch({
//     query:
//       "from:@Ofermord BringYourBrave https://t.co/nBixDMQwCq has:hashtags has:links",
//     "search_count.fields": ["tweet_count"],
//   });

//   console.log("response", JSON.stringify(response, null, 2));
// };

// require("dotenv").config({ path: "./twitter.env" });
// const { twitterClient } = require("./twitterClient.js");

// const tweet = async () => {
//   try {
//     await twitterClient.v2.tweet("Hello world!");
//   } catch (e) {
//     console.log(e);
//   }
// };

// tweet();

export const HomePage = (props) => {
  const { userDetails } = useContext(UserDetailsContext);
  return (
    <div className="home">
      <div className="information">
        <h3 className="title">#PromoIt</h3>
        <h1>Let's Promote Our Society</h1>
        <p className="home-text">
          Today ProLobby expresses its unique vision of the future.
          <br />A System to promote Social Agenda for a better society.
          <br />A better society means more opportunities.
          <br />
          you can find out those opportunities here.
        </p>
        {userDetails.Role === "Social Activist" ? (
          <>
            <Link to="/activist/campaigns">
              <button className="buy-now">START NOW</button>
            </Link>
          </>
        ) : userDetails.Role === "Company" ? (
          <>
            <Link to="/company/campaigns">
              <button className="buy-now">START NOW</button>
            </Link>
          </>
        ) : userDetails.Role === "Non-Profit Organization" ? (
          <>
            <Link to="/organization/campaign/create">
              <button className="buy-now">START NOW</button>
            </Link>
          </>
        ) : (
          <>
            <Link to="/register">
              <button className="buy-now">START NOW</button>
            </Link>
          </>
        )}
      </div>
    </div>
  );
};
