import React, { useState, useEffect } from "react";
import { OrganizationProduct } from "../../components/product/organizationProduct";
import { getProducts } from "../../../services/product.service";
import "./organizationProducts.css";
import { useAuth0 } from "@auth0/auth0-react";
import { useLocation } from "react-router-dom";

export const OrganizationProductsPage = (props) => {
  const [productsArr, setProductsArr] = useState([]);
  const { user } = useAuth0();
  const location = useLocation();
  const { Id } = location.state;
  let check = false; // to check if there are not connected products

  let CampaignID = Id;
  const fetchData = async () => {
    let response = await getProducts();
    if (response.status === 200) {
      setProductsArr(response.data);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div className="productsPage">
      {productsArr.map((product) => {
        if (product.CampaignID === Id) {
          check = true;
          let { Id, Name, Description, Price } = product;
          return (
            <OrganizationProduct
              CampaignID={CampaignID}
              Id={Id}
              Name={Name}
              Description={Description}
              Price={Price}
              // Image={Image}
            ></OrganizationProduct>
          );
        }
      })}
      <>
        {check === false ? (
          <>
            <div className="noConnectedProducts">
              <p>No Connected Products</p>
            </div>
          </>
        ) : (
          <></>
        )}
      </>
    </div>
  );
};
