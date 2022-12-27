import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { useLocation } from "react-router-dom";
import "./style.css";
import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import { getProductById } from "../../services/product.service";

export const ProductDetailsPage = () => {
  const [product, setProduct] = useState({});

  const location = useLocation();
  const { ProductID } = location.state;

  const fetchData = async () => {
    let response = await getProductById(ProductID);
    if (response.status === 200) {
      setProduct(response.data);
      //console.log(product);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div className="productDetailsPage">
      <div className="productDetails-info">
        <div className="productDetails-title">
          <h1>{product.ProductName}</h1>
          <h2>by NorthWind</h2>
        </div>
        <div className="productDetails-text">
          <p>
            Lorem ipsum dolor sit amet consectetur.
            <br /> qui deserunt placeat voluptas.
            <br /> aperiam consectetur facere alias itaque tenetur,
            <br /> voluptates iure dolore!
          </p>
        </div>
        <div className="productDetails-details">
          <p>
            <br />
            Id: {product.ProductID}
            <br />
            Price: {product.UnitPrice}$
            <br />
            Units in stock: {product.UnitsInStock}
            <br />
            Units on order: {product.UnitsOnOrder}
            <br />
            Supplier Id: {product.SupplierID}
            <br />
            Category Id: {product.CategoryID}
            <br />
            Quantity per unit: {product.QuantityPerUnit}
            <br />
            Reorder level: {product.ReorderLevel}
            <br />
            Discontinued: {product.Discontinued}
          </p>
        </div>
        <div className="productDetails-btn">
          <Link to="/products" className="link-btn">
            To Shop
          </Link>
        </div>
      </div>
    </div>
  );
};
