import { createStore } from "vuex";
import Cookies from "js-cookie";

export default createStore({
  namespaced: true,
  state: {
    token: Cookies.get("token"),
    isAuth: Cookies.get("token") !== "",
    isAdmin:  Cookies.get("admin") !== ""
  },
  mutations: {
    login(state) {
      state.token = Cookies.get("token");
      state.isAuth = Cookies.get("token") !== "";
    },
    setAdmin(state) {
      state.isAdmin = true;
    },
    logout(state) {
      state.token = "";
      state.isAuth = false;
      Cookies.set("token", "");
      Cookies.set("admin", "");
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
