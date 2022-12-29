import axios from "axios";
import { startEndpoint } from "./api";

//add company details
export const addCompanyDetails = async (details) => {
  try {
    let endpoint = startEndpoint + "/Companies/Add";
    await axios.post(endpoint, details);
  } catch (e) {
    console.log(e);
  }
};

//get Companies
export const getCompanies = async () => {
  try {
    let endpoint = startEndpoint + "/Companies/Get";
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

//get Company by id
export const getCompanyById = async (UserID) => {
  try {
    let endpoint = startEndpoint + `/Companies/Get/${UserID}`;
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

//update Company
export const updateCompanyById = async (UserID, Company) => {
  try {
    let endpoint = startEndpoint + `/Companies/Update/${UserID}`;
    await axios.post(endpoint, Company);
  } catch (e) {
    console.log(e);
  }
};

//delete Company
export const removeCompanyById = async (UserID) => {
  try {
    let endpoint = startEndpoint + `/Companies/Remove/${UserID}`;
    await axios.delete(endpoint);
  } catch (e) {
    console.log(e);
  }
};
