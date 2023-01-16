import React, { useContext, useState, useEffect } from "react";
import { Link } from "react-router-dom";
import "./activistProduct.css";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import { UserDetailsContext } from "../../../context/userDetails.context";
import { updateProductById } from "../../../services/product.service";
import { getProductById, addProduct } from "../../../services/product.service";
import { useAuth0 } from "@auth0/auth0-react";
import { ToastContainer, toast } from "react-toastify";
import { getUserById, updateUserById } from "../../../services/user.service";
import { postTweetsByManager } from "../../../services/twitter.service";

const showToastMessage = () => {
  toast.success(
    "Congratulations! you promoted the society and also purchased nice product. We will inform the company to suplly your product.",
    {
      position: "top-left",
      autoClose: 4000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "light",
    }
  );
};

const showErrorMessage = () => {
  toast.error(
    "You dont have enough money! you can keep promot campaigns to earn money and then come back.",
    {
      position: "top-left",
      autoClose: 4000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "light",
    }
  );
};

const showActivistInfo = () => {
  toast.info(
    "Please notice you can choose to donate this product and return it to the campaign!",
    {
      position: "top-left",
      autoClose: 6000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "light",
    }
  );
};

const showDonateMessage = () => {
  toast.success("Thank you for donating back this product!", {
    position: "top-left",
    autoClose: 4000,
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
    theme: "light",
  });
};

export const ActivistProduct = ({
  CompanyID,
  CampaignID,
  Id,
  Name,
  Description,
  Price,
  Image,
  Stock,
}) => {
  const [product, setProduct] = useState({});
  const { userDetails, setUserDetails } = useContext(UserDetailsContext);
  const { user } = useAuth0();
  const [CompanyName, SetCompanyName] = useState("");
  const [CompanyTwitterHandle, SetCompanyTwitterHandle] = useState("");
  const fetchData1 = async () => {
    let response = await getUserById(CompanyID);
    if (response.status === 200) {
      SetCompanyName(response.data.Name);
      SetCompanyTwitterHandle(response.data.TwitterHandle);
    }
  };

  const fetchData2 = async () => {
    let response = await getProductById(Id);
    if (response.status === 200) {
      setProduct(response.data);
      //console.log(product.Name);
    }
  };

  useEffect(() => {
    fetchData1();
    fetchData2();
  }, []);

  return (
    <div className="orgproduct-info">
      <div className="orgproduct-text">
        <h1>{Name}</h1>
        <h2>By {CompanyName}</h2>
        <img src={Image} className="image-product" />
        <p>{Description}</p>
      </div>
      <div className="orgproduct-price-btn">
        <p>
          <span>{Price}$</span>
          {Stock > 0 ? (
            <>
              <br />
              <span>Stock: {Stock}</span>
            </>
          ) : (
            <></>
          )}
        </p>
        <>
          {product.ActivistID !== user.sub ? (
            <button
              // to={`/activist/campaigns/${CampaignID}/products/${Id}`}
              // state={{ CampaignID, Id }}
              className="link-btn"
              onClick={async () => {
                if (userDetails.Status >= product.Price) {
                  showToastMessage();
                  showActivistInfo();
                  product.ActivistID = user.sub;
                  //console.log(product);
                  await updateProductById(Id, product);
                  let newStatus = userDetails.Status - product.Price;
                  let newUserDetails = userDetails;
                  newUserDetails.Status = newStatus;
                  setUserDetails(newUserDetails);
                  await updateUserById(user.sub, newUserDetails);
                  await postTweetsByManager(
                    userDetails.TwitterHandle,
                    CompanyTwitterHandle,
                    Id
                  );
                } else {
                  showErrorMessage();
                }
              }}
            >
              Buy
            </button>
          ) : product.Shipped === false &&
            product.DonatedByActivist === false ? (
            <button
              className="link-btn"
              onClick={async () => {
                showDonateMessage();
                product.DonatedByActivist = true;
                await updateProductById(Id, product);
                product.ActivistID = "";
                product.DonatedByActivist = false;
                await addProduct(product);
              }}
            >
              Donate
            </button>
          ) : product.Shipped === true ? (
            <p className="product-status-info">Shipped</p>
          ) : (
            <p className="product-status-info">Donated</p>
          )}
        </>

        <ToastContainer />
      </div>
    </div>
  );
};
