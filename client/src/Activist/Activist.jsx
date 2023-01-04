import "./Activist.css";
import { HomePage, AboutPage, ContactsPage, NotFoundPage } from "../pages";
import { Route, Routes } from "react-router-dom";
import React, { useState } from "react";
import { ActivistSideBar } from "./components/sideBar/activistSideBar";
import { ActivistProfileEditPage } from "./pages/profile/activistProfileEdit";
import { ActivistProfilePage } from "./pages/profile/activistProfile";
import { ActivistCampaignsPage } from "./pages/campaigns/activistCampaigns";
import { ActivistCampaignDetailsPage } from "./pages/campaignDetails/activistCampaignDetails";
import { ActivistProductDetailsPage } from "./pages/productDetails/activistProductDetails";
import { ActivistProductsPage } from "./pages/products/activistProducts";
import { ActivistMyProductsPage } from "./pages/myProducts/activistMyProducts";

function Activist() {
  return (
    <div className="ActivistDashBoard">
      <ActivistSideBar />
      <Routes>
        <Route path="/" element={<HomePage />}></Route>
        <Route path="/about" element={<AboutPage />}></Route>
        <Route path="/contacts" element={<ContactsPage />}></Route>
        <Route
          path="/activist/campaigns"
          element={<ActivistCampaignsPage />}
        ></Route>
        <Route
          path="/activist/campaigns/:ID"
          element={<ActivistCampaignDetailsPage />}
        ></Route>
        <Route
          path="/activist/campaigns/:ID/products"
          element={<ActivistProductsPage />}
        ></Route>
        <Route
          path="/activist/campaigns/:ID/products/:ID"
          element={<ActivistProductDetailsPage />}
        ></Route>
        <Route
          path="/activist/myProducts"
          element={<ActivistMyProductsPage />}
        ></Route>
        <Route
          path="/activistProfile"
          element={<ActivistProfilePage />}
        ></Route>
        <Route
          path="/activistProfile/edit"
          element={<ActivistProfileEditPage />}
        ></Route>
        <Route path="*" element={<NotFoundPage />}></Route>
      </Routes>
    </div>
  );
}

export default Activist;
