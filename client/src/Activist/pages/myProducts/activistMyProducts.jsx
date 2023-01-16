import React, { useState, useEffect } from "react";
import { ActivistProduct } from "../../components/product/activistProduct";
import { getProducts } from "../../../services/product.service";
import "./activistMyProducts.css";
import { useAuth0 } from "@auth0/auth0-react";
import HashLoader from "react-spinners/HashLoader";

export const ActivistMyProductsPage = (props) => {
  const [productsArr, setProductsArr] = useState([]);
  const { user } = useAuth0();
  let check = false; // to check if there are not connected products

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
      {
        /*productsArr &&*/
        productsArr.map((product) => {
          if (
            product.ActivistID === user.sub /*&&
          product.DonatedByActivist === false*/
          ) {
            check = true;
            let { CompanyID, Id, Name, Description, Price, Image } = product;
            return (
              <ActivistProduct
                CompanyID={CompanyID}
                Id={Id}
                Name={Name}
                Description={Description}
                Price={Price}
                Image={Image}
              ></ActivistProduct>
            );
          }
        })
      }
      <>
        {
          /*!productsArr &&*/ check === false ? (
            <>
              <div className="noConnectedProducts">
                <p>You don't have any product, keep tweeting to earn points</p>
              </div>
            </>
          ) : (
            <></>
          )
        }
      </>
    </div>
  );
};
