import router from '@/router/index';
import { userService } from '../services';

const authentication = {
    state: {
        loggedIn: false,
        loginError: null,
        username: null,
        token: null
    },
    getters: {
        getLoginError(state: any) {
            return state.loginError || "";
        }
    },
    mutations: {
        loggedIn(state: any, username: string, token: string) {
            state.loggedIn = true;
            state.loginError = null;
            state.username = username || "";
            state.token = token;

            router.push("/");
        },

        loggedOut(state: any) {
            state.loggedIn = false;
            router.push("/login");
        },

        loginError(state: any, error: string) {
            state.loginError = error;
        }
    },
    // actions: {
    //     login({ dispatch, commit }, { email, username, password }) {
    //         userService.login(email, username, password)
    //             .then(res => {
    //                 if (!res.ok) {
    //                     if (res.status === 401) {
    //                         logout();
    //                         location.reload(true);
    //                     }
                        
    //                     commit('loginFailure', res.title);
    //                     return;
    //                 }

    //                 commit('loggedIn', user.token);
    //                 router.push('/');
    //             }
    //             );
    //     },
    //     logout({ commit }) {
    //         userService.logout();
    //         commit('loggedOut');
    //     }
    // }
};

export default authentication;