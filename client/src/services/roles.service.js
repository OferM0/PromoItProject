import axios from "axios";
import { startEndpoint } from "./api";

/*export const getRoles = async () => {
  try {
    let endpoint = startEndpoint + "/Roles";
    let result = await axios.get(endpoint);
    return result.data;
  } catch (e) {
    console.log(e);
  }
};*/

export const getRolesById = async (id) => {
  try {
    let endpoint = startEndpoint + `/Roles/Get/${id}`;
    let result = await axios.get(endpoint);
    return result.data;
  } catch (e) {
    console.log(e);
  }
};

export const addRoleById = async (userId, roleType) => {
  try {
    let endpoint = startEndpoint + `/Roles/Add/${userId}/${roleType}`;
    await axios.post(endpoint);
  } catch (e) {
    console.log(e);
  }
};
