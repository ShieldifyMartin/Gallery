import config from '@/config';

const get = async () => {
    const requestOptions = {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' },
    };

    return await fetch(`${config.restAPI}/posts/all`, requestOptions)        
        .then(res => {            
            if(res.status >= 200 && res.status < 300) {
                var posts = res.json();
                return posts;
            }
        });
}

const getCategories = async () => {
    const requestOptions = {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' },
    };

    return await fetch(`${config.restAPI}/categories/all`, requestOptions)        
        .then(res => {            
            if(res.status >= 200 && res.status < 300) {
                var categories = res.json();
                return categories;
            }
        });
}

export const postService = {
    get,
    getCategories
};