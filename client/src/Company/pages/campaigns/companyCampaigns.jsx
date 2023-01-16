import React, { useState, useEffect } from "react";
import { CompanyCampaign } from "../../components/campaign/companyCampaign";
import { getCampaigns } from "../../../services/campaign.service";
import "./companyCampaigns.css";
import { useAuth0 } from "@auth0/auth0-react";

export const CompanyCampaignsPage = (props) => {
  const [campaignsArr, setCampaignsArr] = useState([]);
  const [FilterCampaigns, setFilterCampaigns] = useState([]);
  const { user } = useAuth0();

  const fetchData = async () => {
    let response = await getCampaigns();
    if (response.status === 200) {
      setCampaignsArr(response.data);
      setFilterCampaigns(response.data);
    }
  };

  const handleFilter = (onChangeEvent) => {
    let freeText = onChangeEvent.target.value;
    if (freeText) {
      let filteredArr = campaignsArr.filter((campaign) =>
        campaign.Name.toLowerCase().includes(freeText.toLowerCase())
      );
      setFilterCampaigns(filteredArr);
    } else {
      setFilterCampaigns(campaignsArr);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div className="campaignsPage">
      <div classNameName="input-group input-group-sm mb-3 filterContainer">
        <input
          placeholder="Search campaign by name"
          type="text"
          className="form-control filter"
          aria-label="Small"
          aria-describedby="inputGroup-sizing-sm"
          onChange={handleFilter}
        />
      </div>
      {FilterCampaigns.map((campaign) => {
        if (campaign.Active === true) {
          let { OrganizationID, Id, Name, Description, Hashtag, Url } =
            campaign;

          return (
            <CompanyCampaign
              OrganizationID={OrganizationID}
              Id={Id}
              Name={Name}
              Description={Description}
              Url={Url}
              Hashtag={Hashtag}
            ></CompanyCampaign>
          );
        }
      })}
    </div>
  );
};
