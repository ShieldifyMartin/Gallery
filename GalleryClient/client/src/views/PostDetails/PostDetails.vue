<template>
  <div class="details">
    <p v-if="state.message" class="message">{{state.message}}</p>
    <div class="post-header">
      <div>
        <img v-if="state.post.pictureUrl" :src="state.post.pictureUrl" class="profile-icon" />
        <img v-else src="@/assets/icons/profile.png" class="profile-icon" />
        <p>Created by: {{state.post.userName}}</p>        
      </div>
      <div>
        <img
          class="calendar-icon"
          src="@/assets/icons/calendar-alt-solid.svg"
          alt="calendar"
        /><br>
        <p>{{ state.createdOn }}</p>
      </div>
      <div>
        <img v-if="state.isLiked" class="heart-icon" src="@/assets/icons/heart-solid.svg" @click="unlike" alt="liked heart" />
        <img v-else class="heart-icon" src="@/assets/icons/heart-regular.svg" @click="like" alt="like heart" />
        <p>Likes: {{ state.likes }}</p>        
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
      likes: 0,
      isLiked: false,
      createdOn: null,      
      message: ''
    });

    watchEffect(async () => {
      const id = window.location.href.split("/")[3];
      const post = await postService.getById(id);

      state.likes = post.likes;
      state.isLiked = post.isLiked;
      state.post = post;      
      state.createdOn = moment(post.createdOn, "YYYYMMDD").fromNow();
    });

    const like = async() => {      
      let status = await postService.like(localStorage.getItem('token'), state.post.id);
      
      if(status == 200) {
        state.likes = state.post.likes + 1;
        state.isLiked = true;
      } else if(status == 400) {
        state.message = "You had already liked this post.";
      }
    }
    
    const unlike = async() => {
      let status = await postService.unlike(localStorage.getItem('token'), state.post.id);      
      if(status == 200) {
        state.likes = state.likes - 1;          
        state.isLiked = false;          
      } else if(status == 400) {
        state.message = "You had not liked this post.";
      }
    }

    return {
      state,
      like,
      unlike
    };
  }
});
</script>

<styles src="./index.scss"></styles>