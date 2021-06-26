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

const add = async (token, name) => {
    axios.defaults.headers = {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
    };
    const body = JSON.stringify({ name });

    try {
        const response = await axios
            .post(`${config.restAPI}/collections/add`, body);        
        if (response.status >= 200 && response.status < 300) {
            const status = response.status;
            return status;
        }
    }
    catch (err) {
        if (err.response.status === 400) {
            return err.response.data;
        }

        return "Something went wrong!";
    }    
};

export const collectionService = {
    get,
    add
};
