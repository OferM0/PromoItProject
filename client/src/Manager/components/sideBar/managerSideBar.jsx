import React from "react";
import "./managerSideBar.css";
import UserIcon from "@mui/icons-material/SupervisedUserCircle";
import CampaignIcon from "@mui/icons-material/Campaign";
import TweetIcon from "@mui/icons-material/Twitter";
import FaceIconfrom from "@mui/icons-material/Face";
import LogOutIconfrom from "@mui/icons-material/Logout";
import { Link } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

export const ManagerSideBar = (props) => {
  const { logout } = useAuth0();

  return (
    <>
      <div className="container2 p-3">
        <ul className="nav nav-pills flex-column">
          <li className="nav-item">
            <Link to="/" className="nav-link link-dark aa">
              <UserIcon />
              <span className="section-name">Users</span>
            </Link>
          </li>
          <li>
            <Link to="/about" className="nav-link link-dark aa">
              <CampaignIcon />
              <span className="section-name">Campaigns</span>
            </Link>
          </li>
          <li>
            <Link to="/contacts" className="nav-link link-dark aa">
              <TweetIcon />
              <span className="section-name">Tweets</span>
            </Link>
          </li>

          <li>
            <Link to="/profile" className="my-name nav-link link-dark aa">
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
