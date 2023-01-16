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

function isValidName(name) {
  if (name.length < 3) {
    return false;
  }
  return true;
}

function validateURL(textval) {
  var urlregex = new RegExp(
    "^(http|https|ftp)://([a-zA-Z0-9.-]+(:[a-zA-Z0-9.&amp;%$-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]).(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0).(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0).(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|([a-zA-Z0-9-]+.)*[a-zA-Z0-9-]+.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(:[0-9]+)*(/($|[a-zA-Z0-9.,?'\\+&amp;%$#=~_-]+))*$"
  );
  return urlregex.test(textval);
}

// function validateHashtag(textval) {
//   var hashtagRegex = /^[a-zA-Z0-9]+$/;
//   return hashtagRegex.test(textval);
// }

export const CreateCampaign = () => {
  const { user } = useAuth0();
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [url, setUrl] = useState("");
  const [hashtag, setHashtag] = useState("");

  const handleSubmit = async () => {
    let date = new Date();
    let day = date.getDate();
    let month = date.getMonth();
    // set the day to the current month /----------------replace day and month because when enter normal to sql it's replaced
    //date.setDate(month + 1);
    // set the month to the current day
    //date.setMonth(day - 1);

    let details = {
      OrganizationID: user.sub,
      Name: name,
      Description: description,
      Url: url,
      Hashtag: hashtag,
      Active: true,
      CreateDate: date.toISOString().slice(0, 10),
    };
    //console.log(details);
    await addCampaignDetails(details);
    setName("");
    setDescription("");
    setHashtag("");
    setUrl("");
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
        <button
          className="btnSaveCampaignEdit"
          onClick={() => {
            if (
              name == "" ||
              description == "" ||
              hashtag == "" ||
              url == "" ||
              description.length < 10 ||
              hashtag.length < 2 ||
              isValidName(name) === false ||
              /*url.length < 10 ||*/
              validateURL(url) === false
            ) {
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
