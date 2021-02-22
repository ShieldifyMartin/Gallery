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
      <li @click="setFilter(1)">Sort by date</li>
      <li @click="setFilter(2)">Top 5</li>
      <li></li>
    </ul>
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
import { postService, categoryService, signalRService } from "../../services";
import store from "../../store";

export default defineComponent({
  setup() {
    const state = reactive({
      posts: [],
      categories: [],
      openFiltersMenu: false,
      searchInput: "",
      loading: true,
      category: "",
      messages: []      
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

      const connection = signalRService.buildConnection();

      connection.on("ReceiveMessage", function(user, message) {
        state.info.user = user;
        state.info.message = message;
        console.log({ user, message });
      });
      connection.on("ReceivePosts", function(posts) {
        state.posts = posts;
      });

      signalRService.startAndStoreConnection(connection);
    });

    const submit = () => {
      store.state.connection
        .invoke("SendMessage", "Pesho", "Misho")
        .catch(function(err) {
          return console.error(err);
        });
    };

    const search = async () => {
      const posts = await postService.search(state.searchInput);

      state.posts = posts;
    };

    const filter = () => {
      state.openFiltersMenu = !state.openFiltersMenu;
    };

    const setFilter = async(filter) => {
      switch (filter) {        
        case 1: {
          console.log("Posts sorted by date");
          break;
        }
        case 2: {
          const posts = await postService.getTop5();          
          state.posts = posts;
          break;
        }
        default: {
          console.log(`No filter`);
        }
      }
      state.openFiltersMenu = false;
    };

    const setCategoryId = (id) => {
      if (state.category == id) {
        state.category = "";
      } else {
        state.category = id;
      }
    };

    return {
      state,
      getCategoriesLink,
      search,
      setCategoryId,
      submit,
      filter,
      setFilter,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
