<template>
  <div id="nav">
    <div class="profile-picture">
      <router-link v-if="isAuth()" to="/profile">
        <img
          v-if="state.pictureUrl"
          :src="state.pictureUrl"
          class="profile-icon"
        />
        <img v-else src="@/assets/icons/profile.png" class="profile-icon" />
      </router-link>
    </div>
    <div class="links">
      <router-link to="/">Home</router-link>
      <router-link to="/submit" v-if="isAuth()">Submit a photo</router-link>
      <router-link to="/users" v-if="isAuth()">Users</router-link>
      <a href="#" @click="logout" v-if="isAuth()">Logout</a>
      <router-link to="/login" v-else>Login</router-link>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import store from "../../store";
import { profileService } from "../../services";

export default defineComponent({
  name: "Header",
  setup() {
    const state = reactive({
      pictureUrl: "",
    });

    watchEffect(async () => {
      if (isAuth()) {
        const data = await profileService.get();
        state.pictureUrl = data.picture;
      }
    });

    function isAuth() {
      return store.state.auth.state.isAuth;
    }

    function logout() {
      store.state.auth.dispatch("logout");
      localStorage.setItem("token", "");
      localStorage.setItem("username", "");

      window.location.href = "/";
    }

    return {
      state,
      isAuth,
      logout,
    };
  },
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
      height: 4em;
      border-radius: 50%;
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
