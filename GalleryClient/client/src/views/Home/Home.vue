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
    <ul class="categories">      
      <router-link :to="getCategoriesLink(category.title)" v-for="category in state.categories" :key="category.id">
      
        {{ category.title }}({{ category.posts.length }})</router-link
      >
    </ul>
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
import { postService, categoryService } from "../../services";

export default defineComponent({
  setup() {
    const state = reactive({
      posts: [],
      categories: [],
      searchInput: "",
      loading: true,
    });

    const getCategoriesLink = (title) => {
      return `/categories/${title.toLowerCase()}`;
    }

    watchEffect(async () => {
      const posts = await postService.get();
      const categories = await categoryService.get();

      state.posts = posts;
      state.categories = categories;
      state.loading = false;
    });

    const search = async () => {
      const posts = await postService.search(state.searchInput);

      state.posts = posts;
    };

    return {
      state,
      getCategoriesLink,
      search,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
