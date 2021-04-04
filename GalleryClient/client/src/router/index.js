import { createRouter, createWebHistory } from "vue-router";
import store from "@/store";

const routes = [
  {
    path: "/",
    name: "Home",
    props: true,
    component: () => import("@/views/Home/Home.vue")
  },
  {
    path: "/login",
    name: "Login",
    component: () => import("@/views/Login/Login.vue")
  },
  {
    path: "/register",
    name: "Register",
    component: () => import("@/views/Register/Register.vue")
  },
  {
    path: "/submit",
    name: "Submit",
    component: () => import("@/views/SubmitPhoto/SubmitPhoto.vue")
  },
  {
    path: "/:id",
    name: "Details",
    component: () => import("@/views/PostDetails/PostDetails.vue")
  },
  {
    path: "/profile/:id?",
    name: "ProfileDetails",
    component: () => import("@/views/ProfileDetails/ProfileDetails.vue")
  },
  {
    path: "/edit/:id",
    name: "EditPost",
    component: () => import("@/views/EditPost/EditPost.vue")
  },
  {
    path: "/users",
    name: "Users",
    component: () => import("@/views/Users/Users.vue")
  },
  {
    path: "/dashboard",
    name: "AdminDashboard",
    component: () => import("@/views/AdminDashboard/AdminDashboard.vue"),
    beforeEnter: (to, from, next) => {      
      if (!isAdmin() || !isAuth()) next({ name: 'Login' })
      else next()
    }
  }
];

function isAuth() {
  return store.state.auth.state.isAuth;
}

function isAdmin() {
  return store.state.auth.state.isAdmin;
}

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
