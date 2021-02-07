import { createStore, createNamespacedHelpers } from "vuex";
import auth from "./authModule";

export default createStore({
  state: {
    auth,
    profilePicture: "",
    connection: null
  },
  getters: {},
  mutations: {
    setProfilePicture(state, pictureUrl) {
      state.profilePicture = pictureUrl;
    },
    setConnection(state, connection) {
      state.connection = connection;
    }
  },
  actions: {
    setProfilePicture(context, pictureUrl) {
      context.commit("setProfilePicture", pictureUrl);
    },
    setConnection(context, connection) {
      context.commit("setConnection", connection);
    }
  },
  modules: {},
});

export const authHelper = createNamespacedHelpers("auth");
