import React, { useContext, useState, useEffect } from "react";
import { Link } from "react-router-dom";
import "./managerProduct.css";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import { UserDetailsContext } from "../../../context/userDetails.context";
import { getProductById } from "../../../services/product.service";
import { useAuth0 } from "@auth0/auth0-react";
import { getUserById } from "../../../services/user.service";

export const ManagerProduct = ({
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
      </div>
    </div>
  );
};
