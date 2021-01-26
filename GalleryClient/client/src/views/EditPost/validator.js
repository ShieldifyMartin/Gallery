import config from "@/config";
import useAlert from "../../components/Alert/UseAlert";

const validate = (state) => {
    if (state.picture === null || state.description.length === 0) {
        useAlert("Invalid data!", false);
        return false;
    }

    if (state.description.length > config.maxDescriptionLength) {
        useAlert(`Max description length is {config.maxDescriptionLength}!`, false);
        return false;
    }

    if (state.location && state.location.length > config.maxLocationLength) {
        useAlert(`Max location length is ${config.maxLocationLength}!`, false);
        return false;
    }

    return true;
};

export default validate;