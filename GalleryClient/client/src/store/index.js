import { createStore, createNamespacedHelpers } from 'vuex';
import auth from './authModule';

export default createStore({
  state: {
    auth,
    profilePicture: ''
  },
  getters: {    
  },
  mutations: {
    setProfilePicture(state, pictureUrl) {      
      state.profilePicture = pictureUrl;
    }
  },
  actions: {
    setProfilePicture(context, pictureUrl) {      
      context.commit("setProfilePicture", pictureUrl);
    },
  },
  modules: {
  }
});

export const authHelper = createNamespacedHelpers('auth');
