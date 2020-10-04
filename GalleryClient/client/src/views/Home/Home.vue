<template>
  <div class="home">
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
      loading: true
    });

    function isAuth() {
      return this.$store.state.auth.state.isAuth;
    }

    watchEffect(async () => {
      const posts = await postService.get();
      state.posts = posts;
      state.loading = false;
    });

    return {
      state,
      isAuth
    };
  }
});
</script>

<styles src="./index.scss"></styles>