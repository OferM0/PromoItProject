import React, { useState } from "react";
import "./createCampaign.css";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useAuth0 } from "@auth0/auth0-react";
import { addCampaignDetails } from "../../../services/campaign.service";
//import { Link } from "react-router-dom";
//import ReturnIcon from "@mui/icons-material/KeyboardReturn";

const showToastMessage = () => {
  toast.success("New Campaign Created succsufully!", {
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

export const CreateCampaign = () => {
  const { user } = useAuth0();
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [url, setUrl] = useState("");
  const [hashtag, setHashtag] = useState("");

  const handleSubmit = async () => {
    let details = {
      OrganizationID: user.sub,
      Name: name,
      Description: description,
      Url: url,
      Hashtag: hashtag,
    };
    await addCampaignDetails(details);
    setName("");
    setDescription("");
    setHashtag("");
    setUrl("");
    //console.log(details);
  };

  return (
    <div className="campaignEditPage">
      <div className="campaignEditPanel">
        <h3 className="campaignEditTitle">Create New Campaign</h3>
        <div className="col-md-6">
          <input
            type="text"
            className="form-control"
            placeholder="Name"
            onChange={(e) => {
              setName(e.target.value);
            }}
            value={name}
          />
          <textarea
            type="text"
            className="form-control"
            placeholder="Description"
            maxLength="300"
            onChange={(e) => {
              setDescription(e.target.value);
            }}
            value={description}
          />
          <input
            type="text"
            className="form-control"
            placeholder="Campaign's Url"
            onChange={(e) => {
              setUrl(e.target.value);
            }}
            value={url}
          />
          <input
            type="text"
            maxLength="30"
            className="form-control"
            placeholder="Hashtag"
            onChange={(e) => {
              setHashtag(e.target.value);
            }}
            value={hashtag}
          />
        </div>
        <button
          className="btnSaveCampaignEdit"
          onClick={() => {
            if (name == "" || description == "" || hashtag == "" || url == "") {
              showWarningMessage();
            } else {
              handleSubmit();
              showToastMessage();
            }
          }}
        >
          Create
        </button>
        <ToastContainer />
      </div>
    </div>
  );
};
