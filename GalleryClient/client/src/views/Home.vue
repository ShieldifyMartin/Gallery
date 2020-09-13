<template>
  <div class="home">
    <h1>Home page</h1>    
    <img v-if="state.loading" class="loader" src="@/assets/loading.gif" />
    <div class="posts">
      <router-link :to="post.id" v-for="post in state.posts" :key="post.id">
        <img :src="post.picture" :alt="post.description" />
        <p>{{post.description}}</p>
        <p>{{post.location}}</p>
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
      isAuth,
    };
  },
});
</script>

<style lang="scss">
.home {
  .posts {    
    width: 90%;
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    grid-template-rows: repeat(8, 3em);
    justify-items: center;
    justify-content: space-between;
    grid-gap: 3em;
    margin: 2em auto 0 auto;    
  }

  .posts a {
    text-decoration: none;
    color: #000;
  }

  img {
    width: 15em;
    height: auto;    
  }

  .loader {
    margin: 0 auto;
  }
  
  @media only screen and (max-width: 998px) {  
    .posts {
      grid-template-columns: repeat(2, 1fr);      
    }
  }

  @media only screen and (max-width: 767px) {  
    .posts {
      display: block;      
    }

    .posts a  {
      margin-top: 2em;
    }

    .posts a:first-child  {
      margin-top: 0;
    }
  }
}
</style>