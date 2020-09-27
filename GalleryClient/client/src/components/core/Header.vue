<template>
  <div id="nav">
    <div class="profile-picture">    
      <router-link v-if="isAuth()" to="/profile">
        <img v-if="pictureUrl" :src="pictureUrl" class="profile-icon" />
        <img v-else src="@/assets/icons/profile.png" class="profile-icon" />     
      </router-link>
    </div>
    <div class="links">      
      <router-link to="/">Home</router-link>
      <router-link to="/submit" v-if="isAuth()">Submit a photo</router-link>
      <a href="#" @click="logout" v-if="isAuth()">Logout</a>
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
      pictureUrl: ''      
    });

    function isAuth() {
      return this.$store.state.auth.state.token !== "";
    }

    function logout() {      
      this.$store.state.auth.dispatch("logout");
      localStorage.setItem('token', '');
      localStorage.setItem('username', '');
      
      router.push("/");
    }

    return {
      state,
      isAuth,      
      logout
    }
  }
});
</script>

<style lang="scss">
  #nav {    
    display: flex;
    justify-content: center;
    padding: 2em;    

  .links {
    a {
      font-weight: bold;
      color: #2c3e50;
      margin: 0 0.5em;

      &.router-link-exact-active {
        color: #42b983;
      }
    }    
  }
  
  .profile-picture {
    margin-right: auto;

    img {
      width: 4em;
      margin-left: 5em;
      margin-top: -1em;
    }
  }

  @media only screen and (max-width: 900px) {
    .profile-picture img {
      margin-left: 0;       
    }
  }
}
</style>