<template>
  <div class="home">
    <h1>Details page</h1>
    <img v-if="state.loading" class="loader" src="@/assets/loading.gif" />
    <div class="post">    
        <img :src="state.post.picture" :alt="state.post.description" />
        <p>{{state.post.description}}</p>
        <p>{{state.post.location}}</p>
        <p>{{state.post.createdOn}}</p>
        <p>{{state.post.createdBy}}</p>      
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import { postService } from "../services";

export default defineComponent({
  setup() {
    const state = reactive({
      post: [],
      loading: true
    });

    watchEffect(async () => {
      const id = window.location.href.split('/')[3];
      const post = await postService.getById(id);      
      state.post = post;
      state.loading = false;
    });

    return {
      state      
    };
  }
});
</script>

<style lang="scss">
.details {
}
</style>
