import Swal from 'sweetalert2';

const validate = (state) => {
    if (
        state.email.length === 0 ||
        state.username.length === 0 ||
        state.password.length === 0
    ) {
        Swal.fire({
            position: state.isMobile ? "top" : "top-end",
            icon: "error",
            title: "Invalid date!",
            showConfirmButton: false,
            timer: 1500,
            width: state.isMobile ? 250 : 300,
        });
        return false;
    }

    if (state.email.length < 4) {
        Swal.fire({
            position: state.isMobile ? "top" : "top-end",
            icon: "error",
            title: "Invalid email!",
            showConfirmButton: false,
            timer: 1500,
            width: state.isMobile ? 250 : 300,
        });
        return false;
    }

    if (state.password.length < 6) {
        Swal.fire({
            position: state.isMobile ? "top" : "top-end",
            icon: "error",
            title: "Invalid password!",
            showConfirmButton: false,
            timer: 1500,
            width: state.isMobile ? 250 : 300,
        });
        return false;
    }

    return true;
};

export default validate;