import "./Manager.css";
import { HomePage, AboutPage, ContactsPage, NotFoundPage } from "../pages";
import { Route, Routes } from "react-router-dom";
import React, { useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import UnAuthUser from "../pages/unAuthUser/unAuthUser.page";
import { ManagerSideBar } from "./components/sideBar/managerSideBar";
import { ManagerUsersPage } from "./pages/users/managerUsers";
import { ManagerCampaignsPage } from "./pages/campaigns/managerCampaigns";
import { ManagerCampaignDetailsPage } from "./pages/campaignDetails/managerCampaignDetails";
import { ManagerProductDetailsPage } from "./pages/productDetails/managerProductDetails";
import { ManagerProductsPage } from "./pages/products/managerProducts";
import { ManagerTweetsPage } from "./pages/tweets/managerTweets";

function Manager() {
  return (
    //<UserDetailsContext.Provider value={getUserById(user.sub).data}>
    <div className="ManagerDashBoard">
      <ManagerSideBar />
      <Routes>
        {/* <Route path="/" element={<HomePage />}></Route>
        <Route path="/about" element={<AboutPage />}></Route>
        <Route path="/contacts" element={<ContactsPage />}></Route> */}
        <Route path="/" element={<ManagerUsersPage />}></Route>{" "}
        {/*manager/users*/}
        <Route
          path="/manager/campaigns"
          element={<ManagerCampaignsPage />}
        ></Route>
        <Route
          path="/manager/campaigns/:ID"
          element={<ManagerCampaignDetailsPage />}
        ></Route>
        <Route
          path="/manager/campaigns/:ID/products"
          element={<ManagerProductsPage />}
        ></Route>
        <Route
          path="/manager/campaigns/:ID/products/:ID"
          element={<ManagerProductDetailsPage />}
        ></Route>
        <Route path="/manager/tweets" element={<ManagerTweetsPage />}></Route>
        <Route path="*" element={<NotFoundPage />}></Route>
      </Routes>
    </div>
    //</UserDetailsContext.Provider>
  );
}

export default Manager;
