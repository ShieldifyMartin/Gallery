import { createStore } from 'vuex';

export default createStore({
  namespaced: true,
  state: {
    token: '',
    username: ''
  },
  mutations: {
    login(state) {
      state.token = localStorage.getItem('token');
      state.username = localStorage.getItem('username');      
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