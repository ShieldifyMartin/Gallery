import { createRouter, createWebHistory } from "vue-router";

const routes = [
  {
    path: "/",
    name: "Home",
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
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
