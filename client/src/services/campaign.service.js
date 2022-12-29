import axios from "axios";
import { startEndpoint } from "./api";

//add campaign details
export const addCampaignDetails = async (details) => {
  try {
    let endpoint = startEndpoint + "/Campaigns/Add";
    await axios.post(endpoint, details);
  } catch (e) {
    console.log(e);
  }
};

//get Campaigns
export const getCampaigns = async () => {
  try {
    let endpoint = startEndpoint + "/Campaigns/Get";
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

//get Campaign by id
export const getCampaignById = async (CampaignID) => {
  try {
    let endpoint = startEndpoint + `/Campaigns/Get/${CampaignID}`;
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

//update Campaign
export const updateCampaignById = async (CampaignID, Campaign) => {
  try {
    let endpoint = startEndpoint + `/Campaigns/Update/${CampaignID}`;
    await axios.post(endpoint, Campaign);
  } catch (e) {
    console.log(e);
  }
};

//delete Campaign
export const removeCampaignById = async (CampaignID) => {
  try {
    let endpoint = startEndpoint + `/Campaigns/Remove/${CampaignID}`;
    await axios.delete(endpoint);
  } catch (e) {
    console.log(e);
  }
};
