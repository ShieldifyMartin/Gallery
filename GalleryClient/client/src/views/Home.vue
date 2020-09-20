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
import { postService } from "../services";

export default defineComponent({
  setup() {
    const state = reactive({
      posts: [],
      loading: true
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
      isAuth
    };
  }
});
</script>

<style lang="scss">
.home {
  .posts {
    display: grid;
    position: relative;
    grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
    grid-gap: 1em;
    margin: 2em auto;
    padding: 0 5em;
  }

  .posts a {
    text-decoration: none;
    color: #000;
  }

  .posts img {
    width: 100%;
    height: 25em;
    object-fit: cover;
    border-radius: 0.75rem;
    transition: .3s ease-in-out;
    background-size:100%;
  }

  .image:hover {    
    -webkit-transform: scale(1.01);
    transform: scale(1.01);
    //transform: scale(0.99);
  }

  .loader {
    margin: 0 auto;
    width: 10em;
  }

  @media only screen and (max-width: 998px) {
    .posts {
      padding: 0 1em;
    }
  }

  @media only screen and (max-width: 580px) {
    .posts {
      display: block;
      padding: 0 0.5em;
    }

    .posts a {
      margin-top: 2em;
    }

    .posts a:first-child {
      margin-top: 0;
    }
  }
}
</style>
