<template>
  <div class="home">
    <div class="search">
      <button type="submit" class="search-icon" @click="search"></button>
      <input
        type="text"
        v-model="state.searchInput"
        class="search-input"
        placeholder="Search photo"
        v-on:keyup.enter="search"
      />
      <button type="submit" class="filter-icon" @click="filter"></button>
    </div>
    <ul v-if="state.openFiltersMenu" class="filters-menu">
      <li @click="setFilter(1)">Top 5</li>
      <li @click="setFilter(2)">Sort by date</li>
      <li @click="setFilter(3)">Random order</li>
    </ul>
    <ul class="categories">
      <li
        v-for="category in state.categories"
        :key="category.id"
        @click="applyCategory(category.id)"
      >
        <span v-if="category.id == state.settedCategory" class="clicked">
          {{ category.title }}({{ category.posts.length }})
        </span>
        <span v-else> {{ category.title }}({{ category.posts.length }}) </span>
      </li>
    </ul>
    <img v-if="state.loading" class="loader" src="@/assets/loading.gif" />
    <div v-else-if="state.posts && state.posts.length" class="posts">
      <transition-group name="fade" appear>
        <router-link :to="post.id" v-for="post in state.posts" :key="post.id">
          <div class="image">
            <img :src="post.picture" :alt="post.description" />
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
import { defineComponent, reactive, watchEffect, toRef } from "vue";
import { postService, categoryService, signalRService } from "../../services";
import getFilteredPosts from "./filters";

export default defineComponent({
  props: {
    isAdminRoute: Boolean,
  },
  setup(props) {
    const state = reactive({
      posts: [],
      categories: [],
      openFiltersMenu: false,
      searchInput: "",
      loading: true,
      settedFilter: null,
      settedCategory: null,
      pageCount: 0,
    });

    watchEffect(async () => {
      const categories = await categoryService.get();
      const isAdminRoute = toRef(props, "isAdminRoute");

      let posts = [];
      if (isAdminRoute.value) {
        posts = await postService.getAllWithDeleted();
      } else {
        posts = await postService.get(state.pageCount);
      }

      state.posts = posts;
      state.categories = categories;
      state.loading = false;

      const connection = signalRService.buildConnection();

      connection.on("ReceivePosts", function(posts) {
        state.posts = posts;
      });

      signalRService.startAndStoreConnection(connection);
    });

    window.onscroll = async () => {
      const bottomOfWindow =
        window.scrollY > document.body.offsetHeight - window.outerHeight;

      //when the user reaches the bootom of the page and posts aren't only top 5 or there is a selected category
      if (bottomOfWindow && state.settedFilter != 1 && !state.settedCategory) {
        state.pageCount = state.pageCount + 1;
        const posts = await postService.get(state.pageCount);
        state.posts = posts;
      }
    };

    const search = async () => {
      const posts = await postService.search(state.searchInput);
      state.posts = posts;
    };

    const filter = () => {
      state.openFiltersMenu = !state.openFiltersMenu;
    };

    const setFilter = async (filter) => {
      const filteredPosts = await getFilteredPosts(filter);

      state.posts = filteredPosts;
      state.settedFilter = filter;
      state.openFiltersMenu = false;
    };

    const applyCategory = async(id) => {
      console.log(id)
      if(state.settedCategory === id) {
        state.settedCategory = null;
        return;
      }
      const posts = await categoryService.getPostsByCategory(id, state.pageCount);
      console.log(posts);
      state.posts = posts;
      state.settedCategory = id;
    };

    return {
      state,
      search,
      filter,
      setFilter,
      applyCategory,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
