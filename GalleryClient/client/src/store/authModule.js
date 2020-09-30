import { createStore } from 'vuex';

export default createStore({
  namespaced: true,
  state: {
    token: localStorage.getItem('token'),
    username: localStorage.getItem('username'),
    isAuth: localStorage.getItem('token') && localStorage.getItem('token') !== ''
  },
  mutations: {
    login(state) {
      state.token = '';
      state.username = '';
    },
    logout(state) {
      state.token = '';
      state.username = '';
    }
  },
  actions: {
    login(context) {      
      context.commit("login");
    },
    logout(context) {      
      context.commit("logout");
    }
  }
});