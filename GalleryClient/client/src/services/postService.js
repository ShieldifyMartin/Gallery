import config from "@/config";
import axios from "axios";

const get = async () => {
  try {
    const response = await axios.get(`${config.restAPI}/posts/all`);
    if (response.status >= 200 && response.status < 300) {
      return response.data;
    }
  } catch (err) {
    return [];
  }
};

const getById = async (id) => {
  try {
    const response = await axios.get(`${config.restAPI}/posts/byId/${id}`);
    if (response.status >= 200 && response.status < 300) {
      var post = response.data;
      return post;
    }
  } catch (err) {
    return {};
  }
};

const getCategories = async () => {
  try {
    const response = await axios.get(`${config.restAPI}/categories/all`);
    if (response.status >= 200 && response.status < 300) {
      var categories = response.data;
      return categories;
    }
  } catch (err) {
    return [];
  }
};

const getTop5 = async () => {
  try {
    const response = await axios.get(`${config.restAPI}/posts/top5`);
    if (response.status >= 200 && response.status < 300) {
      return response.data;
    }
  } catch (err) {
    return [];
  }
}

const search = async (input) => {
  if (!input.length) {
    const posts = await get();
    return posts;
  } else {
    try {
      const response = await axios.get(
        `${config.restAPI}/posts/search/${input}`
      );
      if (response.status >= 200 && response.status < 300) {
        var posts = response.data;
        return posts;
      }
    } catch (err) {
      return [];
    }
  }
};

const create = async (token, picture, location, description, categoryId) => {
  axios.defaults.headers = {
    "Content-Type": "multipart/form-data",
    Authorization: "Bearer " + token,
  };

  const formData = new FormData();
  formData.append("picture", picture);
  formData.append("location", location);
  formData.append("description", description);
  formData.append("categoryId", categoryId);

  try {
    const response = await axios.post(
      `${config.restAPI}/posts/create`,
      formData
    );

    if (response.status >= 200 && response.status < 300) {
      var postId = response.data;
      return postId;
    }
  } catch (err) {
    return err.response.status;
  }
};

const edit = async (token, id, picture, location, description, categoryId) => {
  axios.defaults.headers = {
    "Content-Type": "multipart/form-data",
    Authorization: "Bearer " + token,
  };

  const formData = new FormData();
  formData.append("picture", picture);
  formData.append("location", location);
  formData.append("description", description);
  formData.append("categoryId", categoryId);

  try {
    const response = await axios.post(
      `${config.restAPI}/posts/update/${id}`,
      formData
    );
    return response.status;
  } catch (err) {
    return err.response.status;
  }
};

const deletePost = async (token, postId) => {
  axios.defaults.headers = {
    "Content-Type": "application/json",
    Authorization: "Bearer " + token,
  };

  try {
    const response = await axios.delete(
      `${config.restAPI}/posts/delete/${postId}`
    );
    return response.status;
  } catch (err) {
    return err.response.status;
  }
};

const like = async (token, postId) => {
  axios.defaults.headers = {
    "Content-Type": "application/json",
    Authorization: "Bearer " + token,
  };

  try {
    const response = await axios.post(`${config.restAPI}/posts/like/${postId}`);
    return response.status;
  } catch (err) {
    return err.response.status;
  }
};

const unlike = async (token, postId) => {
  axios.defaults.headers = {
    "Content-Type": "application/json",
    Authorization: "Bearer " + token,
  };

  try {
    const response = await axios.post(
      `${config.restAPI}/posts/unlike/${postId}`
    );
    return response.status;
  } catch (err) {
    return err.response.status;
  }
};

export const postService = {
  get,
  getById,
  getCategories,
  getTop5,
  search,
  create,
  edit,
  deletePost,
  like,
  unlike,
};
