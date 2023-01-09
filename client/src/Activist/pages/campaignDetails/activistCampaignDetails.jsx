import React, { useEffect, useState } from "react";
import { Link, useLocation } from "react-router-dom";
import "./activistCampaignDetails.css";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import {
  getCampaignById,
  removeCampaignById,
} from "../../../services/campaign.service";
import { getProducts } from "../../../services/product.service";

export const ActivistCampaignDetailsPage = () => {
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

  const handleRemove = async () => {
    await removeCampaignById(Id);
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
            {/* <br />
            Id: {campaign.Id} */}
            <br />
            Url: {campaign.Url}
            <br />
            Hashtag: #{campaign.Hashtag}
            <br />
            Available Products:{" "}
            {
              productsArr.filter(
                (obj) => obj.CampaignID === campaign.Id && obj.ActivistID === ""
              ).length
            }
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
          <Link to="/activist/campaigns" className="link-btn1">
            To Campaigns
          </Link>
        </div>
        <div className="campaignDetails-btn2">
          <a
            className="link-btn2"
            href={`https://twitter.com/intent/tweet?text=Please%20donate%20to%20${campaign.Name}%20campaign%20%23${campaign.Hashtag}%20${campaign.Url}`}
            target="_blank"
          >
            Tweet
          </a>
        </div>
        <div className="activistCampaignDetails-btn4">
          <Link
            className="activistLink-btn4"
            to={`/activist/campaigns/${Id}/products`}
            state={{ Id }}
          >
            Products
          </Link>
        </div>
      </div>
    </div>
  );
};
