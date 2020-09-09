<template>
  <div id="nav">
    <router-link to="/">Home</router-link>|
    <router-link to="/submit">Submit a photo</router-link> |
    <a  href="#" @click="logout" v-if="isAuth()">Logout</a>
    <router-link to="/login" v-else>Login</router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import router from "../../router";

export default defineComponent({
  name: "Header",
  setup() {
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
      isAuth,
      logout
    }
  }
});
</script>
