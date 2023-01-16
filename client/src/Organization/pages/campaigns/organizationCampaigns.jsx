import React, { useState, useEffect } from "react";
import { OrganizationCampaign } from "../../components/campaign/organizationCampaign";
import { getCampaigns } from "../../../services/campaign.service";
import "./organizationCampaigns.css";
import { useAuth0 } from "@auth0/auth0-react";

export const OrganizationCampaignsPage = (props) => {
  const [campaignsArr, setCampaignsArr] = useState([]);
  const { user } = useAuth0();
  let check = false; // to check if there are not connected campaigns
  const [isActive, setIsActive] = useState("Active");
  const [activeBool, setIsActiveBool] = useState(true);
  const handleActiveChange = (event) => {
    setIsActive(event.target.value);
    if (event.target.value === "Active") {
      setIsActiveBool(true);
    } else {
      setIsActiveBool(false);
    }
  };

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
      <div onChange={(event) => handleActiveChange(event)} className="wrapper2">
        <input
          type="radio"
          id="Active"
          name="select"
          value="Active"
          checked={isActive === "Active"}
          onChange={handleActiveChange}
        />
        <label htmlFor="Active" className="option2 Active">
          <div class="dot2"></div>
          <span>Active</span>
        </label>
        <input
          type="radio"
          id="UnActive"
          name="select"
          value="UnActive"
          checked={isActive === "UnActive"}
          onChange={handleActiveChange}
        />
        <label htmlFor="UnActive" className="option2 UnActive">
          <div class="dot2"></div>
          <span>UnActive</span>
        </label>
      </div>
      {campaignsArr.map((campaign) => {
        if (
          campaign.OrganizationID === user.sub &&
          campaign.Active === activeBool
        ) {
          check = true;
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
      <>
        {check === false && activeBool === true ? (
          <>
            <div className="noConnectedProducts">
              <p>
                You don't have any campaigns, Please create some to promote your
                agenda.
              </p>
            </div>
          </>
        ) : (
          <></>
        )}
      </>
    </div>
  );
};
