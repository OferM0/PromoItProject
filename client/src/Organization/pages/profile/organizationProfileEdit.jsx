import React, { useState, useContext } from "react";
import "./organizationEditProfile.css";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { updateUserById } from "../../../services/user.service";
import { useAuth0 } from "@auth0/auth0-react";
import { Link } from "react-router-dom";
import ReturnIcon from "@mui/icons-material/KeyboardReturn";
import { UserDetailsContext } from "../../../context/userDetails.context";

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
  toast.error("Please check all fields are valid!", {
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

function isValidFullName(name) {
  // let nameRegex = /^[a-zA-Z]+\s[a-zA-Z]+$/;
  // if (nameRegex.test(name) === false) {
  //   return false;
  // }

  if (name.length < 2) {
    return false;
  }

  // let nameArray = name.split(" ");
  // if (nameArray.length < 2) {
  //   return false;
  // }
  // if (nameArray[0].length < 2 || nameArray[1].length < 2) {
  //   return false;
  // }
  return true;
}

function validateURL(textval) {
  var urlregex = new RegExp(
    "^(http|https|ftp)://([a-zA-Z0-9.-]+(:[a-zA-Z0-9.&amp;%$-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]).(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0).(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0).(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|([a-zA-Z0-9-]+.)*[a-zA-Z0-9-]+.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(:[0-9]+)*(/($|[a-zA-Z0-9.,?'\\+&amp;%$#=~_-]+))*$"
  );
  return urlregex.test(textval);
}

export const OrganizationProfileEditPage = () => {
  const { user } = useAuth0();
  const { userDetails } = useContext(UserDetailsContext);
  const [name, setName] = useState(userDetails.Name);
  const [address, setAddress] = useState(userDetails.Address);
  const [phone, setPhone] = useState(userDetails.Phone);
  const [url, setUrl] = useState(userDetails.Url);

  const handleSubmit = async () => {
    let details = {
      Name: name,
      Address: address,
      Phone: phone,
      Url: url,
    };
    userDetails.Phone = phone;
    userDetails.Name = name;
    userDetails.Address = address;
    userDetails.Url = url;

    await updateUserById(user.sub, details);
    setName(userDetails.Name);
    setAddress(userDetails.Address);
    setPhone(userDetails.Phone);
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
            maxLength="30"
            onChange={(e) => {
              setName(e.target.value);
            }}
            value={name}
          />
          <input
            type="text"
            className="form-control"
            maxLength="40"
            placeholder="Address"
            onChange={(e) => {
              setAddress(e.target.value);
            }}
            value={address}
          />
          <input
            type="text"
            minLength="10"
            maxLength="10"
            className="form-control"
            placeholder="Phone"
            onChange={(e) => {
              setPhone(e.target.value);
            }}
            value={phone}
          />
          <input
            type="text"
            className="form-control"
            placeholder="Site's Url"
            onChange={(e) => {
              setUrl(e.target.value);
            }}
            value={url}
          />
        </div>
        <Link to="/organizationProfile" className="returnToProfile">
          <ReturnIcon className="returnIcon" />
        </Link>
        <button
          className="btnSaveProfileEdit"
          onClick={() => {
            if (
              name == "" ||
              address == "" ||
              address.length < 6 ||
              phone == "" ||
              url == "" ||
              /*url.length < 10 ||*/
              /^0\d{9}$/.test(phone) === false ||
              isValidFullName(name) === false ||
              validateURL(url) === false
            ) {
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
