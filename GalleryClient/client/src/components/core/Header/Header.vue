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
      <div v-if="isAuth()">
        <router-link to="/">Home</router-link>
        <router-link to="/submit">Submit a photo</router-link>
        <router-link to="/users">Users</router-link>
        <a href="#" @click="logout">Logout</a>
      </div>
      <div v-else>
        <router-link to="/">Home</router-link>
        <router-link to="/login">Login</router-link>
      </div>
    </div>

    <div v-if="state.showMenu" class="dropdown">
      <div v-if="isAuth()">
        <li>
          <router-link to="/">Home</router-link>
        </li>
        <li>
          <router-link to="/submit">
            Submit a photo
          </router-link>
        </li>
        <li>
          <router-link to="/users">
            Users
          </router-link>
        </li>
        <li>
          <a href="#" @click="logout">Logout</a>
        </li>
      </div>
      <div v-else>
        <li><router-link to="/">Home</router-link></li>
        <li>
          <router-link to="/login" @click="toggleMenu">
            Login
          </router-link>
        </li>
      </div>
    </div>
    <img
      v-if="state.showMenu"
      src="@/assets/icons/times-solid.svg"
      class="menu-icon"
      alt="close menu icon"
      @click="toggleMenu"
    />
    <img
      v-else
      src="@/assets/icons/bars-solid.svg"
      alt="menu"
      class="menu-icon"
      @click="toggleMenu"
    />
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import store from "../../../store";
import router from "../../../router";
import { profileService } from "../../../services";

export default defineComponent({
  name: "Header",
  setup() {
    const state = reactive({
      pictureUrl: "",
      showMenu: false,
    });

    watchEffect(async () => {
      if (isAuth()) {
        const data = await profileService.get();
        state.pictureUrl = data.picture;
      }
    });

    function toggleMenu () {
      state.showMenu = !state.showMenu;
    }

    function isAuth() {
      return store.state.auth.state.isAuth;
    }

    const logout = () => {
      router.push("/");
      toggleMenu();
      store.state.auth.dispatch("logout");
    };

    return {
      state,
      toggleMenu,
      isAuth,
      logout,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
