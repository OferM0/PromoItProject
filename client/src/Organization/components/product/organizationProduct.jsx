import React, { useContext, useState, useEffect } from "react";
import { Link } from "react-router-dom";
import "./organizationProduct.css";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import { UserDetailsContext } from "../../../context/userDetails.context";
import { getUserById } from "../../../services/user.service";

export const OrganizationProduct = ({
  CompanyID,
  CampaignID,
  Id,
  Name,
  Description,
  Price,
  Image,
}) => {
  const { userDetails } = useContext(UserDetailsContext);
  const [CompanyName, SetCompanyName] = useState("");
  const fetchData = async () => {
    let response = await getUserById(CompanyID);
    if (response.status === 200) {
      SetCompanyName(response.data.Name);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);
  return (
    <div className="orgproduct-info">
      <div className="orgproduct-text">
        <h1>{Name}</h1>
        <h2>By {CompanyName}</h2>
        <img src={Image} className="image-product" />
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
