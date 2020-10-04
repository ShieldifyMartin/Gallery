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

const getUserPosts = async (page) => {
    var token = localStorage.getItem('token');

    axios.defaults.headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    };

    return await axios.get(`${config.restAPI}/profiles/userposts/` + page)
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

    return await axios.get(`${config.restAPI}/profiles/likedPosts/` + page)
        .then(res => {            
            if (res.status >= 200 && res.status < 300) {
                var data = res.data;                
                return data;
            }
        });
};

export const profileService = {
    get,
    getUserPosts,
    getUserLikedPosts
};