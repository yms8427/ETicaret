import axios from "axios";
import session from "./session";

function getHeader() {
  let result = { Accept: "application/json", "X-Client-Type": "admin" };
  if (session.isAuthenticated()) {
    result.authorization = "Bearer " + session.getSession();
  }
  return result;
}
const ax = axios.create({
  baseURL: "https://localhost:5001",
  headers: getHeader(),
});

function get(url, callback) {
  ax.get(url)
    .then((response) => {
      callback(response.data);
    })
    .catch((e) => handleError(e));
}

function post(url, data, callback) {
  ax.post(url, data)
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
};
