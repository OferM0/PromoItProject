import "./Manager.css";
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
import { useAuth0 } from "@auth0/auth0-react";
import UnAuthUser from "../pages/unAuthUser/unAuthUser.page";
import { ManagerSideBar } from "./components/sideBar/managerSideBar";

function Manager() {
  return (
    //<UserDetailsContext.Provider value={getUserById(user.sub).data}>
    <div className="ManagerDashBoard">
      <ManagerSideBar />
      <Routes>
        <Route path="/" element={<HomePage />}></Route>
        <Route path="/about" element={<AboutPage />}></Route>
        <Route path="/contacts" element={<ContactsPage />}></Route>
        <Route path="/products" element={<ProductsPage />}></Route>
        <Route
          path="/products/:productID"
          element={<ProductDetailsPage />}
        ></Route>
        <Route path="*" element={<NotFoundPage />}></Route>
        {/*<Route path="/" element={<Dashboard />}></Route>*/}
      </Routes>
    </div>
    //</UserDetailsContext.Provider>
  );
}

export default Manager;
