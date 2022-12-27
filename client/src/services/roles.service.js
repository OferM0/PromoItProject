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
    let endpoint = startEndpoint + `/Roles/${id}`;
    let result = await axios.get(endpoint);
    return result.data;
  } catch (e) {
    console.log(e);
  }
};
