import React, { useState } from "react";
import "./companyDonateProduct.css";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useAuth0 } from "@auth0/auth0-react";
import { addProduct } from "../../../services/product.service";
import { Link, useNavigate, useLocation } from "react-router-dom";
import ReturnIcon from "@mui/icons-material/KeyboardReturn";
import { width } from "@mui/system";

const showToastMessage = () => {
  toast.success("New Product Created succsufully!", {
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

export const CompanyDonateProduct = () => {
  const location = useLocation();
  const { Id, OrganizationID } = location.state;
  const { user } = useAuth0();
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState(0);
  const [image, setimage] = useState("");
  const [units, setUnits] = useState("");
  const navigate = useNavigate();

  const convertToBase64 = (file) => {
    return new Promise((resolve, reject) => {
      const fileReader = new FileReader();
      fileReader.readAsDataURL(file);
      fileReader.onload = () => {
        resolve(fileReader.result);
      };
      fileReader.onerror = (error) => {
        reject(error);
      };
    });
  };

  const handleChangeImage = async (e) => {
    //console.log(URL.createObjectURL(e.target.files[0]));
    const base64 = await convertToBase64(e.target.files[0]);
    setimage(base64);
    //setimage(URL.createObjectURL(e.target.files[0]));
    //console.log(base64);
  };

  const handleSubmit = async () => {
    let details = {
      Name: name,
      Description: description,
      Price: parseFloat(price),
      //ActivistID: null,
      CompanyID: user.sub,
      OrganizationID: OrganizationID,
      CampaignID: Id,
      Image: image,
      //DonatedByActivist: null,
    };
    for (let i = 0; i < parseInt(units); i++) {
      await addProduct(details);
      //console.log(details);
    }
    setName("");
    setDescription("");
    setUnits("");
    setPrice("");
    //console.log(details);
  };

  return (
    <div className="productEditPage">
      <div className="productEditPanel">
        <h3 className="productEditTitle">Donate Product</h3>
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
            type="number"
            className="form-control"
            placeholder="Price"
            onChange={(e) => {
              setPrice(e.target.value);
            }}
            value={price}
          />
          <input
            type="number"
            className="form-control"
            placeholder="Units"
            onChange={(e) => {
              setUnits(e.target.value);
            }}
            value={units}
          />
          <input
            type="file"
            className="form-control"
            accept="image/*"
            onChange={handleChangeImage}
          ></input>
        </div>
        <div className="returnToCampaign">
          <ReturnIcon className="returnIcon" onClick={() => navigate(-1)} />
        </div>
        <button
          className="btnSaveProductEdit"
          onClick={() => {
            if (
              name === "" ||
              description === "" ||
              units === "" ||
              units === 0 ||
              price === "" ||
              price === 0 ||
              image === ""
            ) {
              showWarningMessage();
            } else {
              handleSubmit();
              showToastMessage();
            }
          }}
        >
          Donate
        </button>
        <ToastContainer />
      </div>
    </div>
  );
};
