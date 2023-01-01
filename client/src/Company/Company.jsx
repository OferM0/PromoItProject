import "./Company.css";
import { AboutPage, ContactsPage, HomePage, NotFoundPage } from "../pages";
import { Route, Routes } from "react-router-dom";
import React, { useState } from "react";
import { CompanySideBar } from "./components/sideBar/companySideBar";
import { CompanyProfileEditPage } from "./pages/profile/companyProfileEdit";
import { CompanyProfilePage } from "./pages/profile/companyProfile";
import { CompanyCampaignsPage } from "./pages/campaigns/companyCampaigns";
import { CompanyCampaignDetailsPage } from "./pages/campaignDetails/companyCampaignDetails";
import { CompanyDonateProduct } from "./pages/donateProduct/companyDonateProduct";
import { CompanyProductsPage } from "./pages/products/companyProducts";
import { CompanyProductDetailsPage } from "./pages/productDetails/companyProductDetails";

function Company() {
  return (
    <div className="CompanyDashBoard">
      <CompanySideBar />
      <Routes>
        <Route path="/" element={<HomePage />}></Route>
        <Route path="/about" element={<AboutPage />}></Route>
        <Route path="/contacts" element={<ContactsPage />}></Route>
        <Route
          path="/company/campaigns"
          element={<CompanyCampaignsPage />}
        ></Route>
        <Route
          path="/company/campaigns/:ID"
          element={<CompanyCampaignDetailsPage />}
        ></Route>
        <Route
          path="/company/campaigns/:ID/donate"
          element={<CompanyDonateProduct />}
        ></Route>
        <Route
          path="/company/products"
          element={<CompanyProductsPage />}
        ></Route>
        <Route
          path="/company/products/:ID"
          element={<CompanyProductDetailsPage />}
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
