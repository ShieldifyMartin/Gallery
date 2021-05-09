import axios from "axios";
import Cookies from "js-cookie";
import config from "@/config";
import store from "@/store";

const login = async (username, password) => {
  axios.defaults.headers = {
    "Content-Type": "application/json"
  };
  const body = JSON.stringify({ username, password });

  try {
    const response = await axios
      .post(`${config.restAPI}/identity/login`, body);
    if (response.data.token) {
      Cookies.set("token", response.data.token, { expires: 27 });
    }
    if (response.data.isAdmin === true) {
      store.state.auth.dispatch("setAdmin");
      Cookies.set("admin", response.data.isAdmin, { expires: 27 });
    }

    return response.status;
  }
  catch (err) {
    return err.response.status;
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
  }
  catch (err) {
    if (err.response.status === 400) {
      return err.response.data;
    }

    return "Something went wrong!";
  }
};

const getAllUsers = async () => {
  try {
    const response = await axios
      .get(`${config.restAPI}/identity/getAllUsers`)
    if (response.status >= 200 && response.status < 300) {
      var users = response.data;
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