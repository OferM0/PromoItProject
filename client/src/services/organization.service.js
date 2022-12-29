import axios from "axios";
import { startEndpoint } from "./api";

//add organization details
export const addOrganizationDetails = async (details) => {
  try {
    let endpoint = startEndpoint + "/Organizations/Add";
    await axios.post(endpoint, details);
  } catch (e) {
    console.log(e);
  }
};

//get Organizations
export const getOrganizations = async () => {
  try {
    let endpoint = startEndpoint + "/Organizations/Get";
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

//get Organization by id
export const getOrganizationById = async (UserID) => {
  try {
    let endpoint = startEndpoint + `/Organizations/Get/${UserID}`;
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

//update Organization
export const updateOrganizationById = async (UserID, Organization) => {
  try {
    let endpoint = startEndpoint + `/Organizations/Update/${UserID}`;
    await axios.post(endpoint, Organization);
  } catch (e) {
    console.log(e);
  }
};

//delete Organization
export const removeOrganizationById = async (UserID) => {
  try {
    let endpoint = startEndpoint + `/Organizations/Remove/${UserID}`;
    await axios.delete(endpoint);
  } catch (e) {
    console.log(e);
  }
};
