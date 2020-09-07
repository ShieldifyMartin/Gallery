import config from '@/config';

const get = async () => {
    const requestOptions = {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' },
    };

    return await fetch(`${config.restAPI}/posts/all`, requestOptions)
        .then(res => {
            if (res.status >= 200 && res.status < 300) {
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
            if (res.status >= 200 && res.status < 300) {
                var categories = res.json();
                return categories;
            }
        });
}

const create = async (picture, location, description, categoryId) => {
    const authtorization = 'Bearer ' + localStorage.getItem('token');
    
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': authtorization
        },
        body: JSON.stringify({ location, description, categoryId })
    };    

    return await fetch(`${config.restAPI}/posts/create`, requestOptions)
        .then(res => {            
            if (res.status >= 200 && res.status < 300) {                
                var postId = res.json();
                return postId;
            } else if(res.status === 401) {                
                return res.status;
            }
        })
        .catch(err => console.log(err));
}

export const postService = {
    get,
    getCategories,
    create
};