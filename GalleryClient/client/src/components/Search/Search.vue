<template>
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
</template>

<script lang="ts">
import { defineComponent, reactive } from "vue";
import { postService } from "../../services";

export default defineComponent({
  setup() {
    const state = reactive({
      posts: [],
      searchInput: "",
      loading: true,
    });

    const search = async () => {
      const posts = await postService.search(state.searchInput);
      const postsArray = posts.posts;      

      state.posts = postsArray;
    };

    return {
      state,
      search
    };
  },
});
</script>

<styles src="./index.scss"></styles>
