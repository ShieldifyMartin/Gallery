import { createRouter, createWebHistory } from 'vue-router'
import Home from '@/views/Home.vue'
import Login from '@/views/Login.vue'
import Register from '@/views/Register.vue'
import SubmitPhoto from '@/views/SubmitPhoto.vue'
import PostDetails from '@/views/PostDetails.vue'
import ProfileDetails from '@/views/ProfileDetails.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/register',
    name: 'Register',
    component: Register
  },
  {
    path: '/submit',
    name: 'Submit',
    component: SubmitPhoto
  },
  {
    path: '/:id',
    name: 'Details',
    component: PostDetails
  },
  {
    path: '/profile',
    name: 'ProfileDetails',
    component: ProfileDetails
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
