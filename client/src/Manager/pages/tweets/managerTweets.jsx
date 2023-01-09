import React from "react";
import "./managerTweets.css";

export const ManagerTweetsPage = (props) => {
  return (
    <div className="tweetsPage">
      {/* <div className="tweetsList"> */}
      <a
        class="twitter-timeline"
        data-lang="en"
        data-width="900"
        data-height="600"
        href="https://twitter.com/Ofermord?ref_src=twsrc%5Etfw"
      >
        Tweets by Ofermord
      </a>{" "}
      <script
        async
        src="https://platform.twitter.com/widgets.js"
        charset="utf-8"
      ></script>
      {/* </div> */}
    </div>
  );
};
