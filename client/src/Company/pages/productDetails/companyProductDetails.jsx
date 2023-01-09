import React, { useEffect, useState, useContext } from "react";
import { Link } from "react-router-dom";
import { useLocation } from "react-router-dom";
import "./companyProductDetails.css";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import { getProductById } from "../../../services/product.service";
import { UserDetailsContext } from "../../../context/userDetails.context";
import { getUserById } from "../../../services/user.service";

export const CompanyProductDetailsPage = () => {
  const [product, setProduct] = useState([]);
  const { userDetails } = useContext(UserDetailsContext);
  const location = useLocation();
  const { Id, OrganizationID } = location.state;
  const [organizationName, setOrganizationName] = useState("");

  const fetchData = async () => {
    let response = await getProductById(Id);
    if (response.status === 200) {
      setProduct(response.data);
      //console.log(product);
    }
  };

  const fetchData2 = async () => {
    let response = await getUserById(OrganizationID);
    if (response.status === 200) {
      setOrganizationName(response.data.Name);
      //console.log(organizationDetails);
    }
  };

  useEffect(() => {
    fetchData();
    fetchData2();
  }, []);

  return (
    <div className="companyproductDetailsPage">
      <div className="companyproductDetails-info">
        <div className="companyproductDetails-title">
          <h1>{product.Name}</h1>
          {/* <img src={product.Image} className="image-product-company2" /> */}
        </div>
        <div className="companyproductDetails-text">
          <p>{product.Description}</p>
        </div>
        <div className="companyproductDetails-details">
          <p>
            <br />
            Id: {product.Id}
            <br />
            Price: {product.Price}$
            <br />
            Donated to: {organizationName}
          </p>
        </div>
        <div className="companyproductDetails-btn">
          <Link to="/company/products" className="companylink-btn">
            To Products
          </Link>
        </div>
      </div>
    </div>
  );
};
