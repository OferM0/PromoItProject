import React, { useState, useEffect } from "react";
import { ManagerProduct } from "../../components/product/managerProduct";
import { getProducts } from "../../../services/product.service";
import "./managerProducts.css";
import { useAuth0 } from "@auth0/auth0-react";
import { useLocation, useNavigate } from "react-router-dom";

export const ManagerProductsPage = (props) => {
  const [productsArr, setProductsArr] = useState([]);
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
          let { CompanyID, Id, Name, Description, Price } = product;
          return (
            <ManagerProduct
              CampaignID={CampaignID}
              CompanyID={CompanyID}
              Id={Id}
              Name={Name}
              Description={Description}
              Price={Price}
              // Image={Image}
            ></ManagerProduct>
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
