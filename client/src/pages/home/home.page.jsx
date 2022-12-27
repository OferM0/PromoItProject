import React from "react";
import "./style.css";
import { Link } from "react-router-dom";
export const HomePage = (props) => {
  return (
    <div className="home">
      <div className="information">
        <h3 className="title">Our Luxury</h3>
        <h1>Find Your Watch Here</h1>
        <p className="home-text">
          Today NorthWind expresses its unique vision of the past, present and
          future of mechanical watchmaking through its numerous collections of
          watches for men.
        </p>
        <Link to="/products">
          <button className="buy-now">BUY NOW</button>
        </Link>
      </div>
    </div>
  );
};
