import React from "react";
import { useAuth0 } from "@auth0/auth0-react";
import "./style.css";
import logo from "../../images/logo4.png";

function UnAuthUser(props) {
  const { loginWithRedirect } = useAuth0();

  return (
    <div className="instruction">
      <img src={logo} className="logo"></img>
      <button
        className="LogIn"
        onClick={async () => await loginWithRedirect("http://localhost:3000")}
      >
        Log In
      </button>
    </div>
  );
}

export default UnAuthUser;
