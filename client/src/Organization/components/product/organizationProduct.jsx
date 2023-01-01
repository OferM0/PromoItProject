import React, { useContext } from "react";
import { Link } from "react-router-dom";
import "./organizationProduct.css";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import { UserDetailsContext } from "../../../context/userDetails.context";

export const OrganizationProduct = ({
  CampaignID,
  Id,
  Name,
  Description,
  Price,
  Image,
}) => {
  const { userDetails } = useContext(UserDetailsContext);

  return (
    <div className="orgproduct-info">
      <div className="orgproduct-text">
        <h1>{Name}</h1>
        {/* <img src={Image} alt={Name} /> */}
        <p>{Description}</p>
      </div>
      <div className="orgproduct-price-btn">
        <p>
          <span>{Price}</span>$
        </p>
        {/* <Link
          to={`/organization/campaigns/${CampaignID}/orgproducts/${Id}`}
          state={{ CampaignID, Id }}
          className="link-btn"
        >
          More...
        </Link> */}
      </div>
    </div>
  );
};
