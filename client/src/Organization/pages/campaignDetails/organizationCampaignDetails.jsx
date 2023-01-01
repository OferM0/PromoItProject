import React, { useEffect, useState } from "react";
import { Link, useLocation } from "react-router-dom";
import "./organizationCampaignDetails.css";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import {
  getCampaignById,
  removeCampaignById,
} from "../../../services/campaign.service";
import { getProducts } from "../../../services/product.service";

export const OrganizationCampaignDetailsPage = () => {
  const [productsArr, setProductsArr] = useState([]);
  const [campaign, setCampaign] = useState({});
  const location = useLocation();
  const { Id } = location.state;

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
        </div>
        <div className="campaignDetails-details-text">
          <p>{campaign.Description}</p>
        </div>
        <div className="campaignDetails-details">
          <p>
            <br />
            Id: {campaign.Id}
            <br />
            Url: {campaign.Url}
            <br />
            Hashtag: #{campaign.Hashtag}
            <br />
            Connected Products:{" "}
            {productsArr.filter((obj) => obj.CampaignID === campaign.Id).length}
            <br />
            Donation Until Now:{" "}
            {productsArr
              .filter((obj) => obj.CampaignID === campaign.Id)
              .reduce((accumulator, object) => {
                return accumulator + object.Price;
              }, 0)}{" "}
            $
          </p>
        </div>
        <div className="campaignDetails-btn1">
          <Link to="/organization/campaigns" className="link-btn1">
            To Campaigns
          </Link>
        </div>
        <div className="campaignDetails-btn2">
          <Link
            to={"/organization/campaigns/" + campaign.Id + "/edit"}
            className="link-btn2"
            state={{ Id }}
          >
            Edit
          </Link>
        </div>
        <div className="campaignDetails-btn3">
          <Link
            className="link-btn3"
            // onClick={() => {
            //   handleRemove(); /// not working ---------------------------
            // }}
            to={"/organization/campaigns"}
          >
            Delete
          </Link>
        </div>
        <div className="campaignDetails-btn4">
          <Link
            className="link-btn4"
            to={`/organization/campaigns/${Id}/products`}
            state={{ Id }}
          >
            Products
          </Link>
        </div>
      </div>
    </div>
  );
};
