<template>
  <div class="home">
    <h1>Home page</h1>    
    <ul class="posts">
      <img v-if="state.loading" class="loader" src="@/assets/loading.gif" />
      <li v-for="post in state.posts" :key="post.id">
        <img :src="post.picture" :alt="post.description" />
        <p>{{post.description}}</p>
        <p>{{post.location}}</p>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import { postService } from "../services";

export default defineComponent({
  setup() {
    const state = reactive({
      posts: [],
      loading: true,
    });

    function isAuth() {
      return this.$store.state.auth.state.token !== "";
    }

    watchEffect(async () => {
      const posts = await postService.get();
      state.posts = posts;
      state.loading = false;
    });

    return {
      state,
      isAuth,
    };
  },
});
</script>

<style lang="scss">
.home {
  .posts {
    display: flex;
    justify-content: space-between;
    width: 80vw;
    margin: 0 auto;
  }

  .posts li {
    list-style: none;
  }

  img {
    height: 10em;
    width: auto;
  }

  .loader {
    margin: 0 auto;
  }
}
</style>