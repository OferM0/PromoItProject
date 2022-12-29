import React from "react";
import "./organizationSideBar.css";
import HomeIcon from "@mui/icons-material/Home";
import InfoIcon from "@mui/icons-material/Info";
import CallIcon from "@mui/icons-material/Call";
import CreateIcon from "@mui/icons-material/Create";
import CampaignIcon from "@mui/icons-material/Campaign";
import ProductIcon from "@mui/icons-material/LocalMall";
import FaceIconfrom from "@mui/icons-material/Face";
import LogOutIconfrom from "@mui/icons-material/Logout";
import { Link } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

export const OrganizationSideBar = (props) => {
  const { logout } = useAuth0();

  return (
    <>
      <div className="container2 p-3">
        <ul className="nav nav-pills flex-column">
          <li className="nav-item">
            <Link to="/" className="nav-link link-dark aa">
              <HomeIcon />
              <span className="section-name">Home</span>
            </Link>
          </li>
          <li>
            <Link to="/about" className="nav-link link-dark">
              <InfoIcon />
              <span className="section-name">About</span>
            </Link>
          </li>
          <li>
            <Link to="/contacts" className="nav-link link-dark">
              <CallIcon />
              <span className="section-name">Contact Us</span>
            </Link>
          </li>
          <li>
            <Link to="/organization/campaigns" className="nav-link link-dark">
              <CampaignIcon />
              <span className="section-name">My Campaigns</span>
            </Link>
          </li>
          <li>
            <Link to="/organization/campaign" className="nav-link link-dark">
              <CreateIcon />
              <span className="section-name">Create New Campaign</span>
            </Link>
          </li>
          <li>
            <Link to="/organization/products" className="nav-link link-dark">
              <ProductIcon />
              <span className="section-name">Products Donated</span>
            </Link>
          </li>
          <li>
            <Link
              to="/organizationProfile"
              className="my-name nav-link link-dark"
            >
              <FaceIconfrom />
              <span className="section-name">Profile</span>
            </Link>
          </li>
          <li>
            <a
              href="#"
              className="nav-link link-dark"
              onClick={() => logout({ returnTo: window.location.origin })}
            >
              <LogOutIconfrom />
              <span className="section-name">Log Out</span>
            </a>
          </li>
        </ul>
      </div>
    </>
  );
};
