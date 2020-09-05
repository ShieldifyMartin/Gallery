import VueRouter from 'vue-router';
import Home from '../components/Home';
import Login from '../components/Login';
import NotFound from '../components/NotFound';
// import Protected from './components/Protected';
// import store from './store';

const router = new VueRouter({
  mode: 'history',
  routes: [
    {
      path: '/',
      component: Home
    },
    {
        path: '/login',
        component: Login
    },
    // {
    //   path: '/protected',
    //   component: Protected,
    //   beforeEnter: (to, from, next) => {
    //     const redirectUrl = store.user ? undefined : '/'
    //     next(redirectUrl);
    //   }
    // },
    {
      path: '*',
      component: NotFound
    }
  ]
});

export default router;