<template>
  <div class="home">
    <app-search />
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
import AppSearch from "../../components/Search/Search.vue";

export default defineComponent({
  components: {
    AppSearch,
  },
  setup() {
    const state = reactive({
      posts: [],
      loading: true,
    });

    watchEffect(async () => {
      const posts = await postService.get();
      state.posts = posts;
      state.loading = false;
    });

    return {
      state,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
