import React, { useState } from "react";
import "./style.css";
import "../../../node_modules/bootstrap/dist/css/bootstrap.css";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { addContactDetails } from "../../services/contact.service";

const showToastMessage = () => {
  toast.success("🦄 We recived your message!", {
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
  toast.error("🦄 Please check all fields not empty!", {
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

export const ContactsPage = (props) => {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [message, setMessage] = useState("");

  const handleSubmit = async () => {
    let details = { Name: name, Email: email, Phone: phone, Message: message };
    await addContactDetails(details);
    //console.log(details);
    setName("");
    setEmail("");
    setPhone("");
    setMessage("");
  };

  return (
    <div className="contacts">
      <div className=" information2">
        <h3 className="text-center title2">Get in touch with us</h3>
        <div className="row">
          <div className="col-md-12 field">
            <div className="form-group">
              <input
                type="text"
                className="form-control form-control-contact"
                name="name"
                id="name"
                placeholder="Name"
                onChange={(e) => {
                  setName(e.target.value);
                }}
                value={name}
              />
            </div>
          </div>
          <div className="col-md-12 field">
            <div className="form-group">
              <input
                type="email"
                className="form-control form-control-contact"
                name="email"
                id="email"
                placeholder="Email"
                onChange={(e) => {
                  setEmail(e.target.value);
                }}
                value={email}
              />
            </div>
          </div>
          <div className="col-md-12 field">
            <div className="form-group">
              <input
                type="text"
                className="form-control form-control-contact"
                name="phone"
                id="phone"
                placeholder="Phone"
                onChange={(e) => {
                  setPhone(e.target.value);
                }}
                value={phone}
              />
            </div>
          </div>
          <div className="col-md-12 field">
            <div className="form-group">
              <textarea
                name="message"
                className="form-control form-control-contact"
                id="message"
                cols="30"
                rows="8"
                placeholder="Message"
                onChange={(e) => {
                  setMessage(e.target.value);
                }}
                value={message}
              ></textarea>
            </div>
          </div>
          <button
            className="sendbtn"
            onClick={() => {
              if (name == "" || email == "" || phone == "" || message == "") {
                showWarningMessage();
              } else {
                handleSubmit();
                showToastMessage();
              }
            }}
          >
            Send Message
          </button>
          <ToastContainer />
        </div>
      </div>
    </div>
  );
};
