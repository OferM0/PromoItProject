import React from "react";
import { Link } from "react-router-dom";
import "./companyCampaign.css";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";

export const CompanyCampaign = ({ Id, Name, Description, Hashtag, Url }) => {
  //let CompanyName=await getCompanyById(CompanyID).data.Name;

  return (
    <div className="campaign-info">
      <div className="campaign-text">
        <h1>{Name}</h1>
        <p>{Description}</p>
        <br />
        <a href={Url} target="_blank">
          To campaign's website
        </a>
        <span>#{Hashtag}</span>
      </div>
      <Link to={"/company/campaigns/" + Id} state={{ Id }} className="link-btn">
        More Details
      </Link>
    </div>
  );
};
