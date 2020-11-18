import { createStore } from "vuex";

export default createStore({
  namespaced: true,
  state: {
    token: "",    
    isAuth: localStorage.getItem("token") !== "",
  },
  mutations: {
    login(state) {
      state.token = localStorage.getItem("token");      
      state.isAuth = localStorage.getItem("token") !== "";
    },
    logout(state) {
      state.token = "";      
      state.isAuth = false;
    },
  },
  actions: {
    login(context) {
      context.commit("login");
    },
    logout(context) {
      context.commit("logout");
    },
  },
});
