import "./Activist.css";
import {
  AboutPage,
  ContactsPage,
  ProductsPage,
  HomePage,
  NotFoundPage,
  ProductDetailsPage,
} from "../pages";
import { Route, Routes } from "react-router-dom";
import React, { useState } from "react";
import { ActivistSideBar } from "./components/sideBar/activistSideBar";
import { ActivistProfileEditPage } from "./pages/profile/activistProfileEdit";
import { ActivistProfilePage } from "./pages/profile/activistProfile";

function Activist() {
  return (
    <div className="ActivistDashBoard">
      <ActivistSideBar />
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
