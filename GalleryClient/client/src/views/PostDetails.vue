<template>
  <div class="details">
    <h1>Details page</h1>
    <div class="post">
      <p>Created on: {{ state.createdOn }}</p>
      <p>Likes: {{ state.post.likes }}</p>
      <img v-if="state.loading" class="loader" src="@/assets/loading.gif" />
      <img :src="state.post.picture" :alt="state.post.description" /><br />
      <p>Description: {{ state.post.description }}</p>
      <p>Location: {{ state.post.location || "none" }}</p>      
      <p>Created by: {{ state.post.userId }}</p>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import moment from "moment";
import { postService } from "../services";

export default defineComponent({
  setup() {
    const state = reactive({
      post: [],
      loading: true,
      createdOn: null
    });

    watchEffect(async () => {
      const id = window.location.href.split("/")[3];
      const post = await postService.getById(id);

      state.post = post;
      state.loading = false;
      state.createdOn = moment(post.createdOn, "YYYYMMDD").fromNow();
    });

    return {
      state
    };
  }
});
</script>

<style lang="scss">
.details {
  .post {
    margin-top: 1em;

    img {
      width: 15em;
      height: auto;
    }

    .loader {
      margin: 0 auto;
    }
  }
}
</style>
