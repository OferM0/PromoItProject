import React, { useState, useEffect, useContext } from "react";
import "./editCampaign.css";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { Link, useLocation, useNavigate } from "react-router-dom";
import ReturnIcon from "@mui/icons-material/KeyboardReturn";
import { updateCampaignById } from "../../../services/campaign.service";
import { getCampaignById } from "../../../services/campaign.service";

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

export const EditCampaign = () => {
  const [campaign, setCampaign] = useState([]);
  const location = useLocation();
  const { Id } = location.state;
  const fetchData = async () => {
    let response = await getCampaignById(Id);
    if (response.status === 200) {
      setCampaign(response.data);
    }
  };
  //let { Name, cDescription, cHashtag, cUrl } = campaign;

  const [name, setName] = useState(""); // useState(campaign.Name) not working should work
  const [description, setDescription] = useState(""); //useState(campaign.Description) not working should work
  const [url, setUrl] = useState(""); // useState(campaign.Url) not working should work
  const [hashtag, setHashtag] = useState(""); //useState(campaign.Hashtag) not working should work
  const navigate = useNavigate();

  const handleSubmit = async () => {
    let details = {
      Name: name,
      Description: description,
      Url: url,
      Hashtag: hashtag,
    };
    await updateCampaignById(Id, details);
    setName(name);
    setDescription(description);
    setHashtag(url);
    setUrl(hashtag);
    //console.log(details);
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div className="campaignEditPage">
      <div
        className="campaignEditPanel"
        onMouseEnter={() => {
          // --------------------------some dirty code for it to work (valued imported to input fields)------------
          setName(campaign.Name);
          setDescription(campaign.Description);
          setHashtag(campaign.Hashtag);
          setUrl(campaign.Url);
        }}
      >
        <h3 className="campaignEditTitle">Edit Campaign</h3>
        <div className="col-md-6">
          <input
            type="text"
            className="form-control"
            placeholder="Name"
            maxLength="40"
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
            maxLength="20"
            className="form-control"
            placeholder="Hashtag"
            onChange={(e) => {
              setHashtag(e.target.value);
            }}
            value={hashtag}
          />
        </div>
        <div className="returnToCampaign">
          <ReturnIcon className="returnIcon" onClick={() => navigate(-1)} />
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
          Save Changes
        </button>
        <ToastContainer />
      </div>
    </div>
  );
};
