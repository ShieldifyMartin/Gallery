import config from "@/config";
import axios from "axios";

const applyDefaultHeaders = (token) => {
  axios.defaults.headers = {
    "Content-Type": "multipart/form-data",
    Authorization: "Bearer " + token,
  };
};

const get = async (pageCount) => {
  try {
    const response = await axios.get(`${config.restAPI}/posts/all/${pageCount}`);
    if (response.status >= 200 && response.status < 300) {
      return response.data;
    }
  } catch (err) {
    return [];
  }
};

const getAllWithDeleted = async (pageCount) => {
  try {
    const response = await axios.get(`${config.restAPI}/admin/posts/all/${pageCount}`);
    if (response.status >= 200 && response.status < 300) {
      return response.data;
    }
  } catch (err) {
    return [];
  }
}

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

const getPostsSortedByDate = async (pageCount) => {
  try {
    const response = await axios.get(`${config.restAPI}/posts/getAllSortedByDate/${pageCount}`);
    if (response.status >= 200 && response.status < 300) {
      return response.data;
    }
  }
  catch (err) {
    return [];
  }
}

const getRandomPosts = async (pageCount) => {
  try {
    const response = await axios.get(`${config.restAPI}/posts/getRandom/${pageCount}`);
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
  applyDefaultHeaders(token);

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
  applyDefaultHeaders(token);

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

const editAdmin = async (token, id, picture, location, description, categoryId) => {
  applyDefaultHeaders(token);

  const formData = new FormData();
  formData.append("picture", picture);
  formData.append("location", location);
  formData.append("description", description);
  formData.append("categoryId", categoryId);

  try {
    const response = await axios.post(
      `${config.restAPI}/admin/posts/update/${id}`,
      formData
    );
    return response.status;
  } catch (err) {
    return err.response.status;
  }
}

const deletePost = async (token, postId) => {
  applyDefaultHeaders(token);

  try {
    const response = await axios.delete(
      `${config.restAPI}/posts/delete/${postId}`
    );
    return response.status;
  } catch (err) {
    return err.response.status;
  }
};

const deletePostAdmin = async (token, postId) => {
  applyDefaultHeaders(token);

  try {
    const response = await axios.delete(
      `${config.restAPI}/admin/posts/delete/${postId}`
    );
    return response.status;
  } catch (err) {
    return err.response.status;
  }
};

const like = async (token, postId) => {
  applyDefaultHeaders(token);

  try {
    const response = await axios.post(`${config.restAPI}/posts/like/${postId}`);
    return response.status;
  } catch (err) {
    return err.response.status;
  }
};

const unlike = async (token, postId) => {
  applyDefaultHeaders(token);

  try {
    const response = await axios.post(
      `${config.restAPI}/posts/unlike/${postId}`
    );
    return response.status;
  } catch (err) {
    return err.response.status;
  }
};

const addToCollection = async (token, postId, collectionId) => {
  applyDefaultHeaders(token);

  try {
    const response = await axios.post(
      `${config.restAPI}/posts/addToCollection/${postId}/${collectionId}`
    );
    console.log(response);
    return response.status;
  } catch (err) {
    return err.response.status;
  }
};

export const postService = {
  get,
  getAllWithDeleted,
  getById,
  getCategories,
  getTop5,
  getPostsSortedByDate,
  getRandomPosts,
  search,
  create,
  edit,
  editAdmin,
  deletePost,
  deletePostAdmin,
  like,
  unlike,
  addToCollection
};
