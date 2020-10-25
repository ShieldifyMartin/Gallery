import config from '@/config';
import axios from 'axios';

const get = async () => {
    var token = localStorage.getItem('token');

    axios.defaults.headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    };

    return await axios.get(`${config.restAPI}/profiles/details`)
        .then(res => {            
            if (res.status >= 200 && res.status < 300) {
                var data = res.data;
                return data;
            }
        });
};

const getById = async (id) => {
    var token = localStorage.getItem('token');

    axios.defaults.headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    };

    return await axios.get(`${config.restAPI}/profiles/details/${id}`)
        .then(res => {            
            if (res.status >= 200 && res.status < 300) {
                var data = res.data;
                return data;
            }
        });
};

const getUserPosts = async (page) => {
    var token = localStorage.getItem('token');

    axios.defaults.headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    };

    return await axios.get(`${config.restAPI}/profiles/userposts/${page}`)
        .then(res => {            
            if (res.status >= 200 && res.status < 300) {
                var data = res.data;                
                return data;
            }
        });
};

const getUserPostsById = async (page, id) => {
    var token = localStorage.getItem('token');

    axios.defaults.headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    };

    return await axios.get(`${config.restAPI}/profiles/userposts/${page}/${id}`)
        .then(res => {
            if (res.status >= 200 && res.status < 300) {
                var data = res.data;
                return data;
            }
        });
};

const getUserLikedPosts = async (page) => {
    var token = localStorage.getItem('token');

    axios.defaults.headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    };

    return await axios.get(`${config.restAPI}/profiles/likedPosts/${page}`)
        .then(res => {            
            if (res.status >= 200 && res.status < 300) {
                var data = res.data;                
                return data;
            }
        });
};

const getUserLikedPostsById = async (page, id) => {
    var token = localStorage.getItem('token');

    axios.defaults.headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    };

    return await axios.get(`${config.restAPI}/profiles/likedPosts/${page}/${id}`)
        .then(res => {            
            if (res.status >= 200 && res.status < 300) {
                var data = res.data;                
                return data;
            }
        });
};

const uploadProfileImage = async(picture) => {
    var token = localStorage.getItem('token');

    axios.defaults.headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    };

    const formData = new FormData();
    formData.append('picture', picture);

    return await axios.post(`${config.restAPI}/identity/profilePicture`, formData)
        .then(res => {            
            if (res.status >= 200 && res.status < 300) {
                var data = res.data;                
                return data;
            }
        })
        .catch((res) => {
            console.log(res);
        });
}

export const profileService = {
    get,
    getById,
    getUserPosts,
    getUserPostsById,
    getUserLikedPosts,
    getUserLikedPostsById,
    uploadProfileImage
};