import config from '@/config';
import router from "../router";

const login = async (email, username, password) => {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, username, password })
    };

    return await fetch(`${config.restAPI}/identity/login`, requestOptions)
        .then(res => res.json())
        .then(res => {
            if (res.token) {
                localStorage.setItem("token", res.token);
                localStorage.setItem("username", username);
            }

            return res.status || 200;
        });
}

const register = async (email, username, password) => {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, username, password })
    };

    return await fetch(`${config.restAPI}/identity/register`, requestOptions)        
        .then(res => {
            if(res.status >= 300) {                            
                return "Invalid data!";
            } else {
                router.push("/login");                
            }
        });
}

export const userService = {
    login,
    register    
};