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
  const { Id } = location.state;
  const [organizationDetails, setOrganizationDetails] = useState([]);

  const fetchData = async () => {
    let response = await getProductById(Id);
    if (response.status === 200) {
      setProduct(response.data);
      //console.log(product);
    }
  };

  const fetchData2 = async () => {
    //---------------------------------------not working yet--------------------------------
    let response = await getUserById(product.OrganizationID);
    if (response.status === 200) {
      setOrganizationDetails(response.data);
      //console.log(organizationDetails);
    }
  };

  useEffect(() => {
    fetchData();
    fetchData2();
  }, []);

  return (
    <div className="productDetailsPage">
      <div className="productDetails-info">
        <div className="productDetails-title">
          <h1>{product.Name}</h1>
          {/* <img src={product.Image} alt={product.Name} /> */}
        </div>
        <div className="productDetails-text">
          <p>{product.Description}</p>
        </div>
        <div className="productDetails-details">
          <p>
            <br />
            Id: {product.Id}
            <br />
            Price: {product.Price}$
            <br />
            Donated to: {organizationDetails.Name}
          </p>
        </div>
        <div className="productDetails-btn">
          <Link to="/company/products" className="link-btn">
            To Products
          </Link>
        </div>
      </div>
    </div>
  );
};
