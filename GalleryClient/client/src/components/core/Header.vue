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
      <router-link to="/login" v-if="!isAuth()">Login</router-link>
      <a href="#" @click="logout" v-if="isAuth()">Logout</a>
    </div>

    <div v-if="state.showMenu" class="dropdown">
      <li><router-link to="/" @click="toggleMenu">Home</router-link></li>
      <li>
        <router-link to="/submit" v-if="isAuth()" @click="toggleMenu"
          >Submit a photo</router-link
        >
      </li>
      <li>
        <router-link to="/users" v-if="isAuth()" @click="toggleMenu"
          >Users</router-link
        >
      </li>
      <li>
        <router-link to="/login" v-if="!isAuth()" @click="toggleMenu"
          >Login</router-link
        >
      </li>
      <li><a href="#" @click="logout" v-if="isAuth()">Logout</a></li>
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
import store from "../../store";
import router from "../../router";
import { profileService } from "../../services";

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

    const toggleMenu = () => {
      state.showMenu = !state.showMenu;      
    };

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

  .dropdown {
    display: none;
  }

  .menu-icon {
    display: none;
    width: 2em;
    height: 2em;
    margin: 0 0.5em;
    margin-bottom: -0.7em;
  }

  .profile-picture {
    margin-right: auto;

    img {
      width: 5em;
      height: 5em;
      border-radius: 50%;
      margin-left: 5em;
      margin-top: -1em;
    }
  }

  @media only screen and (max-width: 900px) {
    .profile-picture {
      img {
        margin-left: 0;
      }
    }
  }

  @media only screen and (max-width: 750px) {
    #nav {
      margin-bottom: 10em;
    }

    .links {
      a {
        display: none;
      }
    }

    .dropdown {
      display: block;
      position: absolute;
      top: 2em;
      right: 2em;
      margin: 3em 0;
      width: 7em;
      height: 6em;
      padding: 2em;
      list-style: none;
      background-color: #111111;
      min-width: 160px;
      box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
      z-index: 1;
      border-radius: 10%;

      a {
        color: #fff;
        text-decoration: none;
        font-size: 1.2em;
        margin: 2em 0;
      }

      a:hover {
        color: #666;
      }
    }

    .menu-icon {
      display: block;
    }
  }
}
</style>
