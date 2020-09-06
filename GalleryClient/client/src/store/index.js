import { createStore, createNamespacedHelpers } from 'vuex';
import auth from './authModule';

export default createStore({
  state: {
    auth
  },
  getters: {    
  },
  mutations: {
  },
  actions: {
  },
  modules: {
  }
})

export const authHelper = createNamespacedHelpers('auth');
