import React, { useState, useEffect } from "react";
//import { CompanyProduct } from "../../components/product/companyProduct";
import { getProducts } from "../../../services/product.service";
import "./companyUsers.css";
import { useAuth0 } from "@auth0/auth0-react";
import { getUserById } from "../../../services/user.service";
import { updateProductById } from "../../../services/product.service";

export const CompanyUsersPage = (props) => {
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
      <section>
        <h1 className="tbl-h1">Orders List</h1>
        <div className="tbl-header">
          <table cellpadding="0" cellspacing="0" border="0">
            <thead>
              <tr>
                <th className="companyth">Product ID</th>
                <th className="companyth">Name</th>
                <th className="companyth">Address</th>
                <th className="companyth">Phone</th>
                <th className="companyth">Complete</th>
              </tr>
            </thead>
          </table>
        </div>
        <div className="tbl-content">
          <table cellpadding="0" cellspacing="0" border="0">
            <tbody>
              {productsArr.map((product) => {
                if (
                  product.CompanyID === user.sub &&
                  product.ActivistID !== "" &&
                  product.DonatedByActivist === false &&
                  product.Shipped === false
                ) {
                  let { Id, ActivistID } = product;

                  return (
                    <tr>
                      <td className="companytd">{Id}</td>
                      <CompanyUserDetails
                        activistId={ActivistID}
                        Id={Id}
                        product={product}
                      ></CompanyUserDetails>
                    </tr>
                  );
                }
              })}
            </tbody>
          </table>
        </div>
      </section>
    </div>
  );
};

export const CompanyUserDetails = ({ activistId, Id, product }) => {
  const [activistDetails, setActivistDetails] = useState({});
  console.log(activistId);
  const fetchData2 = async () => {
    let response = await getUserById(activistId);
    if (response.status === 200) {
      setActivistDetails(response.data);
      //console.log(activistDetails);
    }
  };

  useEffect(() => {
    fetchData2();
  }, []);
  let { Name, Address, Phone } = activistDetails;
  return (
    <>
      <td className="companytd">{Name}</td>
      <td className="companytd">{Address}</td>
      <td className="companytd">{Phone}</td>
      <td className="companytd">
        <button
          className="complitOrder"
          onClick={async () => {
            product.Shipped = true;
            //console.log(product);
            await updateProductById(Id, product);
          }}
        >
          complete
        </button>
      </td>
    </>
  );
};
