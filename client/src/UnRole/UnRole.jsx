import "./unRole.css";
import { AboutPage, ContactsPage, HomePage, NotFoundPage } from "../pages";
import { Route, Routes } from "react-router-dom";
import React, { useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import UnAuthUser from "../pages/unAuthUser/unAuthUser.page";
import { UnRoleSideBar } from "./unRoleSideBar";
import { RegistrationPage } from "./registration/registration.page";
function UnRole() {
  return (
    //<UserDetailsContext.Provider value={getUserById(user.sub).data}>
    <div className="UnRoleBoard">
      <UnRoleSideBar />
      <Routes>
        <Route path="/" element={<HomePage />}></Route>
        <Route path="/about" element={<AboutPage />}></Route>
        <Route path="/contacts" element={<ContactsPage />}></Route>
        <Route path="/register" element={<RegistrationPage />}></Route>
        <Route path="*" element={<NotFoundPage />}></Route>
        {/*<Route path="/" element={<Dashboard />}></Route>*/}
      </Routes>
    </div>
    //</UserDetailsContext.Provider>
  );
}

export default UnRole;
