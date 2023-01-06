import React, { useState, useEffect } from "react";
import "./registration.css";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useAuth0 } from "@auth0/auth0-react";
import { addUserDetails } from "../../services/user.service";
//---------------------------------------------
import { getRolesById } from "../../services/roles.service";
//---------------------------------------------

const showToastMessage = () => {
  toast.success("We recived your registration, Please Login again!", {
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

const showOrganizationInfo = () => {
  toast.info(
    "You can sign in as Non-Profit Organization represantive and start promoting campaigns!",
    {
      position: "top-left",
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "light",
    }
  );
};

const showActivistInfo = () => {
  toast.info(
    "You can sign in as social activist(you must be twitter's user) and start promoting campaigns by this you can promote our society, also earn money and purchase product you like!",
    {
      position: "top-left",
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "light",
    }
  );
};

const showCompanyInfo = () => {
  toast.info(
    "You can sign in buisness company represantive and start donating products!",
    {
      position: "top-left",
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "light",
    }
  );
};
export const RegistrationPage = () => {
  const [roleText, setRoleText] = useState("Non-Profit Organization");
  const [twitterHandle, setTwitterHandle] = useState("");
  const [name, setName] = useState("");
  const [address, setAddress] = useState("");
  const [phone, setPhone] = useState("");
  const [url, setUrl] = useState("");
  const { user, logout } = useAuth0();
  const sleep = (ms) => {
    return new Promise((resolve) => setTimeout(resolve, ms));
  };
  //-------------------------------------------
  // const [role, setRole] = useState("");
  // const handleRole = async () => {
  //   let userId = user.sub;
  //   let data = await getRolesById(userId);
  //   //console.log(userId);
  //   //console.log(role[0].name);
  //   setRole(data[0].name);
  // };

  // useEffect(() => {
  //   handleRole();
  // }, []);
  //-------------------------------------------------
  const handleRoleChange = (event) => {
    setRoleText(event.target.value);
  };

  const handleSubmit = async () => {
    if (roleText === "Non-Profit Organization") {
      let details = {
        UserID: user.sub,
        Role: roleText,
        Name: name,
        Address: address,
        Phone: phone,
        Url: url,
      };
      await addUserDetails(details);
      setName("");
      setAddress("");
      setPhone("");
      setUrl("");
      //console.log(details);
    } else if (roleText === "Social Activist") {
      let details = {
        UserID: user.sub,
        Role: roleText,
        Name: name,
        Address: address,
        Phone: phone,
        Status: 0,
        TwitterHandle: twitterHandle,
      };
      await addUserDetails(details);
      setName("");
      setAddress("");
      setPhone("");
      setTwitterHandle("");
      //console.log(details);
    } else if (roleText === "Company") {
      let details = {
        UserID: user.sub,
        Role: roleText,
        Name: name,
        Address: address,
        Phone: phone,
        TwitterHandle: twitterHandle,
      };
      await addUserDetails(details);
      setName("");
      setAddress("");
      setPhone("");
      setTwitterHandle("");
      //console.log(details);
    }
  };

  return (
    // <>
    //   {role !== "Company" &&
    //   role !== "Non-Profit Organization" &&
    //   role !== "Social Activist" &&
    //   role !== "Manager" ? (
    //     <>
    <div className="registerPage">
      <div className="container register">
        <div className="row">
          <div className="col-md-3 register-left">
            <h3>Welcome</h3>
            <p>
              To promot a better society you can sign up as
              <br />
            </p>

            <div
              onChange={(event) => handleRoleChange(event)}
              className="radioContainer1"
            >
              <input
                type="radio"
                id="Non-Profit Organization"
                name="fav_language"
                value="Non-Profit Organization"
                checked={roleText === "Non-Profit Organization"}
                onChange={(handleRoleChange, showOrganizationInfo)}
              />
              <label htmlFor="html">Organization</label>
              <br />
              <input
                type="radio"
                id="Company"
                name="fav_language"
                value="Company"
                checked={roleText === "Company"}
                onChange={(handleRoleChange, showCompanyInfo)}
              />
              <label htmlFor="css">Company</label>
              <br />
              <input
                type="radio"
                id="Social Activist"
                name="fav_language"
                value="Social Activist"
                checked={roleText === "Social Activist"}
                onChange={(handleRoleChange, showActivistInfo)}
              />
              <label htmlFor="javascript">Social Activist</label>
            </div>
          </div>

          <div className="col-md-9 register-right">
            <div className="tab-content" id="myTabContent">
              <div
                className="tab-pane fade show active"
                id="home"
                role="tabpanel"
                aria-labelledby="home-tab"
              >
                <h3 className="register-heading">Apply as a {roleText}</h3>
                <div className="row register-form">
                  <div className="col-md-6">
                    {roleText !== "Non-Profit Organization" ? (
                      <input
                        type="text"
                        className="form-control"
                        placeholder="Twitter Handle"
                        maxLength="15"
                        onChange={(e) => {
                          setTwitterHandle(e.target.value);
                        }}
                        value={twitterHandle}
                      />
                    ) : (
                      <></>
                    )}
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
                      placeholder="Address"
                      maxLength="40"
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
                    {roleText === "Non-Profit Organization" ? (
                      <input
                        type="text"
                        className="form-control"
                        placeholder="Site's Url"
                        onChange={(e) => {
                          setUrl(e.target.value);
                        }}
                        value={url}
                      />
                    ) : (
                      <></>
                    )}
                  </div>
                  <button
                    className="btn-Register"
                    onClick={async () => {
                      if (
                        name == "" ||
                        address == "" ||
                        phone == "" ||
                        (url == "" && roleText === "Non-Profit Organization") ||
                        (twitterHandle == "" &&
                          roleText !== "Non-Profit Organization")
                      ) {
                        showWarningMessage();
                      } else {
                        handleSubmit();
                        showToastMessage();
                        await sleep(2000); //we want the user to be notified to the message
                        logout({ returnTo: window.location.origin });
                      }
                    }}
                  >
                    Register
                  </button>
                  <ToastContainer />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    //     </>
    //   ) : (
    //     <>
    //       <h1>not found</h1>
    //       <button onClick={() => logout({ returnTo: window.location.origin })}>
    //         Return
    //       </button>
    //     </>
    //   )}
    // </>
  );
};
