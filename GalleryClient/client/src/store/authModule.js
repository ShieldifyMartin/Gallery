import { createStore } from "vuex";
import Cookies from "js-cookie";

export default createStore({
  namespaced: true,
  state: {
    token: null,    
    isAuth: null,
    isAdmin: null
  },
  mutations: {
    login(state) {
      state.token = Cookies.get("token");
      state.isAuth = Cookies.get("token") && Cookies.get("token").length;
    },
    setAdmin(state) {
      state.isAdmin = true;
    },
    logout(state) {
      state.token = "";
      state.isAuth = false;
      Cookies.set("token", "");
    },
  },
  actions: {
    login(context) {
      context.commit("login");
    },
    setAdmin(context) {
      context.commit("setAdmin");
    },
    logout(context) {
      context.commit("logout");
    },
  },
});
