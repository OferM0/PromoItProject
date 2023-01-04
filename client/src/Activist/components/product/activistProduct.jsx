import React, { useContext, useState, useEffect } from "react";
import { Link } from "react-router-dom";
import "./activistProduct.css";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import { UserDetailsContext } from "../../../context/userDetails.context";
import { updateProductById } from "../../../services/product.service";
import { getProductById } from "../../../services/product.service";
import { useAuth0 } from "@auth0/auth0-react";
import { ToastContainer, toast } from "react-toastify";
import { getUserById } from "../../../services/user.service";

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
}) => {
  const [product, setProduct] = useState({});
  const { userDetails } = useContext(UserDetailsContext);
  const { user } = useAuth0();
  const [CompanyName, SetCompanyName] = useState("");
  const fetchData1 = async () => {
    let response = await getUserById(CompanyID);
    if (response.status === 200) {
      SetCompanyName(response.data.Name);
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
        {/* <img src={Image} alt={Name} /> */}
        <p>{Description}</p>
      </div>
      <div className="orgproduct-price-btn">
        <p>
          <span>{Price}</span>$
        </p>
        {product.ActivistID !== user.sub ? (
          <button
            // to={`/activist/campaigns/${CampaignID}/products/${Id}`}
            // state={{ CampaignID, Id }}
            className="link-btn"
            onClick={async () => {
              showToastMessage();
              showActivistInfo();
              product.ActivistID = user.sub;
              //console.log(product);
              await updateProductById(Id, product);
            }}
          >
            Buy
          </button>
        ) : (
          <button
            className="link-btn"
            onClick={async () => {
              showDonateMessage();
              product.DonatedByActivist = true;
              //product.ActivistID = "";
              await updateProductById(Id, product);
            }}
          >
            Donate
          </button>
        )}

        <ToastContainer />
      </div>
    </div>
  );
};
