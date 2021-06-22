import axios from "axios";
import config from "@/config";

const get = async () => {
    try {
        const response = await axios.get(`${config.restAPI}/collections/all`);
        if (response.status >= 200 && response.status < 300) {
            var collections = response.data;
            console.log(collections);
            return collections;
        }
    } catch (err) {
        return [];
    }
};

export const collectionService = {
    get,
};
