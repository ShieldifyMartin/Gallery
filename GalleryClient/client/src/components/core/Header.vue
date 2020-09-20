<template>
  <div id="nav">
    <router-link to="/profile" class="left profile-picture">
      <img v-if="pictureUrl" :src="pictureUrl" class="profile-icon" />
      <img v-else src="@/assets/icons/profile.png" class="profile-icon" />
    </router-link>
    <router-link to="/" class="right">Home</router-link>
    <router-link to="/submit" v-if="isAuth()" class="right">Submit a photo</router-link>
    <a href="#" @click="logout" v-if="isAuth()" class="right">Logout</a>
    <router-link to="/login" v-else class="right">Login</router-link>
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
      return this.$store.state.auth.state.token !== '';
    }

    function logout() {
      localStorage.setItem('token', '');
      localStorage.setItem('username', '');
      this.$store.state.auth.dispatch("logout");
      
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
    justify-items: space-between;
    padding: 30px;

    a {
      font-weight: bold;
      color: #2c3e50;
      margin: 0 0.2em;

      &.router-link-exact-active {
        color: #42b983;
      }
    }

    .left {      
      margin-right: 70%;
    }    

    .profile-picture > img {
      margin-left: 2em;
      width: 3em;
    }

    @media only screen and (max-width: 608px) {
      .left {      
        margin-right: 40%;
      }
    }

    @media only screen and (max-width: 440px) {
      .left {      
        margin-right: 20%;
      }

      .profile-picture > img {
        margin-left: 0;       
      }
    }
  }
</style>