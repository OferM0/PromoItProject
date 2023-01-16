import React, { useEffect, useState } from "react";
import { Link, useLocation } from "react-router-dom";
import "./managerCampaignDetails.css";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import { getCampaignById } from "../../../services/campaign.service";
import { getProducts } from "../../../services/product.service";

export const ManagerCampaignDetailsPage = () => {
  const [productsArr, setProductsArr] = useState([]);
  const [campaign, setCampaign] = useState({});
  const location = useLocation();
  const { Id, OrganizationName } = location.state;

  const fetchData = async () => {
    let response = await getCampaignById(Id);
    if (response.status === 200) {
      setCampaign(response.data);
      //console.log(campaign);
    }
  };

  const fetchData2 = async () => {
    let response = await getProducts();
    if (response.status === 200) {
      setProductsArr(response.data);
    }
  };

  useEffect(() => {
    fetchData();
    fetchData2();
  }, []);

  return (
    <div className="campaignDetailsPage">
      <div className="campaignDetails-info">
        <div className="campaignDetails-title">
          <h1>{campaign.Name}</h1>
          <h2>By {OrganizationName}</h2>
        </div>
        <div className="campaignDetails-details-text">
          <p>{campaign.Description}</p>
        </div>
        <div className="campaignDetails-details">
          <p>
            Id: {campaign.Id}
            <br />
            Url: {campaign.Url}
            <br />
            Hashtag: #{campaign.Hashtag}
            <br />
            <p className="spanOnlyDate">Created At: {campaign.CreateDate}</p>
            <br />
            Connected Products:{" "}
            {productsArr.filter((obj) => obj.CampaignID === campaign.Id).length}
            <br />
            Donation Until Now:{" "}
            {productsArr
              .filter(
                (obj) => obj.CampaignID === campaign.Id && obj.ActivistID !== ""
              )
              .reduce((accumulator, object) => {
                return accumulator + object.Price;
              }, 0)}{" "}
            $
          </p>
        </div>
        <div className="campaignDetails-btn1">
          <Link to="/manager/campaigns" className="link-btn1">
            To Campaigns
          </Link>
        </div>
        {/* <div className="campaignDetails-btn2">
          <Link to="" className="link-btn2">
            Delete
          </Link>
        </div> */}
        <div className="managerCampaignDetails-btn4">
          <Link
            className="managerLink-btn4"
            to={`/manager/campaigns/${Id}/products`}
            state={{ Id }}
          >
            Products
          </Link>
        </div>
      </div>
    </div>
  );
};
