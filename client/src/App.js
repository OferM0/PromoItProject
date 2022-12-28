import "./App.css";
import {
  AboutPage,
  ContactsPage,
  ProductsPage,
  HomePage,
  NotFoundPage,
  ProductDetailsPage,
  Dashboard,
  RegistrationPage,
  ProfileEditPage,
} from "./pages";
import ProfilePage from "./pages/profile/profile.page";
import { Route, Routes } from "react-router-dom";
import React, { useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import UnAuthUser from "./pages/unAuthUser/unAuthUser.page";
import { SideBar } from "./components/sideBar/sidebar.component";
//import { UserDetailsContext } from "./context/userDetails.context";
//import { getUserById } from "./services/user.service";

function App() {
  const { isAuthenticated, isLoading, user } = useAuth0();
  //const [userDetails, setUserDetails] = useState({});

  // const fetchData = async () => {
  //   let response = await getUserById(user.sub);
  //   if (response.status === 200) {
  //     setUserDetails(response.data);
  //     //console.log(userDetails);
  //   }
  // };

  // useEffect(() => {
  //   fetchData();
  // }, []);

  if (!isLoading) {
    if (isAuthenticated) {
      return (
        //<UserDetailsContext.Provider value={getUserById(user.sub).data}>
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
            <Route path="/register" element={<RegistrationPage />}></Route>
            <Route path="/profile/edit" element={<ProfileEditPage />}></Route>
            <Route path="*" element={<NotFoundPage />}></Route>
            {/*<Route path="/" element={<Dashboard />}></Route>*/}
          </Routes>
        </div>
        //</UserDetailsContext.Provider>
      );
    } else {
      return <UnAuthUser />;
    }
  } else {
    <h1>loading</h1>;
  }
}

export default App;
