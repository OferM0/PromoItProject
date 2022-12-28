import React from "react";
import "./style.css";
import { Link } from "react-router-dom";
export const HomePage = (props) => {
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
        <Link to="/register">
          <button className="buy-now">START NOW</button>
        </Link>
      </div>
    </div>
  );
};
