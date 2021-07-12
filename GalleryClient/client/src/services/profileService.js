import axios from "axios";
import Cookies from "js-cookie";
import config from "@/config";

const applyDefaultHeaders = (token) => {
  axios.defaults.headers = {
    "Content-Type": "multipart/form-data",
    Authorization: "Bearer " + token,
  };
};

const get = async () => {
  const token = Cookies.get("token");
  applyDefaultHeaders(token);

  try {
    const response = await axios
      .get(`${config.restAPI}/profiles/details`);
    if (response.status >= 200 && response.status < 300) {
      const data = response.data;      
      return data;
    }
  }
  catch (err) {
    return [];
  }
};

const getById = async (id) => {
  const token = Cookies.get("token");
  applyDefaultHeaders(token);

  try {
    const response = await axios
      .get(`${config.restAPI}/profiles/details/${id}`);
    if (response.status >= 200 && response.status < 300) {
      const data = response.data;
      return data;
    }
  }
  catch (err) {
    return {};
  }
};

const getUserPosts = async (page) => {
  const token = Cookies.get("token");
  applyDefaultHeaders(token);

  try {
    const response = await axios
      .get(`${config.restAPI}/profiles/userposts/${page}`);
    if (response.status >= 200 && response.status < 300) {
      const data = response.data;
      return data;
    }
  }
  catch (err) {
    return [];
  }
};

const getUserPostsById = async (page, id) => {
  const token = Cookies.get("token");
  applyDefaultHeaders(token);

  try {
    const response = await axios
      .get(`${config.restAPI}/profiles/userposts/${page}/${id}`);
    if (response.status >= 200 && response.status < 300) {
      const data = response.data;
      return data;
    }
  }
  catch (err) {
    return [];
  }
};

const getUserLikedPosts = async (page) => {
  const token = Cookies.get("token");
  applyDefaultHeaders(token);

  try {
    const response = await axios
      .get(`${config.restAPI}/profiles/likedPosts/${page}`);
    if (response.status >= 200 && response.status < 300) {
      const data = response.data;
      return data;
    }
  }
  catch (err) {
    return [];
  }
};

const getUserLikedPostsById = async (page, id) => {
  const token = Cookies.get("token");
  applyDefaultHeaders(token);

  try {
    const response = await axios
      .get(`${config.restAPI}/profiles/likedPosts/${page}/${id}`);
    if (response.status >= 200 && response.status < 300) {
      const data = response.data;
      return data;
    }
  }
  catch (err) {
    return [];
  }
};

const uploadProfileImage = async (picture) => {
  const token = Cookies.get("token");
  applyDefaultHeaders(token);

  const formData = new FormData();
  formData.append("picture", picture);

  try {
    const response = await axios
      .post(`${config.restAPI}/identity/profilePicture`, formData)
    if (response.status >= 200 && response.status < 300) {
      const data = response.data;
      return data;
    }
  } catch (err) {
    return [];
  }
};

export const profileService = {
  get,
  getById,
  getUserPosts,
  getUserPostsById,
  getUserLikedPosts,
  getUserLikedPostsById,
  uploadProfileImage,
};