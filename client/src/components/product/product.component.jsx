import React from "react";
import { Link } from "react-router-dom";
import "./style.css";
import "../../../node_modules/bootstrap/dist/css/bootstrap.css";

export const Product = ({ ProductID, ProductName, UnitPrice }) => {
  return (
    <div className="product-info">
      <div className="product-text">
        <h1>{ProductName}</h1>
        <h2>by NorthWind</h2>
        <p>
          Lorem ipsum dolor sit amet consectetur.
          <br /> qui deserunt placeat voluptas.
          <br /> aperiam consectetur facere alias itaque tenetur,
          <br /> voluptates iure dolore!
        </p>
      </div>
      <div className="product-price-btn">
        <p>
          <span>{UnitPrice}</span>$
        </p>
        <Link
          to={"/products/" + ProductID}
          state={{ ProductID }}
          className="link-btn"
        >
          buy now
        </Link>
      </div>
    </div>
  );
};
