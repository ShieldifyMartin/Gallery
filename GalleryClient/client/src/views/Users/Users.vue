<template>
  <div class="users">
    <div class="search">
      <button type="submit" class="icon" @click="search"></button>
      <input
        type="text"
        v-model="state.searchInput"
        class="search-input"
        placeholder="Search free high-resolution photos"
        v-on:keyup.enter="search"
      />
    </div>
    <img v-if="state.loading" class="loader" src="@/assets/loading.gif" />
    <div v-if="state.users && state.users.length" class="users">
      <router-link
        :to="getProfileUrl(user.id)"
        v-for="user in state.users"
        :key="user.id"
        class="user"
      >
        <img :src="user.pictureUrl" alt="profile picture" />
        <div class="info">
          <h2>{{ user.userName }}</h2>
          <h3>@{{ user.userName.toLowerCase() }}</h3>
        </div>
      </router-link>
    </div>
    <div v-else class="not-content">
      <img src="@/assets/not-found.png" alt="no-content" />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import { userService } from "../../services";

export default defineComponent({
  setup() {
    const state = reactive({
      users: [],
      searchInput: "",
      loading: true,
    });

    const getProfileUrl = (userId) => {
      return `/profile/${userId}`;
    };

    const search = async () => {
      const users = await userService.search(state.searchInput);

      state.users = users;
    };

    watchEffect(async () => {
      const users = await userService.getAllUsers();

      state.users = users;
      state.loading = false;
    });

    return {
      state,
      getProfileUrl,
      search,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
