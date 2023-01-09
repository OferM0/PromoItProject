import React, { useState, useEffect } from "react";
import { CompanyProduct } from "../../components/product/companyProduct";
import { getProducts } from "../../../services/product.service";
import "./companyProducts.css";
import { useAuth0 } from "@auth0/auth0-react";

export const CompanyProductsPage = (props) => {
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
        if (product.CompanyID === user.sub) {
          let { OrganizationID, Id, Name, Description, Price, Image } = product;
          return (
            <CompanyProduct
              OrganizationID={OrganizationID}
              Id={Id}
              Name={Name}
              Description={Description}
              Price={Price}
              Image={Image}
            ></CompanyProduct>
          );
        }
      })}
    </div>
  );
};
