import React, { useState, useEffect, useContext } from "react";
import { ActivistProduct } from "../../components/product/activistProduct";
import { getProducts } from "../../../services/product.service";
import "./activistProducts.css";
import { useAuth0 } from "@auth0/auth0-react";
import { useLocation, useNavigate } from "react-router-dom";
import { UserDetailsContext } from "../../../context/userDetails.context";

export const ActivistProductsPage = (props) => {
  const [productsArr, setProductsArr] = useState([]);
  const [sortProductsArr, setSortProductsArr] = useState([]);
  const { userDetails, setUserDetails } = useContext(UserDetailsContext);
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
      // const filterArr = Object.values(productsArr).filter(
      //   (product) => product.CampaignID === Id && product.ActivistID === ""
      // );

      // const groupedObjects = filterArr.reduce((acc, obj) => {
      //   const modifiedObject = { ...obj };
      //   delete modifiedObject.Id;
      //   const key = JSON.stringify(modifiedObject);
      //   if (!acc[key]) {
      //     acc[key] = [];
      //   }
      //   acc[key].push(obj);
      //   return acc;
      // }, {});

      //console.log(Object.values(groupedObjects));
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
      <div className="statusInfo">Your Status: {userDetails.Status} $</div>
      {sortProductsArr.map((product) => {
        if (product[0].CampaignID === Id && product[0].ActivistID === "") {
          check = true;
          let { CompanyID, Id, Name, Description, Price, Image } = product[0];
          return (
            <ActivistProduct
              CampaignID={CampaignID}
              CompanyID={CompanyID}
              Id={Id}
              Name={Name}
              Description={Description}
              Price={Price}
              Image={Image}
              Stock={product.length}
            ></ActivistProduct>
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
