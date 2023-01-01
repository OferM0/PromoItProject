import React, { useState, useEffect } from "react";
import { CompanyCampaign } from "../../components/campaign/companyCampaign";
import { getCampaigns } from "../../../services/campaign.service";
import "./companyCampaigns.css";
import { useAuth0 } from "@auth0/auth0-react";

export const CompanyCampaignsPage = (props) => {
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
        let { Id, Name, Description, Hashtag, Url } = campaign;

        return (
          <CompanyCampaign
            Id={Id}
            Name={Name}
            Description={Description}
            Url={Url}
            Hashtag={Hashtag}
          ></CompanyCampaign>
        );
      })}
    </div>
  );
};
