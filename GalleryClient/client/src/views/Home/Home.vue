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
import { postService, signalRService } from "../../services";
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
      pageCount: 0,
    });

    watchEffect(async () => {
      let posts = [];
      const isAdminRoute = toRef(props, "isAdminRoute");

      if (isAdminRoute.value) {
        posts = await postService.getAllWithDeleted();
      } else {
        posts = await postService.get(state.pageCount);
      }

      state.posts = posts;
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

      if (bottomOfWindow) {        
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
      state.openFiltersMenu = false;
    };

    return {
      state,
      search,
      filter,
      setFilter,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
