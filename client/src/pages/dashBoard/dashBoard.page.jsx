import "./dashBoard.css";
import React, { useState, useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { getRolesById } from "../../services/roles.service";
import {
  AboutPage,
  ContactsPage,
  ProductsPage,
  HomePage,
  NotFoundPage,
  ProductDetailsPage,
} from "../../pages";
import ProfilePage from "../../pages/profile/profile.page";
import { Route, Routes } from "react-router-dom";
import { SideBar } from "../../components/sideBar/sidebar.component";

export const Dashboard = () => {
  const { logout, user } = useAuth0();
  const [role, setRole] = useState("");
  const handleRoles = async () => {
    let userId = user.sub;
    let data = await getRolesById(userId);
    //console.log(userId);
    //console.log(role[0].name);
    setRole(data[0].name);
  };

  useEffect(() => {
    handleRoles();
  }, []);

  return (
    <>
      {role === "Company" ? (
        <>
          <SideBar></SideBar>
          <Routes>
            <Route path="/" element={<HomePage />}></Route>
            <Route path="/about" element={<AboutPage />}></Route>
            <Route path="/contacts" element={<ContactsPage />}></Route>
            <Route path="/products" element={<ProductsPage />}></Route>
            <Route
              path="/products/:productID"
              element={<ProductDetailsPage />}
            ></Route>
            <Route path="/profile" element={<ProfilePage />}></Route>
            <Route path="*" element={<NotFoundPage />}></Route>
          </Routes>
        </>
      ) : (
        <>
          <h1>not found</h1>
          <button onClick={() => logout({ returnTo: window.location.origin })}>
            Return
          </button>
        </>
      )}
    </>
  );
};
