import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import "./managerCampaign.css";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import { getUserById } from "../../../services/user.service";

export const ManagerCampaign = ({
  OrganizationID,
  Id,
  Name,
  Description,
  Hashtag,
  Url,
}) => {
  //let ManagerName=await getManagerById(ManagerID).data.Name;
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
    <div className="campaign-info11">
      <div className="campaign-text11">
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
        to={"/manager/campaigns/" + Id}
        state={{ Id, OrganizationName }}
        className="link-btn"
      >
        More Details
      </Link>
    </div>
  );
};
