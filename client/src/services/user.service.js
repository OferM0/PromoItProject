import axios from "axios";
import { startEndpoint } from "./api";

//add user details
export const addUserDetails = async (details) => {
  try {
    let endpoint = startEndpoint + "/Users/Add";
    await axios.post(endpoint, details);
  } catch (e) {
    console.log(e);
  }
};

//get Users
export const getUsers = async () => {
  try {
    let endpoint = startEndpoint + "/Users/Get";
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

//get User by id
export const getUserById = async (UserID) => {
  try {
    let endpoint = startEndpoint + `/Users/Get/${UserID}`;
    let response = await axios.get(endpoint);
    return response.data;
  } catch (e) {
    console.log(e);
  }
};

//update User
export const updateUserById = async (UserID, User) => {
  try {
    let endpoint = startEndpoint + `/Users/Update/${UserID}`;
    await axios.post(endpoint, User);
  } catch (e) {
    console.log(e);
  }
};

//delete User
export const removeUserById = async (UserID) => {
  try {
    let endpoint = startEndpoint + `/Users/Remove/${UserID}`;
    await axios.delete(endpoint);
  } catch (e) {
    console.log(e);
  }
};
