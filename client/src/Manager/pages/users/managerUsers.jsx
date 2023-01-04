import React, { useState, useEffect } from "react";
import "./managerUsers.css";
import { useAuth0 } from "@auth0/auth0-react";
//import { getUserById } from "../../../services/user.service";
import { getUsers } from "../../../services/user.service";

export const ManagerUsersPage = (props) => {
  const { user } = useAuth0();
  const [usersDetails, setUsersDetails] = useState([]);
  const [roleText, setRoleText] = useState("Non-Profit Organization");
  const handleRoleChange = (event) => {
    setRoleText(event.target.value);
  };
  const fetchData = async () => {
    let response = await getUsers();
    if (response.status === 200) {
      setUsersDetails(Object.values(response.data));
      console.log(usersDetails);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div className="productsPage">
      <section>
        <h1 className="managertbl-h1">Users List</h1>
        <div onChange={(event) => handleRoleChange(event)} className="wrapper">
          <input
            type="radio"
            id="Non-Profit-Organization"
            name="select"
            value="Non-Profit Organization"
            checked={roleText === "Non-Profit Organization"}
            onChange={handleRoleChange}
          />
          <label
            htmlFor="Non-Profit-Organization"
            className="option Non-Profit-Organization"
          >
            <div class="dot"></div>
            <span>Organization</span>
          </label>
          <input
            type="radio"
            id="Company"
            name="select"
            value="Company"
            checked={roleText === "Company"}
            onChange={handleRoleChange}
          />
          <label htmlFor="Company" className="option Company">
            <div class="dot"></div>
            <span>Company</span>
          </label>
          <input
            type="radio"
            id="Social-Activist"
            name="select"
            value="Social Activist"
            checked={roleText === "Social Activist"}
            onChange={handleRoleChange}
          />
          <label htmlFor="Social-Activist" className="option Social-Activist">
            <div class="dot"></div>
            <span>Social Activist</span>
          </label>
        </div>
        <div className="tbl-header">
          <table cellPadding="0" cellSpacing="0" border="0">
            <thead>
              <tr>
                <th className="managerth">User ID</th>
                {/* <th className="managerth">Role</th> */}
                <th className="managerth">Name</th>
                <th className="managerth">Address</th>
                <th className="managerth">Phone</th>
                {roleText === "Non-Profit Organization" ? (
                  <th className="managerth">Url</th>
                ) : (
                  <></>
                )}
                <th className="managerth">Complete</th>
              </tr>
            </thead>
          </table>
        </div>
        <div className="tbl-content">
          <table cellPadding="0" cellSpacing="0" border="0">
            <tbody>
              {usersDetails.map((userDetsils) => {
                if (userDetsils.Role === roleText) {
                  //get all users except manager
                  let { UserID, Role, Name, Address, Phone, Url } = userDetsils;
                  return (
                    <tr>
                      <td className="managertd">{UserID}</td>
                      {/* <td className="managertd">{Role}</td> */}
                      <td className="managertd">{Name}</td>
                      <td className="managertd">{Address}</td>
                      <td className="managertd">{Phone}</td>
                      {userDetsils.Role === "Non-Profit Organization" ? (
                        <td className="managertd">{Url}</td>
                      ) : (
                        <></>
                      )}
                      <td className="managertd">
                        <button className="deleteUser">Delete</button>
                      </td>
                    </tr>
                  );
                }
              })}
            </tbody>
          </table>
        </div>
      </section>
    </div>
  );
};
