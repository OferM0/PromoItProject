import axios from "axios";
import { startEndpoint } from "./api";

//get products
export const getProducts = async () => {
  try {
    let endpoint = startEndpoint + "/Products/Get";
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

//get product by id
export const getProductById = async (id) => {
  try {
    let endpoint = startEndpoint + `/Products/Get/${id}`;
    let response = await axios.get(endpoint);
    return response;
  } catch (e) {
    console.log(e);
  }
};

//add product
export const addProduct = async (product) => {
  try {
    let endpoint = startEndpoint + "/Products/Add";
    await axios.post(endpoint, product);
  } catch (e) {
    console.log(e);
  }
};

//update product
export const updateProductById = async (id, product) => {
  try {
    let endpoint = startEndpoint + `/Products/Update/${id}`;
    await axios.post(endpoint, product);
  } catch (e) {
    console.log(e);
  }
};

//delete product
export const removeProductById = async (id) => {
  try {
    let endpoint = startEndpoint + `/Products/Remove/${id}`;
    await axios.delete(endpoint);
  } catch (e) {
    console.log(e);
  }
};
