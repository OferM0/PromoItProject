import React, { useState, useEffect } from "react";
import { ManagerCampaign } from "../../components/campaign/managerCampaign";
import { getCampaigns } from "../../../services/campaign.service";
import "./managerCampaigns.css";
import { useAuth0 } from "@auth0/auth0-react";

export const ManagerCampaignsPage = (props) => {
  const [campaignsArr, setCampaignsArr] = useState([]);
  const { user } = useAuth0();

  const fetchData = async () => {
    let response = await getCampaigns();
    if (response.status === 200) {
      setCampaignsArr(response.data);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div className="campaignsPage">
      {campaignsArr.map((campaign) => {
        let { OrganizationID, Id, Name, Description, Hashtag, Url } = campaign;

        return (
          <ManagerCampaign
            OrganizationID={OrganizationID}
            Id={Id}
            Name={Name}
            Description={Description}
            Url={Url}
            Hashtag={Hashtag}
          ></ManagerCampaign>
        );
      })}
    </div>
  );
};
