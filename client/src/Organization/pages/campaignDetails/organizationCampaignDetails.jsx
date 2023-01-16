import React, { useEffect, useState } from "react";
import { Link, useLocation } from "react-router-dom";
import "./organizationCampaignDetails.css";
import { ToastContainer, toast } from "react-toastify";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import {
  getCampaignById,
  updateCampaignById,
} from "../../../services/campaign.service";
import { getProducts } from "../../../services/product.service";

const showUnActiveMessage = () => {
  toast.error(
    "You choosed to unactive this campaign weach means it will be hidden from companies and activists!",
    {
      position: "top-right",
      autoClose: 3000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "light",
    }
  );
};

const showActiveMessage = () => {
  toast.success(
    "You choosed to active this campaign weach means it will be shown to companies and activists!",
    {
      position: "top-right",
      autoClose: 3000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "light",
    }
  );
};

export const OrganizationCampaignDetailsPage = () => {
  const [productsArr, setProductsArr] = useState([]);
  const [campaign, setCampaign] = useState({});
  const location = useLocation();
  const { Id } = location.state;

  const fetchData = async () => {
    let response = await getCampaignById(Id);
    if (response.status === 200) {
      setCampaign(response.data);
      //let date = new Date();
      //console.log(date.toISOString().slice(0, 10));
      //console.log(campaign.CreateDate.substring(0, 10));
    }
  };

  const fetchData2 = async () => {
    let response = await getProducts();
    if (response.status === 200) {
      setProductsArr(response.data);
    }
  };

  const handleUpdateStatus = async () => {
    let details = {
      Name: campaign.Name,
      Description: campaign.Description,
      Url: campaign.Url,
      Hashtag: campaign.Hashtag,
      Active: !campaign.Active,
    };
    await updateCampaignById(campaign.Id, details);
    //console.log(details);
  };

  useEffect(() => {
    fetchData();
    fetchData2();
  }, []);

  return (
    <div className="orgcampaignDetailsPage">
      <div className="orgcampaignDetails-info">
        <div className="orgcampaignDetails-title">
          <h1>{campaign.Name}</h1>
        </div>
        <div className="orgcampaignDetails-details-text">
          <p>{campaign.Description}</p>
        </div>
        <div className="orgcampaignDetails-details">
          <p>
            <br />
            Id: {campaign.Id}
            <br />
            Url: {campaign.Url}
            <br />
            Hashtag: #{campaign.Hashtag}
            <br />
            Active: {campaign.Active === true ? <>true</> : <>false</>}
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
        <div className="orgcampaignDetails-btn1">
          <Link to="/organization/campaigns" className="orglink-btn1">
            To Campaigns
          </Link>
        </div>
        <div className="orgcampaignDetails-btn2">
          <Link
            to={"/organization/campaigns/" + campaign.Id + "/edit"}
            className="orglink-btn2"
            state={{ Id }}
          >
            Edit
          </Link>
        </div>
        <div className="orgcampaignDetails-btn3">
          <button
            className="orglink-btn3"
            onClick={async () => {
              if (campaign.Active === true) {
                showUnActiveMessage();
              } else {
                showActiveMessage();
              }
              handleUpdateStatus();
            }}
          >
            <>{campaign.Active === true ? <>UnActive</> : <>Active</>}</>
          </button>
          <ToastContainer />
        </div>
        <div className="orgcampaignDetails-btn4">
          <Link
            className="orglink-btn4"
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
