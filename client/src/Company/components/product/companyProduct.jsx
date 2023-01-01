import React, { useContext } from "react";
import { Link } from "react-router-dom";
import "./companyProduct.css";
import { UserDetailsContext } from "../../../context/userDetails.context";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";

export const CompanyProduct = ({ Id, Name, Description, Price, Image }) => {
  const { userDetails } = useContext(UserDetailsContext);
  return (
    <div className="product-info">
      <div className="product-text">
        <h1>{Name}</h1>
        {/* <img src={Image} alt={Name} /> */}
      </div>
      <div className="product-price-btn">
        <p>
          <span>{Price}</span>$
        </p>
        <Link
          to={"/company/products/" + Id}
          state={{ Id }}
          className="link-btn"
        >
          More...
        </Link>
      </div>
    </div>
  );
};
