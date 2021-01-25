import useAlert from "../../components/Alert/UseAlert";

const validate = (state) => {
    if (state.picture === null || state.description.length === 0) {
        useAlert("Invalid data!", false);
        return false;
    }

    if (state.description.length > 100) {
        useAlert("Max description length is 100!", false);
        return false;
    }

    if (state.location && state.location.length > 40) {
        useAlert("Max location length is 40!", false);
        return false;
    }

    return true;
};

export default validate;