import axios from "axios";
import config from "@/config";
import router from "../router";

const login = async (token, email, username, password) => {
  axios.defaults.headers = {
    "Content-Type": "application/json",
    Authorization: "Bearer " + token,
  };
  const body = JSON.stringify({ email, username, password });

  try {
    const response = await axios
      .post(`${config.restAPI}/identity/login`, body);
    if (response.data.token) {
      localStorage.setItem("token", response.data.token);
    }
    return response.status;
  }
  catch (err) {
    return 400;
  }
};

const register = async (token, email, username, password) => {
  axios.defaults.headers = {
    "Content-Type": "application/json",
    Authorization: "Bearer " + token,
  };
  const body = JSON.stringify({ email, username, password });

  try {
    await axios
      .post(`${config.restAPI}/identity/register`, body);
    router.push("/login");
  }
  catch (err) {
    return "Invalid data!";
  }
};

const getAllUsers = async () => {
  try {
    const response = await axios
      .get(`${config.restAPI}/identity/getAllUsers`)
    if (response.status >= 200 && response.status < 300) {
      var users = response.data.users;
      return users;
    }
  }
  catch (err) {
    return [];
  }
};

const search = async (input) => {
  let users = [];

  try {
    if (!input.length) {
      users = await getAllUsers();
    } else {
      const response = await axios
        .get(`${config.restAPI}/identity/search/${input}`)
      if (response.status >= 200 && response.status <= 300) {
        return response.data.users;
      }
    }
  }
  catch (err) {        
    return users;
  }
  return users;
};

export const userService = {
  login,
  register,
  getAllUsers,
  search,
};