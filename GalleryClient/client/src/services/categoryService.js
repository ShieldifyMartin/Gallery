import axios from "axios";
import config from "@/config";

const get = async () => {
  try {
    const response = await axios.get(`${config.restAPI}/categories/all`);

    if (response.status >= 200 && response.status < 300) {
      var categories = response.data.categories;
      return categories;
    }
  } catch (err) {
    return [];
  }
};

const getPostsByCategory = async (categoryId) => {
  try {
    const response = await axios.get(`${config.restAPI}/categories/posts/${categoryId}`);
    if (response.status >= 200 && response.status < 300) {
      var posts = response.data;      
      return posts;
    }
  } catch (err) {
    return [];
  }
}

export const categoryService = {
  get,
  getPostsByCategory
};
