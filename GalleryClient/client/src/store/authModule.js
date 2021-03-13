import { createStore } from "vuex";
import Cookies from "js-cookie";

export default createStore({
  namespaced: true,
  state: {
    token: Cookies.get("token"),    
    isAuth: Cookies.get("token"),
  },
  mutations: {
    login(state) {
      state.token = Cookies.get("token");
      state.isAuth = Cookies.get("token") && Cookies.get("token").length;
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
    logout(context) {
      context.commit("logout");
    },
  },
});
