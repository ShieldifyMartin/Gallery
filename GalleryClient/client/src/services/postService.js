import config from '@/config';
import axios from 'axios';

const get = async () => {   
    return await axios.get(`${config.restAPI}/posts/all`)
        .then(res => {            
            if (res.status >= 200 && res.status < 300) {
                var posts = res.data;
                return posts;
            }
        });
}

const getById = async (id) => {
    return await axios.get(`${config.restAPI}/posts/byId/${id}`)
        .then(res => {            
            if (res.status >= 200 && res.status < 300) {
                var post = res.data;
                return post;
            }
        });
}

const getCategories = async () => {
    return await axios.get(`${config.restAPI}/categories/all`)
        .then(res => {
            if (res.status >= 200 && res.status < 300) {
                var categories = res.data;
                return categories;
            }
        });
}

const create = async (token, picture, location, description, categoryId) => {
    axios.defaults.headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    };

    const formData = new FormData();
    formData.append('picture', picture);
    formData.append('location', location);
    formData.append('description', description);
    formData.append('categoryId', categoryId);

    return await axios.post(`${config.restAPI}/posts/create`, formData)
      .then(res => {
        if (res.status >= 200 && res.status < 300) {                
            var postId = res.data;
            return postId;
        } else if(res.status === 401) {
            return res.status;
        }})
        .catch(err => console.log(err));      
}

export const postService = {
    get,
    getById,
    getCategories,
    create  
};