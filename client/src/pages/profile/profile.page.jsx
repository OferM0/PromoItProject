import React from "react";
import { useAuth0 } from "@auth0/auth0-react";
import "./style.css";

function ProfilePage(props) {
  const { user } = useAuth0();
  //console.log(user);
  return (
    <div className="profilePage">
      <div class="row container d-flex justify-content-center profilePanel">
        <div class="col-xl-6 col-md-12">
          <div class="card user-card-full">
            <div class="row m-l-0 m-r-0">
              <div class="col-sm-4 bg-c-lite-green user-profile">
                <div class="card-block text-center text-white">
                  <div class="m-b-25">
                    <img
                      src={user.picture}
                      alt={user.name}
                      class="img-radius"
                    />
                  </div>
                  <h6 class="profileName">{user.name}</h6>
                  <h6>full stack developer</h6>
                  <i class=" mdi mdi-square-edit-outline feather icon-edit m-t-10 f-16"></i>
                </div>
              </div>
              <div class="col-sm-8">
                <div class="card-block">
                  <h6 class="m-b-20 p-b-5 b-b-default f-w-600">Information</h6>
                  <div class="row">
                    <div class="col-sm-6">
                      <p class="m-b-10 f-w-600">Email</p>
                      <h6 class="text-muted f-w-400">{user.email}</h6>
                    </div>
                    <div class="col-sm-6">
                      <p class="m-b-10 f-w-600">Phone</p>
                      <h6 class="text-muted f-w-400">{user.phone_number}</h6>
                    </div>
                  </div>
                  <h6 class="m-b-20 m-t-40 p-b-5"></h6>
                  <div class="row">
                    <div class="col-sm-6">
                      <p class="m-b-10 f-w-600">Address</p>
                      <h6 class="text-muted f-w-400">{user.address}</h6>
                    </div>
                    <div class="col-sm-6">
                      <p class="m-b-10 f-w-600">Gender</p>
                      <h6 class="text-muted f-w-400">{user.gender}</h6>
                    </div>
                  </div>
                  <ul class="social-link list-unstyled m-t-40 m-b-10">
                    <li>
                      <a
                        href="#!"
                        data-toggle="tooltip"
                        data-placement="bottom"
                        title=""
                        data-original-title="facebook"
                        data-abc="true"
                      >
                        <i
                          class="mdi mdi-facebook feather icon-facebook facebook"
                          aria-hidden="true"
                        ></i>
                      </a>
                    </li>
                    <li>
                      <a
                        href="#!"
                        data-toggle="tooltip"
                        data-placement="bottom"
                        title=""
                        data-original-title="twitter"
                        data-abc="true"
                      >
                        <i
                          class="mdi mdi-twitter feather icon-twitter twitter"
                          aria-hidden="true"
                        ></i>
                      </a>
                    </li>
                    <li>
                      <a
                        href="#!"
                        data-toggle="tooltip"
                        data-placement="bottom"
                        title=""
                        data-original-title="instagram"
                        data-abc="true"
                      >
                        <i
                          class="mdi mdi-instagram feather icon-instagram instagram"
                          aria-hidden="true"
                        ></i>
                      </a>
                    </li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
export default ProfilePage;

/*<div className="profilePage">
   <div className="profilePanel">
      <div className="profileInformation">
        <img src={user.picture} alt={user.name} className="profileImage" />
        <h5>Name: {user.name}</h5>
        <h5>Email: {user.email}</h5>
        <h5>Adress: {user.address}</h5>
        <h5>Gender: {user.gender}</h5>
      </div>
    </div>
</div>*/
