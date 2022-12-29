import React, { useContext } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import "./companyProfile.css";
import EditIcon from "@mui/icons-material/Edit";
import { Link } from "react-router-dom";
import { UserDetailsContext } from "../../../context/userDetails.context";

export const CompanyProfilePage = (props) => {
  const { user } = useAuth0();
  const { userDetails } = useContext(UserDetailsContext);
  //console.log(userDetails);
  return (
    <div className="profilePage">
      <div className="row container d-flex justify-content-center profilePanel">
        <div className="col-xl-6 col-md-12">
          <div className="card user-card-full">
            <div className="row m-l-0 m-r-0">
              <div className="col-sm-4 bg-c-lite-green user-profile">
                <div className="card-block text-center text-white">
                  <div className="m-b-25">
                    <img
                      src={user.picture}
                      alt={user.name}
                      className="img-radius"
                    />
                  </div>
                  <h6 className="profileName">{userDetails.Name}</h6>
                  <h6>{userDetails.Role}</h6>
                  <div className="moneyStatus">Money Donated: 0 $</div>
                  <Link to="/companyProfile/edit" className="editBtn">
                    <span>Edit Profile</span>
                    <EditIcon />
                  </Link>
                </div>
              </div>
              <div className="col-sm-8">
                <div className="card-block">
                  <h6 className="m-b-20 p-b-5 b-b-default f-w-600">
                    Information
                  </h6>
                  <div className="row">
                    <div className="col-sm-6">
                      <p className="m-b-10 f-w-600">User ID</p>
                      <h6 className="text-muted f-w-400">{user.sub}</h6>
                    </div>
                    <div className="col-sm-6">
                      <p className="m-b-10 f-w-600">Email</p>
                      <h6 className="text-muted f-w-400">{user.email}</h6>
                    </div>
                  </div>
                  <h6 className="m-b-20 m-t-40 p-b-5"></h6>
                  <div className="row">
                    <div className="col-sm-6">
                      <p className="m-b-10 f-w-600">Address</p>
                      <h6 className="text-muted f-w-400">
                        {userDetails.Address}
                      </h6>
                    </div>
                    <div className="col-sm-6">
                      <p className="m-b-10 f-w-600">Phone</p>
                      <h6 className="text-muted f-w-400">
                        {userDetails.Phone}
                      </h6>
                    </div>
                  </div>
                  <h6 className="m-b-20 m-t-40 p-b-5"></h6>
                  <div className="row">
                    <div className="col-sm-6">
                      <p className="m-b-10 f-w-600">Products Donated</p>
                      <h6 className="text-muted f-w-400"></h6>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
