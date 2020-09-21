<template>
  <div id="nav">        
    <div class="links">
      <router-link v-if="state.isAuth" to="/profile" class="profile-picture">
        <img v-if="pictureUrl" :src="pictureUrl" class="profile-icon" />
        <img v-else src="@/assets/icons/profile.png" class="profile-icon" />     
      </router-link>
      <router-link to="/">Home</router-link>
      <router-link to="/submit" v-if="state.isAuth">Submit a photo</router-link>
      <a href="#" @click="logout" v-if="state.isAuth">Logout</a>
      <router-link to="/login" v-else>Login</router-link>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive } from "vue";
import router from "../../router";

export default defineComponent({
  name: "Header",
  setup() {
    const state = reactive({
      pictureUrl: '',
      isAuth: localStorage.getItem('token') !== ''
    }); 

    function logout() {
      localStorage.setItem('token', '');
      localStorage.setItem('username', '');
      
      state.isAuth = false;
      router.push("/");
    }

    return {
      state,      
      logout
    }
  }
});
</script>

<style lang="scss">
  #nav {    
    display: flex;
    justify-content: space-between;
    padding: 2em;

  .links {
    text-align: center;
    margin: 0 auto;

    a {
      font-weight: bold;
      color: #2c3e50;
      margin: 0 0.5em;      

      &.router-link-exact-active {
        color: #42b983;
      }
    }

    .profile-picture > img {      
      width: 2.5em;
      margin-right: 4em;
    }
  }

    @media only screen and (max-width: 440px) {
      .profile-picture > img {
        margin-left: 0;       
      }
    }
  }
</style>