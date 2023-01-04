import React, { useState, useEffect } from "react";
import { ActivistProduct } from "../../components/product/activistProduct";
import { getProducts } from "../../../services/product.service";
import "./activistMyProducts.css";
import { useAuth0 } from "@auth0/auth0-react";

export const ActivistMyProductsPage = (props) => {
  const [productsArr, setProductsArr] = useState([]);
  const { user } = useAuth0();

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
        if (product.ActivistID === user.sub) {
          let { CompanyID, Id, Name, Description, Price } = product;
          return (
            <ActivistProduct
              CompanyID={CompanyID}
              Id={Id}
              Name={Name}
              Description={Description}
              Price={Price}
              // Image={Image}
            ></ActivistProduct>
          );
        }
      })}
    </div>
  );
};
