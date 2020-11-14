import axios from "axios";
import config from "@/config";
import router from "../router";

const login = async (token, email, username, password) => {
  axios.defaults.headers = {
    "Content-Type": "application/json",
    "Authorization": "Bearer " + token,
  };
  var body = JSON.stringify({ email, username, password });

  return await axios
    .post(`${config.restAPI}/identity/login`, body)
    .then((res) => {      
      if (res.data.token) {
        localStorage.setItem("token", res.data.token);
        localStorage.setItem("username", res.data.username);
      }

      return res.status || 200;
    });
};

const register = async (token, email, username, password) => {
  axios.defaults.headers = {
    "Content-Type": "application/json",
    Authorization: "Bearer " + token,
  };
  var body = JSON.stringify({ email, username, password });

  return await axios
    .post(`${config.restAPI}/identity/register`, body)
    .then((res) => {
      if (res.status >= 300) {
        return "Invalid data!";
      } else {
        router.push("/login");
      }
    });
};

const getAllUsers = async () => {
  return await axios
    .get(`${config.restAPI}/identity/getAllUsers`)
    .then((res) => {
      if (res.status >= 200 && res.status < 300) {
        var users = res.data;
        return users;
      }
    });
};

export const userService = {
  login,
  register,
  getAllUsers,
};
