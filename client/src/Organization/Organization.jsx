import "./Organization.css";
import {
  AboutPage,
  ContactsPage,
  ProductsPage,
  HomePage,
  NotFoundPage,
  ProductDetailsPage,
  ProfileEditPage,
} from "../pages";
import ProfilePage from "../pages/profile/profile.page";
import { Route, Routes } from "react-router-dom";
import React, { useState } from "react";
import { OrganizationSideBar } from "./components/sideBar/organizationSideBar";
import { OrganizationProfileEditPage } from "./pages/profile/organizationProfileEdit";
import { OrganizationProfilePage } from "./pages/profile/organizationProfile";
import { CreateCampaign } from "./pages/createCampaign/createCampaign";
import { OrganizationCampaignsPage } from "./pages/campaigns/organizationCampaigns";
import { OrganizationCampaignDetailsPage } from "./pages/campaignDetails/organizationCampaignDetails";
import { EditCampaign } from "./pages/editCampaign/editCampaign";
import { OrganizationProductDetailsPage } from "./pages/productDetails/organizationProductDetails";
import { OrganizationProductsPage } from "./pages/products/organizationProducts";

function Organization() {
  return (
    <div className="OrganizationDashBoard">
      <OrganizationSideBar />
      <Routes>
        <Route path="/" element={<HomePage />}></Route>
        <Route path="/about" element={<AboutPage />}></Route>
        <Route path="/contacts" element={<ContactsPage />}></Route>
        <Route path="/products" element={<ProductsPage />}></Route>
        <Route
          path="/products/:productID"
          element={<ProductDetailsPage />}
        ></Route>
        <Route
          path="/organization/campaigns"
          element={<OrganizationCampaignsPage />}
        ></Route>
        <Route
          path="/organization/campaign/create"
          element={<CreateCampaign />}
        ></Route>
        <Route
          path="/organization/campaigns/:ID"
          element={<OrganizationCampaignDetailsPage />}
        ></Route>
        <Route
          path="/organization/campaigns/:ID/edit"
          element={<EditCampaign />}
        ></Route>
        <Route
          path="/organization/campaigns/:ID/products"
          element={<OrganizationProductsPage />}
        ></Route>
        <Route
          path="/organization/campaigns/:ID/products/:ID"
          element={<OrganizationProductDetailsPage />}
        ></Route>
        <Route
          path="/organizationProfile"
          element={<OrganizationProfilePage />}
        ></Route>
        <Route
          path="/organizationProfile/edit"
          element={<OrganizationProfileEditPage />}
        ></Route>
        <Route path="*" element={<NotFoundPage />}></Route>
      </Routes>
    </div>
  );
}

export default Organization;
