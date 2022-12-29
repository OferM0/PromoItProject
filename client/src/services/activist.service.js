import axios from "axios";
import { startEndpoint } from "./api";

//add activist details
export const addActivistDetails = async (details) => {
  try {
    let endpoint = startEndpoint + "/Activists/Add";
    await axios.post(endpoint, details);
  } catch (e) {
    console.log(e);
  }
};

//get Activists
export const getActivists = async () => {
  try {
    let endpoint = startEndpoint + "/Activists/Get";
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

//get Activist by id
export const getActivistById = async (UserID) => {
  try {
    let endpoint = startEndpoint + `/Activists/Get/${UserID}`;
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

//update Activist
export const updateActivistById = async (UserID, Activist) => {
  try {
    let endpoint = startEndpoint + `/Activists/Update/${UserID}`;
    await axios.post(endpoint, Activist);
  } catch (e) {
    console.log(e);
  }
};

//delete Activist
export const removeActivistById = async (UserID) => {
  try {
    let endpoint = startEndpoint + `/Activists/Remove/${UserID}`;
    await axios.delete(endpoint);
  } catch (e) {
    console.log(e);
  }
};
