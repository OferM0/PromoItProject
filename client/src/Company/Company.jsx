import "./Company.css";
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
import { CompanySideBar } from "./components/sideBar/companySideBar";
import { CompanyProfileEditPage } from "./pages/profile/companyProfileEdit";
import { CompanyProfilePage } from "./pages/profile/companyProfile";

function Company() {
  return (
    <div className="CompanyDashBoard">
      <CompanySideBar />
      <Routes>
        <Route path="/" element={<HomePage />}></Route>
        <Route path="/about" element={<AboutPage />}></Route>
        <Route path="/contacts" element={<ContactsPage />}></Route>
        <Route path="/products" element={<ProductsPage />}></Route>
        <Route
          path="/products/:productID"
          element={<ProductDetailsPage />}
        ></Route>
        <Route path="/companyProfile" element={<CompanyProfilePage />}></Route>
        <Route
          path="/companyProfile/edit"
          element={<CompanyProfileEditPage />}
        ></Route>
        <Route path="*" element={<NotFoundPage />}></Route>
      </Routes>
    </div>
  );
}

export default Company;
