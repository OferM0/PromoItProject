import React, { useContext } from "react";
import { Link } from "react-router-dom";
import "./companyUser.css";
import { UserDetailsContext } from "../../../context/userDetails.context";
//import "../../../node_modules/bootstrap/dist/css/bootstrap.css";

export const CompanyUser = ({
  OrganizationID,
  Id,
  Name,
  Description,
  Price,
  Image,
}) => {
  const { userDetails } = useContext(UserDetailsContext);
  return (
    <div className="user-info">
      <div className="user-text">
        <h1>{Name}</h1>
        {/* <img src={Image} alt={Name} /> */}
      </div>
      <div className="user-price-btn">
        <p>
          <span>{Price}</span>$
        </p>
        <Link
          to={"/company/users/" + Id}
          state={{ Id, OrganizationID }}
          className="link-btn"
        >
          More...
        </Link>
      </div>
    </div>
  );
};
