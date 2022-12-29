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
