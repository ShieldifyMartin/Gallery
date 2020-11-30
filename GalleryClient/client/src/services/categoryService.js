import axios from "axios";
import config from "@/config";

const get = async () => {
    return await axios
      .get(`${config.restAPI}/categories/all`)
      .then((res) => {        
        if (res.status >= 200 && res.status < 300) {
          var categories = res.data.categories;
          return categories;
        }
      });
};

export const categoryService = {
  get
};