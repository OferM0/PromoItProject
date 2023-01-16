import React, { useState, useEffect } from "react";
import { CompanyProduct } from "../../components/product/companyProduct";
import { getProducts } from "../../../services/product.service";
import "./companyProducts.css";
import { useAuth0 } from "@auth0/auth0-react";

export const CompanyProductsPage = (props) => {
  const [productsArr, setProductsArr] = useState([]);
  const [FilterProducts, setFilterProducts] = useState([]);
  const { user } = useAuth0();
  const fetchData = async () => {
    let response = await getProducts();
    if (response.status === 200) {
      setProductsArr(response.data);
      setFilterProducts(response.data);
    }
  };

  const handleFilter = (onChangeEvent) => {
    let freeText = onChangeEvent.target.value;
    if (freeText) {
      let filteredArr = productsArr.filter(
        (product) => product.Id === parseInt(freeText)
      );
      setFilterProducts(filteredArr);
    } else {
      setFilterProducts(productsArr);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div className="productsPage">
      <div classNameName="input-group input-group-sm mb-3 filterContainer">
        <input
          placeholder="Search product by id"
          type="text"
          className="form-control filter"
          aria-label="Small"
          aria-describedby="inputGroup-sizing-sm"
          onChange={handleFilter}
        />
      </div>
      {FilterProducts.map((product) => {
        if (
          product.CompanyID === user.sub &&
          product.DonatedByActivist === false
        ) {
          let {
            OrganizationID,
            Id,
            Name,
            Description,
            Price,
            Image,
            CampaignID,
          } = product;
          return (
            <CompanyProduct
              OrganizationID={OrganizationID}
              Id={Id}
              Name={Name}
              Description={Description}
              Price={Price}
              Image={Image}
              CampaignID={CampaignID}
            ></CompanyProduct>
          );
        }
      })}
    </div>
  );
};
