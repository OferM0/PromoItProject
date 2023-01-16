import React from "react";
import { Link } from "react-router-dom";
import "./organizationCampaign.css";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";

export const OrganizationCampaign = ({
  Id,
  Name,
  Description,
  Hashtag,
  Url,
}) => {
  //let OrganizationName=await getOrganizationById(OrganizationID).data.Name;

  return (
    <div className="campaign-info2">
      <div className="campaign-text">
        <h1>{Name}</h1>
        <p>{Description}</p>
        <br />
        <a href={Url} target="_blank">
          To campaign's website
        </a>
        <span>#{Hashtag}</span>
      </div>
      <Link
        to={"/organization/campaigns/" + Id}
        state={{ Id }}
        className="link-btn"
      >
        More Details
      </Link>
    </div>
  );
};
