import config from '@/config';

const login = async(email, username, password) => {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, username, password })
    };

    return await fetch(`${config.restAPI}/identity/login`, requestOptions)
        .then(res => res.json())
        .then(res => {                        
            if (res.token) {
                localStorage.setItem('token', JSON.stringify(res.token));
                localStorage.setItem('username', JSON.stringify(username));                
            }
            
            return res.status || 200;
        });
}

const logout = () => {
    localStorage.removeItem('user');
}

export const userService = {
    login,
    logout
};