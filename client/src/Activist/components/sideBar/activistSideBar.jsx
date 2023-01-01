import React from "react";
import "./activistSideBar.css";
import HomeIcon from "@mui/icons-material/Home";
import InfoIcon from "@mui/icons-material/Info";
import CallIcon from "@mui/icons-material/Call";
import CampaignIcon from "@mui/icons-material/Campaign";
import ProductIcon from "@mui/icons-material/LocalMall";
import FaceIconfrom from "@mui/icons-material/Face";
import LogOutIconfrom from "@mui/icons-material/Logout";
import { Link } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

export const ActivistSideBar = (props) => {
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
            <Link to="/about" className="nav-link link-dark aa">
              <InfoIcon />
              <span className="section-name">About</span>
            </Link>
          </li>
          <li>
            <Link to="/contacts" className="nav-link link-dark aa">
              <CallIcon />
              <span className="section-name">Contact Us</span>
            </Link>
          </li>
          <li>
            <Link to="/activist/campaigns" className="nav-link link-dark aa">
              <CampaignIcon />
              <span className="section-name">Campaigns</span>
            </Link>
          </li>
          <li>
            <Link to="/activist/products" className="nav-link link-dark aa">
              <ProductIcon />
              <span className="section-name">My Products</span>
            </Link>
          </li>
          <li>
            <Link
              to="/activistProfile"
              className="my-name nav-link link-dark aa"
            >
              <FaceIconfrom />
              <span className="section-name">Profile</span>
            </Link>
          </li>
          <li>
            <a
              href="#"
              className="nav-link link-dark aa"
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
