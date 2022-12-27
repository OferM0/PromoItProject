import "./App.css";
import {
  AboutPage,
  ContactsPage,
  ProductsPage,
  HomePage,
  NotFoundPage,
  ProductDetailsPage,
  Dashboard,
} from "./pages";
import ProfilePage from "./pages/profile/profile.page";
import { Route, Routes } from "react-router-dom";
import React from "react";
import { useAuth0 } from "@auth0/auth0-react";
import UnAuthUser from "./pages/unAuthUser/unAuthUser.page";
import { SideBar } from "./components/sideBar/sidebar.component";

function App() {
  const { isAuthenticated, isLoading } = useAuth0();

  if (!isLoading) {
    if (isAuthenticated) {
      return (
        <div className="app">
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
            {/*<Route path="/" element={<Dashboard />}></Route>*/}
          </Routes>
        </div>
      );
    } else {
      return <UnAuthUser />;
    }
  } else {
    <h1>loading</h1>;
  }
}

export default App;
