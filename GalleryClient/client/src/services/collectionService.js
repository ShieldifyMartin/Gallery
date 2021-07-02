import axios from "axios";
import config from "@/config";

const get = async () => {
    try {
        const response = await axios.get(`${config.restAPI}/collections/all`);
        if (response.status >= 200 && response.status < 300) {
            var collections = response.data;
            return collections;
        }
    } catch (err) {
        return [];
    }
};

const add = async (token, name) => {
    axios.defaults.headers = {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
    };
    const body = JSON.stringify({ name });

    try {
        const response = await axios
            .post(`${config.restAPI}/collections/add`, body);
        const status = response.status;
        return status;
    }
    catch (err) {
        return err.response.status;
    }
};

export const collectionService = {
    get,
    add
};
