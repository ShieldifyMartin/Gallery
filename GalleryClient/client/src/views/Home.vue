<template>
  <div class="home">
    <h1>Home page</h1>
    isAuth: {{isAuth()}}
    <ul>
      <img v-if="state.loading" src="../assets/loading.gif">
      <li v-for="post in state.posts" :key="post.id">
          <img src="post.picture" alt=post.description>
          <p>{{post.description}}</p>
          <p>{{post.location}}</p>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from 'vue';
import { postService } from "../services";

export default defineComponent({
  setup() {
    const state = reactive({      
      posts: [],
      loading: true
    });

    function isAuth() {
      return this.$store.state.auth.state.token !== null;
    }

    watchEffect(async () => {
      const posts = await postService.get();
      console.log(posts);
      state.posts = posts;
      state.loading = false;
    });

    return {
      state,
      isAuth
    }
  }    
});
</script>
