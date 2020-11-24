import axios from "axios";
import config from "@/config";

const getAllCategories = async () => {
    return await axios
      .get(`${config.restAPI}/categories/all`)
      .then((res) => {
        if (res.status >= 200 && res.status < 300) {
          var users = res.data.categories;
          return users;
        }
      });
};

export const categoryService = {
    getAllCategories
};