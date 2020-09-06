import config from '@/config';

interface User {
    token?: string;    
}

function login(email: string, username: string, password: string) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, username, password })
    };

    return fetch(`${config.restAPI}/users/authenticate`, requestOptions)        
        .then(user: User => {
            console.log(user);
            if (user.token) {
                localStorage.setItem('token', JSON.stringify(user.token));
                localStorage.setItem('username', JSON.stringify(username));                
            }

            return user;
        });
}

function logout() {    
    localStorage.removeItem('user');
}

export const userService = {
    login,
    logout
};