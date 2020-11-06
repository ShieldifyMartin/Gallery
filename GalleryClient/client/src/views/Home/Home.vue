<template>
  <div class="home">
    <div class="search">
      <button class="icon" @click="search"></button>
      <input type="text" v-model="state.searchInput" placeholder="Search free high-resolution photos" />
    </div>
    <img v-if="state.loading" class="loader" src="@/assets/loading.gif" />
    <div class="posts">
      <router-link :to="post.id" v-for="post in state.posts" :key="post.id">
        <div class="image">
          <img :src="post.picture" :alt="post.description" />
        </div>
      </router-link>
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
      loading: true
    });

    const search = async() => {      
      const posts = await postService.search(state.searchInput);
      const postArray = posts.posts;     
      state.posts = postArray;
    }

    watchEffect(async () => {
      const posts = await postService.get();
      state.posts = posts;
      state.loading = false;
    });

    return {
      state,
      search
    };
  }
});
</script>

<styles src="./index.scss"></styles>