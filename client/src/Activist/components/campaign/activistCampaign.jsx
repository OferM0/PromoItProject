import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import "./activistCampaign.css";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import { getUserById } from "../../../services/user.service";

export const ActivistCampaign = ({
  OrganizationID,
  Id,
  Name,
  Description,
  Hashtag,
  Url,
}) => {
  //let ActivistName=await getActivistById(ActivistID).data.Name;
  const [OrganizationName, SetOrganizationName] = useState("");
  const fetchData = async () => {
    let response = await getUserById(OrganizationID);
    if (response.status === 200) {
      SetOrganizationName(response.data.Name);
    }
  };
  useEffect(() => {
    fetchData();
  }, []);
  return (
    <div className="campaign-info">
      <div className="campaign-text">
        <h1>{Name}</h1>
        <h2>By {OrganizationName}</h2>
        <p>{Description}</p>
        <br />
        <a href={Url} target="_blank">
          To campaign's website
        </a>
        <span>#{Hashtag}</span>
      </div>
      <Link
        to={"/activist/campaigns/" + Id}
        state={{ Id, OrganizationName }}
        className="link-btn"
      >
        More Details
      </Link>
    </div>
  );
};
