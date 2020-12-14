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
      <li v-for="category in state.categories" :key="category.id">
        <input
          type="button"
          :id="category.id"
          class="tab-switch"
          @click="setCategoryId(category.id)"
        />
        <label :for="category.id">
          <div v-if="state.category == category.id" class="clicked">
            {{ category.title }}({{
              (category.posts && category.posts.length) || 0
            }})          
          </div>
          <div v-else>
            {{ category.title }}({{
              (category.posts && category.posts.length) || 0
            }})
          </div>
        </label>
      </li>
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
      category: ""      
    });

    const getCategoriesLink = (title) => {
      return `/categories/${title.toLowerCase()}`;
    };

    watchEffect(async () => {
      if (state.category) {
        const posts = await categoryService.getPostsByCategory(state.category);
        
        state.posts = posts;
      } else {
        const posts = await postService.get();
        state.posts = posts;
        state.loading = false;
      }
      const categories = await categoryService.get();
      state.categories = categories;
    });

    const search = async () => {
      const posts = await postService.search(state.searchInput);

      state.posts = posts;
    };

    const setCategoryId = (id) => {
      state.category = id;
    };

    return {
      state,
      getCategoriesLink,
      search,
      setCategoryId,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
