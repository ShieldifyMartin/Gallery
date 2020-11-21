<template>
  <div class="home">
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
    <div v-else-if="state.posts && state.posts.length" class="posts">
      <router-link :to="post.id" v-for="post in state.posts" :key="post.id">
        <div class="image">
          <img :src="post.picture" :alt="post.description" />
        </div>
      </router-link>
    </div>
    <div v-else class="no-content">
      <img src="@/assets/not-found.png" alt="no-content" />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import { postService } from "../../services";

export default defineComponent({
  setup() {
    const state = reactive({
      posts: [],
      searchInput: "",
      loading: true,
    });

    watchEffect(async () => {
      const posts = await postService.get();

      state.posts = posts;
      state.loading = false;
    });

    const search = async () => {
      const posts = await postService.search(state.searchInput);

      state.posts = posts;
    };

    return {
      state,
      search,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
