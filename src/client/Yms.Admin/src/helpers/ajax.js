import axios from "axios";
import session from "./session";

const CREATIONTYPE = {
  JSON: "JSON",
  FORM: "FORM",
};
function getHeader(type) {
  let header = { "X-Client-Type": "admin" };
  if (type === CREATIONTYPE.JSON) {
    header["Content-Type"] = "application/json";
    header["Accept"] = "application/json";
  } else if (type === CREATIONTYPE.FORM) {
    header["Content-Type"] = "multipart/form-data";
    header["Accept"] = "multipart/form-data";
  }
  if (session.isAuthenticated()) {
    header.authorization = "Bearer " + session.getSession().token;
  }
  return header;
}

function createAxios(type) {
  return axios.create({
    baseURL: process.env.VUE_APP_API_URL,
    headers: getHeader(type),
  });
}

function get(url, callback) {
  createAxios(CREATIONTYPE.JSON)
    .get(url)
    .then((response) => {
      callback(response.data);
    })
    .catch((e) => handleError(e));
}

function post(url, data, callback) {
  createAxios(CREATIONTYPE.JSON)
    .post(url, data)
    .then((response) => {
      callback(response.data);
    })
    .catch((e) => handleError(e));
}

function postFile(url, data, callback) {
  if (!(data instanceof FormData)) {
    throw new Error("only 'FormData' is allowed in request data");
  }
  createAxios(CREATIONTYPE.FORM)
    .post(url, data)
    .then((response) => {
      callback(response.data);
    })
    .catch((e) => handleError(e));
}

function handleError(error) {
  if (error.response) {
    console.log(error.response);
  }
}

export default {
  get: get,
  post: post,
  postFile: postFile
};
