import React, { useState, useEffect } from "react";
import { Product } from "../../components/product/product.component.jsx";
import { getProducts } from "../../services/product.service.js";
import "./style.css";

export const ProductsPage = (props) => {
  const [productsArr, setProductsArr] = useState([]);

  const fetchData = async () => {
    let response = await getProducts();
    if (response.status === 200) {
      setProductsArr(response.data); //Object.values(response.data)
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div className="productsPage">
      {productsArr.map((products) => {
        let {
          ProductID,
          ProductName,
          SupplierID,
          CategoryID,
          QuantityPerUnit,
          UnitPrice,
          UnitsInStock,
          UnitsOnOrder,
          ReorderLevel,
          Discontinued,
        } = products;
        return (
          <Product
            ProductID={ProductID}
            ProductName={ProductName}
            SupplierID={SupplierID}
            CategoryID={CategoryID}
            QuantityPerUnit={QuantityPerUnit}
            UnitPrice={UnitPrice}
            UnitsInStock={UnitsInStock}
            UnitsOnOrder={UnitsOnOrder}
            ReorderLevel={ReorderLevel}
            Discontinued={Discontinued}
          ></Product>
        );
      })}
    </div>
  );
};
