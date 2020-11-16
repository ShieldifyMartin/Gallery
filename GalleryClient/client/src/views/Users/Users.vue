<template>
  <div class="users">
    <app-search />
    <img v-if="state.loading" class="loader" src="@/assets/loading.gif" />
    <div class="users">
      <router-link
        :to="getProfileUrl(user.id)"
        v-for="user in state.users"
        class="user"
        :key="user.id"
      >
        <img :src="user.pictureUrl" alt="profile picture" />
        <h2>{{ user.userName }}</h2>
      </router-link>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import { userService } from "../../services";
import AppSearch from "../../components/Search/Search.vue";

export default defineComponent({
  components: {
    AppSearch,
  },
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
      const usersArray = users.users;

      state.users = usersArray;
    };

    watchEffect(async () => {
      const users = await userService.getAllUsers();
      const usersArray = users.users;

      state.users = usersArray;
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
