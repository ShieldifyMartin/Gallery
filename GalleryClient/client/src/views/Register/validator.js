import useAlert from "../../components/Alert/UseAlert";

const validate = (state) => {
    if (
        state.email.length === 0 ||
        state.username.length === 0 ||
        state.password.length === 0
    ) {
        useAlert("Invalid date!");
        return false;
    }

    if (state.email.length < 4) {
        useAlert("Invalid email!");
        return false;
    }

    if (state.password.length < 6) {
        useAlert("Invalid password!");
        return false;
    }

    return true;
};

export default validate;