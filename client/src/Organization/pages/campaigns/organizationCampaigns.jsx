import React, { useState, useEffect } from "react";
import { OrganizationCampaign } from "../../components/campaign/organizationCampaign";
import { getCampaigns } from "../../../services/campaign.service";
import "./organizationCampaigns.css";
import { useAuth0 } from "@auth0/auth0-react";

export const OrganizationCampaignsPage = (props) => {
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
        if (campaign.OrganizationID === user.sub) {
          let { Id, Name, Description, Hashtag, Url } = campaign;

          return (
            <OrganizationCampaign
              Id={Id}
              Name={Name}
              Description={Description}
              Url={Url}
              Hashtag={Hashtag}
            ></OrganizationCampaign>
          );
        }
      })}
    </div>
  );
};
