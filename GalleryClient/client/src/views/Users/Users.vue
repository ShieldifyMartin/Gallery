<template>
  <div class="users">
    <div class="search">
      <button type="submit" class="icon" @click="search"></button>
      <input
        type="text"
        v-model="state.searchInput"
        class="search-input"
        placeholder="Search photo"
        v-on:keyup.enter="search"
      />
    </div>
    <img v-if="state.loading" class="loader" src="@/assets/loading.gif" />
    <div v-else-if="state.users && state.users.length" class="users">
      <transition-group name="fade" appear>
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
      </transition-group>
    </div>
    <div v-else class="no-content">
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

    watchEffect(async () => {
      const users = await userService.getAllUsers();
      state.users = users;
      state.loading = false;
    });

    const search = async () => {
      const users = await userService.search(state.searchInput);
      state.users = users;
    };

    return {
      state,
      getProfileUrl,
      search,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
