import { createRouter, createWebHistory } from 'vue-router'
import Home from '@/views/Home/Home.vue'
import Login from '@/views/Login/Login.vue'
import Register from '@/views/Register/Register.vue'
import SubmitPhoto from '@/views/SubmitPhoto/SubmitPhoto.vue'
import PostDetails from '@/views/PostDetails/PostDetails.vue'
import ProfileDetails from '@/views/ProfileDetails/ProfileDetails.vue'
import ChangeProfilePicture from '../views/ChangeProfilePicture/ChangeProfilePicture.vue';

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
  },
  {
    path: '/profile/edit',
    name: 'ChangeProfilePicture',
    component: ChangeProfilePicture
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
