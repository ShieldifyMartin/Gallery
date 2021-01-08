import Swal from 'sweetalert2';

const validate = (state) => {
    if (state.picture === null || state.description.length === 0) {
        Swal.fire({
            position: 'top-end',
            icon: 'error',
            title: 'Invalid data!',
            showConfirmButton: false,
            timer: 1500,
            width: 300
        });
        return false;
    }

    if (state.description.length > 100) {
        Swal.fire({
            position: 'top-end',
            icon: 'error',
            title: 'Max description length is 100!',
            showConfirmButton: false,
            timer: 1500,
            width: 300
        });
        return false;        
    }

    if (state.location.length > 40) {        
        Swal.fire({
            position: 'top-end',
            icon: 'error',
            title: 'Max location length is 40!',
            showConfirmButton: false,
            timer: 1500,
            width: 300
        });
        return false;
    }

    return true;
};

export default validate;