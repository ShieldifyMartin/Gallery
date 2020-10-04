<template>
  <div class="details">
    <div class="post-header">
      <div>
        <img v-if="state.post.pictureUrl" :src="state.post.pictureUrl" class="profile-icon" />
        <img v-else src="@/assets/icons/profile.png" class="profile-icon" />
        <p>Created by: {{state.post.userName}}</p>
      </div>
      <div>
        <img v-if="state.isLiked" class="heart-icon" src="@/assets/icons/heart-solid.svg" @click="like" alt="liked heart" />
        <img v-else class="heart-icon" src="@/assets/icons/heart-regular.svg" @click="like" alt="like heart" />
        <p>Likes: {{ state.post.likes }}</p>
        {{ state.createdOn }}
      </div>
    </div>

    <img v-if="state.loading" class="loader" src="@/assets/loading.gif" />
    <img :src="state.post.picture" :alt="state.post.description" /><br />

    <div class="post-footer">
      <p>{{ state.post.description }}</p>
      <div v-if="state.post.location">
        <img
          class="location-icon"
          src="@/assets/icons/map-marker-solid.svg"
          alt="location"
        />
        <p>{{ state.post.location || "none" }}</p>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import moment from "moment";
import { postService } from "../../services";

export default defineComponent({
  setup() {
    const state = reactive({
      post: [],      
      createdOn: null,
      isLiked: false
    });

    watchEffect(async () => {
      const id = window.location.href.split("/")[3];
      const post = await postService.getById(id);
      
      state.post = post;
      state.createdOn = moment(post.createdOn, "YYYYMMDD").fromNow();
    });

    const like = () => {
      state.isLiked = true;
      console.log('like');
    }

    return {
      state,
      like
    };
  }
});
</script>

<styles src="./index.scss"></styles>