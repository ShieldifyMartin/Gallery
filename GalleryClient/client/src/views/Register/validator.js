import useAlert from "../../components/Alert/UseAlert";

const validate = (state) => {
    if (
        state.email.length === 0 ||
        state.username.length === 0 ||
        state.password.length === 0
    ) {
        useAlert("Invalid date!", false);
        return false;
    }

    if (state.email.length < 4) {
        useAlert("Invalid email!", false);
        return false;
    }

    if (state.password.length < 6) {
        useAlert("Invalid password!", false);
        return false;
    }

    return true;
};

export default validate;