import React, { useState, useEffect } from "react";
import { OrganizationProduct } from "../../components/product/organizationProduct";
import { getProducts } from "../../../services/product.service";
import "./organizationProducts.css";
import { useAuth0 } from "@auth0/auth0-react";
import { useLocation, useNavigate } from "react-router-dom";

export const OrganizationProductsPage = (props) => {
  const [productsArr, setProductsArr] = useState([]);
  const [sortProductsArr, setSortProductsArr] = useState([]);
  const navigate = useNavigate();
  const { user } = useAuth0();
  const location = useLocation();
  const { Id } = location.state;
  let check = false; // to check if there are not connected products

  let CampaignID = Id;
  const fetchData = async () => {
    let response = await getProducts();
    if (response.status === 200) {
      setProductsArr(response.data);
      setSortProductsArr(
        Object.values(
          Object.values(response.data)
            .filter(
              (product) =>
                product.CampaignID === Id && product.ActivistID === ""
            )
            .reduce((acc, obj) => {
              const modifiedObject = { ...obj };
              delete modifiedObject.Id;
              const key = JSON.stringify(modifiedObject);
              if (!acc[key]) {
                acc[key] = [];
              }
              acc[key].push(obj);
              return acc;
            }, {})
        )
      );
      console.log(sortProductsArr);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div className="productsPage">
      {sortProductsArr.map((product) => {
        if (product[0].CampaignID === Id) {
          check = true;
          let { CompanyID, Id, Name, Description, Price, Image } = product[0];
          return (
            <OrganizationProduct
              CampaignID={CampaignID}
              CompanyID={CompanyID}
              Id={Id}
              Name={Name}
              Description={Description}
              Price={Price}
              Image={Image}
              Stock={product.length}
            ></OrganizationProduct>
          );
        }
      })}
      <>
        {check === false ? (
          <>
            <div className="noConnectedProducts">
              <p>No Connected Products</p>
              <button
                className="btnReturn"
                onClick={() => {
                  navigate(-1);
                }}
              >
                Return To Campaign
              </button>
            </div>
          </>
        ) : (
          <>
            <button
              className="btnReturn2"
              onClick={() => {
                navigate(-1);
              }}
            >
              Return To Campaign
            </button>
          </>
        )}
      </>
    </div>
  );
};
