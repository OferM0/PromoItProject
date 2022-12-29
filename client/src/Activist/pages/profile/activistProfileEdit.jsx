import React, { useState } from "react";
import "./activistEditProfile.css";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { updateUserById } from "../../../services/user.service";
import { useAuth0 } from "@auth0/auth0-react";
import { Link } from "react-router-dom";
import ReturnIcon from "@mui/icons-material/KeyboardReturn";

const showToastMessage = () => {
  toast.success("Edited succsufully!", {
    position: "top-right",
    autoClose: 3000,
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
    theme: "light",
  });
};

const showWarningMessage = () => {
  toast.error("Please check all fields not empty!", {
    position: "top-right",
    autoClose: 3000,
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
    theme: "light",
  });
};

export const ActivistProfileEditPage = () => {
  const { user } = useAuth0();
  const [name, setName] = useState("");
  const [address, setAddress] = useState("");
  const [phone, setPhone] = useState("");

  const handleSubmit = async () => {
    let details = {
      Name: name,
      Address: address,
      Phone: phone,
    };
    await updateUserById(user.sub, details);
    setName("");
    setAddress("");
    setPhone("");
    //console.log(details);
  };

  return (
    <div className="profileEditPage">
      <div className="profileEditPanel">
        <h3 className="profileEditTitle">Edit Your Profile</h3>
        <div className="col-md-6">
          <input
            type="text"
            className="form-control"
            placeholder="Full Name"
            onChange={(e) => {
              setName(e.target.value);
            }}
            value={name}
          />
          <input
            type="text"
            className="form-control"
            placeholder="Address"
            onChange={(e) => {
              setAddress(e.target.value);
            }}
            value={address}
          />
          <input
            type="text"
            minLength="8"
            maxLength="12"
            className="form-control"
            placeholder="Phone"
            onChange={(e) => {
              setPhone(e.target.value);
            }}
            value={phone}
          />
        </div>
        <Link to="/activistProfile" className="returnToProfile">
          <ReturnIcon className="returnIcon" />
        </Link>
        <button
          className="btnSaveProfileEdit"
          onClick={() => {
            if (name == "" || address == "" || phone == "") {
              showWarningMessage();
            } else {
              handleSubmit();
              showToastMessage();
            }
          }}
        >
          Save
        </button>
        <ToastContainer />
      </div>
    </div>
  );
};